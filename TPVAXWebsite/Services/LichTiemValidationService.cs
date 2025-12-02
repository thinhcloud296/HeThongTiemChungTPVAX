using System;
using System.Data.Entity;
using System.Linq;
using TPVAXWebsite.DAL;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Services
{
    /// <summary>
    /// Service validate logic nghiệp vụ lịch tiêm
    /// Fix lỗi #2: Không kiểm tra trùng lịch tiêm
    /// Fix lỗi #3: Không kiểm tra khoảng cách giữa các mũi tiêm
    /// </summary>
    public class LichTiemValidationService : IDisposable
    {
        private readonly TPVAXDbContext _context;

        public LichTiemValidationService()
        {
            _context = new TPVAXDbContext();
        }

        public LichTiemValidationService(TPVAXDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Kết quả validation lịch tiêm
        /// </summary>
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; }
            public int SoMuiTiepTheo { get; set; } // Mũi tiếp theo cần tiêm
            public DateTime? NgayHenToiThieu { get; set; } // Ngày sớm nhất có thể đặt lịch

            public static ValidationResult Success(int soMuiTiepTheo, DateTime? ngayHenToiThieu = null)
            {
                return new ValidationResult
                {
                    IsValid = true,
                    SoMuiTiepTheo = soMuiTiepTheo,
                    NgayHenToiThieu = ngayHenToiThieu
                };
            }

            public static ValidationResult Fail(string message)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = message
                };
            }
        }

        /// <summary>
        /// Kiểm tra có thể đặt lịch tiêm vaccine này cho hồ sơ này không
        /// </summary>
        public ValidationResult ValidateDatLichTiem(string maHSTC, string maVC, DateTime ngayHenTiem)
        {
            // 1. Lấy thông tin vaccine
            var vaccine = _context.Vaccines.Find(maVC);
            if (vaccine == null)
            {
                return ValidationResult.Fail("Vaccine không tồn tại.");
            }

            int soMuiToiDa = vaccine.SoMuiToiDa ?? 1;
            int soThangCho = vaccine.SoThangCho ?? 1;

            // Vaccine tiêm nhắc hàng năm (SoMuiToiDa = 99) - không giới hạn số mũi
            bool isVaccineTiemNhac = soMuiToiDa >= 99;

            // 2. Lấy lịch sử tiêm vaccine này của hồ sơ
            var lichSuTiem = _context.LichTiems
                .Where(lt => lt.MaHSTC == maHSTC && lt.MaVC == maVC)
                .OrderByDescending(lt => lt.SoMui)
                .ToList();

            // 3. Đếm số mũi đã tiêm và đang chờ
            int soMuiDaTiem = lichSuTiem.Count(lt => lt.TrangThai == "Đã tiêm");
            int soMuiDangCho = lichSuTiem.Count(lt => lt.TrangThai == "Chưa tiêm");

            // 4. Kiểm tra đã tiêm đủ số mũi chưa (trừ vaccine tiêm nhắc)
            if (!isVaccineTiemNhac && soMuiDaTiem >= soMuiToiDa)
            {
                return ValidationResult.Fail(
                    $"Hồ sơ này đã tiêm đủ {soMuiToiDa} mũi vaccine {vaccine.TenVC}. " +
                    "Không cần tiêm thêm.");
            }

            // 5. Kiểm tra có lịch đang chờ không
            var lichDangCho = lichSuTiem.FirstOrDefault(lt => lt.TrangThai == "Chưa tiêm");
            if (lichDangCho != null)
            {
                return ValidationResult.Fail(
                    $"Hồ sơ này đã có lịch hẹn tiêm {vaccine.TenVC} mũi {lichDangCho.SoMui} " +
                    $"vào ngày {lichDangCho.NgayHenTiem:dd/MM/yyyy HH:mm}. " +
                    "Vui lòng hủy lịch cũ trước khi đặt lịch mới.");
            }

            // 6. Tính số mũi tiếp theo
            int soMuiTiepTheo = soMuiDaTiem + 1;

            // 7. Kiểm tra khoảng cách với mũi trước (nếu có)
            if (soMuiDaTiem > 0)
            {
                var muiTruoc = lichSuTiem
                    .Where(lt => lt.TrangThai == "Đã tiêm")
                    .OrderByDescending(lt => lt.NgayTiemThucTe)
                    .FirstOrDefault();

                if (muiTruoc != null && muiTruoc.NgayTiemThucTe.HasValue)
                {
                    // Tính ngày tối thiểu có thể tiêm mũi tiếp theo
                    DateTime ngayToiThieu = muiTruoc.NgayTiemThucTe.Value.AddMonths(soThangCho);

                    if (ngayHenTiem < ngayToiThieu)
                    {
                        return ValidationResult.Fail(
                            $"Chưa đủ thời gian giữa các mũi tiêm. " +
                            $"Mũi {muiTruoc.SoMui} đã tiêm ngày {muiTruoc.NgayTiemThucTe:dd/MM/yyyy}. " +
                            $"Cần chờ ít nhất {soThangCho} tháng. " +
                            $"Ngày sớm nhất có thể đặt: {ngayToiThieu:dd/MM/yyyy}.");
                    }

                    return ValidationResult.Success(soMuiTiepTheo, ngayToiThieu);
                }
            }

            // 8. Mũi đầu tiên hoặc không có ràng buộc
            return ValidationResult.Success(soMuiTiepTheo, DateTime.Now.AddDays(1));
        }

        /// <summary>
        /// Kiểm tra có thể đặt lịch tiêm gói vaccine này không
        /// Trả về danh sách vaccine trong gói cần kiểm tra
        /// </summary>
        public ValidationResult ValidateDatLichGoiVaccine(string maHSTC, string maGoi)
        {
            // Lấy chi tiết gói vaccine (chỉ lấy mũi 1 của mỗi vaccine)
            var chiTietGoi = _context.ChiTietGoiVaccines
                .Include(ct => ct.Vaccine)
                .Where(ct => ct.MaGoi == maGoi && ct.SoMui == 1)
                .ToList();

            foreach (var ctGoi in chiTietGoi)
            {
                if (ctGoi.Vaccine == null) continue;

                // Kiểm tra từng vaccine trong gói
                var lichSuTiem = _context.LichTiems
                    .Where(lt => lt.MaHSTC == maHSTC && lt.MaVC == ctGoi.MaVC)
                    .ToList();

                int soMuiDaTiem = lichSuTiem.Count(lt => lt.TrangThai == "Đã tiêm");
                int soMuiToiDa = ctGoi.Vaccine.SoMuiToiDa ?? 1;

                // Bỏ qua vaccine tiêm nhắc hàng năm
                if (soMuiToiDa >= 99) continue;

                if (soMuiDaTiem >= soMuiToiDa)
                {
                    return ValidationResult.Fail(
                        $"Hồ sơ này đã tiêm đủ {soMuiToiDa} mũi vaccine {ctGoi.Vaccine.TenVC} trong gói. " +
                        "Không thể mua gói này.");
                }

                // Kiểm tra có lịch đang chờ không
                var lichDangCho = lichSuTiem.FirstOrDefault(lt => lt.TrangThai == "Chưa tiêm");
                if (lichDangCho != null)
                {
                    return ValidationResult.Fail(
                        $"Hồ sơ này đã có lịch hẹn tiêm {ctGoi.Vaccine.TenVC} " +
                        $"vào ngày {lichDangCho.NgayHenTiem:dd/MM/yyyy}. " +
                        "Vui lòng hủy lịch cũ trước khi mua gói.");
                }
            }

            return ValidationResult.Success(1);
        }

        /// <summary>
        /// Tính ngày hẹn cho các mũi tiêm tiếp theo dựa trên lịch sử
        /// </summary>
        public DateTime TinhNgayHenMuiTiepTheo(string maHSTC, string maVC, int soMui, DateTime ngayHenMui1)
        {
            var vaccine = _context.Vaccines.Find(maVC);
            if (vaccine == null) return ngayHenMui1;

            int soThangCho = vaccine.SoThangCho ?? 1;

            if (soMui == 1)
            {
                // Mũi 1: kiểm tra xem đã tiêm mũi nào chưa
                var muiCuoi = _context.LichTiems
                    .Where(lt => lt.MaHSTC == maHSTC && lt.MaVC == maVC && lt.TrangThai == "Đã tiêm")
                    .OrderByDescending(lt => lt.NgayTiemThucTe)
                    .FirstOrDefault();

                if (muiCuoi != null && muiCuoi.NgayTiemThucTe.HasValue)
                {
                    // Đã tiêm trước đó, tính từ ngày tiêm cuối
                    DateTime ngayToiThieu = muiCuoi.NgayTiemThucTe.Value.AddMonths(soThangCho);
                    return ngayHenMui1 > ngayToiThieu ? ngayHenMui1 : ngayToiThieu;
                }

                return ngayHenMui1;
            }
            else
            {
                // Mũi 2 trở đi: cộng thêm tháng từ mũi 1
                return ngayHenMui1.AddMonths((soMui - 1) * soThangCho);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
