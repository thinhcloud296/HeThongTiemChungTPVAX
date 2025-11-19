using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class HoaDonInDAL
    {
        public DataTable GetHoaDonInData(string maHD)
        {
            return DBConnect.ExecuteQuery("dbo.usp_Report_GetHoaDonIn", CommandType.StoredProcedure,
                DBConnect.Param("@MaHD", maHD, SqlDbType.Char, 10));
        }
    }
}
