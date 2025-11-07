using System;
using System.Data;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_GUI;

namespace TPVAXWinform.UserControls
{
    public partial class HoSoTiemChungControl : UserControl
    {
        private DataTable dtRecords;
        private HoSoTiemChungBLL _bll = new HoSoTiemChungBLL();

        public HoSoTiemChungControl()
        {
            InitializeComponent();
            InitializeActionButtons();
        }

        private void InitializeActionButtons()
        {
            ConfigureDataGridViewStyling();
            SetupContextMenu();

            if (xo.Columns["colEdit"] == null) // tránh add trùng
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
                xo.Columns.Add(btnEditColumn);
            }

            // Thêm sự kiện cho button "Thêm hồ sơ"
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            frmThemHSTC frmThem = new frmThemHSTC();
            frmThem.ShowDialog();

            // Refresh data sau khi đóng form
            RefreshData();
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
            xo.ColumnHeadersDefaultCellStyle = headerStyle;
            xo.ColumnHeadersHeight = 45;
            xo.EnableHeadersVisualStyles = false;

            // căn giữa các cột ID/ngày
            string[] center = { "colMaHSTC", "colGioiTinh", "colNgaySinh" };
            foreach (var name in center)
                if (xo.Columns[name] != null)
                    xo.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            xo.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            xo.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            xo.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            xo.RowTemplate.Height = 40;
            xo.BorderStyle = BorderStyle.None;
            xo.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            xo.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            xo.RowHeadersVisible = false;
        }


        private void HoSoTiemChungControl_Load(object sender, EventArgs e)
        {

            InitializeFilters();
            LoadDSHSTC();
            SetupEventHandlers();
        }
        private void InitializeFilters()
        {
            // Set default values - removed old filters
        }

        private void SetupEventHandlers()
        {
            btnSearch.Click += BtnSearch_Click;
            btnReset.Click += BtnReset_Click;
            xo.CellContentClick += DgvRecords_CellContentClick;

            // Add hover effects for buttons
            btnSearch.MouseEnter += (s, e) =>
            {
                btnSearch.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            };
            btnSearch.MouseLeave += (s, e) =>
            {
                btnSearch.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            };

            btnReset.MouseEnter += (s, e) =>
            {
                btnReset.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            };
            btnReset.MouseLeave += (s, e) =>
            {
                btnReset.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            };
        }

        private void LoadDSHSTC()
        {
            // Tạo DataTable với dữ liệu mẫu
            dtRecords = new DataTable();
            dtRecords = _bll.GetHSTC_KHHG();

            BindDataToGrid(dtRecords);
        }

        private void BindDataToGrid(DataTable dt)
        {
            xo.AutoGenerateColumns = false;

            colMaHSTC.DataPropertyName = "MaHSTC";
            colHoTen.DataPropertyName = "HoTen";
            colGioiTinh.DataPropertyName = "GioiTinh";
            colNgaySinh.DataPropertyName = "NgaySinh";
            colHoTenKHHG.DataPropertyName = "TenKhachHang";
            colQuanHe.DataPropertyName = "VaiTro";
            xo.DataSource = dt;

            xo.Columns["colNgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            xo.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            xo.RowTemplate.Height = 36;
        }



        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var filtered = dtRecords.Clone(); // giữ schema

            string kwName = txtSearchName.Text.Trim().ToLower();
            string kwMaHS = txtSearchRecordId.Text.Trim().ToLower();   // MaHSTC
            string kwMaKH = txtSearchCustomerId.Text.Trim().ToLower(); // MaKH

            foreach (DataRow row in dtRecords.Rows)
            {
                bool match = true;

                if (!string.IsNullOrEmpty(kwName))
                {
                    string hoTen = (row["HoTen"]?.ToString() ?? "").ToLower();
                    if (!hoTen.Contains(kwName)) match = false;
                }

                if (!string.IsNullOrEmpty(kwMaHS))
                {
                    string maHSTC = (row["MaHSTC"]?.ToString() ?? "").ToLower();
                    if (!maHSTC.Contains(kwMaHS)) match = false;
                }

                if (!string.IsNullOrEmpty(kwMaKH))
                {
                    string maKH = (row["MaKH"]?.ToString() ?? "").ToLower();
                    if (!maKH.Contains(kwMaKH)) match = false;
                }

                if (match) filtered.ImportRow(row);
            }

            BindDataToGrid(filtered);

            if (filtered.Rows.Count == 0)
                MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reset filters
            InitializeFilters();

            // Clear text search boxes
            txtSearchName.Clear();
            txtSearchRecordId.Clear();
            txtSearchCustomerId.Clear();

            // Reload all data
            BindDataToGrid(dtRecords);
        }

        private void DgvRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string maHSTC = xo.Rows[e.RowIndex].Cells["colMaHSTC"].Value?.ToString() ?? "";
            string hoTen = xo.Rows[e.RowIndex].Cells["colHoTen"].Value?.ToString() ?? "";

            if (e.ColumnIndex == xo.Columns["colEdit"].Index)
                EditRecord(maHSTC, hoTen);
        }

        private void EditRecord(string maHSTC, string hoTen)
        {
            MessageBox.Show(
                $"Chỉnh sửa hồ sơ:\nMã HS: {maHSTC}\nKhách hàng: {hoTen}\n\n(Chức năng sẽ được phát triển sau)",
                "Chỉnh sửa hồ sơ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // Public method để refresh data từ bên ngoài
        public void RefreshData()
        {
            LoadDSHSTC();
        }

        private void dgvRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Enabled = true;
        }

        private void SetupContextMenu()
        {
            // Tạo Context Menu
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Menu item: Xem thông tin
            ToolStripMenuItem viewInfoItem = new ToolStripMenuItem("📄 Xem thông tin");
            viewInfoItem.Click += ViewInfo_Click;
            contextMenu.Items.Add(viewInfoItem);

            // Menu item: Sửa thông tin
            ToolStripMenuItem editInfoItem = new ToolStripMenuItem("✏️ Sửa thông tin");
            editInfoItem.Click += EditInfo_Click;
            contextMenu.Items.Add(editInfoItem);

            // Separator
            contextMenu.Items.Add(new ToolStripSeparator());

            // Menu item: Thêm mũi tiêm
            ToolStripMenuItem addDoseItem = new ToolStripMenuItem("💉 Thêm mũi tiêm");
            addDoseItem.Click += AddDose_Click;
            contextMenu.Items.Add(addDoseItem);

            // Gán context menu cho DataGridView
            xo.ContextMenuStrip = contextMenu;

            // Xử lý sự kiện mở context menu để lấy thông tin dòng được chọn
            xo.MouseDown += Xo_MouseDown;
        }

        private int selectedRowIndex = -1;

        private void Xo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = xo.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    // Chọn dòng được click chuột phải
                    xo.ClearSelection();
                    xo.Rows[hitTest.RowIndex].Selected = true;
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
            if (selectedRowIndex >= 0 && selectedRowIndex < xo.Rows.Count)
            {
                string maHSTC = xo.Rows[selectedRowIndex].Cells["colMaHSTC"].Value?.ToString() ?? "";
                string hoTen = xo.Rows[selectedRowIndex].Cells["colHoTen"].Value?.ToString() ?? "";
                string gt = xo.Rows[selectedRowIndex].Cells["colGioiTinh"].Value?.ToString() ?? "";
                var valNgaySinh = xo.Rows[selectedRowIndex].Cells["colNgaySinh"].Value;
                string ns =
                    valNgaySinh is DateTime dt ? dt.ToString("dd-MM-yyyy") :
                    DateTime.TryParse(valNgaySinh?.ToString(), out var d) ? d.ToString("dd-MM-yyyy") :
                    "";
                string quanhe = xo.Rows[selectedRowIndex].Cells["colQuanHe"].Value?.ToString() ?? "";
                string tenKH = xo.Rows[selectedRowIndex].Cells["colHoTenKHHG"].Value?.ToString() ?? "";

                MessageBox.Show(
                    "📋 THÔNG TIN HỒ SƠ TIÊM CHỦNG\n\n" +
                    $"Mã hồ sơ: {maHSTC}\n" +
                    $"Họ tên: {hoTen}\n" +
                    $"Giới tính: {gt}\n" +
                    $"Ngày sinh: {ns}\n" +
                    $"Họ tên khách hàng : {tenKH}\n" +
                    $"Quan hệ với khách hàng: {quanhe}\n",
                    "Thông tin hồ sơ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditInfo_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < xo.Rows.Count)
            {
                string maHSTC = xo.Rows[selectedRowIndex].Cells["colMaHSTC"].Value?.ToString() ?? "";
                string hoTen = xo.Rows[selectedRowIndex].Cells["colHoTen"].Value?.ToString() ?? "";
                MessageBox.Show(
                    $"Chỉnh sửa thông tin hồ sơ:\n\nMã HS: {maHSTC}\nKhách hàng: {hoTen}\n\n(Chức năng sẽ được phát triển sau)",
                    "Sửa thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddDose_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < xo.Rows.Count)
            {
                string maHSTC = xo.Rows[selectedRowIndex].Cells["colMaHSTC"].Value?.ToString() ?? "";
                string hoTen = xo.Rows[selectedRowIndex].Cells["colHoTen"].Value?.ToString() ?? "";
                MessageBox.Show(
                    $"Thêm mũi tiêm cho:\n\nMã HS: {maHSTC}\nKhách hàng: {hoTen}\n\n(Chức năng sẽ được phát triển sau)",
                    "Thêm mũi tiêm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

