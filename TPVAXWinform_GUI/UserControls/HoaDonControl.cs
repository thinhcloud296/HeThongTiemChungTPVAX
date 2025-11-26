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

namespace TPVAXWinform_GUI.UserControls
{
    public partial class HoaDonControl : UserControl
    {
        DataTable dtRecords = new DataTable();
        HoaDonBLL hoaDonBLL = new HoaDonBLL();

        bool RoleNVThuNgan = RoleManager.RoleNVThuNgan();
        bool RoleNVTiepNhan = RoleManager.RoleNVTiepNhan();
        public HoaDonControl()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }
        private void HoaDonControl_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }
            LoadDSHD();
            InitializeFilters();
            contextMenuStripHoaDon.Items["toolStripMenuItemXacNhanThanhToan"].Visible = RoleNVThuNgan;
            contextMenuStripHoaDon.Items["toolStripMenuItemXemChiTiet"].Visible = RoleNVThuNgan || RoleNVTiepNhan;
            contextMenuStripHoaDon.Items["toolStripMenuItemInHoaDon"].Visible = RoleNVThuNgan || RoleNVTiepNhan;
        }

        // Public method để refresh data từ bên ngoài
        public void RefreshData()
        {
            LoadDSHD();
        }

        private void InitializeEventHandlers()
        {
            // Gán event handler để tô màu các dòng theo trạng thái
            dgvHoaDon.CellFormatting += dgvHoaDon_CellFormatting;
        }

        private void InitializeFilters()
        {
            // Reset các control về mặc định
            dtpTuNgay.Value = DateTime.Now.AddYears(-5);
            dtpDenNgay.Value = DateTime.Now;
            numGiaTu.Value = 0;
            numGiaDen.Value = 100000000;
            txtSearchMaHD.Clear();

            // Gán event handlers cho các control filter
            dtpTuNgay.ValueChanged += dtpNgay_ValueChanged;
            dtpDenNgay.ValueChanged += dtpNgay_ValueChanged;
            numGiaTu.ValueChanged += numGia_ValueChanged;
            numGiaDen.ValueChanged += numGia_ValueChanged;

            // Áp dụng lại filter
            ApplyFilters();
        }

        private void dgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Bỏ qua hàng tiêu đề
            if (e.RowIndex < 0)
                return;

            // Format cột Trạng thái
            string trangThaiColumnName = "colTrangThai";
            if (e.ColumnIndex == dgvHoaDon.Columns[trangThaiColumnName].Index && e.Value != null)
            {
                string trangThaiValue = e.Value.ToString();
                DataGridViewCellStyle style = e.CellStyle;
                style.Font = new Font(e.CellStyle.Font, FontStyle.Regular);

                if (trangThaiValue.Equals("True", StringComparison.OrdinalIgnoreCase) || trangThaiValue.Equals("1"))
                {
                    e.Value = "Đã thanh toán";
                    style.BackColor = Color.FromArgb(200, 230, 201); // Xanh lá
                    style.ForeColor = Color.Black;
                }
                else
                {
                    e.Value = "Chưa thanh toán";
                    style.BackColor = Color.FromArgb(255, 224, 178); // Cam
                    style.ForeColor = Color.Black;
                }

                e.FormattingApplied = true;
            }

            // Format cột Tổng tiền
            string tongTienColumnName = "colTongTien";
            if (e.ColumnIndex == dgvHoaDon.Columns[tongTienColumnName].Index && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal tongTien))
                {
                    e.Value = tongTien.ToString("N0") + " VNĐ";
                    e.FormattingApplied = true;
                }
            }
        }

        private void LoadDSHD()
        {
            dtRecords = hoaDonBLL.GetData();
            ApplyFilters();
        }



        private void BindDataToGridHD(DataTable dt)
        {
            dgvHoaDon.AutoGenerateColumns = false;

            colMaHD.DataPropertyName = "MaHD";
            colNgayLap.DataPropertyName = "NgayLap";
            colTongTien.DataPropertyName = "TongTien";
            colTrangThai.DataPropertyName = "TrangThai";
            colMaKH.DataPropertyName = "MaKH";
            colMaNV.DataPropertyName = "MaNV";
            colMaKM.DataPropertyName = "MaKM";

            dgvHoaDon.DataSource = dt;

            dgvHoaDon.Columns["colNgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvHoaDon.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvHoaDon.RowTemplate.Height = 36;
        }

        #region Bộ lọc

        private void ApplyFilters()
        {
            try
            {
                if (dtRecords == null || dtRecords.Rows.Count == 0)
                {
                    BindDataToGridHD(dtRecords);
                    return;
                }

                DataView dv = dtRecords.DefaultView;
                List<string> filters = new List<string>();

                // 1. Lọc theo ngày
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
                filters.Add($"[NgayLap] >= '{tuNgay:yyyy-MM-dd HH:mm:ss}' AND [NgayLap] <= '{denNgay:yyyy-MM-dd HH:mm:ss}'");

                // 2. Lọc theo khoảng giá
                decimal giaTu = numGiaTu.Value;
                decimal giaDen = numGiaDen.Value;
                filters.Add($"[TongTien] >= {giaTu} AND [TongTien] <= {giaDen}");

                // 3. Tìm kiếm theo mã hóa đơn
                if (!string.IsNullOrWhiteSpace(txtSearchMaHD.Text))
                {
                    string searchText = txtSearchMaHD.Text.Trim().Replace("'", "''");
                    filters.Add($"[MaHD] LIKE '%{searchText}%'");
                }

                // Áp dụng filter
                if (filters.Count > 0)
                {
                    dv.RowFilter = string.Join(" AND ", filters);
                }
                else
                {
                    dv.RowFilter = "";
                }

                DataTable dtFiltered = dv.ToTable();
                BindDataToGridHD(dtFiltered);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpNgay_ValueChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void numGia_ValueChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void txtSearchMaHD_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            RefreshData();
            // Reset các control về mặc định
            dtpTuNgay.Value = DateTime.Now.AddYears(-5);
            dtpDenNgay.Value = DateTime.Now;
            numGiaTu.Value = 0;
            numGiaDen.Value = 100000000;
            txtSearchMaHD.Clear();

            // Áp dụng lại filter
            ApplyFilters();
        }

        #endregion

        #region Context Menu

        private void contextMenuStripHoaDon_Opening(object sender, CancelEventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvHoaDon.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            // Lấy trạng thái của dòng được chọn
            DataGridViewRow selectedRow = dgvHoaDon.SelectedRows[0];
            string trangThai = selectedRow.Cells["colTrangThai"].Value?.ToString() ?? "False";
            bool isDaThanhToan = trangThai.Equals("True", StringComparison.OrdinalIgnoreCase) || trangThai.Equals("1");

            // Disable "Xác nhận thanh toán" nếu đã thanh toán
            toolStripMenuItemXacNhanThanhToan.Enabled = !isDaThanhToan;
        }

        private void toolStripMenuItemXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết!", "Thông báo",
             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvHoaDon.SelectedRows[0];
            string maHD = selectedRow.Cells["colMaHD"].Value?.ToString() ?? "";

            frmChiTietHoaDon frmChiTiet = new frmChiTietHoaDon(maHD);
            frmChiTiet.ShowDialog();
        }

        private void toolStripMenuItemInHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để in!", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvHoaDon.SelectedRows[0];
            string maHD = selectedRow.Cells["colMaHD"].Value?.ToString() ?? "";

            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Không thể lấy mã hóa đơn!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                frmInHoaDon frmIn = new frmInHoaDon(maHD);
                frmIn.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form in hóa đơn:\n{ex.Message}",
       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItemXacNhanThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xác nhận thanh toán!", "Thông báo",
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvHoaDon.SelectedRows[0];
            string maHD = selectedRow.Cells["colMaHD"].Value?.ToString() ?? "";
            string trangThai = selectedRow.Cells["colTrangThai"].Value?.ToString() ?? "False";

            // Kiểm tra trạng thái
            if (trangThai.Equals("True", StringComparison.OrdinalIgnoreCase) || trangThai.Equals("1"))
            {
                MessageBox.Show("Hóa đơn này đã được thanh toán rồi!", "Thông báo",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Xác nhận với người dùng
            decimal tongTien = decimal.Parse(selectedRow.Cells["colTongTien"].Value?.ToString() ?? "0");

            DialogResult result = MessageBox.Show(
                  $"Xác nhận thanh toán cho hóa đơn:\n\n" +
          $"📋 Mã hóa đơn: {maHD}\n" +
               $"💰 Tổng tiền: {tongTien:N0} VNĐ\n\n" +
          $"Bạn có chắc chắn muốn xác nhận thanh toán?",
                         "Xác nhận thanh toán",
               MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Cập nhật trạng thái hóa đơn
                    HoaDonDTO hd = new HoaDonDTO();
                    hd.MaHD = maHD;
                    hd.NgayLap = Convert.ToDateTime(selectedRow.Cells["colNgayLap"].Value);
                    hd.TongTien = tongTien;
                    hd.TrangThai = true; // Đã thanh toán
                    hd.MaKH = selectedRow.Cells["colMaKH"].Value?.ToString();
                    hd.MaNV = selectedRow.Cells["colMaNV"].Value?.ToString();
                    hd.MaKM = selectedRow.Cells["colMaKM"].Value?.ToString();

                    hoaDonBLL.Edit(hd);

                    MessageBox.Show("Xác nhận thanh toán thành công!", "Thành công",
         MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh lại danh sách
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xác nhận thanh toán:\n{ex.Message}",
         "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}
