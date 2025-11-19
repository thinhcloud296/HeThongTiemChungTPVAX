using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_BLL
{
    public class HoaDonInBLL
    {
        TPVAXWinform_DAL.HoaDonInDAL hoaDonInDAL = new TPVAXWinform_DAL.HoaDonInDAL();
        public DataTable GetHoaDonInData(string maHD)
        {
            
            return hoaDonInDAL.GetHoaDonInData(maHD);
        }
    }
}
