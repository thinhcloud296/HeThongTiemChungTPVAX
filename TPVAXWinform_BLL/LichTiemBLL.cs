using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPVAXWinform_BLL
{
    public class LichTiemBLL
    {
        private readonly TPVAXWinform_DAL.LichTiemDAL lichTiemDAL = new TPVAXWinform_DAL.LichTiemDAL();
        public System.Data.DataTable GetData()
        {
            return lichTiemDAL.GetData();
        }
        public DataTable GetGetLichTiemWithHSTC()
        {
            return lichTiemDAL.GetLichTiemWithHSTC();
        }
        public string CreateNewMaLT()
        {
            return lichTiemDAL.CreateNewMaLT();
        }
        public void Insert(TPVAXWinform_DTO.LichTiemDTO lichTiem)
        {
            lichTiemDAL.Insert(lichTiem);
        }
        public void Edit(TPVAXWinform_DTO.LichTiemDTO lichTiem)
        {
            lichTiemDAL.Edit(lichTiem);
        }

    }
}
