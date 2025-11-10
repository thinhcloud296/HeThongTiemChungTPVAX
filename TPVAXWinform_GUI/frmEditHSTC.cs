using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI
{
    public partial class frmEditHSTC : Form
    {
        private HoSoTiemChungBLL _hsctBLL = new HoSoTiemChungBLL();
    private HoSoTiemChungDTO _currentHSTC;
        private string _maHSTC;

        public frmEditHSTC()
        {
          InitializeComponent();
   }

     private void frmEditHSTC_Load(object sender, EventArgs e)
        {
    ApplyStyles();
   CenterFormOnScreen();
        }

     /// <summary>
        /// Tải dữ liệu hồ sơ theo MaHSTC
        /// </summary>
        public void LoadHoSoTiemChungData(string maHSTC)
 {
       try
            {
      _maHSTC = maHSTC;
  
          // Lấy dữ liệu từ BLL
      DataTable dt = _hsctBLL.GetData();
           
 if (dt != null && dt.Rows.Count > 0)
          {
        // Tìm dòng có MaHSTC khớp
             DataRow[] rows = dt.Select($"MaHSTC = '{maHSTC}'");
        
  if (rows.Length > 0)
   {
          DataRow row = rows[0];
             
    // Gán dữ liệu vào các control
            txtMaHSTC.Text = row["MaHSTC"]?.ToString() ?? "";
      txtHoTen.Text = row["HoTen"]?.ToString() ?? "";
             cboGioiTinh.Text = row["GioiTinh"]?.ToString() ?? "";
              
// Xử lý ngày sinh
   if (row["NgaySinh"] != DBNull.Value)
        {
            if (DateTime.TryParse(row["NgaySinh"].ToString(), out DateTime ngaySinh))
              {
     dtpNgaySinh.Value = ngaySinh;
              }
                  }
    
         txtCCCD.Text = row["CCCD"]?.ToString() ?? "";
     txtGhiChu.Text = row["GhiChu"]?.ToString() ?? "";
      chkTrangThai.Checked = Convert.ToBoolean(row["TrangThai"]);
    
          // Lưu object DTO hiện tại
_currentHSTC = new HoSoTiemChungDTO
 {
               MaHSTC = row["MaHSTC"]?.ToString() ?? "",
     HoTen = row["HoTen"]?.ToString() ?? "",
       GioiTinh = row["GioiTinh"]?.ToString() ?? "",
       NgaySinh = (DateTime)row["NgaySinh"],
       CCCD = row["CCCD"]?.ToString() ?? "",
    GhiChu = row["GhiChu"]?.ToString() ?? "",
              TrangThai = Convert.ToBoolean(row["TrangThai"])
         };
        }
       else
       {
            MessageBox.Show("Không tìm thấy hồ sơ tiêm chủng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               this.Close();
          }
     }
    }
          catch (Exception ex)
        {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
 }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
    // Kiểm tra thông tin bắt buộc
         if (string.IsNullOrEmpty(txtHoTen.Text.Trim()))
         {
         MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
     txtHoTen.Focus();
        return;
                }

        if (string.IsNullOrEmpty(cboGioiTinh.Text.Trim()))
                {
          MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
   cboGioiTinh.Focus();
                    return;
    }

     if (string.IsNullOrEmpty(txtCCCD.Text.Trim()))
  {
          MessageBox.Show("Vui lòng nhập CCCD!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtCCCD.Focus();
        return;
         }

    // Tạo object DTO với dữ liệu mới
    HoSoTiemChungDTO updatedHSTC = new HoSoTiemChungDTO
         {
          MaHSTC = txtMaHSTC.Text.Trim(),
              HoTen = txtHoTen.Text.Trim(),
      GioiTinh = cboGioiTinh.Text.Trim(),
    NgaySinh = dtpNgaySinh.Value,
  CCCD = txtCCCD.Text.Trim(),
         GhiChu = txtGhiChu.Text.Trim(),
             TrangThai = chkTrangThai.Checked
     };

       // Gọi BLL để cập nhật
       _hsctBLL.Edit(updatedHSTC);

   MessageBox.Show("Cập nhật thông tin hồ sơ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
       this.DialogResult = DialogResult.OK;
  this.Close();
       }
       catch (Exception ex)
  {
   MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
   }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
      }

        private void ApplyStyles()
  {
 // Áp dụng hover effect cho button Update
            btnUpdate.MouseEnter += (s, e) =>
 {
         btnUpdate.BackColor = Color.FromArgb(52, 152, 219);
       };
   btnUpdate.MouseLeave += (s, e) =>
            {
     btnUpdate.BackColor = Color.FromArgb(41, 128, 185);
          };

      // Áp dụng hover effect cho button Cancel
 btnCancel.MouseEnter += (s, e) =>
       {
     btnCancel.BackColor = Color.FromArgb(127, 140, 141);
     };
 btnCancel.MouseLeave += (s, e) =>
        {
       btnCancel.BackColor = Color.FromArgb(149, 165, 166);
     };
        }

        private void CenterFormOnScreen()
        {
            // Form sẽ được đặt ở giữa màn hình nhờ StartPosition = FormStartPosition.CenterScreen
// Tuy nhiên nếu muốn custom, có thể thêm logic ở đây
 }
    }
}
