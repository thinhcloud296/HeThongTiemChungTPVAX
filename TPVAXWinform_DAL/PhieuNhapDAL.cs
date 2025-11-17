using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class PhieuNhapDAL
    {
        private string selectSql = "SELECT * FROM dbo.PhieuNhapVaccine";
        private string lastMaPN = "";

        public DataTable GetDataDetail()
        {
            return DBConnect.ExecuteQuery("dbo.usp_PhieuNhap_GetAllWithDetails", CommandType.StoredProcedure);
        }

        public DataTable GetDetailByMaPN(string maPN)
        {
            return DBConnect.ExecuteQuery("dbo.usp_PhieuNhap_GetDetailByMaPN", CommandType.StoredProcedure,
                     DBConnect.Param("@MaPN", maPN, SqlDbType.Char, 10));
        }

        public string GetLastMaPN()
        {
            const string sql = "SELECT TOP 1 MaPN FROM dbo.PhieuNhapVaccine ORDER BY MaPN DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaPN = dt.Rows[0]["MaPN"].ToString();
            }
            return lastMaPN;
        }

        public string CreateNewMaPN()
        {
            if (string.IsNullOrEmpty(lastMaPN))
            {
                lastMaPN = GetLastMaPN();
            }
            if (string.IsNullOrEmpty(lastMaPN))
            {
                return "PNVC000001";
            }
            string numericPart = lastMaPN.Substring(4);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string nextMaPN = "PNVC" + number.ToString("D6");
                lastMaPN = nextMaPN;
                return nextMaPN;
            }
            else
            {
                throw new Exception("Invalid MaPN format in database.");
            }
        }

        public void Insert(PhieuNhapDTO pn)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();

                    row["MaPN"] = pn.MaPN;
                    row["NgayLap"] = pn.NgayLap;
                    row["MaNV"] = (object)pn.MaNV ?? DBNull.Value;
                    row["MaNCC"] = (object)pn.MaNCC ?? DBNull.Value;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm phiếu nhập: " + ex.Message);
            }
        }

        public void Edit(PhieuNhapDTO pn)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(pn.MaPN);
                    if (row != null)
                    {
                        row["NgayLap"] = pn.NgayLap;
                        row["MaNV"] = (object)pn.MaNV ?? DBNull.Value;
                        row["MaNCC"] = (object)pn.MaNCC ?? DBNull.Value;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy phiếu nhập với mã: {pn.MaPN}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa phiếu nhập: " + ex.Message);
            }
        }
    }
}
