using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_BLL
{
    public class LienKetHoSoBLL
    {
        private readonly TPVAXWinform_DAL.LienKetHoSoDAL _dal = new TPVAXWinform_DAL.LienKetHoSoDAL();
        TaoMaTuDong autoGen = new TaoMaTuDong();
        public string CreateMaLK(string CCCD)
        {
            return autoGen.GenMaLK();
        }
        public void Insert(TPVAXWinform_DTO.LienKetHoSoDTO newLKHS)
        {
            _dal.Insert(newLKHS);
        }
        public void Edit(LienKetHoSoDTO MaLK)
        {
            _dal.Edit(MaLK);
        }
    }
}
