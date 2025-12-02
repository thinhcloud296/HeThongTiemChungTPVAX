using System;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.Models.Domain;
using System.Data.Entity;
using TPVAXWebsite.DAL;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý lịch tiêm
    /// FIX #4: Đã thêm logic hoàn trả tồn kho khi hủy lịch
    /// </summary>
    public class LichTiemController : Controller
    {
        private TPVAXDbContext _context = new TPVAXDbContext();

        // Hiển thị danh sách lịch tiêm (toàn bộ hoặc theo khách hàng)
        public ActionResult Index()
        {
            var lichTiems = _context.LichTiems
                .Include(l => l.Vaccine)
                .Include(l => l.HoSoTiemChung)
                .ToList();

            return View(lichTiems);
        }

        // GET: LichTiem/DatLich
        public ActionResult DatLich(string maVC)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để đặt lịch tiêm.";
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrEmpty(maVC))
            {
                TempData["ErrorMessage"] = "Không tìm thấy thông tin văc xin.";
                return RedirectToAction("Index", "VaccinePhongBenh");
            }

            var vaccine = _context.Vaccines.Find(maVC);
            if (vaccine == null)
            {
                TempData["ErrorMessage"] = "Văc xin không tồn tại.";
                return RedirectToAction("Index", "VaccinePhongBenh");
            }

            // Lấy danh sách hồ sơ tiêm chủng của khách hàng
            var hoSosData = (from lk in _context.LienKetHoSos
                             join hs in _context.HoSoTiemChungs on lk.MaHSTC equals hs.MaHSTC
                             where lk.MaKH == kh.MaKH && hs.TrangThai == true
                             select new
                             {
                                 hs.MaHSTC,
                                 hs.HoTen,
                                 hs.NgaySinh,
                                 lk.VaiTro
                             }).ToList();

            var hoSos = hoSosData.Select(x => new SelectListItem
            {
                Value = x.MaHSTC,
                Text = $"{x.HoTen} - {x.NgaySinh:dd/MM/yyyy} ({x.VaiTro})"
            }).ToList();

            var model = new TPVAXWebsite.Models.ViewModels.DatLichTiemViewModel
            {
                MaVC = vaccine.MaVC,
                TenVaccine = vaccine.TenVC,
                GiaBan = vaccine.GiaBan,
                HinhAnh = vaccine.HinhAnh,
                DanhSachHoSo = hoSos,
                NgayHenTiem = DateTime.Now.AddDays(1).Date.AddHours(9) // Mặc định ngày mai 9h sáng
            };

            return View(model);
        }

        // POST: LichTiem/DatLich
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatLich(TPVAXWebsite.Models.ViewModels.DatLichTiemViewModel model)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập.";
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                // Reload danh sách hồ sơ nếu lỗi
                var hoSosData = (from lk in _context.LienKetHoSos
                                 join hs in _context.HoSoTiemChungs on lk.MaHSTC equals hs.MaHSTC
                                 where lk.MaKH == kh.MaKH && hs.TrangThai == true
                                 select new
                                 {
                                     hs.MaHSTC,
                                     hs.HoTen,
                                     hs.NgaySinh,
                                     lk.VaiTro
                                 }).ToList();

                model.DanhSachHoSo = hoSosData.Select(x => new SelectListItem
                {
                    Value = x.MaHSTC,
                    Text = $"{x.HoTen} - {x.NgaySinh:dd/MM/yyyy} ({x.VaiTro})"
                }).ToList();
                return View(model);
            }

            try
            {
                // Kiểm tra hồ sơ có thuộc về khách hàng không
                var lienKet = _context.LienKetHoSos
                    .FirstOrDefault(lk => lk.MaKH == kh.MaKH && lk.MaHSTC == model.MaHSTC);
                if (lienKet == null)
                {
                    ModelState.AddModelError("MaHSTC", "Hồ sơ không hợp lệ.");
                    
                    // Reload danh sách hồ sơ
                    var hoSosData = (from lk in _context.LienKetHoSos
                                     join hs in _context.HoSoTiemChungs on lk.MaHSTC equals hs.MaHSTC
                                     where lk.MaKH == kh.MaKH && hs.TrangThai == true
                                     select new
                                     {
                                         hs.MaHSTC,
                                         hs.HoTen,
                                         hs.NgaySinh,
                                         lk.VaiTro
                                     }).ToList();

                    model.DanhSachHoSo = hoSosData.Select(x => new SelectListItem
                    {
                        Value = x.MaHSTC,
                        Text = $"{x.HoTen} - {x.NgaySinh:dd/MM/yyyy} ({x.VaiTro})"
                    }).ToList();
                    return View(model);
                }

                // Kiểm tra vaccine tồn tại
                var vaccine = _context.Vaccines.Find(model.MaVC);
                if (vaccine == null)
                {
                    TempData["ErrorMessage"] = "Vaccine không tồn tại.";
                    return RedirectToAction("Index", "VaccinePhongBenh");
                }

                // Kiểm tra vaccine đã có trong giỏ hàng chưa
                var itemTrongGio = _context.GioHangs
                    .FirstOrDefault(g => g.MaKH == kh.MaKH
                                      && g.MaSanPham == model.MaVC
                                      && g.LoaiSanPham == "VACCINE");

                if (itemTrongGio != null)
                {
                    // Tăng số lượng nếu đã có
                    itemTrongGio.SoLuong += 1;
                }
                else
                {
                    // Thêm mới vào giỏ hàng
                    var itemMoi = new GioHang
                    {
                        MaKH = kh.MaKH,
                        MaSanPham = model.MaVC,
                        LoaiSanPham = "VACCINE",
                        SoLuong = 1
                    };
                    _context.GioHangs.Add(itemMoi);
                }

                _context.SaveChanges();

                TempData["SuccessMessage"] = "Đã thêm vaccine vào giỏ hàng! Vui lòng chọn thời gian tiêm và thanh toán.";
                return RedirectToAction("Index", "GioHang");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                // Reload danh sách hồ sơ
                var hoSosData = (from lk in _context.LienKetHoSos
                                 join hs in _context.HoSoTiemChungs on lk.MaHSTC equals hs.MaHSTC
                                 where lk.MaKH == kh.MaKH && hs.TrangThai == true
                                 select new
                                 {
                                     hs.MaHSTC,
                                     hs.HoTen,
                                     hs.NgaySinh,
                                     lk.VaiTro
                                 }).ToList();

                model.DanhSachHoSo = hoSosData.Select(x => new SelectListItem
                {
                    Value = x.MaHSTC,
                    Text = $"{x.HoTen} - {x.NgaySinh:dd/MM/yyyy} ({x.VaiTro})"
                }).ToList();
                return View(model);
            }
        }

        // Đổi lịch (cập nhật ngày hẹn)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoiLichNgay(string MaLT, DateTime NgayHenTiem)
        {
            try
            {
                var kh = Session["KH"] as KhachHang;
                if (kh == null)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập." });
                }

                var lich = _context.LichTiems.Find(MaLT);
                if (lich == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy lịch hẹn." });
                }

                // Kiểm tra quyền: chỉ cho phép đổi lịch của hồ sơ liên kết với tài khoản
                var lienKet = _context.LienKetHoSos
                    .FirstOrDefault(lk => lk.MaKH == kh.MaKH && lk.MaHSTC == lich.MaHSTC);

                if (lienKet == null)
                {
                    return Json(new { success = false, message = "Bạn không có quyền đổi lịch này." });
                }

                // Kiểm tra trạng thái có thể đổi
                if (lich.TrangThai != "Chưa tiêm")
                {
                    return Json(new { success = false, message = "Chỉ có thể đổi lịch hẹn đang chờ tiêm." });
                }

                // Kiểm tra ngày hẹn mới phải ít nhất là ngày mai
                if (NgayHenTiem.Date <= DateTime.Now.Date)
                {
                    return Json(new { success = false, message = "Ngày hẹn mới phải từ ngày mai trở đi." });
                }

                // Kiểm tra ngày hẹn không quá xa (tối đa 1 năm)
                if (NgayHenTiem.Date > DateTime.Now.AddYears(1).Date)
                {
                    return Json(new { success = false, message = "Ngày hẹn mới không được quá 1 năm kể từ hôm nay." });
                }

                // Kiểm tra không phải Chủ nhật
                if (NgayHenTiem.DayOfWeek == DayOfWeek.Sunday)
                {
                    return Json(new { success = false, message = "Trung tâm không làm việc vào Chủ nhật. Vui lòng chọn ngày khác." });
                }

                // Kiểm tra giờ làm việc (8h - 17h)
                if (NgayHenTiem.Hour < 8 || NgayHenTiem.Hour > 17)
                {
                    return Json(new { success = false, message = "Vui lòng chọn giờ trong khung 08:00 - 17:00." });
                }

                // Kiểm tra không đổi lịch quá sát ngày hẹn cũ (ít nhất 24h trước)
                if (lich.NgayHenTiem <= DateTime.Now.AddHours(24))
                {
                    return Json(new { success = false, message = "Không thể đổi lịch khi còn dưới 24 giờ đến giờ hẹn. Vui lòng liên hệ trung tâm." });
                }

                // *** KIỂM TRA TRÙNG LỊCH ***
                // Kiểm tra xem hồ sơ này đã có lịch nào khác trong khoảng 2 giờ không
                var ngayBatDau = NgayHenTiem.AddHours(-2);
                var ngayKetThuc = NgayHenTiem.AddHours(2);
                
                var lichTrung = _context.LichTiems
                    .Include(lt => lt.Vaccine)
                    .Where(lt => lt.MaHSTC == lich.MaHSTC
                              && lt.MaLT != MaLT  // Loại trừ lịch hiện tại
                              && lt.TrangThai == "Chưa tiêm"
                              && lt.NgayHenTiem >= ngayBatDau
                              && lt.NgayHenTiem <= ngayKetThuc)
                    .FirstOrDefault();

                if (lichTrung != null)
                {
                    return Json(new { 
                        success = false, 
                        message = $"Hồ sơ này đã có lịch hẹn tiêm {lichTrung.Vaccine?.TenVC ?? "vaccine"} " +
                                  $"vào ngày {lichTrung.NgayHenTiem:dd/MM/yyyy} lúc {lichTrung.NgayHenTiem:HH:mm}. " +
                                  "Vui lòng chọn thời gian khác (cách ít nhất 2 giờ)."
                    });
                }

                // Lưu ngày cũ để thông báo
                var ngayCu = lich.NgayHenTiem;
                
                lich.NgayHenTiem = NgayHenTiem;
                _context.SaveChanges();

                return Json(new { 
                    success = true, 
                    message = $"Đổi lịch thành công! Lịch hẹn mới: {NgayHenTiem:dd/MM/yyyy} lúc {NgayHenTiem:HH:mm}",
                    ngayCu = ngayCu.ToString("dd/MM/yyyy HH:mm"),
                    ngayMoi = NgayHenTiem.ToString("dd/MM/yyyy HH:mm")
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // Hủy lịch (chỉ đổi trạng thái, không xóa)
        // FIX #4: Hoàn trả tồn kho khi hủy lịch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult HuyLich(string id, string lyDo = "")
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var kh = Session["KH"] as KhachHang;
                    if (kh == null)
                    {
                        return Json(new { success = false, message = "Vui lòng đăng nhập." });
                    }

                    var lich = _context.LichTiems.Find(id);
                    if (lich == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy lịch hẹn." });
                    }

                    // Kiểm tra quyền
                    var lienKet = _context.LienKetHoSos
                        .FirstOrDefault(lk => lk.MaKH == kh.MaKH && lk.MaHSTC == lich.MaHSTC);

                    if (lienKet == null)
                    {
                        return Json(new { success = false, message = "Bạn không có quyền hủy lịch này." });
                    }

                    // Kiểm tra trạng thái có thể hủy
                    if (lich.TrangThai == "Đã tiêm")
                    {
                        return Json(new { success = false, message = "Không thể hủy lịch đã tiêm." });
                    }

                    if (lich.TrangThai == "Đã hủy")
                    {
                        return Json(new { success = false, message = "Lịch hẹn này đã được hủy trước đó." });
                    }

                    // Kiểm tra không hủy lịch quá sát giờ hẹn (ít nhất 2 giờ trước)
                    if (lich.NgayHenTiem <= DateTime.Now.AddHours(2))
                    {
                        return Json(new { success = false, message = "Không thể hủy lịch khi còn dưới 2 giờ đến giờ hẹn. Vui lòng liên hệ trung tâm." });
                    }

                    // FIX #4: Hoàn trả tồn kho vaccine (đơn giản)
                    if (!string.IsNullOrEmpty(lich.MaVC))
                    {
                        var vaccine = _context.Vaccines.Find(lich.MaVC);
                        if (vaccine != null)
                        {
                            vaccine.SoLuong += 1; // Hoàn trả 1 liều
                        }
                    }

                    lich.TrangThai = "Đã hủy";
                    
                    // Lưu lý do hủy vào GhiChu nếu có
                    string ghiChuCu = lich.GhiChu ?? "";
                    if (!string.IsNullOrEmpty(lyDo))
                    {
                        lich.GhiChu = $"{ghiChuCu} [Hủy bởi KH - {DateTime.Now:dd/MM/yyyy HH:mm}] Lý do: {lyDo} | Đã hoàn kho";
                    }
                    else
                    {
                        lich.GhiChu = $"{ghiChuCu} [Hủy bởi KH - {DateTime.Now:dd/MM/yyyy HH:mm}] | Đã hoàn kho";
                    }
                    
                    _context.SaveChanges();
                    transaction.Commit();

                    return Json(new { success = true, message = "Hủy lịch thành công! Vaccine đã được hoàn trả vào kho." });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
