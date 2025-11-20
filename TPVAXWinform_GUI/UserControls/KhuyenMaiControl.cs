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
    public partial class KhuyenMaiControl : UserControl
    {
        private KhuyenMaiBLL khuyenMaiBLL = new KhuyenMaiBLL();
        private DataTable dtKhuyenMai;

        public KhuyenMaiControl()
        {
            InitializeComponent();
        }

        private void KhuyenMaiControl_Load(object sender, EventArgs e)
        {
            LoadKhuyenMai();
            LoadFilters();

            // THÊM: Đăng ký sự kiện format để hiển thị trạng thái đẹp hơn
            dgvKhuyenMai.CellFormatting += dgvKhuyenMai_CellFormatting;
        }

        // Phương thức public để form khác gọi khi cần refresh
        public void RefreshData()
        {
            LoadKhuyenMai();
        }

        private void LoadKhuyenMai()
        {
            dtKhuyenMai = khuyenMaiBLL.GetAll();
            BindDataToGrid(dtKhuyenMai);
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvKhuyenMai.AutoGenerateColumns = false;
            colMaKM.DataPropertyName = "MaKM";
            colTenKM.DataPropertyName = "TenKM";
            colLoaiKM.DataPropertyName = "LoaiKM";
            colKieuGiam.DataPropertyName = "KieuGiam";
            colGiaTriGiam.DataPropertyName = "GiaTriGiam";
            colNgayBatDau.DataPropertyName = "NgayBatDau";
            colNgayKetThuc.DataPropertyName = "NgayKetThuc";
            colTrangThai.DataPropertyName = "TrangThai";
            colMoTa.DataPropertyName = "MoTa";

            dgvKhuyenMai.DataSource = dt;

            // Format giá trị giảm (ví dụ: 10.00 hoặc 50,000)
            colGiaTriGiam.DefaultCellStyle.Format = "N0";
            colGiaTriGiam.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Format ngày tháng
            colNgayBatDau.DefaultCellStyle.Format = "dd/MM/yyyy";
            colNgayKetThuc.DefaultCellStyle.Format = "dd/MM/yyyy";

            // Căn giữa các cột
            string[] centerColumns = { "colMaKM", "colLoaiKM", "colKieuGiam", "colTrangThai" };
            foreach (var name in centerColumns)
            {
                if (dgvKhuyenMai.Columns[name] != null)
                    dgvKhuyenMai.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // THÊM: Hàm này giúp hiển thị "Đang chạy/Tạm dừng" thay vì True/False
        private void dgvKhuyenMai_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Format cột Trạng Thái
            if (dgvKhuyenMai.Columns[e.ColumnIndex].Name == "colTrangThai" && e.Value != null)
            {
                bool isActive = false;
                if (e.Value != DBNull.Value)
                {
                    isActive = Convert.ToBoolean(e.Value);
                }

                e.Value = isActive ? "Đang chạy" : "Tạm dừng/Hết hạn";
                e.CellStyle.BackColor = isActive ? Color.FromArgb(200, 230, 201) : Color.FromArgb(255, 224, 178);
                e.FormattingApplied = true;
            }

            // Format cột Kiểu Giảm (Hiển thị tiếng Việt)
            if (dgvKhuyenMai.Columns[e.ColumnIndex].Name == "colKieuGiam" && e.Value != null)
            {
                string kieu = e.Value.ToString();
                if (kieu == "PhanTram") e.Value = "Phần trăm (%)";
                else if (kieu == "SoTien") e.Value = "Tiền mặt (VNĐ)";
                e.FormattingApplied = true;
            }
        }

        private void LoadFilters()
        {
            // Load trạng thái (Sửa tiếng Việt)
            DataTable dtTrangThai = new DataTable();
            dtTrangThai.Columns.Add("Value", typeof(string));
            dtTrangThai.Columns.Add("Display", typeof(string));

            dtTrangThai.Rows.Add("", "-- Tất cả --");
            dtTrangThai.Rows.Add("True", "Đang chạy");
            dtTrangThai.Rows.Add("False", "Tạm dừng/Hết hạn");
            cboTrangThai.DisplayMember = "Display";
            cboTrangThai.ValueMember = "Value";
            cboTrangThai.DataSource = dtTrangThai;

            cboTrangThai.SelectedIndex = 0;
        }

        private void ApplyFilters()
        {
            if (dtKhuyenMai == null) return;

            DataView dv = dtKhuyenMai.DefaultView;
            List<string> filters = new List<string>();

            // Filter by trạng thái
            if (cboTrangThai.SelectedIndex > 0 && cboTrangThai.SelectedValue != null)
            {
                // Kiểm tra nếu SelectedValue bị lỗi thành DataRowView thì bỏ qua
                if (cboTrangThai.SelectedValue is DataRowView)
                {
                    return; // Chưa load xong, thoát hàm để tránh lỗi
                }

                string trangThai = cboTrangThai.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(trangThai))
                {
                    filters.Add($"TrangThai = {trangThai}");
                }
            }

            // Filter by search text
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string searchText = txtSearch.Text.Trim().Replace("'", "''");
                filters.Add($"(TenKM LIKE '%{searchText}%' OR MaKM LIKE '%{searchText}%' OR LoaiKM LIKE '%{searchText}%')");
            }

            // Apply filter
            try
            {
                if (filters.Count > 0)
                {
                    dv.RowFilter = string.Join(" AND ", filters);
                }
                else
                {
                    dv.RowFilter = "";
                }
                dgvKhuyenMai.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                // Bắt lỗi để không crash chương trình
                Console.WriteLine("Lỗi lọc: " + ex.Message);
            }

            // Không cần gán lại DataSource nếu dùng DefaultView, nhưng gán lại table cũng an toàn
            dgvKhuyenMai.DataSource = dv.ToTable();
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboTrangThai.SelectedIndex = 0;
            LoadKhuyenMai();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThemSuaKhuyenMai frm = new frmThemSuaKhuyenMai();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadKhuyenMai();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhuyenMai.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKM = dgvKhuyenMai.SelectedRows[0].Cells["colMaKM"].Value.ToString();
            frmThemSuaKhuyenMai frm = new frmThemSuaKhuyenMai(maKM);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadKhuyenMai();
            }
        }
    }
}