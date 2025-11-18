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
    public partial class NhanVienControl : UserControl
    {
        private DataTable dtNhanVien;
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();
        private TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
        private int selectedRowIndex = -1;
        private readonly Dictionary<int, string> chucVuOptions = new Dictionary<int, string>
        {
            { 1, "Quản lý" },
            { 2, "Nhân viên y tế" },
            { 3, "Nhân viên tiếp nhận" },
            { 4, "Nhân viên kho" }
        };
        public NhanVienControl()
        {
            InitializeComponent();
            InitializeActionButtons();
        }

        private void InitializeActionButtons()
        {
            ConfigureDataGridViewStyling();
            SetupContextMenu();

            if (dgvNhanVien.Columns["colEdit"] == null)
            {
                var btnEditColumn = new DataGridViewButtonColumn
                {
                    Name = "colEdit",
                    HeaderText = "Sửa",
                    Text = "✏️ Sửa",
                    UseColumnTextForButtonValue = true,
                    Width = 80,
                    FlatStyle = FlatStyle.Flat
                };
                dgvNhanVien.Columns.Add(btnEditColumn);
            }
        }

        private void ConfigureDataGridViewStyling()
        {
            var headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.FromArgb(41, 128, 185),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(5)
            };
            dgvNhanVien.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvNhanVien.ColumnHeadersHeight = 45;
            dgvNhanVien.EnableHeadersVisualStyles = false;

            // Căn giữa các cột
            string[] centerColumns = { "colMaNV", "colGioiTinh", "colNgaySinh", "colCCCD", "colNgayVaoLam", "colChucVu", "colTrangThai" };
            foreach (var name in centerColumns)
            {
                if (dgvNhanVien.Columns[name] != null)
                    dgvNhanVien.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvNhanVien.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvNhanVien.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            dgvNhanVien.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvNhanVien.RowTemplate.Height = 40;
            dgvNhanVien.BorderStyle = BorderStyle.None;
            dgvNhanVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvNhanVien.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            dgvNhanVien.RowHeadersVisible = false;
        }

        private void NhanVienControl_Load(object sender, EventArgs e)
        {
            LoadDSNhanVien();
            SetupEventHandlers();
            AdjustTitlePosition();
        }

        private void AdjustTitlePosition()
        {
            lblTitle.Left = (panelHeader.Width - lblTitle.Width) / 2;
            lblTitle.Top = (panelHeader.Height - lblTitle.Height) / 2;
        }

        private void SetupEventHandlers()
        {
            btnSearch.Click += BtnSearch_Click;
            btnReset.Click += BtnReset_Click;
            dgvNhanVien.CellContentClick += DgvNhanVien_CellContentClick;

            // Hover effects for buttons
            btnSearch.MouseEnter += (s, e) => { btnSearch.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); };
            btnSearch.MouseLeave += (s, e) => { btnSearch.BackColor = System.Drawing.Color.FromArgb(41, 128, 185); };
            btnReset.MouseEnter += (s, e) => { btnReset.BackColor = System.Drawing.Color.FromArgb(127, 140, 141); };
            btnReset.MouseLeave += (s, e) => { btnReset.BackColor = System.Drawing.Color.FromArgb(149, 165, 166); };
        }

        private void LoadDSNhanVien()
        {
            dtNhanVien = nhanVienBLL.GetData();
            BindDataToGrid(dtNhanVien);
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvNhanVien.AutoGenerateColumns = false;

            colMaNV.DataPropertyName = "MaNV";
            colHoTen.DataPropertyName = "HoTen";
            colGioiTinh.DataPropertyName = "GioiTinh";
            colNgaySinh.DataPropertyName = "NgaySinh";
            colCCCD.DataPropertyName = "CCCD";
            colNgayVaoLam.DataPropertyName = "NgayVaoLam";
            colChucVu.DataPropertyName = "ChucVu";
            colTrangThai.DataPropertyName = "TrangThai";
            colSoDT.DataPropertyName = "SoDT";

            dgvNhanVien.DataSource = dt;

            dgvNhanVien.Columns["colNgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvNhanVien.Columns["colNgayVaoLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvNhanVien.RowTemplate.Height = 36;
        }

        // Tô màu trạng thái theo yêu cầu
        private void dgvNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "colTrangThai")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    string trangThai = e.Value.ToString().Trim();

                    if (trangThai == "1")
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                        e.Value = "Đang hoạt động";
                    }
                    else if (trangThai == "0")
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkRed;
                        e.Value = "Ngưng hoạt động";
                    }
                }
            }

            // Hiển thị Chức vụ
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "colChucVu")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    e.Value = chucVuOptions.ContainsKey(Convert.ToInt32(e.Value)) ?
                        chucVuOptions[Convert.ToInt32(e.Value)] : "Không xác định";
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var filtered = dtNhanVien.Clone();

            string kwName = txtSearchName.Text.Trim().ToLower();
            string kwMaNV = txtSearchMaNV.Text.Trim().ToLower();
            string kwCCCD = txtSearchCCCD.Text.Trim().ToLower();

            foreach (DataRow row in dtNhanVien.Rows)
            {
                bool match = true;
                if (!string.IsNullOrEmpty(kwName) &&
                         !(row["HoTen"]?.ToString() ?? "").ToLower().Contains(kwName)) match = false;

                if (!string.IsNullOrEmpty(kwMaNV) &&
             !(row["MaNV"]?.ToString() ?? "").ToLower().Contains(kwMaNV)) match = false;

                if (!string.IsNullOrEmpty(kwCCCD) &&
           !(row["CCCD"]?.ToString() ?? "").ToLower().Contains(kwCCCD)) match = false;

                if (match) filtered.ImportRow(row);
            }

            BindDataToGrid(filtered);

            if (filtered.Rows.Count == 0)
                MessageBox.Show("Không tìm thấy kết quả phù hợp!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtSearchName.Clear();
            txtSearchMaNV.Clear();
            txtSearchCCCD.Clear();
            BindDataToGrid(dtNhanVien);
        }

        public void RefreshData()
        {
            LoadDSNhanVien();
        }

        private void SetupContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Font = new System.Drawing.Font("Segoe UI", 10F);

            ToolStripMenuItem viewInfoItem = new ToolStripMenuItem("📄 Xem thông tin");
            viewInfoItem.Click += ViewInfo_Click;
            contextMenu.Items.Add(viewInfoItem);

            ToolStripMenuItem editInfoItem = new ToolStripMenuItem("✏️ Sửa thông tin");
            editInfoItem.Click += EditInfo_Click;
            contextMenu.Items.Add(editInfoItem);

            ToolStripMenuItem resetPasswordItem = new ToolStripMenuItem("🔑 Đặt lại mật khẩu");
            resetPasswordItem.Click += ResetPassword_Click;
            contextMenu.Items.Add(resetPasswordItem);

            dgvNhanVien.ContextMenuStrip = contextMenu;
            dgvNhanVien.MouseDown += DgvNhanVien_MouseDown;
        }

        private void DgvNhanVien_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvNhanVien.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dgvNhanVien.ClearSelection();
                    dgvNhanVien.Rows[hitTest.RowIndex].Selected = true;
                    selectedRowIndex = hitTest.RowIndex;
                }
                else
                {
                    selectedRowIndex = -1;
                }
            }
        }

        private void ViewInfo_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < dgvNhanVien.Rows.Count)
            {
                string maNV = dgvNhanVien.Rows[selectedRowIndex].Cells["colMaNV"].Value?.ToString() ?? "";
                string hoTen = dgvNhanVien.Rows[selectedRowIndex].Cells["colHoTen"].Value?.ToString() ?? "";
                string gioiTinh = dgvNhanVien.Rows[selectedRowIndex].Cells["colGioiTinh"].Value?.ToString() ?? "";
                var valNgaySinh = dgvNhanVien.Rows[selectedRowIndex].Cells["colNgaySinh"].Value;
                string ngaySinh = valNgaySinh is DateTime dt ? dt.ToString("dd/MM/yyyy") :
                    DateTime.TryParse(valNgaySinh?.ToString(), out var d) ? d.ToString("dd/MM/yyyy") : "";
                string cccd = dgvNhanVien.Rows[selectedRowIndex].Cells["colCCCD"].Value?.ToString() ?? "";
                string soDT = dgvNhanVien.Rows[selectedRowIndex].Cells["colSoDT"].Value?.ToString() ?? "";

                MessageBox.Show(
            "📋 THÔNG TIN NHÂN VIÊN\n\n" +
               $"Mã nhân viên: {maNV}\n" +
                 $"Họ tên: {hoTen}\n" +
                $"Giới tính: {gioiTinh}\n" +
                    $"Ngày sinh: {ngaySinh}\n" +
                   $"CCCD: {cccd}\n" +
                $"Số điện thoại: {soDT}\n",
                  "Thông tin nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditInfo_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < dgvNhanVien.Rows.Count)
            {
                string maNV = dgvNhanVien.Rows[selectedRowIndex].Cells["colMaNV"].Value?.ToString() ?? "";

                if (string.IsNullOrEmpty(maNV))
                {
                    MessageBox.Show("Không tìm thấy mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmEditNV frmEdit = new frmEditNV();
                frmEdit.LoadNhanVienData(maNV);

                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadDSNhanVien();
                }
            }
        }

        private void DgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvNhanVien.Columns["colEdit"]?.Index)
            {
                string maNV = dgvNhanVien.Rows[e.RowIndex].Cells["colMaNV"].Value?.ToString() ?? "";
                if (string.IsNullOrEmpty(maNV))
                {
                    MessageBox.Show("Không tìm thấy mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmEditNV frmEdit = new frmEditNV();
                frmEdit.LoadNhanVienData(maNV);

                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadDSNhanVien();
                }
            }
            else if (e.ColumnIndex == dgvNhanVien.Columns["colResetPassword"]?.Index)
            {
                string maNV = dgvNhanVien.Rows[e.RowIndex].Cells["colMaNV"].Value?.ToString() ?? "";
                string tenNV = dgvNhanVien.Rows[e.RowIndex].Cells["colHoTen"].Value?.ToString() ?? "";
                if (string.IsNullOrEmpty(maNV))
                {
                    MessageBox.Show("Không tìm thấy mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ResetEmployeePassword(maNV, tenNV);
            }
        }

        private void ResetPassword_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < dgvNhanVien.Rows.Count)
            {
                string maNV = dgvNhanVien.Rows[selectedRowIndex].Cells["colMaNV"].Value?.ToString() ?? "";
                string tenNV = dgvNhanVien.Rows[selectedRowIndex].Cells["colHoTen"].Value?.ToString() ?? "";
                if (string.IsNullOrEmpty(maNV))
                {
                    MessageBox.Show("Không tìm thấy mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ResetEmployeePassword(maNV, tenNV);
            }
        }

        private void ResetEmployeePassword(string maNV, string tenNV)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                 $"Bạn có chắc chắn muốn đặt lại mật khẩu cho nhân viên '{tenNV}' thành '123456Aa@'?",
                    "Xác nhận đặt lại mật khẩu",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    taiKhoanBLL.ResetPassword(maNV, "123456Aa@");
                    MessageBox.Show(
                      $"Đã đặt lại mật khẩu thành công!\n\nMật khẩu mới: 123456Aa@",
                       "Thành công",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
   $"Lỗi khi đặt lại mật khẩu: {ex.Message}",
          "Lỗi",
    MessageBoxButtons.OK,
    MessageBoxIcon.Error);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            frmThemNV frmThem = new frmThemNV();
            if (frmThem.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }
    }
}
