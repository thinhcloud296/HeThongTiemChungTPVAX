using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class HoSoTiemChungDAL
    {
        public DataTable GetData()
        {
            const string sql = "SELECT * FROM dbo.HoSoTiemChung";
            return DBConnect.ExecuteQuery(sql);
        }
        public DataTable GetHSTC_KHHG()
        {
            return DBConnect.ExecuteQuery(
                    "dbo.usp_HoSoTiemChung_GetAllWithKhachHang",
                    CommandType.StoredProcedure
                );
        }
        public DataTable GetHSTC_QuanHe_KH(string MaKH)
        {             return DBConnect.ExecuteQuery(
                    "dbo.usp_HoSoTiemChung_GetQuanHeVoiKH",
                    CommandType.StoredProcedure,
                    DBConnect.Param("@MaKH", MaKH, SqlDbType.Char, 10)
                );
        }
        public string CreateMaHSTC(string CCCD)
        {
            string cccdSuffix = CCCD.Length == 12 ? CCCD.Substring(6, 6) : string.Empty;
            return string.Equals(cccdSuffix, string.Empty) ? string.Empty : "HSTC" + cccdSuffix;
        }
        public void Insert(HoSoTiemChungDTO newHSTC)
        {
            try
            {
                // 1. Mở buffer kết nối đến bảng HoSoTiemChung
                using (var buffer = new DBConnect.EditableBuffer("SELECT * FROM dbo.HoSoTiemChung"))
                {
                    // 2. Tạo một dòng mới
                    var row = buffer.Table.NewRow();

                    // 3. Gán giá trị từ DTO vào các cột
                    row["MaHSTC"] = newHSTC.MaHSTC;
                    row["HoTen"] = newHSTC.HoTen;
                    row["GioiTinh"] = newHSTC.GioiTinh;
                    row["NgaySinh"] = newHSTC.NgaySinh; // DTO khai báo NOT NULL
                    row["CCCD"] = newHSTC.CCCD;
                    row["GhiChu"] = newHSTC.GhiChu;
                    row["TrangThai"] = newHSTC.TrangThai;

                    // 4. Thêm dòng vào buffer và lưu
                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                // Ném lỗi ra ngoài để lớp GUI xử lý
                throw new Exception("Error inserting HoSoTiemChung: " + ex.Message);
            }
        }
        /// <summary>
        /// Cập nhật thông tin một hồ sơ tiêm chủng đã có.
        /// </summary>
        public void Edit(HoSoTiemChungDTO hstc)
        {
            try
            {
                // 1. Mở buffer kết nối đến bảng HoSoTiemChung
                using (var buffer = new DBConnect.EditableBuffer("SELECT * FROM dbo.HoSoTiemChung"))
                {
                    // 2. Tìm dòng cần sửa bằng Primary Key (MaHSTC)
                    DataRow rowUpdate = buffer.Table.Rows.Find(hstc.MaHSTC);

                    // 3. Kiểm tra xem có tìm thấy không
                    if (rowUpdate == null)
                    {
                        throw new Exception($"Không tìm thấy hồ sơ cần sửa với mã: {hstc.MaHSTC}");
                    }

                    // 4. Gán giá trị mới từ DTO (Không gán lại Primary Key)
                    rowUpdate["HoTen"] = hstc.HoTen;
                    rowUpdate["GioiTinh"] = hstc.GioiTinh;
                    rowUpdate["NgaySinh"] = hstc.NgaySinh;
                    rowUpdate["CCCD"] = hstc.CCCD;
                    rowUpdate["GhiChu"] = hstc.GhiChu;
                    rowUpdate["TrangThai"] = hstc.TrangThai;

                    // 5. Lưu các thay đổi
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                // Ném lỗi ra ngoài để lớp GUI xử lý
                throw new Exception("Error editing HoSoTiemChung: " + ex.Message);
            }
        }
    }
}
