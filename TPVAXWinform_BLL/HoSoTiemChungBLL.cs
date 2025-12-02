using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;

namespace TPVAXWinform_BLL
{
    public class HoSoTiemChungBLL
    {
        private readonly HoSoTiemChungDAL _dal = new HoSoTiemChungDAL();
        TaoMaTuDong autoGen = new TaoMaTuDong();
        public DataTable GetData()
        {
            return _dal.GetData();
        }
        public DataTable GetHSTC_KHHG()
        {
            return _dal.GetHSTC_KHHG();
        }   
        public DataTable GetHSTC_QuanHe_KH(string MaKH)
        {
            return _dal.GetHSTC_QuanHe_KH(MaKH);
        }   
        public string CreateMaHSTC(string CCCD)
        {
            return autoGen.GenMaHSTC();
        }
        public void Insert(TPVAXWinform_DTO.HoSoTiemChungDTO newHSTC)
        {
            _dal.Insert(newHSTC);
        }
        public void Edit(TPVAXWinform_DTO.HoSoTiemChungDTO hSTC)
        {
            _dal.Edit(hSTC);
        }
        public bool IsHSTCExists(string CCCD)
        {
            return _dal.IsHSTCExists(CCCD);
        }
    }
}
