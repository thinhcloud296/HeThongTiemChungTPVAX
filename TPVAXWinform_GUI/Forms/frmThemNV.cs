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
string[] gioiTinhOptions = { "Nam", "N?", "Khác" };
        string[] chucVuOptions = { "Qu?n lý", "Nhân viên" };

        private const string REGEX_HOTEN = @"^[\p{L}\s']+$";
 private const string REGEX_SODT = @"^0\d{9}$";
        private const string REGEX_CCCD = @"^\d{12}$";
  private const string REGEX_EMAIL = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
   private const string REGEX_DIACHI = @"^[\p{L}\d\s.,/-]+$";

        public frmThemNV()
        {
        InitializeComponent();
            this.Load += FrmThemNV_Load;
        }

        private void FrmThemNV_Load(object sender, EventArgs e)
        {
   cboGioiTinh.DataSource = gioiTinhOptions;
 cboChucVu.DataSource = chucVuOptions;
     dtpNgaySinh.Value = DateTime.Now.AddYears(-20);
dtpNgayVaoLam.Value = DateTime.Now;

    btnAdd.Click += BtnAdd_Click;
  btnCancel.Click += BtnCancel_Click;
     }

        private void BtnAdd_Click(object sender, EventArgs e)
      {
            try
            {
       // Validation
           errorProvider1.Clear();
    bool valid = true;

    if (string.IsNullOrWhiteSpace(txtHoTen.Text) || !Regex.IsMatch(txtHoTen.Text.Trim(), REGEX_HOTEN))
        {
    errorProvider1.SetError(txtHoTen, "H? tên không h?p l?.");
           valid = false;
    }

       if (string.IsNullOrWhiteSpace(txtCCCD.Text) || !Regex.IsMatch(txtCCCD.Text.Trim(), REGEX_CCCD))
          {
         errorProvider1.SetError(txtCCCD, "CCCD ph?i là 12 s?.");
valid = false;
       }

    if (string.IsNullOrWhiteSpace(txtSoDT.Text) || !Regex.IsMatch(txtSoDT.Text.Trim(), REGEX_SODT))
       {
        errorProvider1.SetError(txtSoDT, "S? ?i?n tho?i ph?i là 10 s?, b?t ??u b?ng 0.");
         valid = false;
    }

     if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text.Trim(), REGEX_EMAIL))
      {
             errorProvider1.SetError(txtEmail, "Email không h?p l?.");
          valid = false;
           }

          if (!string.IsNullOrWhiteSpace(txtDiaChi.Text) && !Regex.IsMatch(txtDiaChi.Text.Trim(), REGEX_DIACHI))
     {
     errorProvider1.SetError(txtDiaChi, "??a ch? ch?a ký t? không h?p l?.");
        valid = false;
         }

    if (!valid) return;

            // T?o DTO
                NhanVienDTO newNV = new NhanVienDTO
                {
             MaNV = nhanVienBLL.CreateNewMaNV(),
   HoTen = txtHoTen.Text.Trim(),
      GioiTinh = cboGioiTinh.SelectedItem.ToString(),
        NgaySinh = dtpNgaySinh.Value,
        CCCD = txtCCCD.Text.Trim(),
        NgayVaoLam = dtpNgayVaoLam.Value,
 ChucVu = cboChucVu.SelectedIndex + 1, // 1: Qu?n lý, 2: Nhân viên
            TrangThai = "1", // ?ang ho?t ??ng
  SoDT = txtSoDT.Text.Trim(),
  DiaChi = txtDiaChi.Text.Trim(),
       Email = txtEmail.Text.Trim(),
         MaTK = null
   };

           nhanVienBLL.Insert(newNV);

  MessageBox.Show("Thêm nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
  this.DialogResult = DialogResult.OK;
       this.Close();
       }
            catch (Exception ex)
         {
         MessageBox.Show("L?i khi thêm nhân viên: " + ex.Message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
        this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
