using System;
using System.Collections.Generic;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Models.ViewModels
{
    /// <summary>
    /// ViewModel cho danh sách bài viết
    /// </summary>
    public class BaiVietViewModel
    {
        public string MaBaiViet { get; set; }
        public string TieuDe { get; set; }
        public string TomTat { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public string TacGia { get; set; }
        public DateTime NgayDang { get; set; }
        public int LuotXem { get; set; }
        public string DanhMuc { get; set; }
        public List<string> Tags { get; set; }

        public BaiVietViewModel()
        {
            Tags = new List<string>();
        }
    }

    /// <summary>
    /// ViewModel cho chi tiết bài viết
    /// </summary>
    public class BaiVietDetailViewModel
    {
        public BaiVietViewModel BaiViet { get; set; }
        public List<BaiVietViewModel> BaiVietLienQuan { get; set; }
        public List<BinhLuanViewModel> BinhLuans { get; set; }

        public BaiVietDetailViewModel()
        {
            BaiVietLienQuan = new List<BaiVietViewModel>();
            BinhLuans = new List<BinhLuanViewModel>();
        }
    }

    /// <summary>
    /// ViewModel cho bình luận
    /// </summary>
    public class BinhLuanViewModel
    {
        public string MaBinhLuan { get; set; }
        public string NoiDung { get; set; }
        public string NguoiDung { get; set; }
        public DateTime NgayBinhLuan { get; set; }
    }
}
