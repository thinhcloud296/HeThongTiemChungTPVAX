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
    public class ChiTietGoiVaccineDAL
    {
        private string selectSql = "SELECT * FROM dbo.ChiTietGoiVaccine";
        public DataTable GetData()
        {
            return DBConnect.ExecuteQuery(selectSql);
        }

        public DataTable GetVaccinesByGoiVaccine(string maGoi)
        {
            string sql = @"
            SELECT 
          v.MaVC AS [Mã Vaccine],
                v.TenVC AS [Tên Vaccine],
       STRING_AGG(lb.TenBenh, ', ') AS [Loại bệnh],
            lv.TenLoai AS [Loại Vaccine],
           COALESCE(ctpn.NuocSanXuat, N'Chưa xác định') AS [Nước sản xuất],
   v.GiaBan AS [Giá bán],
       ct.SoMui AS [Số mũi],
 ct.GhiChu AS [Ghi chú]
              FROM ChiTietGoiVaccine ct
      INNER JOIN Vaccine v ON ct.MaVC = v.MaVC
     LEFT JOIN LoaiVaccine lv ON v.MaLoai = lv.MaLoai
                LEFT JOIN VaccinePhongBenh vpb ON v.MaVC = vpb.MaVC
      LEFT JOIN LoaiBenh lb ON vpb.MaLoaiBenh = lb.MaLoaiBenh
       LEFT JOIN (
        SELECT MaVC, NuocSanXuat,
 ROW_NUMBER() OVER (PARTITION BY MaVC ORDER BY HanSuDung DESC) AS rn
FROM ChiTietPhieuNhap
      ) ctpn ON v.MaVC = ctpn.MaVC AND ctpn.rn = 1
      WHERE ct.MaGoi = @MaGoi
       GROUP BY v.MaVC, v.TenVC, lv.TenLoai, ctpn.NuocSanXuat, v.GiaBan, ct.SoMui, ct.GhiChu";

            return DBConnect.ExecuteQuery(sql, CommandType.Text, 
 new SqlParameter("@MaGoi", maGoi));
        }

        public void Insert(ChiTietGoiVaccineDTO ct)
        {
       try
            {
        using (var buffer = DBConnect.CreateBuffer(selectSql))
   {
DataRow row = buffer.Table.NewRow();

        row["MaCTGoi"] = ct.MaCTGoi;
          row["SoMui"] = (object)ct.SoMui ?? DBNull.Value;
 row["GhiChu"] = ct.GhiChu;
               row["MaGoi"] = ct.MaGoi;
              row["MaVC"] = ct.MaVC;

                buffer.Table.Rows.Add(row);
        buffer.Save();
                }
            }
     catch (Exception ex)
          {
      throw new Exception("Lỗi khi thêm chi tiết gói vaccine: " + ex.Message);
      }
        }

        public void Edit(ChiTietGoiVaccineDTO ct)
        {
  try
  {
     using (var buffer = DBConnect.CreateBuffer(selectSql))
    {
 DataRow row = buffer.Table.Rows.Find(ct.MaCTGoi);
           if (row != null)
          {
             row["SoMui"] = (object)ct.SoMui ?? DBNull.Value;
           row["GhiChu"] = ct.GhiChu;
    row["MaGoi"] = ct.MaGoi;
   row["MaVC"] = ct.MaVC;

      buffer.Save();
  }
       else
        {
             throw new Exception($"Không tìm thấy chi tiết gói vaccine với mã: {ct.MaCTGoi}");
          }
      }
            }
       catch (Exception ex)
    {
          throw new Exception("Lỗi khi sửa chi tiết gói vaccine: " + ex.Message);
            }
        }
    }
}
