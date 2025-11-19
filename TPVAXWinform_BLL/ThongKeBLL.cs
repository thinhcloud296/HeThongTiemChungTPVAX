using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;

namespace TPVAXWinform_BLL
{
    public class ThongKeBLL
    {
        private ThongKeDAL dal = new ThongKeDAL();
        public (decimal DoanhThu, int LuotTiem, int KhachMoi, int SapHetHan) GetKPI()
        {
            DataRow row = dal.GetDashboardKPI();
            if (row != null)
            {
                decimal dt = row["DoanhThu"] != DBNull.Value ? Convert.ToDecimal(row["DoanhThu"]) : 0;
                int lt = row["LuotTiem"] != DBNull.Value ? Convert.ToInt32(row["LuotTiem"]) : 0;
                int km = row["KhachMoi"] != DBNull.Value ? Convert.ToInt32(row["KhachMoi"]) : 0;
                int shh = row["SapHetHan"] != DBNull.Value ? Convert.ToInt32(row["SapHetHan"]) : 0;
                return (dt, lt, km, shh);
            }
            return (0, 0, 0, 0);
        }

        public DataTable GetDoanhThu7Ngay()
        {
            return dal.GetDoanhThu7Ngay();
        }

        public DataTable GetTyLeDoanhThu()
        {
            return dal.GetTyLeDoanhThu();
        }

        public DataTable GetVaccineSapHetHan()
        {
            return dal.GetVaccineSapHetHan();
        }
        public DataTable GetDoanhThuChiTiet()
        {
            return dal.GetDoanhThuChiTiet();
        }

        public DataTable GetXuatNhapTon()
        {
            return dal.GetXuatNhapTon();
        }
    }
}
