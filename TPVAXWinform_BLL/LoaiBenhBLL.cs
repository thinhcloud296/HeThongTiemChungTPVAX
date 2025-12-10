using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;

namespace TPVAXWinform_BLL
{
    public class LoaiBenhBLL
    {
        private readonly LoaiBenhDAL _dal = new LoaiBenhDAL();
        TaoMaTuDong autoGen = new TaoMaTuDong();
        public DataTable GetData()
        {
            return _dal.GetData();
        }
        public string CreateNewMaLoaiBenh()
        {
            return autoGen.GenMaLoaiBenh();
        }
        public void Insert(TPVAXWinform_DTO.LoaiBenhDTO loaiBenh)
        {
            _dal.Insert(loaiBenh);
        }
        public void Edit(TPVAXWinform_DTO.LoaiBenhDTO loaiBenh)
        {
            _dal.Edit(loaiBenh);
        }
    }
}
