using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPVAXWebsite.Models.ViewModels
{
    public class CreateHoSoViewModel
    {
        [Display(Name = "Họ và tên người tiêm")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string HoTen { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [Display(Name = "CCCD/Mã định danh")]
        [Required(ErrorMessage = "Vui lòng nhập CCCD (hoặc mã định danh trẻ em)")]
        public string CCCD { get; set; }

        [Display(Name = "Mối quan hệ với bạn")]
        [Required(ErrorMessage = "Vui lòng chọn mối quan hệ")]
        public string QuanHe { get; set; } // Con, Bố, Mẹ, Vợ, Chồng...

        [Display(Name = "Ghi chú sức khỏe")]
        public string GhiChu { get; set; }
    }
}