using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class LoaiVaccineDAL
    {
        public DataTable GetData()
        {
            const string sql = "SELECT * FROM dbo.LoaiVaccine";
            return DBConnect.ExecuteQuery(sql);
        }
        private string lastMaLoaiVaccine = "";
        private string selectSql = "SELECT * FROM dbo.LoaiVaccine";
        public string GetLastMaLoaiVaccine()
        {
            const string sql = "SELECT TOP 1 MaLoai FROM dbo.LoaiVaccine ORDER BY MaLoai DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaLoaiVaccine = dt.Rows[0]["MaLoai"].ToString();
            }
            return lastMaLoaiVaccine;
        }
        public string CreateNewMaLoaiVaccine()
        {
            if (string.IsNullOrEmpty(lastMaLoaiVaccine))
            {
                lastMaLoaiVaccine = GetLastMaLoaiVaccine();
            }
            if (string.IsNullOrEmpty(lastMaLoaiVaccine))
            {
                return "LV000001";
            }
            string numericPart = lastMaLoaiVaccine.Substring(2);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string MaLoaiVaccine = "LV" + number.ToString("D6");
                lastMaLoaiVaccine = MaLoaiVaccine;
                return MaLoaiVaccine;
            }
            else
            {
                throw new Exception("Invalid MaLoai format in database.");
            }
        }
        public void Insert(LoaiVaccineDTO loaiVaccine)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();
                    row["MaLoai"] = loaiVaccine.MaLoai;
                    row["TenLoai"] = loaiVaccine.TenLoai;
                    row["MoTa"] = loaiVaccine.MoTa;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm loại vaccine: " + ex.Message);
            }
        }

        public void Edit(LoaiVaccineDTO loaiVaccine)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(loaiVaccine.MaLoai);
                    if (row != null)
                    {
                        row["TenLoai"] = loaiVaccine.TenLoai;
                        row["MoTa"] = loaiVaccine.MoTa;
                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy loại vaccine với mã: {loaiVaccine.MaLoai}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa loại vaccine: " + ex.Message);
            }
        }
    }
}
