using System;
using System.Linq;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;
using TPVAXWebsite.Helpers;

namespace TPVAXWebsite.Services
{
    public interface IAccountService
    {
        KhachHang Login(string soDienThoai, string matKhau);
        bool Register(RegisterViewModel model, out string errorMessage);
        KhachHang GetKhachHangByMaKH(string maKH);
        bool ChangePassword(string maKH, string oldPassword, string newPassword, out string errorMessage);
    }

    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public KhachHang Login(string soDienThoai, string matKhau)
        {
            try
            {
                // Tìm khách hàng theo số điện thoại
                var khachHang = _unitOfWork.Repository<KhachHang>()
                    .FirstOrDefault(k => k.SoDT == soDienThoai);

                if (khachHang == null)
                    return null;

                // Kiểm tra tài khoản
                if (string.IsNullOrEmpty(khachHang.MaTK))
                    return null;

                var taiKhoan = _unitOfWork.Repository<TaiKhoan>()
                    .GetById(khachHang.MaTK);

                if (taiKhoan == null)
                    return null;

                // Verify mật khẩu
                if (PasswordHelper.VerifyPassword(matKhau, taiKhoan.MatKhau))
                {
                    return khachHang;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Register(RegisterViewModel model, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // Kiểm tra CCCD đã tồn tại
                var existingByCCCD = _unitOfWork.Repository<KhachHang>()
                    .Any(k => k.CCCD == model.CCCD);

                if (existingByCCCD)
                {
                    errorMessage = "Số CCCD đã được đăng ký";
                    return false;
                }

                // Kiểm tra số điện thoại đã tồn tại
                var existingByPhone = _unitOfWork.Repository<KhachHang>()
                    .Any(k => k.SoDT == model.SoDT);

                if (existingByPhone)
                {
                    errorMessage = "Số điện thoại đã được đăng ký";
                    return false;
                }

                // Tạo mã tài khoản và mã khách hàng
                string maTK = GenerateMaTK();
                string maKH = GenerateMaKH(model.CCCD);

                // Tạo tài khoản
                var taiKhoan = new TaiKhoan
                {
                    MaTK = maTK,
                    MatKhau = PasswordHelper.HashPassword(model.MatKhau)
                };

                _unitOfWork.Repository<TaiKhoan>().Add(taiKhoan);

                // Tạo khách hàng
                var khachHang = new KhachHang
                {
                    MaKH = maKH,
                    HoTen = model.HoTen,
                    CCCD = model.CCCD,
                    NgaySinh = model.NgaySinh,
                    GioiTinh = model.GioiTinh,
                    DiaChi = model.DiaChi,
                    SoDT = model.SoDT,
                    Email = model.Email,
                    MaTK = maTK
                };

                _unitOfWork.Repository<KhachHang>().Add(khachHang);

                // Tạo hồ sơ tiêm chủng cho bản thân
                var hoSo = new HoSoTiemChung
                {
                    MaHSTC = GenerateMaHSTC(model.CCCD),
                    HoTen = model.HoTen,
                    GioiTinh = model.GioiTinh,
                    NgaySinh = model.NgaySinh ?? DateTime.Now,
                    CCCD = model.CCCD,
                    TrangThai = true
                };

                _unitOfWork.Repository<HoSoTiemChung>().Add(hoSo);

                // Tạo liên kết hồ sơ
                var lienKet = new LienKetHoSo
                {
                    MaLK = GenerateMaLK(),
                    VaiTro = "Bản thân",
                    NgayLienKet = DateTime.Now,
                    MaKH = maKH,
                    MaHSTC = hoSo.MaHSTC
                };

                _unitOfWork.Repository<LienKetHoSo>().Add(lienKet);

                // Lưu tất cả thay đổi
                _unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Có lỗi xảy ra: " + ex.Message;
                return false;
            }
        }

        public KhachHang GetKhachHangByMaKH(string maKH)
        {
            return _unitOfWork.Repository<KhachHang>().GetById(maKH);
        }

        public bool ChangePassword(string maKH, string oldPassword, string newPassword, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                var khachHang = _unitOfWork.Repository<KhachHang>().GetById(maKH);

                if (khachHang == null || string.IsNullOrEmpty(khachHang.MaTK))
                {
                    errorMessage = "Không tìm thấy tài khoản";
                    return false;
                }

                var taiKhoan = _unitOfWork.Repository<TaiKhoan>().GetById(khachHang.MaTK);

                if (taiKhoan == null)
                {
                    errorMessage = "Không tìm thấy tài khoản";
                    return false;
                }

                // Verify mật khẩu cũ
                if (!PasswordHelper.VerifyPassword(oldPassword, taiKhoan.MatKhau))
                {
                    errorMessage = "Mật khẩu cũ không đúng";
                    return false;
                }

                // Cập nhật mật khẩu mới
                taiKhoan.MatKhau = PasswordHelper.HashPassword(newPassword);
                _unitOfWork.Repository<TaiKhoan>().Update(taiKhoan);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Có lỗi xảy ra: " + ex.Message;
                return false;
            }
        }

        // Helper methods
        private string GenerateMaTK()
        {
            var count = _unitOfWork.Repository<TaiKhoan>().Count() + 1;
            return "TK" + count.ToString("D6");
        }

        private string GenerateMaKH(string cccd)
        {
            if (cccd.Length >= 12)
            {
                return "KHHG" + cccd.Substring(6, 6);
            }
            var count = _unitOfWork.Repository<KhachHang>().Count() + 1;
            return "KH" + count.ToString("D6");
        }

        private string GenerateMaHSTC(string cccd)
        {
            if (cccd.Length >= 12)
            {
                return "HSTM" + cccd.Substring(6, 6);
            }
            var count = _unitOfWork.Repository<HoSoTiemChung>().Count() + 1;
            return "HS" + count.ToString("D6");
        }

        private string GenerateMaLK()
        {
            var count = _unitOfWork.Repository<LienKetHoSo>().Count() + 1;
            return "LK" + count.ToString("D6");
        }
    }
}
