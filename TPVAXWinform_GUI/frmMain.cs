using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPVAXWinform_GUI;
using TPVAXWinform_GUI.UserControls;

namespace TPVAXWinform
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            // Cải thiện chất lượng rendering
            SetHighQualityRendering();

            // Setup button events
            SetupMenuButtons();

            // Show dashboard by default
            ShowDashboard();
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
            button3.Click += (s, e) => MessageBox.Show("Vaccine - Đang phát triển", "Thông báo");
            button4.Click += (s, e) => MessageBox.Show("Nhà cung cấp - Đang phát triển", "Thông báo");
            button5.Click += (s, e) => MessageBox.Show("Nhân viên - Đang phát triển", "Thông báo");
            button6.Click += (s, e) => ShowHoaDonControl();
            button7.Click += (s, e) => MessageBox.Show("Khuyến mãi - Đang phát triển", "Thông báo");
            button8.Click += (s, e) => ShowDashboard();

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
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = false;
            hoaDonControl1.Visible = false;

            // Bring dashboard to front
            bangDieuKhienControl1.BringToFront();

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
            hoSoTiemChungControl1.Visible = true;
            lichTiemControl1.Visible = false;
            hoaDonControl1.Visible = false;

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
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = true;
            hoaDonControl1.Visible = false;

            // Bring Lich Tiem control to front
            lichTiemControl1.BringToFront();

            // Refresh data để load lại dữ liệu mới nhất
            lichTiemControl1.RefreshData();

            // Reset button styles
            ResetMenuButtons();
            button2.BackColor = Color.FromArgb(52, 152, 219); // Highlight
        }

        private void ShowHoaDonControl()
        {
            // Đóng tất cả các form con
            CloseAllChildForms();

            // Hide all user controls
            bangDieuKhienControl1.Visible = false;
            hoSoTiemChungControl1.Visible = false;
            lichTiemControl1.Visible = false;
            hoaDonControl1.Visible = true;

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

        private void main_Load(object sender, EventArgs e)
        {
            // Dashboard control sẽ tự load khi được khởi tạo
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

    }
}
