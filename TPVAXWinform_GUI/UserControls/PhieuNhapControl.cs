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
using TPVAXWinform_GUI.Forms;

namespace TPVAXWinform_GUI.UserControls
{
    public partial class PhieuNhapControl : UserControl
    {
        private PhieuNhapBLL phieuNhapBLL = new PhieuNhapBLL();
        private DataTable dtPhieuNhap;

        public PhieuNhapControl()
        {
            InitializeComponent();
        }

        private void PhieuNhapControl_Load(object sender, EventArgs e)
        {
            LoadPhieuNhap();
        }

        private void LoadPhieuNhap()
        {
            try
            {
                dtPhieuNhap = phieuNhapBLL.GetDataDetail();
                BindDataToGrid(dtPhieuNhap);
            }
            catch (Exception ex)
            {
                // SỬA LỖI ENCODING: Hiển thị chữ "Lỗi" đúng
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvPhieuNhap.AutoGenerateColumns = false;

            // SỬA LỖI ENCODING: Dùng đúng tên cột từ Stored Procedure
            colMaPN.DataPropertyName = "Mã Phiếu Nhập";
            colNgayLap.DataPropertyName = "Ngày Lập";
            colTenNV.DataPropertyName = "Tên Nhân Viên Lập";
            colTenNCC.DataPropertyName = "Tên Nhà Cung Cấp";

            // SỬA HIỆU NĂNG: Gán DataSource cho DefaultView
            // Điều này cho phép DataGridView tự động cập nhật khi RowFilter thay đổi
            dgvPhieuNhap.DataSource = dt.DefaultView;

            // Căn giữa các cột
            string[] centerColumns = { "colMaPN", "colNgayLap" };
            foreach (var name in centerColumns)
            {
                if (dgvPhieuNhap.Columns[name] != null)
                    dgvPhieuNhap.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void ApplyFilters()
        {
            if (dtPhieuNhap == null) return;

            // Lấy DefaultView (đã được gán cho DataGridView)
            DataView dv = dtPhieuNhap.DefaultView;
            List<string> filters = new List<string>();

            // Filter by search text
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string searchText = txtSearch.Text.Trim().Replace("'", "''");

                // SỬA LỖI ENCODING: Dùng đúng tên cột
                filters.Add($"([Mã Phiếu Nhập] LIKE '%{searchText}%' OR [Tên Nhân Viên Lập] LIKE '%{searchText}%' OR [Tên Nhà Cung Cấp] LIKE '%{searchText}%')");
            }

            // SỬA LỖI LOGIC: Dùng định dạng RowFilter an toàn cho ngày tháng
            if (dtpTuNgay.Checked)
            {
                DateTime tuNgay = dtpTuNgay.Value.Date; // Lấy 00:00:00 của ngày
                filters.Add($"[Ngày Lập] >= '{tuNgay:yyyy-MM-dd HH:mm:ss}'");
            }

            if (dtpDenNgay.Checked)
            {
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1); // Lấy 00:00:00 của ngày hôm sau
                filters.Add($"[Ngày Lập] < '{denNgay:yyyy-MM-dd HH:mm:ss}'");
            }

            // Apply filter
            if (filters.Count > 0)
            {
                dv.RowFilter = string.Join(" AND ", filters);
            }
            else
            {
                dv.RowFilter = "";
            }

            // SỬA HIỆU NĂNG: Xóa dòng này
            // dgvPhieuNhap.DataSource = dv.ToTable(); // <-- KHÔNG CẦN DÒNG NÀY
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dtpTuNgay.Checked = false;
            dtpDenNgay.Checked = false;

            // Cách 1: Load lại (Đơn giản, nhưng gọi DB)
            // LoadPhieuNhap(); 

            // Cách 2: Chỉ reset filter (Nhanh hơn)
            if (dtPhieuNhap != null)
            {
                dtPhieuNhap.DefaultView.RowFilter = "";
            }
        }

        private void dgvPhieuNhap_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.Rows[e.RowIndex].Selected = true;
            }
        }

        private void toolStripMenuItemXemChiTiet_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào đang được chọn không
            if (dgvPhieuNhap.SelectedRows.Count == 0)
            {
                return;
            }

            // Lấy Mã Phiếu Nhập từ dòng đang được chọn
            string maPN = dgvPhieuNhap.SelectedRows[0].Cells[colMaPN.Name].Value?.ToString();

            if (!string.IsNullOrEmpty(maPN))
            {
                frmChiTietPhieuNhap frm = new frmChiTietPhieuNhap(maPN);
                frm.ShowDialog();
            }
        }
    }
}