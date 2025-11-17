using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class PhieuNhapDAL
    {
        public DataTable GetDataDetail()
        {
            return DBConnect.ExecuteQuery("dbo.usp_PhieuNhap_GetAllWithDetails",CommandType.StoredProcedure);
        }
        public DataTable GetDetailByMaPN(string maPN)
        {
            return DBConnect.ExecuteQuery("dbo.usp_PhieuNhap_GetDetailByMaPN", CommandType.StoredProcedure,
                DBConnect.Param("@MaPN",maPN,SqlDbType.Char,10));
        }

    }
}
