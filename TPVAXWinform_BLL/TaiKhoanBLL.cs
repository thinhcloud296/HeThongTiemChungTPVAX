using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;

namespace TPVAXWinform_BLL
{
    public class TaiKhoanBLL
    {
   private TaiKhoanDAL dal = new TaiKhoanDAL();

        public DataTable GetTaiKhoanByMaKH(string MaKH)
        {
            return dal.GetTaiKhoanByMaKH(MaKH);
        }

        public DataTable GetTaiKhoanByMaNV(string MaNV)
        {
     return dal.GetTaiKhoanByMaNV(MaNV);
        }

        public void ResetPassword(string MaNV, string newPassword)
        {
            dal.ResetPassword(MaNV, newPassword);
        }
    }
}
