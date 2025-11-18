using System;
using System.Collections.Generic;
using System.Linq;
using TPVAXWebsite.DAL;
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
        private readonly IUnitOfWork _unitOfWork;

        public VaccineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Vaccine> GetAllVaccines()
        {
            return _unitOfWork.Repository<Vaccine>()
                .Find(v => v.SoLuongTon > 0)
                .OrderBy(v => v.TenVC);
        }

        public Vaccine GetVaccineById(string maVC)
        {
            return _unitOfWork.Repository<Vaccine>().GetById(maVC);
        }

        public IEnumerable<Vaccine> SearchVaccines(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return GetAllVaccines();

            keyword = keyword.ToLower();
            return _unitOfWork.Repository<Vaccine>()
                .Find(v => v.TenVC.ToLower().Contains(keyword) && v.SoLuongTon > 0)
                .OrderBy(v => v.TenVC);
        }

        public IEnumerable<Vaccine> GetVaccinesByLoaiBenh(string maLoaiBenh)
        {
            // Lấy danh sách vaccine phòng bệnh này
            var vaccinePhongBenhs = _unitOfWork.Repository<VaccinePhongBenh>()
                .Find(vpb => vpb.MaLoaiBenh == maLoaiBenh);

            var maVCs = vaccinePhongBenhs.Select(vpb => vpb.MaVC).ToList();

            return _unitOfWork.Repository<Vaccine>()
                .Find(v => maVCs.Contains(v.MaVC) && v.SoLuongTon > 0)
                .OrderBy(v => v.TenVC);
        }

        public IEnumerable<GoiVaccine> GetAllGoiVaccines()
        {
            return _unitOfWork.Repository<GoiVaccine>()
                .Find(g => g.TrangThai == "Đang áp dụng")
                .OrderBy(g => g.TenGoi);
        }

        public GoiVaccine GetGoiVaccineById(string maGoi)
        {
            return _unitOfWork.Repository<GoiVaccine>().GetById(maGoi);
        }

        public VaccineDetailViewModel GetVaccineDetail(string maVC)
        {
            var vaccine = GetVaccineById(maVC);
            if (vaccine == null)
                return null;

            // Lấy thông tin loại vaccine
            var loaiVaccine = _unitOfWork.Repository<LoaiVaccine>().GetById(vaccine.MaLoai);

            // Lấy các bệnh mà vaccine này phòng
            var vaccinePhongBenhs = _unitOfWork.Repository<VaccinePhongBenh>()
                .Find(vpb => vpb.MaVC == maVC);

            var maBenhs = vaccinePhongBenhs.Select(vpb => vpb.MaLoaiBenh).ToList();
            var loaiBenhs = _unitOfWork.Repository<LoaiBenh>()
                .Find(lb => maBenhs.Contains(lb.MaLoaiBenh))
                .ToList();

            // Lấy các gói vaccine chứa vaccine này
            var chiTietGois = _unitOfWork.Repository<ChiTietGoiVaccine>()
                .Find(ct => ct.MaVC == maVC);

            var maGois = chiTietGois.Select(ct => ct.MaGoi).ToList();
            var goiVaccines = _unitOfWork.Repository<GoiVaccine>()
                .Find(g => maGois.Contains(g.MaGoi) && g.TrangThai == "Đang áp dụng")
                .ToList();

            return new VaccineDetailViewModel
            {
                Vaccine = vaccine,
                LoaiVaccine = loaiVaccine,
                LoaiBenhs = loaiBenhs,
                GoiVaccines = goiVaccines
            };
        }
    }
}
