using System;
using System.Data;
using System.Windows.Forms;

namespace TPVAXWinform.UserControls
{
    public partial class HoSoTiemChungControl : UserControl
    {
        private DataTable dtRecords;

        public HoSoTiemChungControl()
        {
            InitializeComponent();
            InitializeActionButtons();
        }

        private void InitializeActionButtons()
        {
            // Cấu hình styling cho DataGridView
            ConfigureDataGridViewStyling();

            // Thêm Context Menu cho chuột phải
            SetupContextMenu();

            // Thêm cột button "Sửa"
            DataGridViewButtonColumn btnEditColumn = new DataGridViewButtonColumn();
            btnEditColumn.Name = "colEdit";
            btnEditColumn.HeaderText = "Sửa";
            btnEditColumn.Text = "✏️ Sửa";
            btnEditColumn.UseColumnTextForButtonValue = true;
            btnEditColumn.Width = 80;
            btnEditColumn.FlatStyle = FlatStyle.Flat;
            xo.Columns.Add(btnEditColumn);
        }

        private void ConfigureDataGridViewStyling()
        {
            // Căn giữa cho column headers
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.Padding = new System.Windows.Forms.Padding(5);

            xo.ColumnHeadersDefaultCellStyle = headerStyle;
            xo.ColumnHeadersHeight = 45;
            xo.EnableHeadersVisualStyles = false;

            // Căn giữa cho các cột cụ thể
            string[] centerAlignColumns = { "colRecordId", "colCustomerId", "colGender",
       "colBirthDate"};

            foreach (string colName in centerAlignColumns)
            {
                if (xo.Columns[colName] != null)
                {
                    xo.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            // Style cho toàn bộ grid
            xo.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            xo.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            xo.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            xo.RowTemplate.Height = 40; // Tăng từ 35 lên 40
            xo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            xo.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            xo.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            xo.RowHeadersVisible = false;
        }

        private void HoSoTiemChungControl_Load(object sender, EventArgs e)
        {
            InitializeFilters();
            LoadSampleData();
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

        private void LoadSampleData()
        {
            // Tạo DataTable với dữ liệu mẫu
            dtRecords = new DataTable();
            dtRecords.Columns.Add("RecordId", typeof(string));
            dtRecords.Columns.Add("CustomerId", typeof(string));
            dtRecords.Columns.Add("CustomerName", typeof(string));
            dtRecords.Columns.Add("Gender", typeof(string));
            dtRecords.Columns.Add("BirthDate", typeof(DateTime));
            dtRecords.Columns.Add("Status", typeof(string));

            // Thêm 20 bản ghi mẫu
            Random rnd = new Random();
            string[] statuses = { "Đã tiêm", "Chưa tiêm", "Đã hủy", "Đã tiêm", "Chưa tiêm" };
            string[] genders = { "Nam", "Nữ" };

            for (int i = 1; i <= 20; i++)
            {
                dtRecords.Rows.Add(
             $"HS{i:D4}",
                   $"KH{i:D4}",
                $"Nguyễn Văn {(char)(64 + i)}",
                   genders[rnd.Next(genders.Length)],
                        DateTime.Now.AddYears(-rnd.Next(18, 65)).AddDays(-rnd.Next(1, 365)),
            statuses[rnd.Next(statuses.Length)]
                      );
            }

            BindDataToGrid(dtRecords);
        }

        private void BindDataToGrid(DataTable dt)
        {
            xo.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = xo.Rows.Add();
                DataGridViewRow dgvRow = xo.Rows[rowIndex];

                dgvRow.Cells["colRecordId"].Value = row["RecordId"];
                dgvRow.Cells["colCustomerId"].Value = row["CustomerId"];
                dgvRow.Cells["colCustomerName"].Value = row["CustomerName"];
                dgvRow.Cells["colGender"].Value = row["Gender"];
                dgvRow.Cells["colBirthDate"].Value = ((DateTime)row["BirthDate"]).ToString("dd/MM/yyyy");




                // Alternating row colors
                if (rowIndex % 2 == 0)
                {
                    dgvRow.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // Lọc dữ liệu
            DataTable filteredData = dtRecords.Clone();

            foreach (DataRow row in dtRecords.Rows)
            {
                bool match = true;

                // Filter by customer name (Họ tên)
                if (!string.IsNullOrWhiteSpace(txtSearchName.Text))
                {
                    string searchName = txtSearchName.Text.Trim().ToLower();
                    string customerName = row["CustomerName"].ToString().ToLower();
                    if (!customerName.Contains(searchName))
                    {
                        match = false;
                    }
                }

                // Filter by record ID (Mã HS)
                if (!string.IsNullOrWhiteSpace(txtSearchRecordId.Text))
                {
                    string searchRecordId = txtSearchRecordId.Text.Trim().ToLower();
                    string recordId = row["RecordId"].ToString().ToLower();
                    if (!recordId.Contains(searchRecordId))
                    {
                        match = false;
                    }
                }

                // Filter by customer ID (Mã KH)
                if (!string.IsNullOrWhiteSpace(txtSearchCustomerId.Text))
                {
                    string searchCustomerId = txtSearchCustomerId.Text.Trim().ToLower();
                    string customerId = row["CustomerId"].ToString().ToLower();
                    if (!customerId.Contains(searchCustomerId))
                    {
                        match = false;
                    }
                }

                if (match)
                {
                    filteredData.ImportRow(row);
                }
            }

            BindDataToGrid(filteredData);

            if (filteredData.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            // Bỏ qua nếu click vào header
            if (e.RowIndex < 0) return;

            string recordId = xo.Rows[e.RowIndex].Cells["colRecordId"].Value.ToString();
            string customerName = xo.Rows[e.RowIndex].Cells["colCustomerName"].Value.ToString();

            // Xử lý click vào button "Sửa"
            if (e.ColumnIndex == xo.Columns["colEdit"].Index)
            {
                EditRecord(recordId, customerName);
            }
        }

        private void EditRecord(string recordId, string customerName)
        {
            MessageBox.Show($"Chỉnh sửa hồ sơ:\nMã HS: {recordId}\nKhách hàng: {customerName}\n\n(Chức năng sẽ được phát triển sau)",
       "Chỉnh sửa hồ sơ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Public method để refresh data từ bên ngoài
        public void RefreshData()
        {
            LoadSampleData();
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
                string recordId = xo.Rows[selectedRowIndex].Cells["colRecordId"].Value?.ToString() ?? "";
                string customerId = xo.Rows[selectedRowIndex].Cells["colCustomerId"].Value?.ToString() ?? "";
                string customerName = xo.Rows[selectedRowIndex].Cells["colCustomerName"].Value?.ToString() ?? "";
                string gender = xo.Rows[selectedRowIndex].Cells["colGender"].Value?.ToString() ?? "";
                string birthDate = xo.Rows[selectedRowIndex].Cells["colBirthDate"].Value?.ToString() ?? "";

                string info = $"📋 THÔNG TIN HỒ SƠ TIÊM CHỦNG\n\n" +
                   $"Mã hồ sơ: {recordId}\n" +
                $"Mã khách hàng: {customerId}\n" +
               $"Họ tên: {customerName}\n" +
                    $"Giới tính: {gender}\n" +
           $"Ngày sinh: {birthDate}\n";

                MessageBox.Show(info, "Thông tin hồ sơ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditInfo_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < xo.Rows.Count)
            {
                string recordId = xo.Rows[selectedRowIndex].Cells["colRecordId"].Value?.ToString() ?? "";
                string customerName = xo.Rows[selectedRowIndex].Cells["colCustomerName"].Value?.ToString() ?? "";

                MessageBox.Show($"Chỉnh sửa thông tin hồ sơ:\n\nMã HS: {recordId}\nKhách hàng: {customerName}\n\n(Chức năng sẽ được phát triển sau)",
               "Sửa thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddDose_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < xo.Rows.Count)
            {
                string recordId = xo.Rows[selectedRowIndex].Cells["colRecordId"].Value?.ToString() ?? "";
                string customerName = xo.Rows[selectedRowIndex].Cells["colCustomerName"].Value?.ToString() ?? "";

                MessageBox.Show($"Thêm mũi tiêm cho:\n\nMã HS: {recordId}\nKhách hàng: {customerName}\n\n(Chức năng sẽ được phát triển sau)",
                      "Thêm mũi tiêm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}