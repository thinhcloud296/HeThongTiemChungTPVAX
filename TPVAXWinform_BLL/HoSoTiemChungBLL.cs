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
        public DataTable GetData()
        {
            return _dal.GetData();
        }
    }
}
