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
    }
}
