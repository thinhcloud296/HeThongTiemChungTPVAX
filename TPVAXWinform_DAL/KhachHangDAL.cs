using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class KhachHangDAL
    {
        public DataTable GetData()
        {
            const string sql = "SELECT * FROM dbo.KhachHang";
            return DBConnect.ExecuteQuery(sql);
        }   
    }
}
