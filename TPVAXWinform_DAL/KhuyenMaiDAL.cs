using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class KhuyenMaiDAL
    {
        private string lastMaKM = "";
        public string GetLastMaKM()
        {
            const string sql = "SELECT TOP 1 MaKM FROM dbo.KhuyenMai ORDER BY MaKM DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaKM = dt.Rows[0]["MaKM"].ToString();
            }
            return lastMaKM;
        }
        public string CreateNewMaKM()
        {
            if (string.IsNullOrEmpty(lastMaKM))
            {
                lastMaKM = GetLastMaKM();
            }
            if (string.IsNullOrEmpty(lastMaKM))
            {
                return "KMAI000001";
            }
            string numericPart = lastMaKM.Substring(4);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string MaKM = "KMAI" + number.ToString("D6");
                lastMaKM = MaKM;
                return MaKM;
            }
            else
            {
                throw new Exception("Invalid MaLoai format in database.");
            }
        }
        // Lấy danh sách tất cả khuyến mãi (để quản lý)
        public DataTable GetAll()
        {
            try
            {
                DataTable dt = DBConnect.ExecuteQuery("SELECT * FROM KhuyenMai ORDER BY NgayBatDau DESC");
                return dt;
            }
            catch
                (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách khuyến mãi: " + ex.Message);
            }


        }

        // Lấy chi tiết sản phẩm trong 1 khuyến mãi
        public DataTable GetChiTietByMaKM(string maKM)
        {
            string sql = "SELECT * FROM ChiTietKhuyenMai WHERE MaKM = @MaKM";
            return DBConnect.ExecuteQuery(sql, CommandType.Text,
                DBConnect.Param("@MaKM", maKM, SqlDbType.Char, 10));
        }

        // Tìm khuyến mãi tốt nhất cho 1 sản phẩm (Dùng khi tính tiền)
        public DataTable GetPromotionForProduct(string maSanPham, string loaiSanPham)
        {
            return DBConnect.ExecuteQuery("dbo.usp_KhuyenMai_GetForProduct", CommandType.StoredProcedure,
                DBConnect.Param("@MaSanPham", maSanPham, SqlDbType.Char, 10),
                DBConnect.Param("@LoaiSanPham", loaiSanPham, SqlDbType.NVarChar, 50)
            );
        }

        public void Insert(KhuyenMaiDTO km)
        {
            try
            {
                // Lấy cấu trúc bảng (WHERE 1=0 để không tải dữ liệu, chỉ lấy cột -> Tối ưu hiệu năng)
                using (var buffer = DBConnect.CreateBuffer("SELECT * FROM KhuyenMai WHERE 1=0"))
                {
                    DataRow row = buffer.Table.NewRow();

                    // Gán dữ liệu từ DTO sang DataRow
                    row["MaKM"] = km.MaKM;
                    row["TenKM"] = km.TenKM;
                    row["MoTa"] = km.MoTa ?? (object)DBNull.Value; // Xử lý null
                    row["LoaiKM"] = km.LoaiKM ?? (object)DBNull.Value;
                    row["KieuGiam"] = km.KieuGiam;
                    row["GiaTriGiam"] = km.GiaTriGiam;
                    row["NgayBatDau"] = km.NgayBatDau;
                    row["NgayKetThuc"] = km.NgayKetThuc;
                    row["TrangThai"] = km.TrangThai;

                    // Thêm dòng vào bảng và Lưu
                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm khuyến mãi (Buffer): " + ex.Message);
            }
        }

        // 2. Cập nhật bằng Buffer
        public void Update(KhuyenMaiDTO km)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer("SELECT * FROM KhuyenMai"))
                {
                    DataRow row = buffer.Table.Rows.Find(km.MaKM);

                    if (row != null)
                    {
                        // Cập nhật giá trị mới
                        row["TenKM"] = km.TenKM;
                        row["MoTa"] = km.MoTa ?? (object)DBNull.Value;
                        row["LoaiKM"] = km.LoaiKM ?? (object)DBNull.Value;
                        row["KieuGiam"] = km.KieuGiam;
                        row["GiaTriGiam"] = km.GiaTriGiam;
                        row["NgayBatDau"] = km.NgayBatDau;
                        row["NgayKetThuc"] = km.NgayKetThuc;
                        row["TrangThai"] = km.TrangThai;

                        // Lưu thay đổi
                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy khuyến mãi có mã {km.MaKM} để sửa.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật khuyến mãi (Buffer): " + ex.Message);
            }
        }
    }
}
