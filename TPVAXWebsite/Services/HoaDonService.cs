using System;
using System.Collections.Generic;
using System.Linq;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Services
{
    public interface IHoaDonService
    {
        IEnumerable<HoaDonViewModel> GetHoaDonsByMaKH(string maKH);
        HoaDonViewModel GetHoaDonDetail(string maHD);
        string TaoHoaDonTuGioHang(string maKH, string maKM = null);
        bool HuyHoaDon(string maHD);
    }

    public class HoaDonService : IHoaDonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGioHangService _gioHangService;

        public HoaDonService(IUnitOfWork unitOfWork, IGioHangService gioHangService)
        {
            _unitOfWork = unitOfWork;
            _gioHangService = gioHangService;
        }

        public IEnumerable<HoaDonViewModel> GetHoaDonsByMaKH(string maKH)
        {
            var hoaDons = _unitOfWork.Repository<HoaDon>()
                .Find(hd => hd.MaKH == maKH)
                .OrderByDescending(hd => hd.NgayLap)
                .ToList();

            var viewModels = new List<HoaDonViewModel>();

            foreach (var hoaDon in hoaDons)
            {
                var vm = new HoaDonViewModel
                {
                    MaHD = hoaDon.MaHD,
                    NgayLap = hoaDon.NgayLap,
                    TongTien = hoaDon.TongTien,
                    TrangThai = hoaDon.TrangThai ? "Đã thanh toán" : "Chưa thanh toán"
                };

                // Lấy chi tiết hóa đơn
                var chiTiets = _unitOfWork.Repository<ChiTietHoaDon>()
                    .Find(ct => ct.MaHD == hoaDon.MaHD)
                    .ToList();

                vm.SoLuongSanPham = chiTiets.Sum(ct => ct.SoLuong);

                viewModels.Add(vm);
            }

            return viewModels;
        }

        public HoaDonViewModel GetHoaDonDetail(string maHD)
        {
            var hoaDon = _unitOfWork.Repository<HoaDon>().GetById(maHD);
            if (hoaDon == null)
                return null;

            var vm = new HoaDonViewModel
            {
                MaHD = hoaDon.MaHD,
                NgayLap = hoaDon.NgayLap,
                TongTien = hoaDon.TongTien,
                TrangThai = hoaDon.TrangThai ? "Đã thanh toán" : "Chưa thanh toán"
            };

            // Lấy thông tin khách hàng
            var khachHang = _unitOfWork.Repository<KhachHang>().GetById(hoaDon.MaKH);
            if (khachHang != null)
            {
                vm.TenKhachHang = khachHang.HoTen;
                vm.SoDienThoai = khachHang.SoDT;
            }

            // Lấy chi tiết hóa đơn
            var chiTiets = _unitOfWork.Repository<ChiTietHoaDon>()
                .Find(ct => ct.MaHD == maHD)
                .ToList();

            vm.ChiTietHoaDons = chiTiets;
            vm.SoLuongSanPham = chiTiets.Sum(ct => ct.SoLuong);

            return vm;
        }

        public string TaoHoaDonTuGioHang(string maKH, string maKM = null)
        {
            try
            {
                // Lấy giỏ hàng
                var gioHangs = _unitOfWork.Repository<GioHang>()
                    .Find(g => g.MaKH == maKH)
                    .ToList();

                if (!gioHangs.Any())
                    return null;

                // Tính tổng tiền
                decimal tongTien = 0;
                foreach (var item in gioHangs)
                {
                    decimal giaBan = 0;
                    if (item.LoaiSanPham == "VACCINE")
                    {
                        var vaccine = _unitOfWork.Repository<Vaccine>().GetById(item.MaSanPham);
                        giaBan = vaccine?.GiaBan ?? 0;
                    }
                    else if (item.LoaiSanPham == "GOIVACCINE")
                    {
                        var goi = _unitOfWork.Repository<GoiVaccine>().GetById(item.MaSanPham);
                        giaBan = goi?.GiaGoi ?? 0;
                    }

                    tongTien += giaBan * item.SoLuong;
                }

                // Áp dụng khuyến mãi nếu có
                if (!string.IsNullOrEmpty(maKM))
                {
                    var khuyenMai = _unitOfWork.Repository<KhuyenMai>().GetById(maKM);
                    if (khuyenMai != null && khuyenMai.TrangThai == true)
                    {
                        if (khuyenMai.KieuGiam == "PhanTram")
                        {
                            tongTien = tongTien * (1 - khuyenMai.GiaTriGiam / 100);
                        }
                        else if (khuyenMai.KieuGiam == "SoTien")
                        {
                            tongTien = tongTien - khuyenMai.GiaTriGiam;
                        }
                    }
                }

                // Tạo mã hóa đơn
                string maHD = GenerateMaHD();

                // Tạo hóa đơn
                var hoaDon = new HoaDon
                {
                    MaHD = maHD,
                    NgayLap = DateTime.Now,
                    TongTien = tongTien,
                    TrangThai = false, // Chưa thanh toán
                    MaKH = maKH,
                    MaKM = maKM
                };

                _unitOfWork.Repository<HoaDon>().Add(hoaDon);

                // Tạo chi tiết hóa đơn
                foreach (var item in gioHangs)
                {
                    decimal giaBan = 0;
                    if (item.LoaiSanPham == "VACCINE")
                    {
                        var vaccine = _unitOfWork.Repository<Vaccine>().GetById(item.MaSanPham);
                        giaBan = vaccine?.GiaBan ?? 0;
                    }
                    else if (item.LoaiSanPham == "GOIVACCINE")
                    {
                        var goi = _unitOfWork.Repository<GoiVaccine>().GetById(item.MaSanPham);
                        giaBan = goi?.GiaGoi ?? 0;
                    }

                    var chiTiet = new ChiTietHoaDon
                    {
                        MaCTHD = GenerateMaCTHD(),
                        MaHD = maHD,
                        MaSanPham = item.MaSanPham,
                        LoaiSanPham = item.LoaiSanPham,
                        SoLuong = item.SoLuong,
                        DonGia = giaBan
                    };

                    _unitOfWork.Repository<ChiTietHoaDon>().Add(chiTiet);
                }

                // Xóa giỏ hàng
                _gioHangService.XoaToanBoGioHang(maKH);

                // Lưu thay đổi
                _unitOfWork.SaveChanges();

                return maHD;
            }
            catch
            {
                return null;
            }
        }

        public bool HuyHoaDon(string maHD)
        {
            try
            {
                var hoaDon = _unitOfWork.Repository<HoaDon>().GetById(maHD);
                if (hoaDon == null)
                    return false;

                // Chỉ hủy được hóa đơn chưa thanh toán
                if (hoaDon.TrangThai == true)
                    return false;

                // Xóa chi tiết hóa đơn
                var chiTiets = _unitOfWork.Repository<ChiTietHoaDon>()
                    .Find(ct => ct.MaHD == maHD);

                _unitOfWork.Repository<ChiTietHoaDon>().RemoveRange(chiTiets);

                // Xóa hóa đơn
                _unitOfWork.Repository<HoaDon>().Remove(hoaDon);

                _unitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateMaHD()
        {
            var count = _unitOfWork.Repository<HoaDon>().Count() + 1;
            return "HD" + count.ToString("D6");
        }

        private string GenerateMaCTHD()
        {
            var count = _unitOfWork.Repository<ChiTietHoaDon>().Count() + 1;
            return "CT" + count.ToString("D6");
        }
    }
}
