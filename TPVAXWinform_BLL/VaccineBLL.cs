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
    public class VaccineBLL
    {
        private readonly VaccineDAL _vaccineDAL = new VaccineDAL();
     
        public DataTable GetData()
        {
          return _vaccineDAL.GetData();
        }
        
        public DataTable GetDataVaccineDetail()
      {
          return _vaccineDAL.GetDataVaccineDetail();
    }

        public VaccineDTO GetVaccineByMaVC(string maVC)
 {
            return _vaccineDAL.GetVaccineByMaVC(maVC);
        }

        public void UpdateSoLuongTon(string maVC, int soLuongThayDoi)
        {
    _vaccineDAL.UpdateSoLuongTon(maVC, soLuongThayDoi);
    }
    }
}
