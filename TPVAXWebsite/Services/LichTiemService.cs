using System;
using System.Collections.Generic;
using System.Linq;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;
using TPVAXWebsite.Models.ViewModels;

namespace TPVAXWebsite.Services
{
    public interface ILichTiemService
    {
        IEnumerable<LichTiem> GetLichTiemByMaKH(string maKH);
        IEnumerable<LichTiem> GetLichTiemByMaHSTC(string maHSTC);
        bool DatLichTiem(DatLichTiemViewModel model);
        bool HuyLichTiem(string maLT);
        IEnumerable<HoSoTiemChung> GetHoSoTiemChungByMaKH(string maKH);
        bool ThemHoSoNguoiThan(string maKH, HoSoTiemChung hoSo, string vaiTro);
    }

    public class LichTiemService : ILichTiemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LichTiemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<LichTiem> GetLichTiemByMaKH(string maKH)
        {
            // Lấy tất cả hồ sơ liên kết với khách hàng
            var lienKets = _unitOfWork.Repository<LienKetHoSo>()
                .Find(lk => lk.MaKH == maKH);

            var maHSTCs = lienKets.Select(lk => lk.MaHSTC).ToList();

            // Lấy tất cả lịch tiêm của các hồ sơ này
            return _unitOfWork.Repository<LichTiem>()
                .Find(lt => maHSTCs.Contains(lt.MaHSTC))
                .OrderByDescending(lt => lt.NgayHenTiem);
        }

        public IEnumerable<LichTiem> GetLichTiemByMaHSTC(string maHSTC)
        {
            return _unitOfWork.Repository<LichTiem>()
                .Find(lt => lt.MaHSTC == maHSTC)
                .OrderByDescending(lt => lt.NgayHenTiem);
        }

        public bool DatLichTiem(DatLichTiemViewModel model)
        {
            try
            {
                var lichTiem = new LichTiem
                {
                    MaLT = GenerateMaLT(),
                    NgayHenTiem = model.NgayHenTiem,
                    SoMui = model.SoMui,
                    TrangThai = false, // false = Chưa tiêm
                    GhiChu = model.GhiChu,
                    MaHSTC = model.MaHSTC,
                    MaVC = model.MaVC
                };

                _unitOfWork.Repository<LichTiem>().Add(lichTiem);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool HuyLichTiem(string maLT)
        {
            try
            {
                var lichTiem = _unitOfWork.Repository<LichTiem>().GetById(maLT);
                if (lichTiem == null)
                    return false;

                // Chỉ hủy được lịch chưa tiêm
                if (lichTiem.TrangThai == true) // true = Đã tiêm
                    return false;

                _unitOfWork.Repository<LichTiem>().Remove(lichTiem);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<HoSoTiemChung> GetHoSoTiemChungByMaKH(string maKH)
        {
            var lienKets = _unitOfWork.Repository<LienKetHoSo>()
                .Find(lk => lk.MaKH == maKH);

            var maHSTCs = lienKets.Select(lk => lk.MaHSTC).ToList();

            return _unitOfWork.Repository<HoSoTiemChung>()
                .Find(hs => maHSTCs.Contains(hs.MaHSTC) && hs.TrangThai == true);
        }

        public bool ThemHoSoNguoiThan(string maKH, HoSoTiemChung hoSo, string vaiTro)
        {
            try
            {
                // Kiểm tra CCCD đã tồn tại chưa
                var existing = _unitOfWork.Repository<HoSoTiemChung>()
                    .Any(hs => hs.CCCD == hoSo.CCCD);

                if (existing)
                    return false;

                // Tạo mã hồ sơ
                hoSo.MaHSTC = GenerateMaHSTC();
                hoSo.TrangThai = true;

                _unitOfWork.Repository<HoSoTiemChung>().Add(hoSo);

                // Tạo liên kết
                var lienKet = new LienKetHoSo
                {
                    MaLK = GenerateMaLK(),
                    VaiTro = vaiTro,
                    NgayLienKet = DateTime.Now,
                    MaKH = maKH,
                    MaHSTC = hoSo.MaHSTC
                };

                _unitOfWork.Repository<LienKetHoSo>().Add(lienKet);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateMaLT()
        {
            var count = _unitOfWork.Repository<LichTiem>().Count() + 1;
            return "LT" + count.ToString("D6");
        }

        private string GenerateMaHSTC()
        {
            var count = _unitOfWork.Repository<HoSoTiemChung>().Count() + 1;
            return "HS" + count.ToString("D6");
        }

        private string GenerateMaLK()
        {
            var count = _unitOfWork.Repository<LienKetHoSo>().Count() + 1;
            return "LK" + count.ToString("D6");
        }
    }
}
