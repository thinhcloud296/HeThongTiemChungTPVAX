using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class ChiTietGoiVaccineDAL
    {
        private string selectSql = "SELECT * FROM dbo.ChiTietGoiVaccine";

        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectSql);
        }

        public DataTable GetVaccinesByGoiVaccine(string maGoi)
        {
            string procName = "dbo.usp_GetVaccinesByGoiVaccine";
            var param = DBConnect.Param("@MaGoi", maGoi, SqlDbType.Char, 10);
            return DBConnect.ExecuteQuery(
                procName,
                CommandType.StoredProcedure,
                param
            );
        }

        public void Insert(ChiTietGoiVaccineDTO ct)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();

                    row["MaCTGoi"] = ct.MaCTGoi;
                    row["SoMui"] = (object)ct.SoMui ?? DBNull.Value;
                    row["GhiChu"] = ct.GhiChu;
                    row["MaGoi"] = ct.MaGoi;
                    row["MaVC"] = ct.MaVC;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết gói vaccine: " + ex.Message);
            }
        }

        public void Edit(ChiTietGoiVaccineDTO ct)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(ct.MaCTGoi);
                    if (row != null)
                    {
                        row["SoMui"] = (object)ct.SoMui ?? DBNull.Value;
                        row["GhiChu"] = ct.GhiChu;
                        row["MaGoi"] = ct.MaGoi;
                        row["MaVC"] = ct.MaVC;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy chi tiết gói vaccine với mã: {ct.MaCTGoi}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa chi tiết gói vaccine: " + ex.Message);
            }
        }

        /// <summary>
        /// Sinh mã chi tiết gói vaccine mới theo format CTGV000001 (10 ký tự)
        /// </summary>
        public string GenerateMaCTGoi()
        {
            DataTable dt = GetData();
            int maxNum = 0;
            foreach (DataRow row in dt.Rows)
            {
                string maCT = row["MaCTGoi"].ToString().Trim();
                if (maCT.StartsWith("CTGV") && maCT.Length == 10)
                {
                    if (int.TryParse(maCT.Substring(4), out int num))
                    {
                        if (num > maxNum) maxNum = num;
                    }
                }
            }
            return $"CTGV{(maxNum + 1).ToString("D6")}";
        }

        /// <summary>
        /// Xóa tất cả chi tiết gói vaccine theo mã gói
        /// </summary>
        public void DeleteByMaGoi(string maGoi)
        {
            try
            {
                // 1. Dùng tham số @MaGoi thay vì cộng chuỗi trực tiếp
                string sql = "DELETE FROM dbo.ChiTietGoiVaccine WHERE MaGoi = @MaGoi";

                // 2. Gọi hàm ExecuteNonQuery với đầy đủ tham số
                DBConnect.ExecuteNonQuery(
                    sql,
                    CommandType.Text,
                    DBConnect.Param("@MaGoi", maGoi, SqlDbType.Char, 10) // Giả sử MaGoi là CHAR(10)
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết gói vaccine: " + ex.Message);
            }
        }
    }
}
