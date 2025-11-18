using System.Collections.Generic;
using TPVAXWinform_GUI.Forms;

namespace TPVAXWinform_GUI
{
    /// <summary>
    /// Lớp static tập trung toàn bộ logic phân quyền (Authorization).
    /// </summary>
    public static class RoleManager
    {
        // --- BƯỚC 1: ĐỊNH NGHĨA ID CÁC CHỨC VỤ ---
        // (Bạn hãy thay đổi các số này cho khớp với CSDL)
        private const int QUAN_LY = 1;
        private const int TIEP_NHAN = 2;
        private const int KHO = 3;
        private const int Y_TE = 4; // (Bác sĩ / Y tá)
        private const int THU_NGAN= 5; // (Bác sĩ / Y tá)

        // (Bạn cũng có thể lưu Dictionary ở đây)
        public static readonly Dictionary<int, string> ChucVuOptions = new Dictionary<int, string>
        {
            { 1, "Quản Lý" },
            { 2, "Nhân Viên Tiếp Nhận" },
            { 3, "Nhân Viên Kho" },
            { 4, "Nhân Viên Y Tế" },
            { 5, "Nhân Viên Thu Ngân" }
        };

        // --- BƯỚC 2: ĐỊNH NGHĨA CÁC QUYỀN HẠN ---

        /// <summary>
        /// Quyền xem/sửa/xóa Nhân Viên và Tài Khoản.
        /// </summary>
        public static bool RoleQLNV()
        {
            // Chỉ Quản Lý
            return UserSession.ChucVu == QUAN_LY;
        }

        /// <summary>
        /// Quyền truy cập module Nhập Kho (Phiếu Nhập)
        /// </summary>
        public static bool RoleNVKho()
        {
            // Quản Lý hoặc Nhân Viên Kho
            return UserSession.ChucVu == QUAN_LY || UserSession.ChucVu == KHO;
        }

        /// <summary>
        /// Quyền tạo Hóa đơn và Lịch hẹn (đón tiếp bệnh nhân)
        /// </summary>
        public static bool RoleNVTiepNhan()
        {
            // Quản Lý hoặc Nhân Viên Tiếp Nhận
            return UserSession.ChucVu == QUAN_LY || UserSession.ChucVu == TIEP_NHAN;
        }

        /// <summary>
        /// Quyền Bác sĩ tư vấn (Tạo chỉ định tiêm - frmThemMuiTiem)
        /// (Giả sử Bác sĩ (Y Tế) và Tiếp Nhận đều có thể làm)
        /// </summary>
        public static bool RoleNVYTe()
        {
            return UserSession.ChucVu == QUAN_LY ||
                   UserSession.ChucVu == Y_TE;
        }
        public static bool RoleNVThuNgan()
        {
            return UserSession.ChucVu == QUAN_LY ||
                   UserSession.ChucVu == THU_NGAN;
        }

        /// <summary>
        /// Quyền xem Thống kê, Báo cáo
        /// </summary>
        public static bool CanViewReports()
        {
            // Chỉ Quản Lý
            return UserSession.ChucVu == QUAN_LY;
        }
    }
}