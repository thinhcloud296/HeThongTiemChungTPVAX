using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class LichTiemDAL
    {
        private string lastMaLT = "";
        private string selectSql = "SELECT * FROM dbo.Vaccine";
        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectSql);
        }
        public DataTable GetLichTiemWithHSTC()
        {
            return DBConnect.ExecuteQuery("dbo.usp_GetDanhSachLichTiemChiTiet", CommandType.StoredProcedure);
        }
        public string GetLastMaLT()
        {
            const string sql = "SELECT TOP 1 MaLT FROM dbo.LichTiem ORDER BY MaLT DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaLT = dt.Rows[0]["MaLT"].ToString();
            }
            return lastMaLT;
        }
        public string CreateNewMaLT()
        {
            if (string.IsNullOrEmpty(lastMaLT))
            {
                lastMaLT = GetLastMaLT();
            }
            if (string.IsNullOrEmpty(lastMaLT))
            {
                return "LTIE000001";
            }
            string numericPart = lastMaLT.Substring(4);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string nextMaLT = "LTIE" + number.ToString("D6");
                lastMaLT = nextMaLT;
                return nextMaLT;
            }
            else
            {
                throw new Exception("Invalid MaLT format in database.");
            }
        }

        public void Insert(LichTiemDTO lichTiem)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();

                    row["MaLT"] = lichTiem.MaLT;
                    row["NgayHenTiem"] = lichTiem.NgayHenTiem;

                    // Xử lý các giá trị có thể NULL
                    row["NgayTiemThucTe"] = (object)lichTiem.NgayTiemThucTe ?? DBNull.Value;
                    row["SoMui"] = (object)lichTiem.SoMui ?? DBNull.Value;
                    row["TrangThai"] = lichTiem.TrangThai;
                    row["GhiChu"] = lichTiem.GhiChu;
                    row["MaHSTC"] = lichTiem.MaHSTC;
                    row["MaVC"] = (object)lichTiem.MaVC ?? DBNull.Value;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm lịch tiêm: " + ex.Message);
            }
        }

        public void Edit(LichTiemDTO lichTiem)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(lichTiem.MaLT);
                    if (row != null)
                    {
                        row["NgayHenTiem"] = lichTiem.NgayHenTiem;
                        row["NgayTiemThucTe"] = (object)lichTiem.NgayTiemThucTe ?? DBNull.Value;
                        row["TrangThai"] = lichTiem.TrangThai;
                        row["GhiChu"] = lichTiem.GhiChu;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy lịch tiêm với mã: {lichTiem.MaLT}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa lịch tiêm: " + ex.Message);
            }
        }
        public int SoMuiDaTiem(string maHSTC,string maVC)
        {
            string sql = $"SELECT COUNT(*) FROM dbo.LichTiem WHERE MaHSTC = '{maHSTC}' AND MaVC = '{maVC}'  AND NgayTiemThucTe IS NOT NULL";
            object result = DBConnect.ExecuteScalar(sql);
            return Convert.ToInt32(result);
        }
        public int SoMuiDangChoTiem(string maHSTC,string maVC)
        {
            string sql = $"SELECT COUNT(*) FROM dbo.LichTiem WHERE MaHSTC = '{maHSTC}'AND MaVC = '{maVC}'   AND NgayTiemThucTe IS NULL";
            object result = DBConnect.ExecuteScalar(sql);
            return Convert.ToInt32(result);
        }
        public VaccineDTO GetDataByMaVC(string maVC)
        {
            try
            {
                // 1. Tải toàn bộ bảng Vaccine vào buffer (Rows.Find yêu cầu PK)
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    // 2. Tìm vaccine bằng Primary Key
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

                        // Lấy 2 cột phác đồ (xử lý DBNull.Value nếu có thể)
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
        public void TaoLichHenKeTiep(string maHSTC, string maVCDaTiem)
        {
            // (Bạn cần tạo hàm GetDataByMaVC trong VaccineBLL)
            VaccineDTO vaccine = GetDataByMaVC(maVCDaTiem);

            if (vaccine == null)
                throw new Exception("Không tìm thấy thông tin vaccine.");

            // 2. Lấy số phác đồ
            int soMuiToiDa = vaccine.SoMuiToiDa;
            int soThangCho = vaccine.SoThangCho;

            // 3. Bỏ qua nếu là vaccine nhắc lại (99) hoặc 1 mũi (1)
            if (soMuiToiDa == 99 || soMuiToiDa <= 1)
            {
                return; // Không tạo lịch hẹn kế tiếp
            }

            // 4. Đếm số mũi đã tiêm (từ CSDL, bao gồm mũi vừa tiêm xong)
            int soMuiDaTiem = this.SoMuiDaTiem(maHSTC, maVCDaTiem);

            // 5. Kiểm tra xem đã đủ phác đồ chưa
            if (soMuiDaTiem >= soMuiToiDa)
            {
                return; // Đã tiêm đủ, không tạo lịch hẹn nữa
            }

            // 6. Nếu chưa đủ -> TẠO LỊCH HẸN MỚI

            // Tính ngày hẹn mới = Ngày hôm nay + Số tháng chờ
            DateTime ngayHenMoi = DateTime.Now.AddMonths(soThangCho);
            int soMuiKeTiep = soMuiDaTiem + 1;

            LichTiemDTO lichHenMoi = new LichTiemDTO();
            lichHenMoi.MaLT = this.CreateNewMaLT(); // Dùng hàm tạo mã mới của bạn
            lichHenMoi.MaHSTC = maHSTC;
            lichHenMoi.MaVC = maVCDaTiem;
            lichHenMoi.NgayHenTiem = ngayHenMoi;
            lichHenMoi.SoMui = soMuiKeTiep;
            lichHenMoi.TrangThai = false; // (Vì bạn đã đổi sang NVARCHAR)
            lichHenMoi.GhiChu = $"Hẹn nhắc lại mũi {soMuiKeTiep} cho {vaccine.TenVC}";

            // Thêm vào CSDL
            this.Insert(lichHenMoi);
        }
    }
}
