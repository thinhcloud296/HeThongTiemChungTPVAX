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
            var param = DBConnect.Param("@MaGoi", maGoi, SqlDbType.Char, 8);
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
    }
}
