using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class VaccineDAL
    {
        public DataTable GetData()
        {
            const string sql = "SELECT * FROM dbo.Vaccine";
            return DBConnect.ExecuteQuery(sql);
        }
        public DataTable GetDataVaccineDetail()
        {
            return DBConnect.ExecuteQuery(
                    "dbo.usp_GetDanhSachVaccineChiTiet",
                    CommandType.StoredProcedure
                );
        }

    }
}
