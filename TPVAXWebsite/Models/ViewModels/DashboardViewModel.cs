using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPVAXWebsite.Models.Domain;

namespace TPVAXWebsite.Models.ViewModels
{
    public class DashboardViewModel
    {
        public KhachHang KhachHang { get; set; }
        public List<HoSoTiemChung> HoSoTiemChungs { get; set; }
        public int SoMuiHoanThanh { get; set; }
        public List<LichTiem> LichHenSapToi { get; set; }
        public List<LichTiem> LichTiems { get; set; }
        // ✅ Đã hủy — thêm dòng này
        public List<LichTiem> LichDaHuy { get; set; } = new List<LichTiem>();


    }


}