using System;
using System.Collections.Generic;
using System.Data;
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
                DBConnect.Param("@MaKH", MaKH, SqlDbType.NVarChar, 10)
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
                DBConnect.Param("@MaNV", MaNV, SqlDbType.NVarChar, 10)
            );
        }

        public void ResetPassword(string maNVorMaKH, string newPassword)
        {
            try
            {
                // Thử tìm MaTK từ NhanVien trước
                string sqlGetMaTK = @"
                    SELECT MaTK FROM dbo.NhanVien WHERE MaNV = @MaNV";

                DataTable dtMaTK = DBConnect.ExecuteQuery(
                    sqlGetMaTK,
                    CommandType.Text,
                    DBConnect.Param("@MaNV", maNVorMaKH, SqlDbType.Char, 10)
                );

                // Nếu không tìm thấy trong NhanVien, thử tìm trong KhachHang
                if (dtMaTK.Rows.Count == 0)
                {
                    sqlGetMaTK = @"
                        SELECT MaTK FROM dbo.KhachHang WHERE MaKH = @MaKH";

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

                // Sử dụng EditableBuffer để cập nhật mật khẩu
                using (var buffer = DBConnect.CreateBuffer("SELECT * FROM dbo.TaiKhoan"))
                {
                    DataRow row = buffer.Table.Rows.Cast<DataRow>()
                        .FirstOrDefault(r => r["MaTK"].ToString().Trim() == maTK);

                    if (row != null)
                    {
                        row["MatKhau"] = newPassword;
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
                throw new Exception("Lỗi khi đặt lại mật khẩu: " + ex.Message);
            }
        }
    }
}
