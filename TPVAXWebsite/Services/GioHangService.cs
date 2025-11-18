using System;
using System.Collections.Generic;
using System.Linq;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Services
{
    public interface IGioHangService
    {
        IEnumerable<GioHangViewModel> GetGioHangByMaKH(string maKH);
        bool ThemVaoGio(string maKH, string maSanPham, string loaiSanPham, int soLuong);
        bool CapNhatSoLuong(int maGH, int soLuong);
        bool XoaKhoiGio(int maGH);
        decimal TinhTongTien(string maKH);
        void XoaToanBoGioHang(string maKH);
    }

    public class GioHangService : IGioHangService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GioHangService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<GioHangViewModel> GetGioHangByMaKH(string maKH)
        {
            var gioHangs = _unitOfWork.Repository<GioHang>()
                .Find(g => g.MaKH == maKH)
                .ToList();

            var viewModels = new List<GioHangViewModel>();

            foreach (var item in gioHangs)
            {
                var vm = new GioHangViewModel
                {
                    MaGH = item.MaGH,
                    SoLuong = item.SoLuong,
                    LoaiSanPham = item.LoaiSanPham
                };

                if (item.LoaiSanPham == "VACCINE")
                {
                    var vaccine = _unitOfWork.Repository<Vaccine>().GetById(item.MaSanPham);
                    if (vaccine != null)
                    {
                        vm.TenSanPham = vaccine.TenVC;
                        vm.GiaBan = vaccine.GiaBan;
                        vm.HinhAnh = vaccine.HinhAnh;
                    }
                }
                else if (item.LoaiSanPham == "GOIVACCINE")
                {
                    var goi = _unitOfWork.Repository<GoiVaccine>().GetById(item.MaSanPham);
                    if (goi != null)
                    {
                        vm.TenSanPham = goi.TenGoi;
                        vm.GiaBan = goi.GiaGoi;
                    }
                }

                vm.ThanhTien = vm.GiaBan * vm.SoLuong;
                viewModels.Add(vm);
            }

            return viewModels;
        }

        public bool ThemVaoGio(string maKH, string maSanPham, string loaiSanPham, int soLuong)
        {
            try
            {
                // Kiểm tra sản phẩm đã có trong giỏ chưa
                var existing = _unitOfWork.Repository<GioHang>()
                    .FirstOrDefault(g => g.MaKH == maKH && 
                                        g.MaSanPham == maSanPham && 
                                        g.LoaiSanPham == loaiSanPham);

                if (existing != null)
                {
                    // Tăng số lượng
                    existing.SoLuong += soLuong;
                    _unitOfWork.Repository<GioHang>().Update(existing);
                }
                else
                {
                    // Thêm mới
                    var gioHang = new GioHang
                    {
                        MaKH = maKH,
                        MaSanPham = maSanPham,
                        LoaiSanPham = loaiSanPham,
                        SoLuong = soLuong
                    };

                    _unitOfWork.Repository<GioHang>().Add(gioHang);
                }

                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CapNhatSoLuong(int maGH, int soLuong)
        {
            try
            {
                var gioHang = _unitOfWork.Repository<GioHang>().GetById(maGH);
                if (gioHang == null || soLuong <= 0)
                    return false;

                gioHang.SoLuong = soLuong;
                _unitOfWork.Repository<GioHang>().Update(gioHang);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool XoaKhoiGio(int maGH)
        {
            try
            {
                var gioHang = _unitOfWork.Repository<GioHang>().GetById(maGH);
                if (gioHang == null)
                    return false;

                _unitOfWork.Repository<GioHang>().Remove(gioHang);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public decimal TinhTongTien(string maKH)
        {
            var gioHangs = GetGioHangByMaKH(maKH);
            return gioHangs.Sum(g => g.ThanhTien);
        }

        public void XoaToanBoGioHang(string maKH)
        {
            var gioHangs = _unitOfWork.Repository<GioHang>()
                .Find(g => g.MaKH == maKH);

            _unitOfWork.Repository<GioHang>().RemoveRange(gioHangs);
            _unitOfWork.SaveChanges();
        }
    }
}
