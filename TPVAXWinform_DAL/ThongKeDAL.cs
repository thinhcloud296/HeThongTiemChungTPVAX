using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_DAL
{
    public class ThongKeDAL
    {
        // timeRange: 0 = Hôm nay, 1 = 7 ngày, 2 = Tháng này
        public DataRow GetDashboardKPI(int timeRange = 2)
        {
            string sql = "dbo.usp_ThongKe_GetDashboardKPI";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TimeRange", timeRange)
            };
            DataTable dt = DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure, parameters);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public DataTable GetDoanhThu7Ngay()
        {
            string sql = "dbo.usp_ThongKe_GetDoanhThu7Ngay";
            return DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        public DataTable GetDoanhThuHomNay()
        {
            string sql = "dbo.usp_ThongKe_GetDoanhThuHomNay";
            return DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        public DataTable GetDoanhThuThangNay()
        {
            string sql = "dbo.usp_ThongKe_GetDoanhThuThangNay";
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

        public DataTable GetDoanhThuChiTiet(int timeRange = 2)
        {
            string sql = "dbo.usp_ThongKe_GetDoanhThuChiTiet";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TimeRange", timeRange)
            };
            return DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure, parameters);
        }

        public DataTable GetXuatNhapTon(int timeRange = 2)
        {
            string sql = "dbo.usp_ThongKe_GetXuatNhapTon";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TimeRange", timeRange)
            };
            return DBConnect.ExecuteQuery(sql, CommandType.StoredProcedure, parameters);
        }
    }
}
