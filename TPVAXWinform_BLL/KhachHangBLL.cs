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
    public class KhachHangBLL
    {
        KhachHangDAL dal = new KhachHangDAL();
        TaoMaTuDong autoGen = new TaoMaTuDong();
        public DataTable GetData()
        {
            return dal.GetData();
        }
        public DataTable GetDataWithHoTenAndCCCD()
        {
            return dal.GetDataWithHoTenAndCCCD();
        }
        public DataTable GetDataByCCCD(string CCCD)
        {
            return dal.GetDataByCCCD(CCCD);
        }
        public bool IsKHExists(string CCCD)
        {
            return dal.IsKHExists(CCCD);
        }
        public bool IsSoDTExists(string soDT)
        {
            return dal.IsSoDTExists(soDT);
        }
        public bool IsLinkedHSTCBanThan(string CCCD)
        {
            return dal.IsLinkedHSTCBanThan(CCCD);
        }
        public void Insert(KhachHangDTO newKH)
        {
            dal.Insert(newKH);
        }
        public string CreateMaKH(string CCCD)
        {
            return autoGen.GenMaKH();
        }
        public void Edit(KhachHangDTO khachHang)
        {
            dal.Edit(khachHang);
        }
    }
}
