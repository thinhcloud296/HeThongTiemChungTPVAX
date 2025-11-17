using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TPVAXWinform_DAL;
using TPVAXWinform_DTO;
namespace TPVAXWinform_BLL
{
    public class PhieuNhapBLL
    {
        private readonly PhieuNhapDAL _dal = new PhieuNhapDAL();
        public DataTable GetDataDetail()
        {
            return _dal.GetDataDetail();
        }
        public DataTable GetDetailByMaPN(string maPN)
        {
            return _dal.GetDetailByMaPN(maPN);
        }
    }
}
