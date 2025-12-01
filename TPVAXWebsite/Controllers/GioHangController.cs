using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Controllers
{
    /// <summary>
    /// Controller quản lý giỏ hàng
    /// </summary>
    public class GioHangController : Controller
    {
        private readonly TPVAXDbContext _context = new TPVAXDbContext();

        // GET: GioHang/Index
        public ActionResult Index()
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để xem giỏ hàng.";
                return RedirectToAction("Login", "Account");
            }

            var gioHangViewModel = LoadGioHang(kh.MaKH);
            return View(gioHangViewModel);
        }

        // GET: GioHang/Cart
        public ActionResult Cart()
        {
            return RedirectToAction("Index");
        }

        // POST: GioHang/ThemVaoGio
        [HttpPost]
        public JsonResult ThemVaoGio(string MaSanPham, string LoaiSanPham, int SoLuong = 1)
        {
            try
            {
                var kh = Session["KH"] as KhachHang;
                if (kh == null)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập để thêm vào giỏ hàng." });
                }

                // Kiểm tra sản phẩm tồn tại
                if (LoaiSanPham == "VACCINE")
                {
                    var vaccine = _context.Vaccines.Find(MaSanPham);
                    if (vaccine == null)
                    {
                        return Json(new { success = false, message = "Vaccine không tồn tại." });
                    }
                    if (vaccine.SoLuong < SoLuong)
                    {
                        return Json(new { success = false, message = $"Chỉ còn {vaccine.SoLuong} liều vaccine." });
                    }
                }
                else if (LoaiSanPham == "GOIVACCINE")
                {
                    var goi = _context.GoiVaccines.Find(MaSanPham);
                    if (goi == null)
                    {
                        return Json(new { success = false, message = "Gói vaccine không tồn tại." });
                    }

                    // FIX: Kiểm tra tồn kho các vaccine trong gói
                    var chiTietGoi = _context.ChiTietGoiVaccines
                        .Where(ct => ct.MaGoi == MaSanPham)
                        .ToList();

                    foreach (var ctGoi in chiTietGoi)
                    {
                        var vaccineInGoi = _context.Vaccines.Find(ctGoi.MaVC);
                        if (vaccineInGoi == null)
                        {
                            return Json(new { success = false, message = $"Vaccine trong gói không tồn tại." });
                        }

                        int soLuongCan = (ctGoi.SoMui ?? 1) * SoLuong;
                        if (vaccineInGoi.SoLuong < soLuongCan)
                        {
                            return Json(new { 
                                success = false, 
                                message = $"Vaccine {vaccineInGoi.TenVC} trong gói không đủ số lượng. Cần {soLuongCan}, còn {vaccineInGoi.SoLuong}." 
                            });
                        }
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Loại sản phẩm không hợp lệ." });
                }

                // Kiểm tra sản phẩm đã có trong giỏ chưa
                var itemTrongGio = _context.GioHangs
                    .FirstOrDefault(g => g.MaKH == kh.MaKH
                                      && g.MaSanPham == MaSanPham
                                      && g.LoaiSanPham == LoaiSanPham);

                if (itemTrongGio != null)
                {
                    // Tăng số lượng
                    itemTrongGio.SoLuong += SoLuong;
                }
                else
                {
                    // Thêm mới
                    var itemMoi = new GioHang
                    {
                        MaKH = kh.MaKH,
                        MaSanPham = MaSanPham,
                        LoaiSanPham = LoaiSanPham,
                        SoLuong = SoLuong
                    };
                    _context.GioHangs.Add(itemMoi);
                }

                _context.SaveChanges();

                // Đếm số lượng item trong giỏ
                var tongSoLuong = _context.GioHangs
                    .Where(g => g.MaKH == kh.MaKH)
                    .Sum(g => g.SoLuong);

                return Json(new
                {
                    success = true,
                    message = "Đã thêm vào giỏ hàng thành công!",
                    cartCount = tongSoLuong
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // POST: GioHang/CapNhatSoLuong
        [HttpPost]
        public JsonResult CapNhatSoLuong(int MaGH, int SoLuong)
        {
            try
            {
                var kh = Session["KH"] as KhachHang;
                if (kh == null)
                {
                    return Json(new { success = false, message = "Phiên đăng nhập hết hạn." });
                }

                var item = _context.GioHangs
                    .FirstOrDefault(g => g.MaGH == MaGH && g.MaKH == kh.MaKH);

                if (item == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng." });
                }

                // Kiểm tra tồn kho nếu là vaccine
                if (item.LoaiSanPham == "VACCINE")
                {
                    var vaccine = _context.Vaccines.Find(item.MaSanPham);
                    if (vaccine != null && vaccine.SoLuong < SoLuong)
                    {
                        return Json(new
                        {
                            success = false,
                            message = $"Chỉ còn {vaccine.SoLuong} liều vaccine."
                        });
                    }
                }

                if (SoLuong <= 0)
                {
                    _context.GioHangs.Remove(item);
                }
                else
                {
                    item.SoLuong = SoLuong;
                }

                _context.SaveChanges();

                // Load lại giỏ hàng để tính toán
                var gioHang = LoadGioHang(kh.MaKH);

                return Json(new
                {
                    success = true,
                    message = "Cập nhật thành công!",
                    tongTien = gioHang.TongTien,
                    tongSoLuong = gioHang.TongSoLuong
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // POST: GioHang/XoaKhoiGio
        [HttpPost]
        public JsonResult XoaKhoiGio(int MaGH)
        {
            try
            {
                var kh = Session["KH"] as KhachHang;
                if (kh == null)
                {
                    return Json(new { success = false, message = "Phiên đăng nhập hết hạn." });
                }

                var item = _context.GioHangs
                    .FirstOrDefault(g => g.MaGH == MaGH && g.MaKH == kh.MaKH);

                if (item != null)
                {
                    _context.GioHangs.Remove(item);
                    _context.SaveChanges();
                }

                // Load lại giỏ hàng
                var gioHang = LoadGioHang(kh.MaKH);

                return Json(new
                {
                    success = true,
                    message = "Đã xóa sản phẩm khỏi giỏ hàng!",
                    tongTien = gioHang.TongTien,
                    tongSoLuong = gioHang.TongSoLuong
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // GET: GioHang/GetCartCount
        public JsonResult GetCartCount()
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }

            var count = _context.GioHangs
                .Where(g => g.MaKH == kh.MaKH)
                .Sum(g => (int?)g.SoLuong) ?? 0;

            return Json(new { count = count }, JsonRequestBehavior.AllowGet);
        }

        // POST: GioHang/XoaToanBo
        [HttpPost]
        public JsonResult XoaToanBo()
        {
            try
            {
                var kh = Session["KH"] as KhachHang;
                if (kh == null)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập." });
                }

                var items = _context.GioHangs.Where(g => g.MaKH == kh.MaKH).ToList();
                _context.GioHangs.RemoveRange(items);
                _context.SaveChanges();

                return Json(new { success = true, message = "Đã xóa toàn bộ giỏ hàng." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // Helper method: Load giỏ hàng
        private GioHangViewModel LoadGioHang(string maKH)
        {
            var gioHang = _context.GioHangs
                .Where(g => g.MaKH == maKH)
                .ToList();

            var cartItems = new List<GioHangItemViewModel>();
            decimal tongTien = 0;
            int tongSoLuong = 0;

            foreach (var item in gioHang)
            {
                GioHangItemViewModel cartItem = null;

                if (item.LoaiSanPham == "VACCINE")
                {
                    var vaccine = _context.Vaccines.Find(item.MaSanPham);
                    if (vaccine != null)
                    {
                        cartItem = new GioHangItemViewModel
                        {
                            MaGH = item.MaGH,
                            MaSanPham = item.MaSanPham,
                            TenSanPham = vaccine.TenVC,
                            LoaiSanPham = item.LoaiSanPham,
                            DonGia = vaccine.GiaBan,
                            SoLuong = item.SoLuong,
                            ThanhTien = vaccine.GiaBan * item.SoLuong,
                            HinhAnh = vaccine.HinhAnh
                        };
                    }
                }
                else if (item.LoaiSanPham == "GOIVACCINE")
                {
                    var goi = _context.GoiVaccines.Find(item.MaSanPham);
                    if (goi != null)
                    {
                        cartItem = new GioHangItemViewModel
                        {
                            MaGH = item.MaGH,
                            MaSanPham = item.MaSanPham,
                            TenSanPham = goi.TenGoi,
                            LoaiSanPham = item.LoaiSanPham,
                            DonGia = goi.GiaGoi,
                            SoLuong = item.SoLuong,
                            ThanhTien = goi.GiaGoi * item.SoLuong,
                            HinhAnh = goi.HinhAnh
                        };
                    }
                }

                if (cartItem != null)
                {
                    cartItems.Add(cartItem);
                    tongTien += cartItem.ThanhTien;
                    tongSoLuong += cartItem.SoLuong;
                }
            }

            return new GioHangViewModel
            {
                MaKH = maKH,
                Items = cartItems,
                TongTien = tongTien,
                TongSoLuong = tongSoLuong
            };
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
