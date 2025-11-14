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
using TPVAXWinform_GUI.Forms;

namespace TPVAXWinform_GUI.UserControls
{
    public partial class LichTiemControl : UserControl
    {
        LichTiemBLL lichTiemBLL = new LichTiemBLL();

        DataTable dtRecords = new DataTable();

        public LichTiemControl()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void LichTiemControl_Load(object sender, EventArgs e)
        {
            LoadDSLT();
            InitializeFilters();
        }

        private void InitializeEventHandlers()
        {
            // Gán event handler để tô màu các dòng theo trạng thái
            dgvLichTiem.CellFormatting += dgvLichTiem_CellFormatting;

            // Gán event handler cho button clicks trong DataGridView
            dgvLichTiem.CellContentClick += dgvLichTiem_CellContentClick;
        }

        private void InitializeFilters()
        {
            cboLoaiTimKiem.SelectedIndex = 0; // "Tên người tiêm"

            // --- SỬA LẠI DÒNG NÀY ---
            // Gán giá trị mặc định là 5 năm trước
            dtpTuThang.Value = DateTime.Now.AddYears(-5);

            // Gán giá trị cho tháng hiện tại
            dtpDenThang.Value = DateTime.Now.AddYears(1);

            // Mặc định check cả 2 checkbox
            chkDaTiem.Checked = true;
            chkChuaTiem.Checked = true;
        }

        private void dgvLichTiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu click vào header
            if (e.RowIndex < 0)
                return;

            DataGridViewRow selectedRow = dgvLichTiem.Rows[e.RowIndex];
            string columnName = dgvLichTiem.Columns[e.ColumnIndex].Name;

            // Xử lý khi click vào cột "Tiêm"
            if (columnName == "colCheckIn")
            {
                XacNhanTiemClick(selectedRow);
            }
            // Xử lý khi click vào cột "Hủy"
            else if (columnName == "colHuy")
            {
                HuyTiemClick(selectedRow);
            }
        }

        private void XacNhanTiemClick(DataGridViewRow selectedRow)
        {
            string trangThai = selectedRow.Cells["colTrangThai"].Value?.ToString() ?? "0";

            // Kiểm tra nếu đã tiêm (TrangThai = true hoặc "1")
            if (trangThai.Equals("Đã tiêm", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Đã tiêm!", "Thông báo",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra nếu đã hủy (null hoặc rỗng)
            if (trangThai.Equals("Đã hủy", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(trangThai) || trangThai == "")
            {
                MessageBox.Show("Không thể xác nhận mũi tiêm đã bị hủy!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy thông tin từ row
                DataRowView drv = selectedRow.DataBoundItem as DataRowView;
                if (drv != null)
                {
                    string maLT = drv.Row["MaLT"]?.ToString() ?? "";
                    string maHSTC = drv.Row["MaHSTC"]?.ToString() ?? "";
                    string maVC = drv.Row["MaVC"]?.ToString() ?? "";
                    DateTime ngayHenTiem = Convert.ToDateTime(drv.Row["Ngày hẹn"]);
                    string tenNguoiTiem = drv.Row["Tên người tiêm"]?.ToString() ?? "";
                    string tenVaccine = drv.Row["Tên Vaccine"]?.ToString() ?? "";
                    string ngayHen = Convert.ToDateTime(drv.Row["Ngày hẹn"]).ToString("dd/MM/yyyy");
                    int? soMui = drv.Row["SoMui"] != DBNull.Value ? Convert.ToInt32(drv.Row["SoMui"]) : (int?)null;

                    // Mở form xác nhận tiêm
                    XacNhanTiemForm form = new XacNhanTiemForm(
                    maLT, maHSTC, maVC, ngayHenTiem,
                   tenNguoiTiem, tenVaccine, ngayHen, soMui);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh lại dữ liệu
                        LoadDSLT();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xác nhận tiêm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HuyTiemClick(DataGridViewRow selectedRow)
        {
            string trangThai = selectedRow.Cells["colTrangThai"].Value?.ToString() ?? "0";

            // Kiểm tra nếu đã tiêm (TrangThai = true hoặc "1")
            if (trangThai.Equals("Đã tiêm", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Không thể hủy mũi tiêm đã được tiêm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận với người dùng
            string tenNguoiTiem = selectedRow.Cells["colTenNguoiTiem"].Value?.ToString() ?? "";
            string tenVaccine = selectedRow.Cells["colTenVC"].Value?.ToString() ?? "";

            DialogResult result = MessageBox.Show(
             $"Hủy lịch tiêm cho:\n\n" +
             $"👤 Người tiêm: {tenNguoiTiem}\n" +
             $"💉 Vaccine: {tenVaccine}\n\n" +
             $"Bạn có chắc chắn muốn hủy?",
            "Hủy lịch tiêm",
            MessageBoxButtons.YesNo,
         MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DataRowView drv = selectedRow.DataBoundItem as DataRowView;
                    if (drv != null)
                    {
                        string maLT = drv.Row["MaLT"]?.ToString() ?? "";
                        string maHSTC = drv.Row["MaHSTC"]?.ToString() ?? "";
                        string maVC = drv.Row["MaVC"]?.ToString() ?? "";
                        DateTime ngayHenTiem = Convert.ToDateTime(drv.Row["Ngày hẹn"]);

                        LichTiemDTO lichTiem = new LichTiemDTO
                        {
                            MaLT = maLT,
                            MaHSTC = maHSTC,
                            MaVC = maVC,
                            NgayHenTiem = ngayHenTiem,
                            TrangThai = null, // Đã hủy - set thành null
                            NgayTiemThucTe = null,
                            GhiChu = "Đã hủy"
                        };

                        lichTiemBLL.Edit(lichTiem);

                        MessageBox.Show("Hủy lịch tiêm thành công!", "Thành công",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh lại dữ liệu
                        LoadDSLT();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi hủy lịch tiêm: {ex.Message}", "Lỗi",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvLichTiem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Bỏ qua hàng tiêu đề
            if (e.RowIndex < 0)
                return;

            // Chỉ thực hiện khi đang format cột "Trạng thái"
            string trangThaiColumnName = "colTrangThai";
            if (e.ColumnIndex == dgvLichTiem.Columns[trangThaiColumnName].Index && e.Value != null)
            {
                string trangThaiValue = e.Value.ToString();
                DataGridViewCellStyle style = e.CellStyle;
                style.Font = new Font(e.CellStyle.Font, FontStyle.Regular);

                if (trangThaiValue.Equals("Đã tiêm", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = "Đã tiêm";
                    style.BackColor = Color.FromArgb(200, 230, 201); // Xanh lá
                    style.ForeColor = Color.Black;
                }
                else if (trangThaiValue.Equals("Chưa tiêm", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = "Chưa tiêm";
                    style.BackColor = Color.FromArgb(255, 224, 178); // Cam
                    style.ForeColor = Color.Black;
                }
                else
                {
                    e.Value = "Đã hủy";
                    style.BackColor = Color.FromArgb(215, 215, 215); // Xám nhạt
                    style.ForeColor = Color.Black;
                }
                e.FormattingApplied = true;
            }
        }

        private void LoadDSLT()
        {
            dtRecords = lichTiemBLL.GetGetLichTiemWithHSTC();
            ApplyFilters();
        }

        private void BindDataToGridLT(DataTable dt)
        {
            dgvLichTiem.AutoGenerateColumns = false;

            colMaLT.DataPropertyName = "MaLT";
            colMaHSTC.DataPropertyName = "MaHSTC";
            colTenNguoiTiem.DataPropertyName = "Tên người tiêm";
            colTenVC.DataPropertyName = "Tên Vaccine";
            colSoMui.DataPropertyName = "SoMui";
            colNgayHen.DataPropertyName = "Ngày hẹn";
            colTrangThai.DataPropertyName = "Trạng thái";
            colNgayTiemThucTe.DataPropertyName = "Ngày tiêm thực tế";
            dgvLichTiem.DataSource = dt;

            dgvLichTiem.Columns["colNgayHen"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichTiem.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvLichTiem.RowTemplate.Height = 36;

            // Căn giữa các cột theo yêu cầu
            dgvLichTiem.Columns["colMaLT"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvLichTiem.Columns["colMaHSTC"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvLichTiem.Columns["colSoMui"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvLichTiem.Columns["colNgayHen"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvLichTiem.Columns["colTrangThai"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvLichTiem.Columns["colNgayTiemThucTe"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        }

        #region Bộ lọc

        private void ApplyFilters()
        {
            try
            {
                if (dtRecords == null || dtRecords.Rows.Count == 0)
                {
                    BindDataToGridLT(dtRecords);
                    return;
                }

                DataView dv = dtRecords.DefaultView;
                List<string> filters = new List<string>();

                // --- SỬA LOGIC LỌC NGÀY THÁNG ---

                // 1. Lấy ngày bắt đầu (luôn là 00:00:00 giờ)
                DateTime tuNgay = dtpTuThang.Value.Date;

                // 2. Lấy ngày kết thúc (lấy 23:59:59 của ngày được chọn)
                DateTime denNgay = dtpDenThang.Value.Date.AddDays(1).AddSeconds(-1);

                // 3. Sửa cú pháp RowFilter (dùng định dạng 'yyyy-MM-dd HH:mm:ss')
                filters.Add($"[Ngày hẹn] >= '{tuNgay:yyyy-MM-dd HH:mm:ss}' AND [Ngày hẹn] <= '{denNgay:yyyy-MM-dd HH:mm:ss}'");

                // --- KẾT THÚC SỬA ---


                // Lọc theo trạng thái (checkbox)
                bool daTiem = chkDaTiem.Checked;
                bool chuaTiem = chkChuaTiem.Checked;

                // (Code lọc trạng thái của bạn đã ĐÚNG, vì cột Trạng thái đang là "0" hoặc "1")
                if (daTiem && !chuaTiem)
                {
                    filters.Add("[Trạng thái] = 'Đã tiêm' ");
                }
                else if (!daTiem && chuaTiem)
                {
                    filters.Add("[Trạng thái] = 'Chưa tiêm' ");
                }
                else if (!daTiem && !chuaTiem)
                {
                    filters.Add("1 = 0"); // Không hiển thị gì
                }
                // (Nếu cả 2 đều checked -> không thêm filter)

                // Lọc theo tìm kiếm (Code này của bạn đã OK)
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string searchText = txtSearch.Text.Trim().Replace("'", "''");
                    string searchColumn = "";

                    switch (cboLoaiTimKiem.SelectedIndex)
                    {
                        case 0: // Tên người tiêm
                            searchColumn = "Tên người tiêm";
                            break;
                        case 1: // Tên Vaccine
                            searchColumn = "Tên Vaccine";
                            break;
                    }

                    if (!string.IsNullOrEmpty(searchColumn))
                    {
                        filters.Add($"[{searchColumn}] LIKE '%{searchText}%'");
                    }
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
                BindDataToGridLT(dtFiltered);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkTrangThai_CheckedChanged(object sender, EventArgs e)
        {
            // Tự động lọc khi thay đổi checkbox
            ApplyFilters();
        }

        private void dtpThang_ValueChanged(object sender, EventArgs e)
        {
            // Tự động lọc khi thay đổi tháng
            ApplyFilters();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Tự động lọc khi thay đổi text tìm kiếm
            ApplyFilters();
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            // Reset các control về mặc định
            dtpTuThang.Value = DateTime.Now.AddYears(-5);
            dtpDenThang.Value = DateTime.Now.AddYears(1);
            chkDaTiem.Checked = true;
            chkChuaTiem.Checked = true;
            txtSearch.Clear();
            cboLoaiTimKiem.SelectedIndex = 0;

            // Áp dụng lại filter
            ApplyFilters();
        }

        #endregion

        #region Context Menu

        private void contextMenuStripLichTiem_Opening(object sender, CancelEventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvLichTiem.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            // Lấy trạng thái của dòng được chọn
            DataGridViewRow selectedRow = dgvLichTiem.SelectedRows[0];
            string trangThai = selectedRow.Cells["colTrangThai"].Value?.ToString() ?? "0";
            bool isDaTiem = trangThai.Equals("1");

            toolStripMenuItemXacNhanTiem.Enabled = !isDaTiem;
            toolStripMenuItemHuyTiem.Enabled = !isDaTiem;
        }

        private void toolStripMenuItemXemThongTin_Click(object sender, EventArgs e)
        {
            if (dgvLichTiem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lịch tiêm để xem thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvLichTiem.SelectedRows[0];

            // Lấy thông tin từ dòng được chọn
            string maHSTC = selectedRow.Cells["colMaHSTC"].Value?.ToString() ?? "";
            string tenNguoiTiem = selectedRow.Cells["colTenNguoiTiem"].Value?.ToString() ?? "";
            string tenVaccine = selectedRow.Cells["colTenVC"].Value?.ToString() ?? "";
            string ngayHen = selectedRow.Cells["colNgayHen"].Value?.ToString() ?? "";
            string trangThai = selectedRow.Cells["colTrangThai"].Value?.ToString() ?? "";
            string ngayTiemThucTe = selectedRow.Cells["colNgayTiemThucTe"].Value?.ToString() ?? "";

            // Chuyển đổi trạng thái để hiển thị
            string trangThaiText = trangThai.Equals("True", StringComparison.OrdinalIgnoreCase) ? "Đã tiêm" : "Chưa tiêm";

            // Hiển thị thông tin chi tiết
            string thongTin = $"═══════════════════════════════════════\n" +
        $"     THÔNG TIN MŨI TIÊM\n" +
        $"═══════════════════════════════════════\n\n" +
   $"📋 Mã HSTC: {maHSTC}\n\n" +
 $"👤 Tên người tiêm: {tenNguoiTiem}\n\n" +
  $"💉 Tên Vaccine: {tenVaccine}\n\n" +
       $"📅 Ngày hẹn: {ngayHen}\n\n" +
          $"📊 Trạng thái: {trangThaiText}\n\n" +
       $"✅ Ngày tiêm thực tế: {ngayTiemThucTe}\n\n" +
     $"═══════════════════════════════════════";

            MessageBox.Show(thongTin, "Thông tin mũi tiêm",
             MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItemXacNhanTiem_Click(object sender, EventArgs e)
        {
            if (dgvLichTiem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lịch tiêm để xác nhận!", "Thông báo",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvLichTiem.SelectedRows[0];
            XacNhanTiemClick(selectedRow);
        }

        private void toolStripMenuItemHuyTiem_Click(object sender, EventArgs e)
        {
            if (dgvLichTiem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lịch tiêm để hủy!", "Thông báo",
     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvLichTiem.SelectedRows[0];
            HuyTiemClick(selectedRow);
        }

        #endregion
    }
}
