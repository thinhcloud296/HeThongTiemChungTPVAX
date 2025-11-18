using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class NhanVienDAL
    {
        string selectAll = "SELECT * FROM NhanVien";
        string lastMaNV = "";

        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectAll);
        }

        public string GetLastMaNV()
        {
            const string sql = "SELECT TOP 1 MaNV FROM dbo.NhanVien ORDER BY MaNV DESC";
            DataTable dt = DBConnect.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                lastMaNV = dt.Rows[0]["MaNV"].ToString();
            }
            return lastMaNV;
        }
        public DataTable GetNhanVienByMaNV(string maNV)
        {
            return DBConnect.ExecuteQuery("SELECT * FROM NhanVien WHERE MaNV = @MaNV"
                ,CommandType.Text,
                DBConnect.Param("@MaNV", maNV, SqlDbType.Char,10));
        }
        public string CreateNewMaNV()
        {
            if (string.IsNullOrEmpty(lastMaNV))
            {
                lastMaNV = GetLastMaNV();
            }
            if (string.IsNullOrEmpty(lastMaNV))
            {
                return "NVIE000001";
            }
            string numericPart = lastMaNV.Substring(2);
            if (int.TryParse(numericPart, out int number))
            {
                number++;
                string MaNV = "NVIE" + number.ToString("D6");
                lastMaNV = MaNV;
                return MaNV;
            }
            else
            {
                throw new Exception("Invalid MaNV format in database.");
            }
        }

        public void Insert(NhanVienDTO nv)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectAll))
                {
                    DataRow row = buffer.Table.NewRow();

                    row["MaNV"] = nv.MaNV;
                    row["HoTen"] = nv.HoTen;
                    row["GioiTinh"] = nv.GioiTinh;
                    row["NgaySinh"] = (object)nv.NgaySinh ?? DBNull.Value;
                    row["CCCD"] = nv.CCCD;
                    row["NgayVaoLam"] = nv.NgayVaoLam;
                    row["ChucVu"] = (object)nv.ChucVu ?? DBNull.Value;
                    row["TrangThai"] = nv.TrangThai;
                    row["SoDT"] = nv.SoDT;
                    row["DiaChi"] = nv.DiaChi;
                    row["Email"] = nv.Email;
                    row["MaTK"] = (object)nv.MaTK ?? DBNull.Value;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm nhân viên: " + ex.Message);
            }
        }

        public void Edit(NhanVienDTO nv)
        {
            try
            {
                using (var buffer = DBConnect.CreateBuffer(selectAll))
                {
                    DataRow row = buffer.Table.Rows.Find(nv.MaNV);
                    if (row != null)
                    {
                        row["HoTen"] = nv.HoTen;
                        row["GioiTinh"] = nv.GioiTinh;
                        row["NgaySinh"] = (object)nv.NgaySinh ?? DBNull.Value;
                        row["CCCD"] = nv.CCCD;
                        row["NgayVaoLam"] = nv.NgayVaoLam;
                        row["ChucVu"] = (object)nv.ChucVu ?? DBNull.Value;
                        row["TrangThai"] = nv.TrangThai;
                        row["SoDT"] = nv.SoDT;
                        row["DiaChi"] = nv.DiaChi;
                        row["Email"] = nv.Email;
                        row["MaTK"] = (object)nv.MaTK ?? DBNull.Value;

                        buffer.Save();
                    }
                    else
                    {
                        throw new Exception($"Không tìm thấy nhân viên với mã: {nv.MaNV}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa nhân viên: " + ex.Message);
            }
        }
        public string GetChucVuString(int? chucVu)
        {
            switch (chucVu)
            {
                case 1:
                    return "Quản Lý";
                case 2:
                    return "Nhân Viên Tiếp Nhận";
                case 3:
                    return "Nhân Viên Kho";
                case 4:
                    return "Nhân Viên Y Tế";
                case 5:
                    return "Nhân Viên Thu Ngân";
                default:
                    return "Không Xác Định";
            }
        }
    }
}
