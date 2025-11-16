using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class NhaCungCapDAL
    {
        private string selectSql = "SELECT * FROM dbo.NhaCungCap";
        private string lastMaNCC = "";

        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectSql);
        }

        public string GetLastMaNCC()
        {
            const string sql = "SELECT TOP 1 MaNCC FROM dbo.NhaCungCap ORDER BY MaNCC DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaNCC = dt.Rows[0]["MaNCC"].ToString();
            }
            return lastMaNCC;
        }

        public string CreateNewMaNCC()
        {
            if (string.IsNullOrEmpty(lastMaNCC))
            {
                lastMaNCC = GetLastMaNCC();
            }
            if (string.IsNullOrEmpty(lastMaNCC))
            {
                return "NCAP0001";
            }
            string numericPart = lastMaNCC.Substring(4);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string MaNCC = "NCAP" + number.ToString("D4");
                lastMaNCC = MaNCC;
                return MaNCC;
            }
            else
            {
                throw new Exception("Invalid MaNCC format in database.");
            }
        }

        public void Insert(NhaCungCapDTO ncc)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.NewRow();

                    row["MaNCC"] = ncc.MaNCC;
                    row["TenNCC"] = ncc.TenNCC;
                    row["DiaChi"] = (object)ncc.DiaChi ?? DBNull.Value;
                    row["Email"] = (object)ncc.Email ?? DBNull.Value;
                    row["SoDT"] = (object)ncc.SoDT ?? DBNull.Value;
                    row["TenNganHang"] = (object)ncc.TenNganHang ?? DBNull.Value;
                    row["SoTK"] = (object)ncc.SoTK ?? DBNull.Value;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm nhà cung cấp: " + ex.Message);
            }
        }

        public void Edit(NhaCungCapDTO ncc)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectSql))
                {
                    DataRow row = buffer.Table.Rows.Find(ncc.MaNCC);
                    if (row != null)
                    {
                        row["TenNCC"] = ncc.TenNCC;
                        row["DiaChi"] = (object)ncc.DiaChi ?? DBNull.Value;
                        row["Email"] = (object)ncc.Email ?? DBNull.Value;
                        row["SoDT"] = (object)ncc.SoDT ?? DBNull.Value;
                        row["TenNganHang"] = (object)ncc.TenNganHang ?? DBNull.Value;
                        row["SoTK"] = (object)ncc.SoTK ?? DBNull.Value;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy nhà cung cấp với mã: {ncc.MaNCC}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa nhà cung cấp: " + ex.Message);
            }
        }
    }
}
