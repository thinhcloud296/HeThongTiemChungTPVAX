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
    public partial class frmEditKH : Form
    {
        private KhachHangBLL khachHangBLL = new KhachHangBLL();
        private KhachHangDTO currentKhachHang;
        string[] gioiTinhOptions = { "Nam", "Nữ", "Khác" };
        public frmEditKH()
        {
            InitializeComponent();
            this.Load += FrmEditKH_Load;
        }

        /// <summary>
        /// Hiển thị thông tin khách hàng dựa trên mã KH
        /// </summary>
        public void LoadKhachHangData(string maKH)
        {
            try
            {
                // Lấy dữ liệu khách hàng từ BLL
                DataTable dt = khachHangBLL.GetData();

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Tìm dòng có MaKH tương ứng
                    DataRow[] rows = dt.Select("MaKH = '" + maKH.Replace("'", "''") + "'");

                    if (rows.Length > 0)
                    {
                        DataRow row = rows[0];

                        // Gán dữ liệu vào các control
                        txtMaKH.Text = row["MaKH"]?.ToString() ?? "";
                        txtHoTen.Text = row["HoTen"]?.ToString() ?? "";
                        txtCCCD.Text = row["CCCD"]?.ToString() ?? "";

                        // Ngày sinh
                        if (row["NgaySinh"] != DBNull.Value && row["NgaySinh"] != null)
                        {
                            if (row["NgaySinh"] is DateTime dt_value)
                            {
                                dtpNgaySinh.Value = dt_value;
                            }
                            else if (DateTime.TryParse(row["NgaySinh"].ToString(), out DateTime parsedDate))
                            {
                                dtpNgaySinh.Value = parsedDate;
                            }
                        }

                        // Giới tính
                        string gioiTinh = row["GioiTinh"]?.ToString() ?? "";
                        if (!string.IsNullOrEmpty(gioiTinh))
                        {
                            if (cboGioiTinh.Items.Contains(gioiTinh))
                                cboGioiTinh.SelectedItem = gioiTinh;
                        }

                        txtDiaChi.Text = row["DiaChi"]?.ToString() ?? "";
                        txtSoDT.Text = row["SoDT"]?.ToString() ?? "";
                        txtEmail.Text = row["Email"]?.ToString() ?? "";

                        // Lưu đối tượng DTO để cập nhật
                        currentKhachHang = new KhachHangDTO
                        {
                            MaKH = txtMaKH.Text,
                            HoTen = txtHoTen.Text,
                            CCCD = txtCCCD.Text,
                            NgaySinh = dtpNgaySinh.Value,
                            GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "",
                            DiaChi = txtDiaChi.Text,
                            SoDT = txtSoDT.Text,
                            Email = txtEmail.Text
                        };
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng với mã: " + maKH, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmEditKH_Load(object sender, EventArgs e)
        {
            cboGioiTinh.DataSource= gioiTinhOptions;    
            // Thiết lập mặc định cho DateTimePicker
            dtpNgaySinh.Value = DateTime.Now;

            // Thiết lập button hành động
            btnUpdate.Click += BtnUpdate_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }

                if (cboGioiTinh.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboGioiTinh.Focus();
                    return;
                }
                // Cập nhật đối tượng DTO
                currentKhachHang.HoTen = txtHoTen.Text.Trim();
                currentKhachHang.CCCD = txtCCCD.Text.Trim();
                currentKhachHang.NgaySinh = dtpNgaySinh.Value;
                currentKhachHang.GioiTinh = cboGioiTinh.SelectedItem.ToString();
                currentKhachHang.DiaChi = txtDiaChi.Text.Trim();
                currentKhachHang.SoDT = txtSoDT.Text.Trim();
                currentKhachHang.Email = txtEmail.Text.Trim();

                // Gọi BLL để cập nhật dữ liệu
                khachHangBLL.Edit(currentKhachHang);

                MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
