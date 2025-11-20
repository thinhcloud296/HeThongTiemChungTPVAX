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
    public class ChiTietPhieuNhapDAL
    {
        private string selectSql = "SELECT * FROM dbo.ChiTietPhieuNhap";
        private string lastMaCTPN = "";

        public DataTable GetDataByMaPN(string maPN)
        {
            return DBConnect.ExecuteQuery("dbo.usp_ChiTietPhieuNhap_GetByMaPN", CommandType.StoredProcedure,
              DBConnect.Param("@MaPN", maPN, SqlDbType.Char, 10));
        }

        public string GetLastMaCTPN()
        {
            const string sql = "SELECT TOP 1 MaCTPN FROM dbo.ChiTietPhieuNhap ORDER BY MaCTPN DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaCTPN = dt.Rows[0]["MaCTPN"].ToString();
            }
            return lastMaCTPN;
        }

        public string CreateNewMaCTPN()
        {
            if (string.IsNullOrEmpty(lastMaCTPN))
            {
                lastMaCTPN = GetLastMaCTPN();
            }
            if (string.IsNullOrEmpty(lastMaCTPN))
            {
                return "CTPN000001";
            }
            string numericPart = lastMaCTPN.Substring(4);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string nextMaCTPN = "CTPN" + number.ToString("D6");
                lastMaCTPN = nextMaCTPN;
                return nextMaCTPN;
            }
            else
            {
                throw new Exception("Invalid MaCTPN format in database.");
            }
        }

        public void Insert(ChiTietPhieuNhapDTO ctpn)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();

                    row["MaCTPN"] = ctpn.MaCTPN;
                    row["NuocSanXuat"] = (object)ctpn.NuocSanXuat ?? DBNull.Value;
                    row["SoLuong"] = ctpn.SoLuong;
                    
                    // === SỬA: Sử dụng giá trị từ DTO thay vì tự động gán ===
                    row["SoLuongTonKho"] = ctpn.SoLuongTonKho; // Lấy từ DTO (đã set = 0 trong GUI)
                    // === KẾT THÚC SỬA ===
                    
                    row["GiaNhap"] = ctpn.GiaNhap;
                    row["HanSuDung"] = (object)ctpn.HanSuDung ?? DBNull.Value;
                    row["MaPN"] = ctpn.MaPN;
                    row["MaVC"] = ctpn.MaVC;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết phiếu nhập: " + ex.Message);
            }
        }

        public void Edit(ChiTietPhieuNhapDTO ctpn)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(ctpn.MaCTPN);
                    if (row != null)
                    {
                        row["NuocSanXuat"] = (object)ctpn.NuocSanXuat ?? DBNull.Value;
                        row["SoLuong"] = ctpn.SoLuong;
                        row["GiaNhap"] = ctpn.GiaNhap;
                        row["HanSuDung"] = (object)ctpn.HanSuDung ?? DBNull.Value;
                        row["MaVC"] = ctpn.MaVC;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy chi tiết phiếu nhập với mã: {ctpn.MaCTPN}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa chi tiết phiếu nhập: " + ex.Message);
            }
        }

        public void Delete(string maCTPN)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(maCTPN);
                    if (row != null)
                    {
                        row.Delete();
                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy chi tiết phiếu nhập với mã: {maCTPN}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết phiếu nhập: " + ex.Message);
            }
        }
        public void XacNhanNhapKho(string maPN)
        {
            try
            {
                string procName = "dbo.usp_XacNhanNhapKho";
                var paramMaPN = DBConnect.Param("@MaPN", maPN, SqlDbType.Char, 10);

                // Dùng ExecuteNonQuery (bạn đã thêm vào DBConnect)
                DBConnect.ExecuteNonQuery(procName, CommandType.StoredProcedure, paramMaPN);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xác nhận nhập kho DAL: " + ex.Message);
            }
        }
    }
}
