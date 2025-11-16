using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI
{
    public partial class frmEditNV : Form
    {
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();
        private NhanVienDTO currentNhanVien;
        string[] gioiTinhOptions = { "Nam", "Nữ", "Khác" };
        private readonly Dictionary<int, string> chucVuOptions = new Dictionary<int, string>
        {
            { 1, "Quản lý" },
            { 2, "Nhân viên y tế" },
            { 3, "Nhân viên tiếp nhận" },
            { 4, "Nhân viên kho" }
        };
        string[] trangThaiOptions = { "Ngừng hoạt động", "Đang hoạt động" };

        private const string REGEX_HOTEN = @"^[\p{L}\s']+$";
        private const string REGEX_SODT = @"^0\d{9}$";
        private const string REGEX_CCCD = @"^\d{12}$";
        private const string REGEX_EMAIL = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        private const string REGEX_DIACHI = @"^[\p{L}\d\s.,/-]+$";

        public frmEditNV()
        {
            InitializeComponent();
            cboGioiTinh.DataSource = gioiTinhOptions;
            cboTrangThai.DataSource = trangThaiOptions;
            cboChucVu.DataSource = new BindingSource(chucVuOptions, null);
            cboChucVu.ValueMember = "Key";
            cboChucVu.DisplayMember = "Value";
            Load += FrmEditNV_Load;
        }

        public void LoadNhanVienData(string maNV)
        {
            try
            {
                DataTable dt = nhanVienBLL.GetNhanVienByMaNV(maNV);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int chucVu = 0;
                    DataRow row = dt.Rows[0];

                    txtMaNV.Text = row["MaNV"]?.ToString() ?? "";
                    txtHoTen.Text = row["HoTen"]?.ToString() ?? "";
                    txtCCCD.Text = row["CCCD"]?.ToString() ?? "";

                    if (row["NgaySinh"] != DBNull.Value && row["NgaySinh"] != null)
                    {
                        if (row["NgaySinh"] is DateTime dt_value)
                            dtpNgaySinh.Value = dt_value;
                        else if (DateTime.TryParse(row["NgaySinh"].ToString(), out DateTime parsedDate))
                            dtpNgaySinh.Value = parsedDate;
                    }

                    if (row["NgayVaoLam"] != DBNull.Value && row["NgayVaoLam"] != null)
                    {
                        if (row["NgayVaoLam"] is DateTime dt_value)
                            dtpNgayVaoLam.Value = dt_value;
                        else if (DateTime.TryParse(row["NgayVaoLam"].ToString(), out DateTime parsedDate))
                            dtpNgayVaoLam.Value = parsedDate;
                    }

                    string gioiTinh = row["GioiTinh"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(gioiTinh) && cboGioiTinh.Items.Contains(gioiTinh))
                        cboGioiTinh.SelectedItem = gioiTinh;


                    if (row["ChucVu"] != DBNull.Value && int.TryParse(row["ChucVu"].ToString(), out chucVu))
                    {
                        cboChucVu.SelectedValue = chucVu;
                    }

                    string trangThai = row["TrangThai"]?.ToString().Trim() ?? "1";
                    cboTrangThai.SelectedIndex = trangThai == "1" ? 1 : 0;

                    txtDiaChi.Text = row["DiaChi"]?.ToString() ?? "";
                    txtSoDT.Text = row["SoDT"]?.ToString() ?? "";
                    txtEmail.Text = row["Email"]?.ToString() ?? "";

                    currentNhanVien = new NhanVienDTO
                    {
                        MaNV = txtMaNV.Text,
                        HoTen = txtHoTen.Text,
                        CCCD = txtCCCD.Text,
                        NgaySinh = dtpNgaySinh.Value,
                        GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "",
                        NgayVaoLam = dtpNgayVaoLam.Value,
                        ChucVu = chucVu,
                        TrangThai = cboTrangThai.SelectedIndex.ToString(),
                        DiaChi = txtDiaChi.Text,
                        SoDT = txtSoDT.Text,
                        Email = txtEmail.Text
                    };
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên mã: " + maNV, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dử liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmEditNV_Load(object sender, EventArgs e)
        {
            dtpNgaySinh.Value = DateTime.Now;
            dtpNgayVaoLam.Value = DateTime.Now;

            btnUpdate.Click += BtnUpdate_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
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
                    errorProvider1.SetError(txtSoDT, "Số ĐT phải là 10 số, bắt Đầu từ 0.");
                    valid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text.Trim(), REGEX_EMAIL))
                {
                    errorProvider1.SetError(txtEmail, "Email không hợp lệ.");
                    valid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtDiaChi.Text) && !Regex.IsMatch(txtDiaChi.Text.Trim(), REGEX_DIACHI))
                {
                    errorProvider1.SetError(txtDiaChi, "Địa chỉ không hợp lệ.");
                    valid = false;
                }

                if (!valid) return;

                currentNhanVien.HoTen = txtHoTen.Text.Trim();
                currentNhanVien.CCCD = txtCCCD.Text.Trim();
                currentNhanVien.NgaySinh = dtpNgaySinh.Value;
                currentNhanVien.GioiTinh = cboGioiTinh.SelectedItem.ToString();
                currentNhanVien.NgayVaoLam = dtpNgayVaoLam.Value;
                currentNhanVien.ChucVu = (int)cboChucVu.SelectedValue;
                currentNhanVien.TrangThai = cboTrangThai.SelectedIndex.ToString();
                currentNhanVien.DiaChi = txtDiaChi.Text.Trim();
                currentNhanVien.SoDT = txtSoDT.Text.Trim();
                currentNhanVien.Email = txtEmail.Text.Trim();

                nhanVienBLL.Edit(currentNhanVien);

                MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
