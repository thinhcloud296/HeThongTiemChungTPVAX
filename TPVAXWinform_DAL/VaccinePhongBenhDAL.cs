using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class VaccinePhongBenhDAL
    {
        private string selectSql = "SELECT * FROM dbo.VaccinePhongBenh";

        /// <summary>
        /// L?y t?t c? b?nh mà m?t vaccine phòng ???c
        /// </summary>
        public DataTable GetBenhByMaVC(string maVC)
        {
            string sql = @"
                SELECT 
                    vpb.MaVC,
                    vpb.MaLoaiBenh,
                    lb.TenBenh,
                    vpb.GhiChu
                FROM dbo.VaccinePhongBenh vpb
                INNER JOIN dbo.LoaiBenh lb ON vpb.MaLoaiBenh = lb.MaLoaiBenh
                WHERE vpb.MaVC = @MaVC";

            return DBConnect.ExecuteQuery(
                sql,
                CommandType.Text,
                DBConnect.Param("@MaVC", maVC, SqlDbType.Char, 10)
            );
        }

        /// <summary>
        /// Thêm m?t b?nh cho vaccine
        /// </summary>
        public void Insert(VaccinePhongBenhDTO vpb)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();
                    row["MaVC"] = vpb.MaVC;
                    row["MaLoaiBenh"] = vpb.MaLoaiBenh;
                    row["GhiChu"] = vpb.GhiChu;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("L?i khi thêm vaccine phòng b?nh: " + ex.Message);
            }
        }

        /// <summary>
        /// Xóa t?t c? b?nh c?a m?t vaccine (dùng khi c?p nh?t)
        /// </summary>
        public void DeleteByMaVC(string maVC)
        {
            try
            {
                string sql = "DELETE FROM dbo.VaccinePhongBenh WHERE MaVC = @MaVC";
                DBConnect.ExecuteNonQuery(
                    sql,
                    CommandType.Text,
                    DBConnect.Param("@MaVC", maVC, SqlDbType.Char, 10)
                );
            }
            catch (Exception ex)
            {
                throw new Exception("L?i khi xóa vaccine phòng b?nh: " + ex.Message);
            }
        }

        /// <summary>
        /// Thêm nhi?u b?nh cho m?t vaccine
        /// </summary>
        public void InsertMultiple(string maVC, List<string> danhSachMaLoaiBenh)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    foreach (string maLoaiBenh in danhSachMaLoaiBenh)
                    {
                        DataRow row = buffer.Table.NewRow();
                        row["MaVC"] = maVC;
                        row["MaLoaiBenh"] = maLoaiBenh;
                        row["GhiChu"] = string.Empty;

                        buffer.Table.Rows.Add(row);
                    }
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("L?i khi thêm nhi?u vaccine phòng b?nh: " + ex.Message);
            }
        }
    }
}
