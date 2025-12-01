using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Controllers
{
    public class HoSoController : Controller
    {
        private readonly TPVAXDbContext _context = new TPVAXDbContext();

        // GET: HoSo/ThemHoSo
        [HttpGet]
        public ActionResult ThemHoSo()
        {
            // Xóa thông báo lỗi cũ
            TempData.Remove("ErrorMessage");
            return View();
        }

        // POST: HoSo/ThemHoSo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemHoSo(string HoTen, string GioiTinh, DateTime? NgaySinh, string CCCD, string VaiTro, string GhiChu)
        {
            try
            {
                var kh = Session["KH"] as KhachHang;
                if (kh == null)
                {
                    TempData["ErrorMessage"] = "Vui lòng đăng nhập để thêm hồ sơ.";
                    return RedirectToAction("Login", "Account");
                }

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(HoTen))
                {
                    TempData["ErrorMessage"] = "Họ tên không được để trống.";
                    return RedirectToAction("Dashboard", "Account");
                }

                if (string.IsNullOrWhiteSpace(GioiTinh))
                {
                    TempData["ErrorMessage"] = "Giới tính không được để trống.";
                    return RedirectToAction("Dashboard", "Account");
                }

                if (!NgaySinh.HasValue)
                {
                    TempData["ErrorMessage"] = "Ngày sinh không được để trống.";
                    return RedirectToAction("Dashboard", "Account");
                }

                if (string.IsNullOrWhiteSpace(VaiTro))
                {
                    TempData["ErrorMessage"] = "Quan hệ không được để trống.";
                    return RedirectToAction("Dashboard", "Account");
                }

                // Xử lý CCCD: bắt buộc cho người lớn, không bắt buộc cho con
                if (VaiTro != "Con" && string.IsNullOrWhiteSpace(CCCD))
                {
                    TempData["ErrorMessage"] = "CCCD/CMND là bắt buộc đối với người thân (không phải con).";
                    return RedirectToAction("Dashboard", "Account");
                }

                // Nếu không nhập CCCD, set thành N/A
                if (string.IsNullOrWhiteSpace(CCCD))
                {
                    CCCD = "N/A";
                }

                // Tạo đối tượng HoSoTiemChung
                var hoSoMoi = new HoSoTiemChung
                {
                    MaHSTC = GenerateMaHSTC(),
                    HoTen = HoTen.Trim(),
                    GioiTinh = GioiTinh.Trim(),
                    NgaySinh = NgaySinh.Value,
                    CCCD = CCCD.Trim(),
                    GhiChu = GhiChu,
                    TrangThai = true
                };

                _context.HoSoTiemChungs.Add(hoSoMoi);
                _context.SaveChanges();

                var lienKet = new LienKetHoSo
                {
                    MaLK = GenerateMaLK(),
                    MaKH = kh.MaKH,
                    MaHSTC = hoSoMoi.MaHSTC,
                    VaiTro = VaiTro.Trim(),
                    NgayLienKet = DateTime.Now
                };

                _context.LienKetHoSos.Add(lienKet);
                _context.SaveChanges();

                TempData["ThongBao"] = "Đã thêm hồ sơ mới thành công!";
                return RedirectToAction("Dashboard", "Account");
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException != null ? ex.InnerException.Message : "";
                TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message} | Inner: {innerException}";
                return RedirectToAction("Dashboard", "Account");
            }
        }


        private string GenerateMaHSTC()
        {
            // Đếm số lượng record hiện tại + 1 (nhanh hơn OrderByDescending)
            int count = _context.HoSoTiemChungs.Count();
            return "HSTC" + (count + 1).ToString("D6");
        }

        private string GenerateMaLK()
        {
            // Đếm số lượng record hiện tại + 1 (nhanh hơn OrderByDescending)
            int count = _context.LienKetHoSos.Count();
            return "LKHS" + (count + 1).ToString("D6");
        }
      
        [HttpGet]
        public ActionResult ChinhSuaHoSo(string maHSTC)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null) return RedirectToAction("Login", "Account");

            // Xóa thông báo lỗi cũ
            TempData.Remove("ErrorMessage");

            // Kiểm tra quyền quản lý hồ sơ
            var lienKet = _context.LienKetHoSos
                .FirstOrDefault(lk => lk.MaKH == kh.MaKH && lk.MaHSTC == maHSTC);

            if (lienKet == null)
            {
                TempData["ThongBao"] = "Bạn không có quyền chỉnh sửa hồ sơ này.";
                return RedirectToAction("Dashboard", "Account");
            }

            var hoSo = _context.HoSoTiemChungs.Find(maHSTC);
            if (hoSo == null) return HttpNotFound();

            return View(hoSo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaHoSo(HoSoTiemChung hoSoSua)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null) return RedirectToAction("Login", "Account");

            var lienKet = _context.LienKetHoSos
                .FirstOrDefault(lk => lk.MaKH == kh.MaKH && lk.MaHSTC == hoSoSua.MaHSTC);

            if (lienKet == null)
            {
                TempData["ThongBao"] = "Bạn không có quyền chỉnh sửa hồ sơ này.";
                return RedirectToAction("Dashboard", "Account");
            }

            if (!ModelState.IsValid)
            {
                TempData["ThongBao"] = "Dữ liệu không hợp lệ.";
                return View(hoSoSua);
            }

            var hoSo = _context.HoSoTiemChungs.Find(hoSoSua.MaHSTC);
            if (hoSo == null) return HttpNotFound();

            hoSo.HoTen = hoSoSua.HoTen;
            hoSo.GioiTinh = hoSoSua.GioiTinh;
            hoSo.NgaySinh = hoSoSua.NgaySinh;
            hoSo.CCCD = hoSoSua.CCCD;
            hoSo.GhiChu = hoSoSua.GhiChu;

            _context.SaveChanges();

            TempData["ThongBao"] = "Đã cập nhật hồ sơ thành công!";
            return RedirectToAction("Dashboard", "Account");
        }


    }
}
