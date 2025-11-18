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

            dal.CreateTaiKhoan(maTK, hashedPassword);
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
    }
}