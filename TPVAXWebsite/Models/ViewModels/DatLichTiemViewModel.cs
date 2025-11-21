using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace TPVAXWebsite.Models.ViewModels
{
    public class DatLichTiemViewModel
    {
        [Display(Name = "Chọn người tiêm")]
        [Required(ErrorMessage = "Vui lòng chọn hồ sơ tiêm chủng")]
        public string MaHSTC { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vaccine")]
        public string MaVC { get; set; }

        [Display(Name = "Ngày hẹn mong muốn")]
        [Required(ErrorMessage = "Vui lòng chọn ngày hẹn tiêm")]
        [DataType(DataType.DateTime)]
        // Mặc định set ngày mai để tránh lỗi chọn quá khứ
        public DateTime NgayHenTiem { get; set; } = DateTime.Now.AddDays(1);

        [Display(Name = "Ghi chú thêm")]
        public string GhiChu { get; set; }

        // --- CÁC TRƯỜNG HIỂN THỊ (READ-ONLY) ---
        public string TenVaccine { get; set; }
        public decimal GiaBan { get; set; }
        public string HinhAnh { get; set; }

        // Danh sách để đổ vào DropdownList chọn người thân
        public IEnumerable<SelectListItem> DanhSachHoSo { get; set; }
    }
}
