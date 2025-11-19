using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class ThongKeDAL
    {
        public DataRow GetDashboardKPI()
        {
            string sql = "dbo.usp_ThongKe_GetDashboardKPI";
            DataTable dt = DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public DataTable GetDoanhThu7Ngay()
        {
            string sql = "dbo.usp_ThongKe_GetDoanhThu7Ngay";
            return DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        public DataTable GetTyLeDoanhThu()
        {
            string sql = "dbo.usp_ThongKe_GetTyLeDoanhThu";
            return DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        public DataTable GetVaccineSapHetHan()
        {
            string sql = "dbo.usp_ThongKe_GetVaccineSapHetHan";
            return DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        public DataTable GetDoanhThuChiTiet()
        {
            string sql = "dbo.usp_ThongKe_GetDoanhThuChiTiet";
            return DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        public DataTable GetXuatNhapTon()
        {
            string sql = "dbo.usp_ThongKe_GetXuatNhapTon";
            return DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure);
        }
    }
}
