using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks; // Bắt buộc có cái này
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;
// Thêm thư viện Service chứa EmailService
using TPVAXWinform_BLL.Services;

namespace TPVAXWinform_GUI
{
    public partial class frmThemNV : Form
    {
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();
        private TaiKhoanBLL taikhoanBLL = new TaiKhoanBLL();
        // Khởi tạo Email Service
        private EmailService _emailService = new EmailService();

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
            cboChucVu.DataSource = new BindingSource(RoleManager.ChucVuOptions, null);
            cboChucVu.DisplayMember = "Value";
            cboChucVu.ValueMember = "Key";
            cboChucVu.SelectedIndex = 0;
            // --- KẾT THÚC SỬA ---

            dtpNgaySinh.Value = DateTime.Now.AddYears(-20);
            dtpNgayVaoLam.Value = DateTime.Now;

            // Gán sự kiện click
            btnAdd.Click += BtnAdd_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        // ==========================================================
        // QUAN TRỌNG: Thêm từ khóa 'async' vào đây
        // ==========================================================
        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // ================== VALIDATION ==================
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

                // ================== XỬ LÝ DATABASE ==================

                // 1. Lấy mã NV mới (Chỉ gọi 1 lần để đồng bộ)
                string MaNV = nhanVienBLL.CreateNewMaNV();
                // MaTK format: "TAIK" + 6 số cuối của MaNV
                // VD: MaNV = "NVIE000001" => MaTK = "TAIK000001" (10 ký tự)
                string MaTK = "TAIK" + MaNV.Substring(4);
                newMaTK = MaTK; // Lưu lại để rollback nếu lỗi

                // 2. Tạo mật khẩu ngẫu nhiên
                string randomPass = GenerateRandomPassword(6);

                // 3. Tạo Tài khoản trong DB (PHẢI THÀNH CÔNG TRƯỚC KHI TẠO NHÂN VIÊN)
                // SỬA: Bỏ try-catch bên trong, để exception được throw ra ngoài
                taikhoanBLL.CreateTaiKhoan(MaTK, randomPass);

                // 4. Tạo DTO Nhân viên
                NhanVienDTO newNV = new NhanVienDTO
                {
                    MaNV = MaNV, // Dùng biến đã lấy ở trên
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                    NgaySinh = dtpNgaySinh.Value,
                    CCCD = txtCCCD.Text.Trim(),
                    NgayVaoLam = dtpNgayVaoLam.Value,
                    ChucVu = (int)cboChucVu.SelectedValue,
                    TrangThai = "1",
                    SoDT = txtSoDT.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    MaTK = MaTK
                };

                // 5. Insert Nhân viên vào DB
                nhanVienBLL.Insert(newNV);

                // ================== GỬI EMAIL (PHẦN MỚI) ==================

                // Khóa nút để tránh bấm nhiều lần
                btnAdd.Enabled = false;
                btnAdd.Text = "Đang gửi mail...";

                string userEmail = txtEmail.Text.Trim();
                string userName = txtHoTen.Text.Trim();

                try
                {
                    // Gọi hàm gửi mail bất đồng bộ (await)
                    // Lưu ý: Đảm bảo class EmailService đã có trong project
                    await _emailService.SendAccountInfoAsync(userEmail, userName, randomPass);

                    // Thông báo thành công trọn vẹn
                    MessageBox.Show(
                        $"Thêm nhân viên thành công!\nĐã gửi mật khẩu về email: {userEmail}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception emailEx)
                {
                    // ==========================================================
                    // LOGIC MỚI: NẾU GỬI MAIL THẤT BẠI -> ĐỔI VỀ MẬT KHẨU MẶC ĐỊNH
                    // ==========================================================

                    string matKhauMacDinh = "123456Aa@";

                    try
                    {
                        // Gọi BLL để update lại mật khẩu trong Database
                        // Lưu ý: Bạn cần đảm bảo hàm UpdateMatKhau tồn tại trong BLL
                        taikhoanBLL.UpdateMatKhau(MaTK, matKhauMacDinh);

                        MessageBox.Show(
                            $"Thêm nhân viên thành công nhưng GỬI MAIL THẤT BẠI.\n\n" +
                            $"Lỗi email: {emailEx.Message}\n\n" +
                            $"👉 Hệ thống đã đặt lại về mật khẩu mặc định: {matKhauMacDinh}",
                            "Cảnh báo - Dùng mật khẩu mặc định",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception)
                    {
                        // Trường hợp cực hiếm: Gửi mail lỗi VÀ Update lại DB cũng lỗi
                        MessageBox.Show($"Lỗi kép nghiêm trọng: Gửi mail thất bại và không thể reset mật khẩu. \nMật khẩu hiện tại vẫn là: {randomPass}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Đóng form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // Nếu lỗi DB thì xóa tài khoản rác (Rollback)
                if (!string.IsNullOrEmpty(newMaTK))
                {
                    taikhoanBLL.Delete(newMaTK);
                }

                MessageBox.Show("Lỗi khi thêm nhân viên (DB): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Mở lại nút bấm nếu lỗi
                btnAdd.Enabled = true;
                btnAdd.Text = "Thêm";
            }
        }
        

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Hàm tạo mật khẩu ngẫu nhiên đơn giản (nằm ngay trong form này cho tiện)
        private string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}