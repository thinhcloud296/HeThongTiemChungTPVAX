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
                    string maTK = row["MaTK"].ToString().Trim();
                    int? chucVuId = null;
                    if (row["ChucVu"] != DBNull.Value)
                    {
                        chucVuId = Convert.ToInt32(row["ChucVu"]);
                    }

                    // ========== KIỂM TRA YÊU CẦU ĐỔI MẬT KHẨU LẦN ĐẦU ==========
                    bool yeuCauDoiMK = false;
                    if (dt.Columns.Contains("YeuCauDoiMK") && row["YeuCauDoiMK"] != DBNull.Value)
                    {
                        yeuCauDoiMK = Convert.ToInt32(row["YeuCauDoiMK"]) == 1;
                    }

                    if (yeuCauDoiMK)
                    {
                        // Bắt buộc đổi mật khẩu lần đầu
                        MessageBox.Show(
                            "Đây là lần đăng nhập đầu tiên của bạn.\n" +
                            "Vui lòng đổi mật khẩu để đảm bảo an toàn!",
                            "Yêu cầu đổi mật khẩu",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Mở form đổi mật khẩu bắt buộc
                        frmDoiMatKhauBatBuoc frmDoiMK = new frmDoiMatKhauBatBuoc(maTK, matKhau);
                        if (frmDoiMK.ShowDialog() == DialogResult.OK)
                        {
                            // Đổi mật khẩu thành công, tiếp tục đăng nhập
                            MessageBox.Show($"Đăng nhập thành công!\nXin chào {hoTen}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // User hủy đổi mật khẩu -> không cho đăng nhập
                            MessageBox.Show("Bạn phải đổi mật khẩu để tiếp tục sử dụng hệ thống!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Đăng nhập thành công!\nXin chào {hoTen}",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // LƯU THÔNG TIN PHIÊN
                    UserSession.MaNV = maNV;
                    UserSession.MaTK = maTK;
                    UserSession.HoTen = hoTen;
                    UserSession.Email = row["Email"].ToString();
                    UserSession.SoDT = row["SoDT"].ToString();
                    UserSession.ChucVu = chucVuId;

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

    // Class UserSession
    public static class UserSession
    {
        public static string MaNV { get; set; }
        public static string HoTen { get; set; }
        public static string Email { get; set; }
        public static string SoDT { get; set; }
        public static string MaTK { get; set; } 
        public static int? ChucVu { get; set; }
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