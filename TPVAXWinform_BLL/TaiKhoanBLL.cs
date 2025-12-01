using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;
using BCrypt.Net; // <-- THÊM THƯ VIỆN BCRYPT

namespace TPVAXWinform_BLL
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL dal = new TaiKhoanDAL();

        public DataTable GetTaiKhoanByMaKH(string MaKH)
        {
            return dal.GetTaiKhoanByMaKH(MaKH);
        }

        public DataTable GetTaiKhoanByMaNV(string MaNV)
        {
            return dal.GetTaiKhoanByMaNV(MaNV);
        }

        // THÊM: Hàm Login với logic xác thực
        public DataTable Login(string maNV, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                throw new Exception("Vui lòng nhập mã nhân viên!"); // Sửa Encoding

            if (string.IsNullOrWhiteSpace(matKhau))
                throw new Exception("Vui lòng nhập mật khẩu!"); // Sửa Encoding

            // 1. Lấy thông tin user (bao gồm mật khẩu đã băm)
            DataTable dt = dal.GetLoginInfoByMaNV(maNV);

            if (dt.Rows.Count > 0)
            {
                string hashedPasswordFromDB = dt.Rows[0]["MatKhau"].ToString();

                // 2. Xác thực mật khẩu
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(matKhau, hashedPasswordFromDB);

                if (isPasswordValid)
                {
                    // Mật khẩu đúng, trả về thông tin user
                    return dt;
                }
            }

            // Nếu user không tồn tại hoặc mật khẩu sai, trả về bảng rỗng
            return new DataTable();
        }

        // THÊM: Hàm kiểm tra tồn tại
        public bool CheckTaiKhoanExists(string maTK)
        {
            return dal.CheckTaiKhoanExists(maTK);
        }
        public bool UpdateMatKhau(string maTK, string matKhauMoi)
        {
            return dal.UpdateMatKhau(maTK, matKhauMoi);
        }

        // THÊM: Hàm tạo mới (thực hiện băm)
        public void CreateTaiKhoan(string maTK, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(maTK))
                throw new Exception("Mã tài khoản không được để trống!"); // Sửa Encoding

            if (string.IsNullOrWhiteSpace(matKhau))
                throw new Exception("Mật khẩu không được để trống!"); // Sửa Encoding

            if (CheckTaiKhoanExists(maTK))
                throw new Exception("Mã tài khoản đã tồn tại!"); // Sửa Encoding

            // Băm mật khẩu
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(matKhau);

            // SỬA: Truyền đủ 3 tham số cho DAL (thêm tham số yeuCauDoiMK)
            dal.CreateTaiKhoan(maTK, hashedPassword, yeuCauDoiMK: true);
        }
        public void Delete(string maTK)
        {
            dal.Delete(maTK);
        }
        public void ResetPassword(string maNVorMaKH, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new Exception("Mật khẩu mới không được để trống!");

            // Băm mật khẩu mới
            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            dal.ResetPassword(maNVorMaKH, hashedNewPassword);
        }

        /// <summary>
        /// Đổi mật khẩu (cần xác thực mật khẩu cũ)
        /// </summary>
        public void ChangePassword(string maTK, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(maTK))
                throw new Exception("Mã tài khoản không hợp lệ!");

            if (string.IsNullOrWhiteSpace(oldPassword))
                throw new Exception("Vui lòng nhập mật khẩu cũ!");

            if (string.IsNullOrWhiteSpace(newPassword))
                throw new Exception("Vui lòng nhập mật khẩu mới!");

            if (newPassword.Length < 6)
                throw new Exception("Mật khẩu mới phải có ít nhất 6 ký tự!");

            // Lấy mật khẩu đã băm từ DB
            string hashedPasswordFromDB = dal.GetHashedPasswordByMaTK(maTK);

            if (string.IsNullOrEmpty(hashedPasswordFromDB))
                throw new Exception("Không tìm thấy tài khoản!");

            // Xác thực mật khẩu cũ
            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(oldPassword, hashedPasswordFromDB);

            if (!isOldPasswordValid)
                throw new Exception("Mật khẩu cũ không đúng!");

            // Băm mật khẩu mới và cập nhật
            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            dal.UpdatePassword(maTK, hashedNewPassword);
        }

        /// <summary>
        /// Xóa cờ yêu cầu đổi mật khẩu sau khi user đổi thành công
        /// </summary>
        public void ClearYeuCauDoiMK(string maTK)
        {
            dal.ClearYeuCauDoiMK(maTK);
        }

        /// <summary>
        /// Đổi mật khẩu lần đầu (bắt buộc) - không cần xác thực mật khẩu cũ
        /// </summary>
        public void ChangePasswordFirstTime(string maTK, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(maTK))
                throw new Exception("Mã tài khoản không hợp lệ!");

            if (string.IsNullOrWhiteSpace(newPassword))
                throw new Exception("Vui lòng nhập mật khẩu mới!");

            if (newPassword.Length < 6)
                throw new Exception("Mật khẩu mới phải có ít nhất 6 ký tự!");

            // Băm mật khẩu mới và cập nhật
            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            dal.UpdatePassword(maTK, hashedNewPassword);

            // Xóa cờ yêu cầu đổi mật khẩu
            dal.ClearYeuCauDoiMK(maTK);
        }
    }
}