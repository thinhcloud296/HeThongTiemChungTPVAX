using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_BLL
{
    public class ChiTietKhuyenMaiBLL
    {
        private ChiTietKhuyenMaiDAL chiTietKhuyenMaiDAL = new ChiTietKhuyenMaiDAL();

        public string CreateNewMaCTKM()
        {
            return chiTietKhuyenMaiDAL.CreateNewMaCTKM();
        }

        public void InsertDetail(ChiTietKhuyenMaiDTO ct)
        {
            ValidateChiTiet(ct);
            chiTietKhuyenMaiDAL.InsertDetail(ct);
        }

        public void DeleteByMaKM(string maKM)
        {
            chiTietKhuyenMaiDAL.DeleteByMaKM(maKM);
        }

        private void ValidateChiTiet(ChiTietKhuyenMaiDTO ct)
        {
            if (string.IsNullOrWhiteSpace(ct.MaKM))
                throw new Exception("Mã khuyến mãi không được để trống.");

            if (string.IsNullOrWhiteSpace(ct.LoaiSanPham))
                throw new Exception("Loại sản phẩm không được để trống.");

            if (string.IsNullOrWhiteSpace(ct.MaSanPham))
                throw new Exception("Mã sản phẩm không được để trống.");
        }
    }
}
