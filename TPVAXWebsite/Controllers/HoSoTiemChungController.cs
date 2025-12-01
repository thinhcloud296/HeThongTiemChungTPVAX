using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    public class HoSoTiemChungController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();

        // GET: Form tạo hồ sơ mới
        public ActionResult Create()
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateHoSoViewModel model)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null) return RedirectToAction("Login", "Account");
            string currentMaKH = kh.MaKH;

            if (ModelState.IsValid)
            {
                _uow.BeginTransaction();
                try
                {
                    string maHSTC_Final;

                    // --- LOGIC THÔNG MINH: Kiểm tra hồ sơ cũ ---
                    HoSoTiemChung hoSoCu = null;

                    // Chỉ tìm nếu người dùng có nhập CCCD
                    if (!string.IsNullOrEmpty(model.CCCD))
                    {
                        hoSoCu = _uow.HoSoTiemChungs.FirstOrDefault(h => h.CCCD == model.CCCD);
                    }

                    if (hoSoCu != null)
                    {
                        // CASE A: Tìm thấy hồ sơ cũ -> Lấy mã để liên kết
                        maHSTC_Final = hoSoCu.MaHSTC;

                        // Kiểm tra đã liên kết chưa (Tránh trùng)
                        if (_uow.LienKetHoSos.Any(lk => lk.MaKH == currentMaKH && lk.MaHSTC == maHSTC_Final))
                        {
                            ModelState.AddModelError("", "Hồ sơ này đã được liên kết với tài khoản của bạn rồi.");
                            _uow.Rollback();
                            return View(model);
                        }
                    }
                    else
                    {
                        // CASE B: Chưa có -> Tạo mới
                        do
                        {
                            maHSTC_Final = TPVAXWebsite.Common.KeyGenerator.GenMaHSTC(model.CCCD);
                        } while (_uow.HoSoTiemChungs.Any(h => h.MaHSTC == maHSTC_Final));

                        var hoSoMoi = new HoSoTiemChung
                        {
                            MaHSTC = maHSTC_Final,
                            HoTen = model.HoTen,
                            NgaySinh = model.NgaySinh,
                            GioiTinh = model.GioiTinh,
                            // Nếu không nhập CCCD, tự sinh mã tạm cho trẻ em (CHILD_...)
                            CCCD = string.IsNullOrEmpty(model.CCCD) ? "CHILD" + DateTime.Now.Ticks.ToString().Substring(13) : model.CCCD,
                            GhiChu = model.GhiChu,
                            TrangThai = true
                        };
                        _uow.HoSoTiemChungs.Add(hoSoMoi);
                    }

                    // --- TẠO LIÊN KẾT ---
                    string maLK;
                    do
                    {
                        maLK = TPVAXWebsite.Common.KeyGenerator.GenMaLK(model.CCCD);
                    } while (_uow.LienKetHoSos.Any(l => l.MaLK == maLK));

                    var lienKet = new LienKetHoSo
                    {
                        MaLK = maLK,
                        MaKH = currentMaKH,
                        MaHSTC = maHSTC_Final,
                        VaiTro = model.QuanHe,
                        NgayLienKet = DateTime.Now
                    };
                    _uow.LienKetHoSos.Add(lienKet);

                    _uow.Commit();

                    TempData["SuccessMessage"] = "Đã thêm người thân thành công!";

                    // Điều hướng thông minh: Nếu đến từ trang Đặt lịch thì quay về Đặt lịch
                    // Bạn có thể dùng TempData hoặc QueryString để biết nguồn gọi
                    return RedirectToAction("DatLich", "LichTiem");
                }
                catch (Exception ex)
                {
                    _uow.Rollback();
                    ModelState.AddModelError("", "Lỗi: " + ex.Message);
                }
            }
            return View(model);
        }
    }
}