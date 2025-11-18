using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // Thêm thư viện này
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class TaiKhoanDAL
    {
        public DataTable GetTaiKhoanByMaKH(string MaKH)
        {
            const string sql = @"
                SELECT tk.MaTK, tk.MatKhau
                FROM dbo.TaiKhoan tk
                INNER JOIN dbo.KhachHang kh ON tk.MaTK = kh.MaTK
                WHERE kh.MaKH = @MaKH";
            return DBConnect.ExecuteQuery(
                sql,
                CommandType.Text,
                // SỬA: Dùng đúng kiểu CHAR
                DBConnect.Param("@MaKH", MaKH, SqlDbType.Char, 10)
            );
        }
        public DataTable GetTaiKhoanByMaNV(string MaNV)
        {
            const string sql = @"
                SELECT tk.MaTK, tk.MatKhau
                FROM dbo.TaiKhoan tk
                INNER JOIN dbo.NhanVien nv ON tk.MaTK = nv.MaTK
                WHERE nv.MaNV = @MaNV";
            return DBConnect.ExecuteQuery(
                sql,
                CommandType.Text,
                // SỬA: Dùng đúng kiểu CHAR
                DBConnect.Param("@MaNV", MaNV, SqlDbType.Char, 10)
            );
        }

        // THÊM: Hàm chỉ lấy thông tin user và mật khẩu (đã băm)
        public DataTable GetLoginInfoByMaNV(string maNV)
        {
            // (Bạn có thể đổi "dbo.usp_TaiKhoan_GetLoginInfoByMaNV" nếu bạn đã tạo proc)
            const string sql = @"
              SELECT nv.MaNV, nv.HoTen, nv.Email, nv.SoDT, nv.ChucVu, tk.MaTK, tk.MatKhau
              FROM dbo.NhanVien nv
                     INNER JOIN dbo.TaiKhoan tk ON nv.MaTK = tk.MaTK
            WHERE nv.MaNV = @MaNV AND nv.TrangThai = '1'";

            return DBConnect.ExecuteQuery(
                 sql,
                 CommandType.Text,
                 DBConnect.Param("@MaNV", maNV, SqlDbType.Char, 10)
            );
        }

        // THÊM: Hàm kiểm tra tồn tại
        public bool CheckTaiKhoanExists(string maTK)
        {
            const string sql = "SELECT COUNT(*) FROM dbo.TaiKhoan WHERE MaTK = @MaTK";
            object result = DBConnect.ExecuteScalar(
                sql,
                CommandType.Text,
                DBConnect.Param("@MaTK", maTK, SqlDbType.Char, 10)
            );
            return (result != null && Convert.ToInt32(result) > 0);
        }

        // THÊM: Hàm tạo mới (nhận mật khẩu đã băm)
        public void CreateTaiKhoan(string maTK, string hashedPassword)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer("SELECT * FROM dbo.TaiKhoan"))
                {
                    DataRow row = buffer.Table.NewRow();
                    row["MaTK"] = maTK;
                    row["MatKhau"] = hashedPassword;
                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo tài khoản DAL: " + ex.Message);
            }
        }
        public void Delete(string maTK)
        {
            try
            {
                // 1. Kiểm tra xem Tài khoản này có đang được dùng không
                // (Kiểm tra trong NhanVien và KhachHang)
                string checkSql = @"
            IF EXISTS (SELECT 1 FROM dbo.NhanVien WHERE MaTK = @MaTK)
               OR EXISTS (SELECT 1 FROM dbo.KhachHang WHERE MaTK = @MaTK)
            BEGIN
                SELECT 1
            END
            ELSE
            BEGIN
                SELECT 0
            END";

                object result = DBConnect.ExecuteScalar(checkSql, CommandType.Text,
                    DBConnect.Param("@MaTK", maTK, SqlDbType.Char, 10));

                if (result != null && Convert.ToInt32(result) == 1)
                {
                    throw new Exception("Tài khoản này đang được sử dụng bởi Nhân viên hoặc Khách hàng. Không thể xóa!");
                }

                // 2. Nếu không ai dùng, thực hiện XÓA trực tiếp
                string deleteSql = "DELETE FROM dbo.TaiKhoan WHERE MaTK = @MaTK";

                int rowsAffected = DBConnect.ExecuteNonQuery(deleteSql, CommandType.Text,
                    DBConnect.Param("@MaTK", maTK, SqlDbType.Char, 10));

                if (rowsAffected == 0)
                {
                    throw new Exception("Không tìm thấy tài khoản để xóa!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa tài khoản DAL: " + ex.Message);
            }
        }

        // SỬA: Hàm Reset nhận mật khẩu đã băm
        public void ResetPassword(string maNVorMaKH, string hashedNewPassword)
        {
            try
            {
                // (Logic tìm MaTK của bạn đã ổn)
                string sqlGetMaTK = @"SELECT MaTK FROM dbo.NhanVien WHERE MaNV = @MaNV";
                DataTable dtMaTK = DBConnect.ExecuteQuery(
                    sqlGetMaTK,
                    CommandType.Text,
                    DBConnect.Param("@MaNV", maNVorMaKH, SqlDbType.Char, 10)
                );

                if (dtMaTK.Rows.Count == 0)
                {
                    sqlGetMaTK = @"SELECT MaTK FROM dbo.KhachHang WHERE MaKH = @MaKH";
                    dtMaTK = DBConnect.ExecuteQuery(
                        sqlGetMaTK,
                        CommandType.Text,
                        DBConnect.Param("@MaKH", maNVorMaKH, SqlDbType.Char, 10)
                    );
                }

                if (dtMaTK.Rows.Count == 0)
                {
                    throw new Exception("Không tìm thấy tài khoản!");
                }

                string maTK = dtMaTK.Rows[0]["MaTK"].ToString().Trim();

                using (var buffer = DBConnect.CreateBuffer("SELECT * FROM dbo.TaiKhoan"))
                {
                    DataRow row = buffer.Table.Rows.Cast<DataRow>()
                        .FirstOrDefault(r => r["MaTK"].ToString().Trim() == maTK);

                    if (row != null)
                    {
                        row["MatKhau"] = hashedNewPassword; // Cập nhật mật khẩu đã băm
                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception("Không tìm thấy tài khoản trong bảng TaiKhoan!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đặt lại mật khẩu DAL: " + ex.Message);
            }
        }
    }
}