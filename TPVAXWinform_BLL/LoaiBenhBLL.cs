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
        public DataTable GetData()
        {
            return _dal.GetData();
        }
        public string CreateNewMaLoaiBenh()
        {
            return _dal.CreateNewMaLoaiBenh();
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
