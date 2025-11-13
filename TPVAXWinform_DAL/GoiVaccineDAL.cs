using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class GoiVaccineDAL
    {
        private string selectSql = "SELECT * FROM dbo.GoiVaccine";
        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectSql);
        }
        public void Insert(GoiVaccineDTO goi)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();

                    row["MaGoi"] = goi.MaGoi;
                    row["TenGoi"] = goi.TenGoi;
                    row["MoTa"] = goi.MoTa;
                    row["DoiTuongApDung"] = goi.DoiTuongApDung;
                    row["GiaGoi"] = goi.GiaGoi;
                    row["TrangThai"] = goi.TrangThai;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm gói vaccine: " + ex.Message);
            }
        }

        public void Edit(GoiVaccineDTO goi)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(goi.MaGoi);
                    if (row != null)
                    {
                        row["TenGoi"] = goi.TenGoi;
                        row["MoTa"] = goi.MoTa;
                        row["DoiTuongApDung"] = goi.DoiTuongApDung;
                        row["GiaGoi"] = goi.GiaGoi;
                        row["TrangThai"] = goi.TrangThai;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy gói vaccine với mã: {goi.MaGoi}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa gói vaccine: " + ex.Message);
            }
        }
    }
}
