using System;
using System.Collections.Generic;

namespace TPVAXWebsite.Models.ViewModels
{
    public class DatLichTiemViewModel
    {
        public string MaHSTC { get; set; }
        public string HoTenNguoiTiem { get; set; }
        public DateTime NgaySinh { get; set; }
        public string MaVC { get; set; }
        public string TenVaccine { get; set; }
        public decimal GiaVaccine { get; set; }
        public DateTime NgayHenTiem { get; set; }
        public string GhiChu { get; set; }
    }

    public class HoSoTiemChungSelectViewModel
    {
        public string MaHSTC { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string VaiTro { get; set; }
    }
}
