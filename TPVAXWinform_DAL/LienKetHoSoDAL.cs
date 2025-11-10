using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DTO;

namespace TPVAXWinform_DAL
{
    public class LienKetHoSoDAL
    {
        /// <summary>
        /// Thêm một liên kết hồ sơ mới (Khách hàng <-> Hồ sơ)
        /// </summary>
        public void Insert(LienKetHoSoDTO newLienKet)
        {
            try
            {
                // Giả sử tên bảng là "LienKetHoSo"
                using (var buffer = new DBConnect.EditableBuffer("SELECT * FROM dbo.LienKetHoSo"))
                {
                    var row = buffer.Table.NewRow();

                    // Gán giá trị từ DTO
                    row["MaLK"] = newLienKet.MaLK;
                    row["VaiTro"] = newLienKet.VaiTro;
                    row["NgayLienKet"] = newLienKet.NgayLienKet;
                    row["MaKH"] = newLienKet.MaKH;
                    row["MaHSTC"] = newLienKet.MaHSTC;

                    buffer.Table.Rows.Add(row);
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lôi liên kết hồ sơ: " + ex.Message);
            }
        }
        /// <summary>
        /// Cập nhật thông tin một liên kết hồ sơ (chủ yếu là VAI TRÒ).
        /// </summary>
        public void Edit(LienKetHoSoDTO MaLK)
        {
            try
            {
                // Giả sử tên bảng là "LienKetHoSo"
                using (var buffer = new DBConnect.EditableBuffer("SELECT * FROM dbo.LienKetHoSo"))
                {
                    // Tìm bằng MaLK (Primary Key)
                    DataRow rowUpdate = buffer.Table.Rows.Find(MaLK.MaLK);

                    if (rowUpdate == null)
                    {
                        throw new Exception($"Không tìm thấy liên kết cần sửa với mã: {MaLK.MaLK}");
                    }

                    // Gán giá trị mới
                    rowUpdate["VaiTro"] = MaLK.VaiTro;
                    buffer.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error editing LienKetHoSo: " + ex.Message);
            }
        }
    }
}
