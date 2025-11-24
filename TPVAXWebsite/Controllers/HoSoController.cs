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
            return View();
        }

        // POST: HoSo/ThemHoSo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemHoSo(HoSoTiemChung hoSoMoi, string VaiTro)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null) return RedirectToAction("Login", "Account");

            hoSoMoi.MaHSTC = GenerateMaHSTC();
            hoSoMoi.TrangThai = true;

            _context.HoSoTiemChungs.Add(hoSoMoi);
            _context.SaveChanges();

            var lienKet = new LienKetHoSo
            {
                MaLK = GenerateMaLK(),
                MaKH = kh.MaKH,
                MaHSTC = hoSoMoi.MaHSTC,
                VaiTro = VaiTro,
                NgayLienKet = DateTime.Now
            };

            _context.LienKetHoSos.Add(lienKet);
            _context.SaveChanges();

            TempData["ThongBao"] = "Đã thêm hồ sơ mới thành công!";
            return RedirectToAction("Dashboard", "Account");
        }


        private string GenerateMaHSTC()
        {
            string maHSTC;
            do
            {
                var last = _context.HoSoTiemChungs
                    .OrderByDescending(h => h.MaHSTC)
                    .Select(h => h.MaHSTC)
                    .FirstOrDefault();

                int next = 1;
                if (!string.IsNullOrEmpty(last) && last.Length > 2)
                {
                    string numPart = last.Substring(2);
                    if (int.TryParse(numPart, out int lastNum))
                    {
                        next = lastNum + 1;
                    }
                }
                maHSTC = "HS" + next.ToString("D8");
            } while (_context.HoSoTiemChungs.Any(h => h.MaHSTC == maHSTC));
            
            return maHSTC;
        }

        private string GenerateMaLK()
        {
            string maLK;
            do
            {
                var last = _context.LienKetHoSos
                    .OrderByDescending(lk => lk.MaLK)
                    .Select(lk => lk.MaLK)
                    .FirstOrDefault();

                int next = 1;
                if (!string.IsNullOrEmpty(last) && last.Length > 2)
                {
                    string numPart = last.Substring(2);
                    if (int.TryParse(numPart, out int lastNum))
                    {
                        next = lastNum + 1;
                    }
                }
                maLK = "LK" + next.ToString("D8");
            } while (_context.LienKetHoSos.Any(lk => lk.MaLK == maLK));
            
            return maLK;
        }
      
        [HttpGet]
        public ActionResult ChinhSuaHoSo(string maHSTC)
        {
            var kh = Session["KH"] as KhachHang;
            if (kh == null) return RedirectToAction("Login", "Account");

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
