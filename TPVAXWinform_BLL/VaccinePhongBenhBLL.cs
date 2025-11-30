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
    public class VaccinePhongBenhBLL
    {
        private readonly VaccinePhongBenhDAL _dal = new VaccinePhongBenhDAL();

        /// <summary>
        /// L?y t?t c? b?nh mà m?t vaccine phòng ???c
        /// </summary>
        public DataTable GetBenhByMaVC(string maVC)
        {
            return _dal.GetBenhByMaVC(maVC);
        }

        /// <summary>
        /// Thêm m?t b?nh cho vaccine
        /// </summary>
        public void Insert(VaccinePhongBenhDTO vpb)
        {
            _dal.Insert(vpb);
        }

        /// <summary>
        /// Xóa t?t c? b?nh c?a m?t vaccine (dùng khi c?p nh?t)
        /// </summary>
        public void DeleteByMaVC(string maVC)
        {
            _dal.DeleteByMaVC(maVC);
        }

        /// <summary>
        /// C?p nh?t danh sách b?nh cho vaccine
        /// (Xóa h?t r?i thêm l?i)
        /// </summary>
        public void UpdateBenhChoVaccine(string maVC, List<string> danhSachMaLoaiBenh)
        {
            try
            {
                // B??c 1: Xóa t?t c? b?nh c?
                _dal.DeleteByMaVC(maVC);

                // B??c 2: Thêm danh sách b?nh m?i
                if (danhSachMaLoaiBenh != null && danhSachMaLoaiBenh.Count > 0)
                {
                    _dal.InsertMultiple(maVC, danhSachMaLoaiBenh);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("L?i khi c?p nh?t b?nh cho vaccine: " + ex.Message);
            }
        }

        /// <summary>
        /// Thêm nhi?u b?nh cho m?t vaccine
        /// </summary>
        public void InsertMultiple(string maVC, List<string> danhSachMaLoaiBenh)
        {
            _dal.InsertMultiple(maVC, danhSachMaLoaiBenh);
        }
    }
}
