using System;
using System.Drawing;
using System.Windows.Forms;
using TPVAXWinform_BLL;

namespace TPVAXWinform_GUI.Forms
{
    public partial class frmDoiMatKhau : Form
    {
        private TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
        private string maTK;

        public frmDoiMatKhau(string maTK)
        {
            InitializeComponent();
            this.maTK = maTK;
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            txtMatKhauCu.Focus();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            try
            {
                string matKhauCu = txtMatKhauCu.Text;
                string matKhauMoi = txtMatKhauMoi.Text;
                string xacNhanMatKhau = txtXacNhanMatKhau.Text;

                // Validate
                if (string.IsNullOrWhiteSpace(matKhauCu))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauCu.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(matKhauMoi))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo",
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

                if (matKhauMoi == matKhauCu)
                {
                    MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauMoi.Focus();
                    return;
                }

                // Gọi BLL để đổi mật khẩu
                taiKhoanBLL.ChangePassword(maTK, matKhauCu, matKhauMoi);

                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            char passChar = chkHienMatKhau.Checked ? '\0' : '●';
            txtMatKhauCu.PasswordChar = passChar;
            txtMatKhauMoi.PasswordChar = passChar;
            txtXacNhanMatKhau.PasswordChar = passChar;
        }
    }
}