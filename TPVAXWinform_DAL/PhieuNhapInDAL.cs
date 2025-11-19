using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class PhieuNhapInDAL
    {
        public DataTable GetPhieuNhapInData(string maPN)
        {
            return DBConnect.ExecuteQuery("dbo.usp_Report_GetPhieuNhapIn", CommandType.StoredProcedure,
                DBConnect.Param("@MaPN", maPN, SqlDbType.Char, 10));
        }
    }
}
