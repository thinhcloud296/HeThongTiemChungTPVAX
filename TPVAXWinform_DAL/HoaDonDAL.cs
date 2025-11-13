using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class HoaDonDAL
    {
        private string selectSql = "SELECT * FROM dbo.HoaDon";
        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectSql);
        }

        public void Insert(HoaDonDTO hd)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();

                    row["MaHD"] = hd.MaHD;
                    row["NgayLap"] = hd.NgayLap;
                    row["TongTien"] = hd.TongTien;

                    // Xử lý các cột cho phép NULL
                    row["TrangThai"] = (object)hd.TrangThai ?? DBNull.Value;
                    row["MaKH"] = (object)hd.MaKH ?? DBNull.Value;
                    row["MaNV"] = (object)hd.MaNV ?? DBNull.Value;
                    row["MaKM"] = (object)hd.MaKM ?? DBNull.Value;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm hóa đơn: " + ex.Message);
            }
        }

        public void Edit(HoaDonDTO hd)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(hd.MaHD);
                    if (row != null)
                    {
                        row["NgayLap"] = hd.NgayLap;
                        row["TongTien"] = hd.TongTien;
                        row["TrangThai"] = (object)hd.TrangThai ?? DBNull.Value;
                        row["MaKH"] = (object)hd.MaKH ?? DBNull.Value;
                        row["MaNV"] = (object)hd.MaNV ?? DBNull.Value;
                        row["MaKM"] = (object)hd.MaKM ?? DBNull.Value;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy hóa đơn với mã: {hd.MaHD}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa hóa đơn: " + ex.Message);
            }
        }

    }
}
