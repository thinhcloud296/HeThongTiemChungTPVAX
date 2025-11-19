using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_GUI;
using TPVAXWinform_GUI.Forms;

namespace TPVAXWinform
{
    public partial class frmMain : Form
    {
        NhanVienBLL nvBLL = new NhanVienBLL();
        public frmMain()
        {
            InitializeComponent();
            SetHighQualityRendering();
            SetupMenuButtons();
            ShowDashboard();
        }
        private void main_Load(object sender, EventArgs e)
        {
            InitialInfoLogin(UserSession.MaNV, UserSession.HoTen, UserSession.ChucVu);
            bool roleQLNV = RoleManager.RoleQLNV();
            bool roleNVKho = RoleManager.RoleNVKho();
            bool roleNVTiepNhan = RoleManager.RoleNVTiepNhan();
            bool roleNVYTe = RoleManager.RoleNVYTe();
            bool roleNVThuNgan = RoleManager.RoleNVThuNgan();
            button1.Visible = roleNVTiepNhan || roleNVYTe;
            button2.Visible = roleNVTiepNhan || roleNVYTe;
            button3.Visible = roleNVKho || roleNVYTe;
            button4.Visible = roleNVKho;
            button5.Visible = roleQLNV;
            button6.Visible = roleNVTiepNhan || roleNVThuNgan;

        }
        public void InitialInfoLogin(string maNV, string hoTen, int? chucVu)
        {
            lbHoTenNVDangNhap.Text = hoTen;
            lbMaNVDangNhap.Text = maNV;
            lbChucVuNVDangNhap.Text = nvBLL.GetChucVuString(chucVu);
        } 
        private void SetHighQualityRendering()
        {
            // Bật double buffering để giảm flickering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
             ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // Cải thiện chất lượng text rendering cho tất cả controls
            SetTextRenderingForControls(this.Controls);
        }

        private void SetTextRenderingForControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                // Cải thiện rendering cho Labels
                if (control is Label label)
                {
                    label.UseCompatibleTextRendering = false;
                }

                // Cải thiện rendering cho Buttons
                if (control is Button button)
                {
                    button.UseCompatibleTextRendering = false;
                }

                // Đệ quy cho các control con
                if (control.HasChildren)
                {
                    SetTextRenderingForControls(control.Controls);
                }
            }
        }

        private void SetupMenuButtons()
        {
            // Wire up button clicks
            button1.Click += (s, e) => ShowHoSoTiemChungControl();
            button2.Click += (s, e) => ShowLichTiemControl();
            button3.Click += (s, e) => ShowVaccineControl();
            button4.Click += (s, e) => ShowPhieuNhapControl();
            button5.Click += (s, e) => ShowNhanVienControl();
            button6.Click += (s, e) => ShowHoaDonControl();
            button7.Click += (s, e) => MessageBox.Show("Khuyến mãi - Đang phát triển", "Thông báo");
            button8.Click += (s, e) => ShowThongKeControl();

            // Logo click để về Dashboard
            logo.Click += (s, e) => ShowDashboard();
        }

        private void CloseAllChildForms()
        {
            // Đóng tất cả các form con đang mở
            foreach (Form childForm in Application.OpenForms.Cast<Form>().ToList())
            {
                if (childForm != this && childForm.Owner == this)
                {
                    childForm.Close();
                }
            }
        }

        private void ShowDashboard()
        {
            // Đóng tất cả các form con
            CloseAllChildForms();

            // Hide all user controls
            bangDieuKhienControl1.Visible = true;
            thongKeControl1.Visible = false;
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = false;
            vaccineControl1.Visible = false;
            hoaDonControl1.Visible = false;
            nhanVienControl1.Visible = false;
            phieuNhapControl1.Visible = false;

            // Bring dashboard to front
            bangDieuKhienControl1.BringToFront();

            // Reset button styles
            ResetMenuButtons();
            // Không highlight button8 vì đây là dashboard cũ (BangDieuKhienControl)
        }

        private void ShowThongKeControl()
        {
            // Đóng tất cả các form con
            CloseAllChildForms();

            // Hide all user controls
            bangDieuKhienControl1.Visible = false;
            thongKeControl1.Visible = true;
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = false;
            vaccineControl1.Visible = false;
            hoaDonControl1.Visible = false;
            nhanVienControl1.Visible = false;
            phieuNhapControl1.Visible = false;

            // Bring Thong Ke control to front
            thongKeControl1.BringToFront();

            thongKeControl1.RefreshData();
            // TODO: Load data for ThongKeControl
            // LoadThongKeData();

            // Reset button styles
            ResetMenuButtons();
            button8.BackColor = Color.FromArgb(52, 152, 219); // Highlight
        }

        private void ShowHoSoTiemChungControl()
        {
            // Đóng tất cả các form con
            CloseAllChildForms();

            // Hide all user controls
            bangDieuKhienControl1.Visible = false;
            thongKeControl1.Visible = false;
            hoSoTiemChungControl1.Visible = true;
            lichTiemControl1.Visible = false;
            vaccineControl1.Visible = false;
            hoaDonControl1.Visible = false;
            nhanVienControl1.Visible = false;
            phieuNhapControl1.Visible = false;

            // Bring immunization record to front
            hoSoTiemChungControl1.BringToFront();

            // Refresh data if needed
            hoSoTiemChungControl1.RefreshData();

            // Reset button styles
            ResetMenuButtons();
            button1.BackColor = Color.FromArgb(52, 152, 219); // Highlight
        }

        private void ShowLichTiemControl()
        {
            // Đóng tất cả các form con
            CloseAllChildForms();

            // Hide all user controls
            bangDieuKhienControl1.Visible = false;
            thongKeControl1.Visible = false;
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = true;
            vaccineControl1.Visible = false;
            hoaDonControl1.Visible = false;
            nhanVienControl1.Visible = false;
            phieuNhapControl1.Visible = false;

            // Bring Lich Tiem control to front
            lichTiemControl1.BringToFront();

            // Refresh data để load lại dữ liệu mới nhất
            lichTiemControl1.RefreshData();

            // Reset button styles
            ResetMenuButtons();
            button2.BackColor = Color.FromArgb(52, 152, 219); // Highlight
        }

        private void ShowVaccineControl()
        {
            // Đóng tất cả các form con
            CloseAllChildForms();

            // Hide all user controls
            bangDieuKhienControl1.Visible = false;
            thongKeControl1.Visible = false;
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = false;
            vaccineControl1.Visible = true;
            hoaDonControl1.Visible = false;
            nhanVienControl1.Visible = false;
            phieuNhapControl1.Visible = false;

            // Bring Vaccine control to front
            vaccineControl1.BringToFront();

            vaccineControl1.RefreshData();
            // Reset button styles
            ResetMenuButtons();
            button3.BackColor = Color.FromArgb(52, 152, 219); // Highlight
        }

        private void ShowPhieuNhapControl()
        {
            // Đóng tất cả các form con
            CloseAllChildForms();

            // Hide all user controls
            bangDieuKhienControl1.Visible = false;
            thongKeControl1.Visible = false;
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = false;
            vaccineControl1.Visible = false;
            hoaDonControl1.Visible = false;
            nhanVienControl1.Visible = false;
            phieuNhapControl1.Visible = true;

            // Bring Phieu Nhap control to front
            phieuNhapControl1.BringToFront();

            phieuNhapControl1.RefreshData();
            // Reset button styles
            ResetMenuButtons();
            button4.BackColor = Color.FromArgb(52, 152, 219); // Highlight
        }

        private void ShowNhanVienControl()
        {
            // Đóng tất cả các form con
            CloseAllChildForms();

            // Hide all user controls
            bangDieuKhienControl1.Visible = false;
            thongKeControl1.Visible = false;
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = false;
            vaccineControl1.Visible = false;
            hoaDonControl1.Visible = false;
            nhanVienControl1.Visible = true;
            phieuNhapControl1.Visible = false;

            // Bring Nhan Vien control to front
            nhanVienControl1.BringToFront();

            // Refresh data để load lại dữ liệu mới nhất
            nhanVienControl1.RefreshData();

            // Reset button styles
            ResetMenuButtons();
            button5.BackColor = Color.FromArgb(52, 152, 219); // Highlight
        }

        private void ShowHoaDonControl()
        {
            // Đóng tất cả các form con
            CloseAllChildForms();

            // Hide all user controls
            bangDieuKhienControl1.Visible = false;
            thongKeControl1.Visible = false;
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = false;
            vaccineControl1.Visible = false;
            hoaDonControl1.Visible = true;
            nhanVienControl1.Visible = false;
            phieuNhapControl1.Visible = false;

            // Bring Hoa Don control to front
            hoaDonControl1.BringToFront();

            // Refresh data để load lại dữ liệu mới nhất
            hoaDonControl1.RefreshData();

            // Reset button styles
            ResetMenuButtons();
            button6.BackColor = Color.FromArgb(52, 152, 219); // Highlight
        }

        private void ResetMenuButtons()
        {
            // Reset all menu buttons to default color
            Color defaultColor = Color.FromArgb(41, 128, 185);
            button1.BackColor = defaultColor;
            button2.BackColor = defaultColor;
            button3.BackColor = defaultColor;
            button4.BackColor = defaultColor;
            button5.BackColor = defaultColor;
            button6.BackColor = defaultColor;
            button7.BackColor = defaultColor;
            button8.BackColor = defaultColor;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // 2. Nếu người dùng chọn "Yes"
            if (confirm == DialogResult.Yes)
            {
                // 3. Xóa thông tin phiên làm việc hiện tại
                UserSession.Clear();

                // 4. Khởi động lại ứng dụng
                Application.Restart();
            }
        }
    }
}
