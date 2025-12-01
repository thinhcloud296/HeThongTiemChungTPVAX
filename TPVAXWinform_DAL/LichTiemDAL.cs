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
        private string selectSql = "SELECT * FROM dbo.LichTiem";
        private VaccineDAL vaccineDAL = new VaccineDAL();
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
        public void TaoLichHenKeTiep(string maHSTC, string maVCDaTiem)
        {
            // (Bạn cần tạo hàm GetDataByMaVC trong VaccineBLL)
            VaccineDTO vaccine = vaccineDAL.GetVaccineByMaVC(maVCDaTiem);

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
            lichHenMoi.TrangThai = "Chưa tiêm"; // (Vì bạn đã đổi sang NVARCHAR)
            lichHenMoi.GhiChu = $"Hẹn nhắc lại mũi {soMuiKeTiep} cho {vaccine.TenVC}";

            // Thêm vào CSDL
            this.Insert(lichHenMoi);
        }
        // Thêm tham số "DateTime ngayHen"
        // Mỗi lịch hẹn sẽ cách nhau 2 tháng từ ngày hẹn đầu tiên
        public int TaoLichHenDauTienChoGoi(string maGoi, string maHSTC, DateTime ngayHen)
        {
            // 1. Lấy danh sách vaccine (chỉ Mũi 1) của gói
            string procName = "dbo.usp_GetChiTietGoi_FirstDoses";
            var param = DBConnect.Param("@MaGoi", maGoi, SqlDbType.Char, 10);
            DataTable dtFirstDoses = DBConnect.ExecuteQuery(procName, CommandType.StoredProcedure, param);

            int count = 0;
            if (dtFirstDoses.Rows.Count == 0)
            {
                return 0; // Gói này không có Mũi 1
            }

            // 2. Lặp qua từng vaccine Mũi 1 và tạo lịch hẹn
            // Mỗi lịch hẹn cách nhau 2 tháng
            int soThangCachNhau = 2;
            int index = 0;

            foreach (DataRow row in dtFirstDoses.Rows)
            {
                try
                {
                    string maVC = row["MaVC"].ToString();

                    LichTiemDTO lichHenMoi = new LichTiemDTO();
                    lichHenMoi.MaLT = this.CreateNewMaLT(); // Dùng hàm tạo mã của bạn
                    lichHenMoi.MaHSTC = maHSTC;
                    lichHenMoi.MaVC = maVC;

                    // Ngày hẹn = ngày đầu tiên + (index * 2 tháng)
                    // Vaccine đầu tiên: ngayHen
                    // Vaccine thứ 2: ngayHen + 2 tháng
                    // Vaccine thứ 3: ngayHen + 4 tháng
                    // ...
                    lichHenMoi.NgayHenTiem = ngayHen.AddMonths(index * soThangCachNhau);

                    lichHenMoi.SoMui = 1; // Vì đây là mũi đầu tiên
                    lichHenMoi.TrangThai = "Chưa tiêm"; // (Kiểu NVARCHAR)
                    lichHenMoi.GhiChu = $"Hẹn Mũi 1 (từ Gói {maGoi})";

                    this.Insert(lichHenMoi); // Gọi hàm Insert BLL
                    count++;
                    index++; // Tăng index để vaccine tiếp theo cách thêm 2 tháng
                }
                catch (Exception ex)
                {
                    // Bỏ qua nếu lỗi 1 mũi (ví dụ: trùng lặp) và tiếp tục mũi khác
                    Console.WriteLine("Lỗi tạo lịch hẹn tự động: " + ex.Message);
                    index++; // Vẫn tăng index để giữ khoảng cách đúng
                }
            }
            return count;
        }
    }
}
