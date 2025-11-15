using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class ChiTietHoaDonDAL
    {
        private string selectSql = "SELECT * FROM dbo.ChiTietHoaDon";
        private string lastMaCTHD = "";
        public DataTable GetDataByMaHD(string MaHD)
        {
            return DBConnect.ExecuteQuery(
                "dbo.usp_ChiTietHoaDon_GetByMaHD",
                CommandType.StoredProcedure,
                DBConnect.Param("@MaHD", MaHD, SqlDbType.Char, 10) 
            );
        }
        public string GetLastMaCTHD()
        {
            const string sql = "SELECT TOP 1 MaCTHD FROM dbo.ChiTietHoaDon ORDER BY MaCTHD DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaCTHD = dt.Rows[0]["MaCTHD"].ToString();
            }
            return lastMaCTHD;
        }
        public string CreateNewMaCTHD()
        {
            if (string.IsNullOrEmpty(lastMaCTHD))
            {
                lastMaCTHD = GetLastMaCTHD();
            }
            if (string.IsNullOrEmpty(lastMaCTHD))
            {
                return "CTHD000001";
            }
            string numericPart = lastMaCTHD.Substring(4);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string nextMaCTHD = "CTHD" + number.ToString("D6");
                lastMaCTHD = nextMaCTHD;
                return nextMaCTHD;
            }
            else
            {
                throw new Exception("Invalid MaCTHD format in database.");
            }
        }
        public void Insert(ChiTietHoaDonDTO cthd)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();

                    row["MaCTHD"] = cthd.MaCTHD;
                    row["SoLuong"] = cthd.SoLuong;
                    row["DonGia"] = cthd.DonGia;
                    row["MaSanPham"] = cthd.MaSanPham;
                    row["LoaiSanPham"] = cthd.LoaiSanPham;
                    row["MaHD"] = cthd.MaHD;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết hóa đơn: " + ex.Message);
            }
        }

        public void Edit(ChiTietHoaDonDTO cthd)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(cthd.MaCTHD);
                    if (row != null)
                    {
                        row["SoLuong"] = cthd.SoLuong;
                        row["DonGia"] = cthd.DonGia;
                        row["MaSanPham"] = cthd.MaSanPham;
                        row["LoaiSanPham"] = cthd.LoaiSanPham;
                        row["MaCTHD"] = cthd.MaCTHD;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy chi tiết hóa đơn với mã: {cthd.MaCTHD}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa chi tiết hóa đơn: " + ex.Message);
            }
        }
    }
}
