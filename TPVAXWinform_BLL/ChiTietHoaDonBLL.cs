using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TPVAXWinform_DTO;
using TPVAXWinform_DAL;
namespace TPVAXWinform_BLL
{
    public class ChiTietHoaDonBLL
    {
        ChiTietHoaDonDAL dal = new ChiTietHoaDonDAL();
        public DataTable GetData()
        {
            return dal.GetData();
        }
        public void Insert(ChiTietHoaDonDTO cthd)
        {
            dal.Insert(cthd);
        }
        public void Edit(ChiTietHoaDonDTO cthd)
        {
            dal.Edit(cthd);
        }
    }
}
