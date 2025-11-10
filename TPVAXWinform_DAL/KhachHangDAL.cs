using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class KhachHangDAL
    {
        public DataTable GetData()
        {
            const string sql = "SELECT * FROM dbo.KhachHang";
            return DBConnect.ExecuteQuery(sql);
        }
        public string CreateMaKH(string CCCD)
        {
            string cccdSuffix = CCCD.Length == 12 ? CCCD.Substring(6, 6) : string.Empty;
            return string.Equals(cccdSuffix, string.Empty) ? string.Empty : "KHHG" + cccdSuffix;
        }
        public bool IsKHExists(string CCCD)
        {
            const string sql = "SELECT COUNT(*) FROM dbo.KhachHang WHERE CCCD = @CCCD";

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(
                sql,
                CommandType.Text,
                DBConnect.Param("@CCCD", CCCD, SqlDbType.Char, 12)
            ));

            return count > 0;
        }
        public bool IsLinkedHSTCBanThan(string CCCD)
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM dbo.LienKetHoSo lk
                INNER JOIN dbo.KhachHang kh ON lk.MaKH = kh.MaKH
                WHERE kh.CCCD = @CCCD AND lk.VaiTro = N'Bản thân'";

            try
            {
                var param = DBConnect.Param("@CCCD", CCCD, SqlDbType.Char, 12);

                int count = Convert.ToInt32(DBConnect.ExecuteScalar(
                    sql,
                    CommandType.Text,
                    param
                ));

                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kiểm tra hồ sơ bản thân bằng CCCD: " + ex.Message);
            }
        }
        public void Insert(KhachHangDTO newKH)
        {
            try
            {
                using (var buffer = new DBConnect.EditableBuffer("SELECT * FROM dbo.KhachHang"))
                {
                    var row = buffer.Table.NewRow();

                    row["MaKH"] = newKH.MaKH;
                    row["HoTen"] = newKH.HoTen;
                    row["CCCD"] = newKH.CCCD;
                    row["NgaySinh"] = (object)newKH.NgaySinh ?? DBNull.Value;
                    row["GioiTinh"] = newKH.GioiTinh;
                    row["DiaChi"] = newKH.DiaChi;
                    row["SoDT"] = newKH.SoDT;
                    row["Email"] = newKH.Email;
                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting customer: " + ex.Message);
            }
        }
        public void Edit(KhachHangDTO khachHang)
        {
            try
            {
                using (var buffer = new DBConnect.EditableBuffer("SELECT * FROM dbo.KhachHang"))
                {
                    DataRow rowUpdate = buffer.Table.Rows.Find(khachHang.MaKH);
                    if (rowUpdate == null)
                        throw new Exception("Không tìm thấy khách hàng cần sửa.");
                    rowUpdate["HoTen"] = khachHang.HoTen;
                    rowUpdate["CCCD"] = khachHang.CCCD;
                    rowUpdate["NgaySinh"] = (object)khachHang.NgaySinh ?? DBNull.Value;
                    rowUpdate["GioiTinh"] = khachHang.GioiTinh;
                    rowUpdate["DiaChi"] = khachHang.DiaChi;
                    rowUpdate["SoDT"] = khachHang.SoDT;
                    rowUpdate["Email"] = khachHang.Email;
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error editing customer: " + ex.Message);
            }
        }
    }
}
