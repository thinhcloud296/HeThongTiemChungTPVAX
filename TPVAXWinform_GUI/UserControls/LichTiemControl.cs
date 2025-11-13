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

                if (trangThaiValue.Equals("1", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = "Đã tiêm";
                    style.BackColor = Color.FromArgb(200, 230, 201); // Xanh lá
                    style.ForeColor = Color.Black;
                }
                else
                {
                    e.Value = "Chưa tiêm";
                    style.BackColor = Color.FromArgb(255, 224, 178); // Cam
                    style.ForeColor = Color.Black;
                }

                // Đánh dấu là đã format
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

            colMaHSTC.DataPropertyName = "MaHSTC";
            colTenNguoiTiem.DataPropertyName = "Tên người tiêm";
            colTenVC.DataPropertyName = "Tên Vaccine";
            colNgayHen.DataPropertyName = "Ngày hẹn";
            colTrangThai.DataPropertyName = "Trạng thái";
            colNgayTiemThucTe.DataPropertyName = "Ngày tiêm thực tế";
            dgvLichTiem.DataSource = dt;

            dgvLichTiem.Columns["colNgayHen"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichTiem.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvLichTiem.RowTemplate.Height = 36;
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
                    filters.Add("[Trạng thái] = '1' OR [Trạng thái] = 'True'");
                }
                else if (!daTiem && chuaTiem)
                {
                    filters.Add("[Trạng thái] = '0' OR [Trạng thái] = 'False'");
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
            string trangThaiText = trangThai.Equals("1") ? "Đã tiêm" : "Chưa tiêm";

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
            string trangThai = selectedRow.Cells["colTrangThai"].Value?.ToString() ?? "0";

            // Kiểm tra trạng thái
            if (trangThai.Equals("1"))
            {
                MessageBox.Show("Mũi tiêm này đã được xác nhận tiêm rồi!", "Thông báo",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Xác nhận với người dùng
            string tenNguoiTiem = selectedRow.Cells["colTenNguoiTiem"].Value?.ToString() ?? "";
            string tenVaccine = selectedRow.Cells["colTenVC"].Value?.ToString() ?? "";

            DialogResult result = MessageBox.Show(
                  $"Xác nhận đã tiêm cho:\n\n" +
                $"👤 Người tiêm: {tenNguoiTiem}\n" +
               $"💉 Vaccine: {tenVaccine}\n\n" +
           $"Bạn có chắc chắn muốn xác nhận?",
     "Xác nhận tiêm",
                 MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Lấy thông tin từ row để cập nhật
                    DataRowView drv = selectedRow.DataBoundItem as DataRowView;
                    if (drv != null)
                    {
                        string maLT = drv.Row["MaLT"]?.ToString() ?? "";
                        string maHSTC = drv.Row["MaHSTC"]?.ToString() ?? "";
                        string maVC = drv.Row["MaVC"]?.ToString() ?? "";
                        DateTime ngayHenTiem = Convert.ToDateTime(drv.Row["Ngày hẹn"]);

                        // Tạo DTO và cập nhật
                        LichTiemDTO lichTiem = new LichTiemDTO
                        {
                            MaLT = maLT,
                            MaHSTC = maHSTC,
                            MaVC = maVC,
                            NgayHenTiem = ngayHenTiem,
                            TrangThai = "1", // Đã tiêm
                            NgayTiemThucTe = DateTime.Now
                        };

                        lichTiemBLL.Edit(lichTiem);

                        MessageBox.Show("Xác nhận tiêm thành công!", "Thành công",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh lại dữ liệu
                        LoadDSLT();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xác nhận tiêm: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            string trangThai = selectedRow.Cells["colTrangThai"].Value?.ToString() ?? "0";

            // Kiểm tra trạng thái
            if (trangThai.Equals("1"))
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
                    // Xóa lịch tiêm khỏi database hoặc đánh dấu đã hủy
                    DataRowView drv = selectedRow.DataBoundItem as DataRowView;
                    if (drv != null)
                    {
                        string maLT = drv.Row["MaLT"]?.ToString() ?? "";

                        // Có thể xóa hoặc update trạng thái thành hủy tùy vào yêu cầu
                        // Ở đây tôi sẽ đặt trạng thái = "0" và xóa ngày tiêm thực tế
                        string maHSTC = drv.Row["MaHSTC"]?.ToString() ?? "";
                        string maVC = drv.Row["MaVC"]?.ToString() ?? "";
                        DateTime ngayHenTiem = Convert.ToDateTime(drv.Row["Ngày hẹn"]);

                        LichTiemDTO lichTiem = new LichTiemDTO
                        {
                            MaLT = maLT,
                            MaHSTC = maHSTC,
                            MaVC = maVC,
                            NgayHenTiem = ngayHenTiem,
                            TrangThai = "0", // Chưa tiêm
                            NgayTiemThucTe = null
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

        #endregion
    }
}
