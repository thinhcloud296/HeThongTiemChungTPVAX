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
    public class NhaCungCapBLL
    {
        private readonly NhaCungCapDAL _dal = new NhaCungCapDAL();

        public DataTable GetData()
        {
            return _dal.GetData();
        }

        public string CreateNewMaNCC()
        {
            return _dal.CreateNewMaNCC();
        }

        public void Insert(NhaCungCapDTO ncc)
        {
            _dal.Insert(ncc);
        }

        public void Edit(NhaCungCapDTO ncc)
        {
            _dal.Edit(ncc);
        }
    }
}
