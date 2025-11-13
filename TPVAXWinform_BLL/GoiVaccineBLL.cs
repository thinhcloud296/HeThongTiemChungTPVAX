using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TPVAXWinform_DAL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_BLL
{
    public class GoiVaccineBLL
    {
        GoiVaccineDAL _dal = new GoiVaccineDAL();
        public DataTable GetData()
        {
            return _dal.GetData();
        }
        public void Insert(GoiVaccineDTO goi)
        {
            _dal.Insert(goi);
        }
        public void Edit(GoiVaccineDTO goi)
        {
            _dal.Edit(goi);
        }

    }
}
