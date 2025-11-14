using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class LoaiBenhDAL
    {
        public DataTable GetData()
        {
            const string sql = "SELECT * FROM dbo.LoaiBenh";
            return DBConnect.ExecuteQuery(sql);
        }
        private string lastMaLoaiBenh = "";
        private string selectSql = "SELECT * FROM dbo.LoaiBenh";
        public string GetLastMaLoaiBenh()
        {
            const string sql = "SELECT TOP 1 MaLoaiBenh FROM dbo.LoaiBenh ORDER BY MaLoaiBenh DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaLoaiBenh = dt.Rows[0]["MaLoaiBenh"].ToString();
            }
            return lastMaLoaiBenh;
        }
        public string CreateNewMaLoaiBenh()
        {
            if (string.IsNullOrEmpty(lastMaLoaiBenh))
            {
                lastMaLoaiBenh = GetLastMaLoaiBenh();
            }
            if (string.IsNullOrEmpty(lastMaLoaiBenh))
            {
                return "LBEN000001";
            }
            string numericPart = lastMaLoaiBenh.Substring(4);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string MaLoaiBenh = "LBEN" + number.ToString("D6");
                lastMaLoaiBenh = MaLoaiBenh;
                return MaLoaiBenh;
            }
            else
            {
                throw new Exception("Invalid MaLT format in database.");
            }
        }


        public void Insert(LoaiBenhDTO loaiBenh)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();
                    row["MaLoaiBenh"] = loaiBenh.MaLoaiBenh;
                    row["TenBenh"] = loaiBenh.TenBenh;
                    row["MoTa"] = loaiBenh.MoTa;
                    row["NhomDoiTuong"] = loaiBenh.NhomDoiTuong;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm loại bệnh: " + ex.Message);
            }
        }

        public void Edit(LoaiBenhDTO loaiBenh)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(loaiBenh.MaLoaiBenh);
                    if (row != null)
                    {
                        row["TenBenh"] = loaiBenh.TenBenh;
                        row["MoTa"] = loaiBenh.MoTa;
                        row["NhomDoiTuong"] = loaiBenh.NhomDoiTuong;
                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy loại bệnh với mã: {loaiBenh.MaLoaiBenh}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa loại bệnh: " + ex.Message);
            }
        }
    }
}

