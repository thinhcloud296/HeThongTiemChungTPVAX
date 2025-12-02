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
    public class HoaDonBLL
    {
        HoaDonDAL dal = new HoaDonDAL();
        TaoMaTuDong autoGen = new TaoMaTuDong();
        public DataTable GetData()
        {
            return dal.GetData();
        }
        public string CreateNewMaHD()
        {
            return autoGen.GenMaHD();
        }
        public void Insert(HoaDonDTO hd)
        {
            dal.Insert(hd);
        }
        public void Edit(HoaDonDTO hd)
        {
            dal.Edit(hd);
        }
        public void Delete(string MaHD)
        {
            dal.Delete(MaHD);
        }
    }
}
