using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPVAXWinform_DAL;
using System.Data;
using TPVAXWinform_DTO;
namespace TPVAXWinform_BLL
{
    public class NhanVienBLL
    {
        private readonly NhanVienDAL nhanVienDAL = new NhanVienDAL();
        public DataTable GetData()
        {
            return nhanVienDAL.GetData();
        }
        public DataTable GetDSNVKho()
        {
            return nhanVienDAL.GetDSNVKho();
        }
        public DataTable GetNhanVienByMaNV(string maNV)
        {
            return nhanVienDAL.GetNhanVienByMaNV(maNV);
        }
        public string CreateNewMaNV()
        {
            return nhanVienDAL.CreateNewMaNV();
        }

        public void Insert(NhanVienDTO nv)
        {
            nhanVienDAL.Insert(nv);
        }

        public void Edit(NhanVienDTO nv)
        {
            nhanVienDAL.Edit(nv);
        }
        public string GetChucVuString(int? chucVu)
        {
            return nhanVienDAL.GetChucVuString(chucVu);
        }
    }
}
