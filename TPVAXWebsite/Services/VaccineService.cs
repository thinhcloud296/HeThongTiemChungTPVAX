using System;
using System.Collections.Generic;
using System.Linq;
using TPVAXWebsite.DAL;
using TPVAXWebsite.DAL.Repositories;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Services
{
    public interface IVaccineService
    {
        IEnumerable<Vaccine> GetAllVaccines();
        Vaccine GetVaccineById(string maVC);
        IEnumerable<Vaccine> SearchVaccines(string keyword);
        IEnumerable<Vaccine> GetVaccinesByLoaiBenh(string maLoaiBenh);
        IEnumerable<GoiVaccine> GetAllGoiVaccines();
        GoiVaccine GetGoiVaccineById(string maGoi);
        VaccineDetailViewModel GetVaccineDetail(string maVC);
    }

    public class VaccineService : IVaccineService
    {
        public IEnumerable<Vaccine> GetAllVaccines()
        {
            throw new NotImplementedException();
        }

        public Vaccine GetVaccineById(string maVC)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vaccine> SearchVaccines(string keyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vaccine> GetVaccinesByLoaiBenh(string maLoaiBenh)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoiVaccine> GetAllGoiVaccines()
        {
            throw new NotImplementedException();
        }

        public GoiVaccine GetGoiVaccineById(string maGoi)
        {
            throw new NotImplementedException();
        }

        public VaccineDetailViewModel GetVaccineDetail(string maVC)
        {
            throw new NotImplementedException();
        }
    }
}
