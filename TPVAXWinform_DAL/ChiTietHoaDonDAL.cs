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
        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectSql);
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
                        row["MaHD"] = cthd.MaHD;

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
