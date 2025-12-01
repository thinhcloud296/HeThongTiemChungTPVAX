using System;
using System.Windows.Forms;
using TPVAXWinform_BLL;

namespace TPVAXWinform_GUI.Forms
{
    /// <summary>
    /// Form đổi mật khẩu bắt buộc khi đăng nhập lần đầu
    /// </summary>
    public partial class frmDoiMatKhauBatBuoc : Form
    {
        private readonly TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        private readonly string _maTK;
        private readonly string _currentPassword;

        public frmDoiMatKhauBatBuoc(string maTK, string currentPassword)
        {
            InitializeComponent();
            _maTK = maTK;
            _currentPassword = currentPassword;

            // Không cho phép đóng form bằng nút X
            this.ControlBox = false;
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            try
            {
                string matKhauMoi = txtMatKhauMoi.Text.Trim();
                string xacNhanMatKhau = txtXacNhanMatKhau.Text.Trim();

                // Validation
                if (string.IsNullOrWhiteSpace(matKhauMoi))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauMoi.Focus();
                    return;
                }

                if (matKhauMoi.Length < 6)
                {
                    MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauMoi.Focus();
                    return;
                }

                if (matKhauMoi != xacNhanMatKhau)
                {
                    MessageBox.Show("Xác nhận mật khẩu không khớp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtXacNhanMatKhau.Focus();
                    return;
                }

                // Không cho phép dùng lại mật khẩu cũ
                if (matKhauMoi == _currentPassword)
                {
                    MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauMoi.Clear();
                    txtXacNhanMatKhau.Clear();
                    txtMatKhauMoi.Focus();
                    return;
                }

                // Gọi BLL để đổi mật khẩu và xóa cờ YeuCauDoiMK
                _taiKhoanBLL.ChangePasswordFirstTime(_maTK, matKhauMoi);

                MessageBox.Show("Đổi mật khẩu thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn phải đổi mật khẩu để tiếp tục sử dụng hệ thống.\n" +
                "Bạn có chắc muốn thoát?",
                "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            char passwordChar = chkHienMatKhau.Checked ? '\0' : '●';
            txtMatKhauMoi.PasswordChar = passwordChar;
            txtXacNhanMatKhau.PasswordChar = passwordChar;
        }
    }
}
