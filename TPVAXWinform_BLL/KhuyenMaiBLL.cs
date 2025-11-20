using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_BLL
{
    public class KhuyenMaiBLL
    {
        private KhuyenMaiDAL khuyenMaiDAL = new KhuyenMaiDAL();

        public DataTable GetAll()
        {
            return khuyenMaiDAL.GetAll();
        }

        public DataTable GetChiTietByMaKM(string maKM)
        {
            return khuyenMaiDAL.GetChiTietByMaKM(maKM);
        }

        public string CreateNewMaKM()
        {
            return khuyenMaiDAL.CreateNewMaKM();
        }

        public void Insert(KhuyenMaiDTO km)
        {
            ValidateKhuyenMai(km);
            khuyenMaiDAL.Insert(km);
        }

        public void Update(KhuyenMaiDTO km)
        {
            ValidateKhuyenMai(km);
            khuyenMaiDAL.Update(km);
        }

        private void ValidateKhuyenMai(KhuyenMaiDTO km)
        {
            if (string.IsNullOrWhiteSpace(km.TenKM))
                throw new Exception("Tên khuyến mãi không được để trống.");

            if (string.IsNullOrWhiteSpace(km.LoaiKM))
                throw new Exception("Loại khuyến mãi không được để trống.");

            if (string.IsNullOrWhiteSpace(km.KieuGiam))
                throw new Exception("Kiểu giảm giá không được để trống.");

            if (km.GiaTriGiam <= 0)
                throw new Exception("Giá trị giảm phải lớn hơn 0.");

            if (km.KieuGiam == "PhanTram" && km.GiaTriGiam > 100)
                throw new Exception("Giá trị giảm theo phần trăm không được vượt quá 100%.");

            if (km.NgayBatDau >= km.NgayKetThuc)
                throw new Exception("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
        }
        public DataTable GetPromotionForProduct(string maSanPham, string loaiSanPham)
        {
            return khuyenMaiDAL.GetPromotionForProduct(maSanPham, loaiSanPham);
        }
    }
}
