using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI
{
    public partial class frmThemNV : Form
    {
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();
        private TaiKhoanBLL taikhoanBLL = new TaiKhoanBLL();
        string[] gioiTinhOptions = { "Nam", "Nữ", "Khác" };


        private const string REGEX_HOTEN = @"^[\p{L}\s']+$";
        private const string REGEX_SODT = @"^0\d{9}$";
        private const string REGEX_CCCD = @"^\d{12}$";
        private const string REGEX_EMAIL = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        private const string REGEX_DIACHI = @"^[\p{L}\d\s.,/-]+$";

        string newMaTK = "";    
        public frmThemNV()
        {
            InitializeComponent();
            this.Load += FrmThemNV_Load;
        }

        private void FrmThemNV_Load(object sender, EventArgs e)
        {
            cboGioiTinh.DataSource = gioiTinhOptions;

            // --- SỬA: NẠP TỪ ROLEMANAGER ---
            // Dùng BindingSource để nạp Dictionary
            cboChucVu.DataSource = new BindingSource(RoleManager.ChucVuOptions, null);
            cboChucVu.DisplayMember = "Value"; // Hiển thị tên ("Quản Lý"...)
            cboChucVu.ValueMember = "Key";     // Lưu giá trị ID (1, 2...)
            cboChucVu.SelectedIndex = 0; // Chọn mặc định mục đầu tiên
            // --- KẾT THÚC SỬA ---

            dtpNgaySinh.Value = DateTime.Now.AddYears(-20);
            dtpNgayVaoLam.Value = DateTime.Now;

            btnAdd.Click += BtnAdd_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation (Giữ nguyên code của bạn)
                errorProvider1.Clear();
                bool valid = true;

                if (string.IsNullOrWhiteSpace(txtHoTen.Text) || !Regex.IsMatch(txtHoTen.Text.Trim(), REGEX_HOTEN))
                {
                    errorProvider1.SetError(txtHoTen, "Họ tên không hợp lệ.");
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(txtCCCD.Text) || !Regex.IsMatch(txtCCCD.Text.Trim(), REGEX_CCCD))
                {
                    errorProvider1.SetError(txtCCCD, "CCCD phải là 12 số.");
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(txtSoDT.Text) || !Regex.IsMatch(txtSoDT.Text.Trim(), REGEX_SODT))
                {
                    errorProvider1.SetError(txtSoDT, "Số điện thoại phải là 10 số, bắt đầu bằng 0.");
                    valid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text.Trim(), REGEX_EMAIL))
                {
                    errorProvider1.SetError(txtEmail, "Email không hợp lệ.");
                    valid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtDiaChi.Text) && !Regex.IsMatch(txtDiaChi.Text.Trim(), REGEX_DIACHI))
                {
                    errorProvider1.SetError(txtDiaChi, "Địa chỉ chứa ký tự không hợp lệ.");
                    valid = false;
                }

                if (!valid) return;

                string MaTK= "TK" + nhanVienBLL.CreateNewMaNV().Substring(2);
                newMaTK = MaTK;
                string MatKhau = "123456Aa@";
                taikhoanBLL.CreateTaiKhoan(MaTK, MatKhau);

                // Tạo DTO
                NhanVienDTO newNV = new NhanVienDTO
                {
                    MaNV = nhanVienBLL.CreateNewMaNV(),
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                    NgaySinh = dtpNgaySinh.Value,
                    CCCD = txtCCCD.Text.Trim(),
                    NgayVaoLam = dtpNgayVaoLam.Value,

                    // --- SỬA: LẤY ID TỪ SELECTEDVALUE ---
                    ChucVu = (int)cboChucVu.SelectedValue,
                    // --- KẾT THÚC SỬA ---

                    TrangThai = "1", // Đang hoạt động
                    SoDT = txtSoDT.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    MaTK = MaTK
                };

                nhanVienBLL.Insert(newNV);

                MessageBox.Show("Thêm nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                taikhoanBLL.Delete(newMaTK);
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}