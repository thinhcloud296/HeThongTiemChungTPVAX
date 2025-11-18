using System;
using System.Data;
using System.Windows.Forms;
using TPVAXWinform_BLL; // <-- Thêm BLL

namespace TPVAXWinform_GUI.Forms
{
    public partial class frmDangNhap : Form
    {
        // THÊM: Khai báo BLL
        private TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();

        public frmDangNhap()
        {
            InitializeComponent();

            // THÊM: Gắn các sự kiện
            InitializeEvents();
        }

        // THÊM: Hàm gắn sự kiện
        private void InitializeEvents()
        {
            this.chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;

            // (Giả sử bạn có button tên 'btnDangNhap' và textbox 'txtTenDangNhap')
            this.btnDangNhap.Click += btnDangNhap_Click;
           
            // Cho phép nhấn Enter để đăng nhập
            this.txtMatKhau.KeyPress += TxtMatKhau_KeyPress;
            this.txtTenDangNhap.KeyPress += TxtMatKhau_KeyPress; // Thêm cho cả txtTenDangNhap
        }

        // THÊM: Hàm xử lý sự kiện Enter
        private void TxtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
                e.Handled = true; // Ngăn tiếng "beep"
            }
        }

        // THÊM: Hàm xử lý Đăng nhập
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                string maNV = txtTenDangNhap.Text.Trim();
                string matKhau = txtMatKhau.Text.Trim();

                if (string.IsNullOrWhiteSpace(maNV))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(matKhau))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return;
                }

                // Gọi BLL (BLL sẽ gọi DAL và xác thực bằng BCrypt)
                DataTable dt = taiKhoanBLL.Login(maNV, matKhau);

                if (dt.Rows.Count > 0)
                {
                    // Đăng nhập thành công
                    DataRow row = dt.Rows[0];
                    string hoTen = row["HoTen"].ToString();
                    string chucVu = row["ChucVu"].ToString();

                    MessageBox.Show($"Đăng nhập thành công!\nXin chào {hoTen}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // LƯU THÔNG TIN PHIÊN
                    UserSession.MaNV = maNV;
                    UserSession.MaTK = row["MaTK"].ToString(); // Lấy MaTK từ kết quả
                    UserSession.HoTen = hoTen;
                    UserSession.Email = row["Email"].ToString();
                    UserSession.SoDT = row["SoDT"].ToString();
                    UserSession.ChucVu = chucVu;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mã nhân viên hoặc mật khẩu không đúng!", "Lỗi đăng nhập",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // (Hàm này của bạn đã đúng)
        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienMatKhau.Checked)
            {
                txtMatKhau.PasswordChar = '\0';
            }
            else
            {
                txtMatKhau.PasswordChar = '●';
            }
        }
    }

    // THÊM: Class UserSession (Rất quan trọng)
    // (Bạn có thể đặt class này trong 1 file riêng, ví dụ: 'UserSession.cs')
    public static class UserSession
    {
        public static string MaNV { get; set; }
        public static string HoTen { get; set; }
        public static string Email { get; set; }
        public static string SoDT { get; set; }
        public static string ChucVu { get; set; }
        public static string MaTK { get; set; } // Dùng để đổi mật khẩu

        public static void Clear()
        {
            MaNV = null;
            HoTen = null;
            Email = null;
            SoDT = null;
            ChucVu = null;
            MaTK = null;
        }
    }
}