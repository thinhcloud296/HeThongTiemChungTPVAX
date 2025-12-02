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
        TaoMaTuDong autoGen = new TaoMaTuDong();
        public DataTable GetDataByMaHD(string MaHD)
        {
            return dal.GetDataByMaHD(MaHD);
        }
        public string CreateNewMaCTHD()
        {
            return autoGen.GenMaCTHD();
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
