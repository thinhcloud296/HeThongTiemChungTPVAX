using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class LoaiVaccineDAL
    {
        public DataTable GetData()
        {
            const string sql = "SELECT * FROM dbo.LoaiVaccine";
            return DBConnect.ExecuteQuery(sql);
        }
    }
}
