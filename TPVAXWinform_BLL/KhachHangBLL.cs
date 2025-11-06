using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;

namespace TPVAXWinform_BLL
{
    public class KhachHangBLL
    {
        KhachHangDAL dal = new KhachHangDAL();
        public DataTable GetData()
        {
            return dal.GetData();
        }
    }
}
