    using System.Collections.Generic;
    using System.Linq;
    using TPVAXWebsite.Models.ViewModels;
    using TPVAXWebsite.DAL;

    namespace TPVAXWebsite.Services
    {
        public class BaiVietService
        {
            private readonly TPVAXDbContext _db;

            public BaiVietService()
            {
                _db = new TPVAXDbContext();
            }

            // Lấy tất cả bài viết đang hiển thị
            public List<BaiVietViewModel> LayTatCa()
            {
                return _db.BaiViets
                          .Where(b => b.TrangThai == true)
                          .OrderByDescending(b => b.NgayDang)
                          .Select(b => new BaiVietViewModel
                          {
                              Id = b.MaBV,
                              TieuDe = b.TieuDe,
                              TomTat = b.TomTat,
                              NoiDung = b.NoiDung,
                              HinhAnh = b.HinhAnh,
                              NgayDang = b.NgayDang,
                              DanhMuc = b.DanhMuc,
                              Tag = b.Tag
                          }).ToList();
            }

            // Lấy chi tiết bài viết theo ID
            public BaiVietViewModel LayChiTiet(int id)
            {
                return _db.BaiViets
                          .Where(b => b.TrangThai == true && b.MaBV == id)
                          .Select(b => new BaiVietViewModel
                          {
                              Id = b.MaBV,
                              TieuDe = b.TieuDe,
                              TomTat = b.TomTat,
                              NoiDung = b.NoiDung,
                              HinhAnh = b.HinhAnh,
                              NgayDang = b.NgayDang,
                              DanhMuc = b.DanhMuc,
                              Tag = b.Tag
                          }).FirstOrDefault();
            }

            // Lấy bài viết theo danh mục
            public List<BaiVietViewModel> LayTheoDanhMuc(string danhMuc)
            {
                return _db.BaiViets
                          .Where(b => b.TrangThai == true && b.DanhMuc == danhMuc)
                          .OrderByDescending(b => b.NgayDang)
                          .Select(b => new BaiVietViewModel
                          {
                              Id = b.MaBV,
                              TieuDe = b.TieuDe,
                              TomTat = b.TomTat,
                              NoiDung = b.NoiDung,
                              HinhAnh = b.HinhAnh,
                              NgayDang = b.NgayDang,
                              DanhMuc = b.DanhMuc,
                              Tag = b.Tag
                          }).ToList();
            }
        }
    }
