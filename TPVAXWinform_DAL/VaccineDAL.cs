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
        private string lastMaVC = "";

        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectSql);
        }
        public DataTable GetDataForComboBox()
        {
            return DBConnect.ExecuteQuery("SELECT   MaVC, TenVC, (MaVC + ' - ' + TenVC) AS MaTenVC FROM dbo.Vaccine");
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
                // 1. Sửa: Dùng câu query đúng (truy vấn bảng Vaccine)
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    // 2. Sửa: Giờ buffer.Table là bảng Vaccine, PK là MaVC
                    DataRow row = buffer.Table.Rows.Find(maVC);

                    if (row != null)
                    {
                        // 3. Ánh xạ (map) dữ liệu từ DataRow sang DTO
                        VaccineDTO vaccine = new VaccineDTO();
                        vaccine.MaVC = row["MaVC"].ToString();
                        vaccine.TenVC = row["TenVC"].ToString();
                        vaccine.GiaBan = Convert.ToDecimal(row["GiaBan"]);
                        vaccine.SoLuongTon = Convert.ToInt32(row["SoLuongTon"]);
                        vaccine.MaLoai = row["MaLoai"].ToString();
                        vaccine.MoTa = row["MoTa"].ToString();
                        vaccine.HinhAnh = row["HinhAnh"].ToString();

                        vaccine.SoMuiToiDa = (row["SoMuiToiDa"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SoMuiToiDa"]);
                        vaccine.SoThangCho = (row["SoThangCho"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SoThangCho"]);

                        return vaccine;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin vaccine: " + ex.Message);
            }

            // 4. Trả về null nếu không tìm thấy
            return null;
        }
        public int GetSoLuongTonThucTe(string maVC)
        {
            try
            {
                string procName = "dbo.usp_Vaccine_GetSoLuongTonThucTe";
                var paramMaVC = DBConnect.Param("@MaVC", maVC, SqlDbType.Char, 10);

                // Dùng ExecuteScalar vì nó chỉ trả về 1 ô (1 con số)
                object result = DBConnect.ExecuteScalar(procName, CommandType.StoredProcedure, paramMaVC);

                return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tồn kho thực tế: " + ex.Message);
            }
        }
        public DataTable GetDataVaccine_SingleDose()
        {
            string procName = "dbo.usp_GetDanhSachVaccine_SingleDose";
            return DBConnect.ExecuteQuery(procName, CommandType.StoredProcedure);
        }
        public void UpdateSoLuongTon(string maVC, int soLuongThayDoi)
        {
            // 1. Hàm này chỉ xử lý việc GIẢM kho (số âm, ví dụ: -1)
            // (Việc TĂNG kho được xử lý bởi nghiệp vụ Nhập hàng)
            if (soLuongThayDoi >= 0)
            {
                // Nếu số lượng là dương hoặc 0, không làm gì cả
                return;
            }

            // 2. Chuyển số thay đổi (ví dụ: -1) thành số lượng giảm (ví dụ: 1)
            //    để truyền vào Stored Procedure
            int soLuongGiam = Math.Abs(soLuongThayDoi);

            try
            {
                // 3. Gọi Stored Procedure đã xử lý logic FEFO (trừ kho lô)
                string procName = "dbo.usp_Vaccine_GiamTonKho";

                // 4. Chuẩn bị tham số
                var paramMaVC = DBConnect.Param("@MaVC", maVC, SqlDbType.Char, 10);
                var paramSoLuong = DBConnect.Param("@SoLuongGiam", soLuongGiam, SqlDbType.Int);

                // 5. Thực thi Stored Procedure
                // (Dùng ExecuteNonQuery vì proc này không trả về bảng dữ liệu)
                DBConnect.ExecuteNonQuery(procName, CommandType.StoredProcedure, paramMaVC, paramSoLuong);
            }
            catch (Exception ex)
            {
                // 6. Ném (throw) lỗi từ Stored Procedure lên (ví dụ: "Không đủ số lượng tồn kho...")
                throw new Exception(ex.Message);
            }
        }

        public string GetLastMaVC()
        {
            const string sql = "SELECT TOP 1 MaVC FROM dbo.Vaccine ORDER BY MaVC DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaVC = dt.Rows[0]["MaVC"].ToString();
            }
            return lastMaVC;
        }

        public string CreateNewMaVC()
        {
            if (string.IsNullOrEmpty(lastMaVC))
            {
                lastMaVC = GetLastMaVC();
            }
            if (string.IsNullOrEmpty(lastMaVC))
            {
                return "VACC000001";
            }
            string numericPart = lastMaVC.Substring(4);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string newMaVC = "VACC" + number.ToString("D6");
                lastMaVC = newMaVC;
                return newMaVC;
            }
            else
            {
                throw new Exception("Invalid MaVC format in database.");
            }
        }

        public void Insert(VaccineDTO vaccine)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();
                    row["MaVC"] = vaccine.MaVC;
                    row["TenVC"] = vaccine.TenVC;
                    row["SoMuiToiDa"] = (object)vaccine.SoMuiToiDa ?? DBNull.Value;
                    row["SoThangCho"] = (object)vaccine.SoThangCho ?? DBNull.Value;
                    row["GiaBan"] = vaccine.GiaBan;
                    row["SoLuongTon"] = vaccine.SoLuongTon;
                    row["MaLoai"] = vaccine.MaLoai;
                    row["MoTa"] = vaccine.MoTa;
                    row["HinhAnh"] = vaccine.HinhAnh;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm vaccine: " + ex.Message);
            }
        }

        public void Edit(VaccineDTO vaccine)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(vaccine.MaVC);
                    if (row != null)
                    {
                        row["TenVC"] = vaccine.TenVC;
                        row["SoMuiToiDa"] = (object)vaccine.SoMuiToiDa ?? DBNull.Value;
                        row["SoThangCho"] = (object)vaccine.SoThangCho ?? DBNull.Value;
                        row["GiaBan"] = vaccine.GiaBan;
                        row["SoLuongTon"] = vaccine.SoLuongTon;
                        row["MaLoai"] = vaccine.MaLoai;
                        row["MoTa"] = vaccine.MoTa;
                        row["HinhAnh"] = vaccine.HinhAnh;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy vaccine với mã: {vaccine.MaVC}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa vaccine: " + ex.Message);
            }
        }
    }
}
