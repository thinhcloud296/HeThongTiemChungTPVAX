using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using TPVAXWinform_DAL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_BLL
{
    public class ChiTietGoiVaccineBLL
    {
        private ChiTietGoiVaccineDAL _dal = new ChiTietGoiVaccineDAL();

        public DataTable GetData()
        {
            return _dal.GetData();
        }

        public DataTable GetVaccinesByGoiVaccine(string maGoi)
        {
            return _dal.GetVaccinesByGoiVaccine(maGoi);
        }

        public void Insert(ChiTietGoiVaccineDTO ct)
        {
            _dal.Insert(ct);
        }

        public void Edit(ChiTietGoiVaccineDTO ct)
        {
            _dal.Edit(ct);
        }

        /// <summary>
        /// Sinh mã chi tiết gói vaccine mới theo format CTGV000001 (10 ký tự)
        /// </summary>
        public string GenerateMaCTGoi()
        {
            return _dal.GenerateMaCTGoi();
        }

        /// <summary>
        /// Xóa tất cả chi tiết gói vaccine theo mã gói
        /// </summary>
        public void DeleteByMaGoi(string maGoi)
        {
            _dal.DeleteByMaGoi(maGoi);
        }
    }
}
