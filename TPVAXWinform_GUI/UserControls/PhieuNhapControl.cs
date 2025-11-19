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
        private ChiTietPhieuNhapBLL chiTietPhieuNhapBLL = new ChiTietPhieuNhapBLL();
        private VaccineBLL vaccineBLL = new VaccineBLL();
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
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvPhieuNhap.AutoGenerateColumns = false;

            colMaPN.DataPropertyName = "Mã Phiếu Nhập";
            colNgayLap.DataPropertyName = "Ngày Lập";
            colTenNV.DataPropertyName = "Tên Nhân Viên Lập";
            colTenNCC.DataPropertyName = "Tên Nhà Cung Cấp";

            dgvPhieuNhap.DataSource = dt.DefaultView;

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

            DataView dv = dtPhieuNhap.DefaultView;
            List<string> filters = new List<string>();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string searchText = txtSearch.Text.Trim().Replace("'", "''");
                filters.Add($"([Mã Phiếu Nhập] LIKE '%{searchText}%' OR [Tên Nhân Viên Lập] LIKE '%{searchText}%' OR [Tên Nhà Cung Cấp] LIKE '%{searchText}%')");
            }

            if (dtpTuNgay.Checked)
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                filters.Add($"[Ngày Lập] >= '{tuNgay:yyyy-MM-dd HH:mm:ss}'");
            }

            if (dtpDenNgay.Checked)
            {
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1);
                filters.Add($"[Ngày Lập] < '{denNgay:yyyy-MM-dd HH:mm:ss}'");
            }

            if (filters.Count > 0)
            {
                dv.RowFilter = string.Join(" AND ", filters);
            }
            else
            {
                dv.RowFilter = "";
            }
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
            if (dgvPhieuNhap.SelectedRows.Count == 0)
            {
                return;
            }

            string maPN = dgvPhieuNhap.SelectedRows[0].Cells[colMaPN.Name].Value?.ToString();

            if (!string.IsNullOrEmpty(maPN))
            {
                frmChiTietPhieuNhap frm = new frmChiTietPhieuNhap(maPN);
                frm.ShowDialog();
            }
        }

        private void toolStripMenuItemInPhieuNhap_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phiếu nhập để in!", "Thông báo",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPN = dgvPhieuNhap.SelectedRows[0].Cells[colMaPN.Name].Value?.ToString();

            if (string.IsNullOrEmpty(maPN))
            {
                MessageBox.Show("Không thể lấy mã phiếu nhập!", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                frmInPhieuNhap frmIn = new frmInPhieuNhap(maPN);
                frmIn.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form in phiếu nhập:\n{ex.Message}",
      "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            frmThemPhieuNhap frm = new frmThemPhieuNhap();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadPhieuNhap();
            }
        }

        private void toolStripMenuItemXacNhanNhap_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần xác nhận!", "Thông báo",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPN = dgvPhieuNhap.SelectedRows[0].Cells[colMaPN.Name].Value?.ToString();

            DialogResult result = MessageBox.Show(
            $"Bạn có chắc chắn muốn xác nhận nhập kho phiếu '{maPN}'?\n\n" +
               "Sau khi xác nhận, số lượng vaccine sẽ được cộng vào kho.",
                   "Xác nhận nhập kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                XacNhanNhapKho(maPN);
            }
        }
        // (Trong PhieuNhapControl.cs)

        // Bạn phải thêm 'using System.Transactions;'
        // (Mặc dù proc đã có transaction, dùng TransactionScope của C# vẫn an toàn hơn)

        private void XacNhanNhapKho(string maPN)
        {
            try
            {
                // Bạn không cần TransactionScope ở đây nữa
                // vì Stored Procedure đã xử lý Transaction rồi.

                // --- SỬA LẠI HOÀN TOÀN ---

                // 1. Gọi hàm BLL (đã tạo ở Bước 2)
                chiTietPhieuNhapBLL.XacNhanNhapKho(maPN);

                // 2. Thông báo thành công
                MessageBox.Show(
                    $"Đã xác nhận nhập kho thành công phiếu '{maPN}'!\n" +
                    $"Số lượng vaccine đã được cập nhật vào kho.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // 3. Refresh lại danh sách
                LoadPhieuNhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xác nhận nhập kho: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshData()
        {
            LoadPhieuNhap();
        }
    }
}