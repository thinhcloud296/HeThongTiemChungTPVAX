using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class ChiTietPhieuNhapDAL
    {
        public DataTable GetDataByMaPN(string maPN)
        {
            return DBConnect.ExecuteQuery("dbo.usp_ChiTietPhieuNhap_GetByMaPN", CommandType.StoredProcedure, DBConnect.Param("@MaPN", maPN, SqlDbType.Char, 10));
        }

    }
}
