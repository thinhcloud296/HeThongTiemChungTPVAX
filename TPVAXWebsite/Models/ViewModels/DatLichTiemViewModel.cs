using System;
using System.ComponentModel.DataAnnotations;

namespace TPVAXWebsite.Models.ViewModels
{
    public class DatLichTiemViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn hồ sơ tiêm chủng")]
        public string MaHSTC { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vaccine")]
        public string MaVC { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày hẹn tiêm")]
        [DataType(DataType.DateTime)]
        public DateTime NgayHenTiem { get; set; }

        public int? SoMui { get; set; }

        public string GhiChu { get; set; }

        public string MaNV { get; set; }

        // Display properties
        public string TenVaccine { get; set; }
        public string TenNguoiTiem { get; set; }
        public string TenNhanVien { get; set; }
    }
}
