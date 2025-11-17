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
    public class ChiTietPhieuNhapBLL
    {
        private readonly ChiTietPhieuNhapDAL _dal = new ChiTietPhieuNhapDAL();

        public DataTable GetDataByMaPN(string maPN)
        {
            return _dal.GetDataByMaPN(maPN);
        }

        public string CreateNewMaCTPN()
        {
            return _dal.CreateNewMaCTPN();
        }

        public void Insert(ChiTietPhieuNhapDTO ctpn)
        {
            _dal.Insert(ctpn);
        }

        public void Edit(ChiTietPhieuNhapDTO ctpn)
        {
            _dal.Edit(ctpn);
        }

        public void Delete(string maCTPN)
        {
            _dal.Delete(maCTPN);
        }
        public void XacNhanNhapKho(string maPN)
        {
            _dal.XacNhanNhapKho(maPN);
        }
    }
}
