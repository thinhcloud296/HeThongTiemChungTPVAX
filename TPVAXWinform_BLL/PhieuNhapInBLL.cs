using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_BLL
{
    public class PhieuNhapInBLL
    {
        public DataTable GetPhieuNhapInData(string maPN)
        {
            TPVAXWinform_DAL.PhieuNhapInDAL phieuNhapInDAL = new TPVAXWinform_DAL.PhieuNhapInDAL();
            return phieuNhapInDAL.GetPhieuNhapInData(maPN);
        }
    }
}
