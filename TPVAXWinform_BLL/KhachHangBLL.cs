using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_BLL
{
    public class KhachHangBLL
    {
        KhachHangDAL dal = new KhachHangDAL();
        public DataTable GetData()
        {
            return dal.GetData();
        }
        public void Insert(KhachHangDTO newKH)
        {
            dal.Insert(newKH);
        }
        public string CreateMaKH(string CCCD)
        {
            return dal.CreateMaKH(CCCD);
        }
        public void Edit(KhachHangDTO khachHang)
        {
            dal.Edit(khachHang);
        }
    }
}
