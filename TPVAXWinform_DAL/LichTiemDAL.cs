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
        public DataTable GetData()
        {
            const string sql = "SELECT * FROM dbo.LichTiem";
            return DBConnect.ExecuteQuery(sql);
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
                return "LT000001";
            }
            string numericPart = lastMaLT.Substring(2);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string nextMaLT = "LT" + number.ToString("D6");
                lastMaLT = nextMaLT;
                return nextMaLT;
            }
            else
            {
                throw new Exception("Invalid MaLT format in database.");
            }
        }
        public string CreateMaLT(string CCCD)
        {

            string cccdSuffix = CCCD.Length == 12 ? CCCD.Substring(6, 6) : string.Empty;
            return string.Equals(cccdSuffix, string.Empty) ? string.Empty : "LT" + cccdSuffix;
        }

        public void Insert(LichTiemDTO lichTiem)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer("SELECT * FROM dbo.LichTiem"))
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
                using (var buffer = DBConnect.CreateBuffer("SELECT * FROM dbo.LichTiem"))
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
    }
}
