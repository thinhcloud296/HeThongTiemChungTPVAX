using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class ChiTietKhuyenMaiDAL
    {
        // Thêm Chi Tiết Khuyến Mãi (Item)
        private string lastMaCTKM = "";
        public string GetLastMaCTKM()
        {
            const string sql = "SELECT TOP 1 MaCTKM FROM dbo.KhuyenMai ORDER BY MaCTKM DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaCTKM = dt.Rows[0]["MaCTKM"].ToString();
            }
            return lastMaCTKM;
        }
        public string CreateNewMaCTKM()
        {
            if (string.IsNullOrEmpty(lastMaCTKM))
            {
                lastMaCTKM = GetLastMaCTKM();
            }
            if (string.IsNullOrEmpty(lastMaCTKM))
            {
                return "CTKM000001";
            }
            string numericPart = lastMaCTKM.Substring(4);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string MaCTKM = "CTKM" + number.ToString("D6");
                lastMaCTKM = MaCTKM;
                return MaCTKM;
            }
            else
            {
                throw new Exception("Invalid MaLoai format in database.");
            }
        }
        public void InsertDetail(ChiTietKhuyenMaiDTO ct)
        {
            try
            {
                // Lấy cấu trúc bảng (không lấy dữ liệu để nhanh hơn)
                using (var buffer = DBConnect.CreateBuffer("SELECT * FROM ChiTietKhuyenMai WHERE 1=0"))
                {
                    DataRow row = buffer.Table.NewRow();

                    // Lưu ý: MaCTKM là IDENTITY (tự tăng) nên KHÔNG gán giá trị cho nó
                    row["MaKM"] = ct.MaKM;
                    row["LoaiSanPham"] = ct.LoaiSanPham;
                    row["MaSanPham"] = ct.MaSanPham;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết khuyến mãi: " + ex.Message);
            }
        }

        // 2. Xóa chi tiết theo MaKM (Buffer)
        public void DeleteByMaKM(string maKM)
        {
            try
            {
                // Load tất cả các dòng chi tiết thuộc về MaKM này
                string query = $"SELECT * FROM ChiTietKhuyenMai WHERE MaKM = '{maKM}'";

                using (var buffer = DBConnect.CreateBuffer(query))
                {
                    // Duyệt qua từng dòng và xóa
                    foreach (DataRow row in buffer.Table.Rows)
                    {
                        row.Delete();
                    }

                    // Lưu thay đổi (SQL sẽ thực hiện DELETE cho các dòng đã đánh dấu)
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết khuyến mãi: " + ex.Message);
            }
        }

        // 3. Cập nhật chi tiết (Buffer)
        public void UpdateDetail(ChiTietKhuyenMaiDTO ct)
        {
            try
            {
                // Chỉ load đúng dòng cần sửa (tối ưu hiệu năng)
                string query = $"SELECT * FROM ChiTietKhuyenMai WHERE MaCTKM = {ct.MaCTKM}";

                using (var buffer = DBConnect.CreateBuffer(query))
                {
                    if (buffer.Table.Rows.Count > 0)
                    {
                        DataRow row = buffer.Table.Rows[0];

                        // Cập nhật dữ liệu
                        row["LoaiSanPham"] = ct.LoaiSanPham;
                        row["MaSanPham"] = ct.MaSanPham;
                        // MaKM thường không đổi ở bảng chi tiết, nhưng nếu cần thì gán:
                        // row["MaKM"] = ct.MaKM;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy chi tiết khuyến mãi ID: {ct.MaCTKM}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật chi tiết khuyến mãi: " + ex.Message);
            }
        }

        // Hàm tạo mã KM tự động (nếu cần)

    }
}
