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
    public class LoaiVaccineBLL
    {
        private readonly LoaiVaccineDAL _dal = new LoaiVaccineDAL();
        public DataTable GetData()
        {
            return _dal.GetData();
        }

        public string CreateNewMaLoaiVaccine()
        {
            return _dal.CreateNewMaLoaiVaccine();
        }

        public void Insert(LoaiVaccineDTO loaiVaccine)
        {
            _dal.Insert(loaiVaccine);
        }

        public void Edit(LoaiVaccineDTO loaiVaccine)
        {
            _dal.Edit(loaiVaccine);
        }
    }
}
