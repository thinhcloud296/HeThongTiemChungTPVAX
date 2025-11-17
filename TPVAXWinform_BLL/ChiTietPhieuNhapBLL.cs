using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;

namespace TPVAXWinform_BLL
{
    public class ChiTietPhieuNhapBLL
    {
        private readonly ChiTietPhieuNhapDAL _dal = new ChiTietPhieuNhapDAL();

        public DataTable GetDataByMaPN(string maPN)
        {
            return _dal.GetDataByMaPN(maPN);
        }
    }
}
