using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class VaccineDAL
    {
        private string selectSql = "SELECT * FROM dbo.Vaccine";

        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectSql);
        }

        public DataTable GetDataVaccineDetail()
        {
            return DBConnect.ExecuteQuery(
                  "dbo.usp_GetDanhSachVaccineChiTiet",
                CommandType.StoredProcedure
                    );
        }

        public VaccineDTO GetVaccineByMaVC(string maVC)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(maVC);

                    if (row != null)
                    {
                        VaccineDTO vaccine = new VaccineDTO();
                        vaccine.MaVC = row["MaVC"].ToString();
                        vaccine.TenVC = row["TenVC"].ToString();
                        vaccine.GiaBan = Convert.ToDecimal(row["GiaBan"]);
                        vaccine.SoLuongTon = Convert.ToInt32(row["SoLuongTon"]);
                        vaccine.MaLoai = row["MaLoai"]?.ToString() ?? string.Empty;
                        vaccine.HinhAnh = row["HinhAnh"]?.ToString() ?? string.Empty;

                        return vaccine;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin vaccine: " + ex.Message);
            }

            return null;
        }

        public void UpdateSoLuongTon(string maVC, int soLuongThayDoi)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(maVC);
                    if (row != null)
                    {
                        int soLuongHienTai = Convert.ToInt32(row["SoLuongTon"]);
                        int soLuongMoi = soLuongHienTai + soLuongThayDoi;

                        if (soLuongMoi < 0)
                        {
                            throw new Exception("Số lượng tồn kho không được âm!");
                        }

                        row["SoLuongTon"] = soLuongMoi;
                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy vaccine với mã: {maVC}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật số lượng tồn: " + ex.Message);
            }
        }
    }
}
