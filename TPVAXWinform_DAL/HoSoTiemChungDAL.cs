using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
