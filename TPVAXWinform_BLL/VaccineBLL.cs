using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;
namespace TPVAXWinform_BLL
{
    public class VaccineBLL
    {
        private readonly VaccineDAL _vaccineDAL = new VaccineDAL();
        public System.Data.DataTable GetDataVaccineDetail()
        {
            return _vaccineDAL.GetDataVaccineDetail();
        }
    }
}
