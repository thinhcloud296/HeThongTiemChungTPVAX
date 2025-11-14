using System;
using System.Data;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;
using TPVAXWinform_GUI;

namespace TPVAXWinform.UserControls
{
    public partial class HoSoTiemChungControl : UserControl
    {
        private DataTable dtHSTC;
        private DataTable dtKH;
        private HoSoTiemChungBLL HSCT_bll = new HoSoTiemChungBLL();
        private KhachHangBLL KH_bll = new KhachHangBLL();
        private int selectedHSTCRowIndex = -1;
        private int selectedKHRowIndex = -1;
        public HoSoTiemChungControl()
        {
            InitializeComponent();
            InitializeActionButtons();
        }

        private void InitializeActionButtons()
        {
            ConfigureDataGridViewHSTCStyling();
            SetupContextMenuHSTC();

            if (dgvHSTC.Columns["colEditHS"] == null)
            {
                var btnEditColumn = new DataGridViewButtonColumn
                {
                    Name = "colEditHS",
                    HeaderText = "Sửa",
                    Text = "✏️ Sửa",
                    UseColumnTextForButtonValue = true,
                    Width = 80,
                    FlatStyle = FlatStyle.Flat
                };
                dgvHSTC.Columns.Add(btnEditColumn);
            }
            if (dgvKhachHang.Columns["colEditKH"] == null)
            {
                var btnEditColumn = new DataGridViewButtonColumn
                {
                    Name = "colEditKH",
                    HeaderText = "Sửa",
                    Text = "✏️ Sửa",
                    UseColumnTextForButtonValue = true,
                    Width = 80,
                    FlatStyle = FlatStyle.Flat
                };
                dgvKhachHang.Columns.Add(btnEditColumn);
            }
            ConfigureDataGridViewKHStyling();
            SetupContextMenuKH();
        }
        private void ConfigureDataGridViewHSTCStyling()
        {
            var headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.FromArgb(41, 128, 185),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(5)
            };
            dgvHSTC.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvHSTC.ColumnHeadersHeight = 45;
            dgvHSTC.EnableHeadersVisualStyles = false;

            // căn giữa các cột ID/ngày
            string[] center = { "colMaHSTC", "colGioiTinhHS", "colNgaySinhHS", "colCCCDHS" };
            foreach (var name in center)
                if (dgvHSTC.Columns[name] != null)
                    dgvHSTC.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvHSTC.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvHSTC.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            dgvHSTC.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvHSTC.RowTemplate.Height = 40;
            dgvHSTC.BorderStyle = BorderStyle.None;
            dgvHSTC.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHSTC.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            dgvHSTC.RowHeadersVisible = false;
        }


        private void HoSoTiemChungControl_Load(object sender, EventArgs e)
        {
            InitializeFilters();
            LoadDSHSTC();
            LoadDSKHHG();
            SetupEventHandlers();
            AdjustTitlePositions();
        }

        private void AdjustTitlePositions()
        {
            // Canh giữa tiêu đề "QUẢN LÝ HỒ SƠ TIÊM CHỦNG"
            lblTitle.Left = (panelHeader.Width - lblTitle.Width) / 2;
            lblTitle.Top = (panelHeader.Height - lblTitle.Height) / 2;

            // Canh giữa tiêu đề "QUẢN LÝ KHÁCH HÀNG"
            label1.Left = (panel1.Width - label1.Width) / 2;
            label1.Top = (panel1.Height - label1.Height) / 2;
        }

        private void InitializeFilters()
        {
            // Set default values - removed old filters
        }

        private void SetupEventHandlers()
        {
            btnSearch.Click += BtnSearch_Click;
            btnReset.Click += BtnReset_Click;

            // Thêm event handlers cho phần Khách hàng
            button4.Click += BtnSearchKH_Click;
            button3.Click += BtnResetKH_Click;
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

            // Hover effects cho buttons Khách hàng
            button4.MouseEnter += (s, e) =>
      {
          button4.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
      };
            button4.MouseLeave += (s, e) =>
              {
                  button4.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
              };

            button3.MouseEnter += (s, e) =>
                    {
                        button3.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
                    };
            button3.MouseLeave += (s, e) =>
             {
                 button3.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
             };
        }

        private void LoadDSHSTC()
        {
            dtHSTC = HSCT_bll.GetHSTC_KHHG();
            BindDataToGridHSTC(dtHSTC);
        }

        private void BindDataToGridHSTC(DataTable dt)
        {
            dgvHSTC.AutoGenerateColumns = false;

            colMaHSTC.DataPropertyName = "MaHSTC";
            colHoTenHS.DataPropertyName = "HoTen";
            colGioiTinhHS.DataPropertyName = "GioiTinh";
            colNgaySinhHS.DataPropertyName = "NgaySinh";
            colHoTenKHHGHS.DataPropertyName = "TenKhachHang";
            colQuanHeHS.DataPropertyName = "VaiTro";
            colCCCDHS.DataPropertyName = "CCCDHS";
            colSoDTKhHSTC.DataPropertyName = "SoDT";
            colMaKHHSTC.DataPropertyName = "MaKH";
            dgvHSTC.DataSource = dt;

            dgvHSTC.Columns["colNgaySinhHS"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvHSTC.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvHSTC.RowTemplate.Height = 36;
        }



        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var filtered = dtHSTC.Clone();

            string kwName = txtSearchName.Text.Trim().ToLower();
            string kwMaHS = txtSearchRecordId.Text.Trim().ToLower();
            string kwCCCD = txtSearchCustomerId.Text.Trim().ToLower();

            foreach (DataRow row in dtHSTC.Rows)
            {
                bool match = true;
                if (!string.IsNullOrEmpty(kwName) &&
              !(row["HoTen"]?.ToString() ?? "").ToLower().Contains(kwName)) match = false;

                if (!string.IsNullOrEmpty(kwMaHS) &&
              !(row["MaHSTC"]?.ToString() ?? "").ToLower().Contains(kwMaHS)) match = false;

                // Tìm kiếm CCCD từ dtHSTCFull
                if (!string.IsNullOrEmpty(kwCCCD) &&
              !(row["CCCDHS"]?.ToString() ?? "").ToLower().Contains(kwCCCD)) match = false;

                if (match) filtered.ImportRow(row);
            }

            BindDataToGridHSTC(filtered);

            if (filtered.Rows.Count == 0)
                MessageBox.Show("Không tìm thấy kết quả phù hợp!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            BindDataToGridHSTC(dtHSTC);
        }

        // Public method để refresh data từ bên ngoài
        public void RefreshData()
        {
            LoadDSHSTC();
            LoadDSKHHG();
        }


        private void SetupContextMenuHSTC()
        {
            // Tạo Context Menu
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Menu item: Xem thông tin
            ToolStripMenuItem viewInfoItem = new ToolStripMenuItem("📄 Xem thông tin");
            viewInfoItem.Click += ViewInfo_Click_HSTC;
            contextMenu.Items.Add(viewInfoItem);

            // Menu item: Sửa thông tin
            ToolStripMenuItem editInfoItem = new ToolStripMenuItem("✏️ Sửa thông tin");
            editInfoItem.Click += EditInfo_Click_HSTC;
            contextMenu.Items.Add(editInfoItem);

            // Separator
            contextMenu.Items.Add(new ToolStripSeparator());

            // Menu item: Thêm mũi tiêm
            ToolStripMenuItem addDoseItem = new ToolStripMenuItem("💉 Thêm mũi tiêm");
            addDoseItem.Click += btnThemMuiTiem_Click;
            contextMenu.Items.Add(addDoseItem);

            // Gán context menu cho DataGridView
            dgvHSTC.ContextMenuStrip = contextMenu;

            // Xử lý sự kiện mở context menu để lấy thông tin dòng được chọn
            dgvHSTC.MouseDown += Xo_MouseDown;
        }

        private void Xo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var hitTest = dgvHSTC.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    // Chọn dòng được click chuột phải
                    dgvHSTC.ClearSelection();
                    dgvHSTC.Rows[hitTest.RowIndex].Selected = true;
                    selectedHSTCRowIndex = hitTest.RowIndex;
                }
                else
                {
                    selectedHSTCRowIndex = -1;
                }
            }
        }

        private void ViewInfo_Click_HSTC(object sender, EventArgs e)
        {
            if (selectedHSTCRowIndex >= 0 && selectedHSTCRowIndex < dgvHSTC.Rows.Count)
            {
                string maHSTC = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colMaHSTC"].Value?.ToString() ?? "";
                string hoTen = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colHoTenHS"].Value?.ToString() ?? "";
                string gt = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colGioiTinhHS"].Value?.ToString() ?? "";
                var valNgaySinh = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colNgaySinhHS"].Value;
                string ns =
          valNgaySinh is DateTime dt ? dt.ToString("dd-MM-yyyy") :
           DateTime.TryParse(valNgaySinh?.ToString(), out var d) ? d.ToString("dd-MM-yyyy") :
            "";
                string quanhe = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colQuanHeHS"].Value?.ToString() ?? "";
                string tenKH = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colHoTenKHHGHS"].Value?.ToString() ?? "";

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

        private void EditInfo_Click_HSTC(object sender, EventArgs e)
        {
            if (selectedHSTCRowIndex >= 0 && selectedHSTCRowIndex < dgvHSTC.Rows.Count)
            {
                string maHSTC = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colMaHSTC"].Value?.ToString() ?? "";

                if (string.IsNullOrEmpty(maHSTC))
                {
                    MessageBox.Show("Không tìm thấy mã hồ sơ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmEditHSTC frmEdit = new frmEditHSTC();
                frmEdit.LoadHoSoTiemChungData(maHSTC);

                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadDSHSTC();
                }
            }
        }

        // ============================================================================== Khách hàng
        private void ConfigureDataGridViewKHStyling()
        {
            // Header style
            var headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.FromArgb(41, 128, 185),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(5)
            };
            dgvKhachHang.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvKhachHang.ColumnHeadersHeight = 45;
            dgvKhachHang.EnableHeadersVisualStyles = false;

            // Căn giữa các cột: Mã KH, Ngày sinh, Giới tính, Số ĐT
            string[] centerColumns = { "colMaKH", "colNgaySinhKH", "colGioiTinhKH", "colSoDTKH", "colCCCDKH" };
            foreach (var name in centerColumns)
            {
                if (dgvKhachHang.Columns[name] != null)
                    dgvKhachHang.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Đổi font chữ thành không in đậm
            dgvKhachHang.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvKhachHang.RowTemplate.Height = 40;
            dgvKhachHang.BorderStyle = BorderStyle.None;
            dgvKhachHang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvKhachHang.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            dgvKhachHang.RowHeadersVisible = false;
        }

        private void SetupContextMenuKH()
        {
            // Tạo Context Menu
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Menu item: Xem thông tin
            ToolStripMenuItem viewInfoItem = new ToolStripMenuItem("📄 Xem thông tin");
            viewInfoItem.Click += ViewInfo_Click_KH;
            contextMenu.Items.Add(viewInfoItem);

            // Menu item: Sửa thông tin
            ToolStripMenuItem editInfoItem = new ToolStripMenuItem("✏️ Sửa thông tin");
            editInfoItem.Click += EditInfo_Click_KH;
            contextMenu.Items.Add(editInfoItem);

            // Gán context menu cho DataGridView
            dgvKhachHang.ContextMenuStrip = contextMenu;

            // Xử lý sự kiện mở context menu để lấy thông tin dòng được chọn
            dgvKhachHang.MouseDown += DgvKhachHang_MouseDown;
        }

        private void DgvKhachHang_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvKhachHang.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    // Chọn dòng được click chuột phải
                    dgvKhachHang.ClearSelection();
                    dgvKhachHang.Rows[hitTest.RowIndex].Selected = true;
                    selectedKHRowIndex = hitTest.RowIndex;
                }
                else
                {
                    selectedKHRowIndex = -1;
                }
            }
        }

        private void ViewInfo_Click_KH(object sender, EventArgs e)
        {
            if (selectedKHRowIndex >= 0 && selectedKHRowIndex < dgvKhachHang.Rows.Count)
            {
                string maKH = dgvKhachHang.Rows[selectedKHRowIndex].Cells["colMaKH"].Value?.ToString() ?? "";
                if (dtKH.PrimaryKey == null || dtKH.PrimaryKey.Length == 0)
                    dtKH.PrimaryKey = new[] { dtKH.Columns["MaKH"] };
                DataRow dr = dtKH.Rows.Find(maKH);
                string hoTen = dr["HoTen"]?.ToString() ?? "";
                string gioiTinh = dr["GioiTinh"]?.ToString() ?? "";
                string cccd = dr["CCCD"]?.ToString() ?? "";
                var valNgaySinh = dr["NgaySinh"];
                string ngaySinh =
                    valNgaySinh is DateTime dt ? dt.ToString("dd/MM/yyyy") :
               DateTime.TryParse(valNgaySinh?.ToString(), out var d) ? d.ToString("dd/MM/yyyy") :
            "";

                string soDT = dgvKhachHang.Rows[selectedKHRowIndex].Cells["colSoDTKH"].Value?.ToString() ?? "";

                MessageBox.Show(
                 "📋 THÔNG TIN KHÁCH HÀNG\n\n" +
             $"Mã khách hàng: {maKH}\n" +
                $"Họ tên: {hoTen}\n" +
                         $"Giới tính: {gioiTinh}\n" +
               $"Ngày sinh: {ngaySinh}\n" +
             $"CCCD: {cccd}\n" +
           $"Số điện thoại: {soDT}\n",
                      "Thông tin khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditInfo_Click_KH(object sender, EventArgs e)
        {
            if (selectedKHRowIndex >= 0 && selectedKHRowIndex < dgvKhachHang.Rows.Count)
            {
                string maKH = dgvKhachHang.Rows[selectedKHRowIndex].Cells["colMaKH"].Value?.ToString() ?? "";

                if (string.IsNullOrEmpty(maKH))
                {
                    MessageBox.Show("Không tìm thấy mã khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmEditKH frmEdit = new frmEditKH();
                frmEdit.LoadKhachHangData(maKH);

                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadDSKHHG();
                }
            }
        }

        private void BindDataToGridKH(DataTable dt)
        {
            dgvKhachHang.AutoGenerateColumns = false;

            colMaKH.DataPropertyName = "MaKH";
            colHoTenKH.DataPropertyName = "HoTen";
            colGioiTinhKH.DataPropertyName = "GioiTinh";
            colNgaySinhKH.DataPropertyName = "NgaySinh";
            colSoDTKH.DataPropertyName = "SoDT";
            colCCCDKH.DataPropertyName = "CCCD";

            dgvKhachHang.DataSource = dt;
            dgvKhachHang.Columns["colNgaySinhKH"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvKhachHang.RowTemplate.Height = 36;
        }

        private void LoadDSKHHG()
        {
            dtKH = KH_bll.GetData();
            BindDataToGridKH(dtKH);
        }

        // Chức năng tìm kiếm cho Khách hàng
        private void BtnSearchKH_Click(object sender, EventArgs e)
        {
            var filtered = dtKH.Clone();

            string kwMaKH = textBox2.Text.Trim().ToLower();
            string kwHoTen = textBox1.Text.Trim().ToLower();
            string kwSoDT = txtSearchPhone.Text.Trim().ToLower();
            string kwCCCD = txtCCCDKH.Text.Trim().ToLower();

            foreach (DataRow row in dtKH.Rows)
            {
                bool match = true;

                if (!string.IsNullOrEmpty(kwMaKH) &&
           !(row["MaKH"]?.ToString() ?? "").ToLower().Contains(kwMaKH)) match = false;

                if (!string.IsNullOrEmpty(kwHoTen) &&
             !(row["HoTen"]?.ToString() ?? "").ToLower().Contains(kwHoTen)) match = false;

                if (!string.IsNullOrEmpty(kwSoDT) &&
             !(row["SoDT"]?.ToString() ?? "").ToLower().Contains(kwSoDT)) match = false;

                if (!string.IsNullOrEmpty(kwCCCD) &&
                !(row["CCCD"]?.ToString() ?? "").ToLower().Contains(kwCCCD)) match = false;

                if (match) filtered.ImportRow(row);
            }

            BindDataToGridKH(filtered);

            if (filtered.Rows.Count == 0)
                MessageBox.Show("Không tìm thấy kết quả phù hợp!",
                      "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnResetKH_Click(object sender, EventArgs e)
        {
            // Clear text search boxes
            textBox2.Clear();
            textBox1.Clear();
            txtSearchPhone.Clear();
            txtCCCDKH.Clear();

            // Reload all data
            BindDataToGridKH(dtKH);
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Chỉ chạy khi click vào cột "colEditKH"
            if (e.ColumnIndex == dgvKhachHang.Columns["colEditKH"].Index)
            {
                // Lấy MaKH từ dòng được click
                string maKH = dgvKhachHang.Rows[e.RowIndex].Cells["colMaKH"].Value?.ToString() ?? "";
                if (string.IsNullOrEmpty(maKH))
                {
                    MessageBox.Show("Không tìm thấy mã khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmEditKH frmEdit = new frmEditKH();
                frmEdit.LoadKhachHangData(maKH); // Giả sử frmEditKH có hàm này

                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    // Refresh data sau khi cập nhật thành công
                    RefreshData();
                }
            }

        }
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            frmThemHSTC_KH frmThem = new frmThemHSTC_KH();
            frmThem.ShowDialog();
            if (frmThem.DialogResult == DialogResult.OK)
            {
                RefreshData();
            }
            // Refresh data sau khi đóng form

        }

        private void dgvHSTC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Chỉ chạy khi click vào cột "colEditKH"
            if (e.ColumnIndex == dgvHSTC.Columns["colEditHS"].Index)
            {
                // Lấy MaHSTC từ dòng được click
                string maHSTC = dgvHSTC.Rows[e.RowIndex].Cells["colMaHSTC"].Value?.ToString() ?? "";
                if (string.IsNullOrEmpty(maHSTC))
                {
                    MessageBox.Show("Không tìm thấy mã khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmEditHSTC frmEdit = new frmEditHSTC();
                frmEdit.LoadHoSoTiemChungData(maHSTC);

                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadDSHSTC();
                }
            }
        }

        private void btnThemMuiTiem_Click(object sender, EventArgs e)
        {
            if (selectedHSTCRowIndex >= 0 && selectedHSTCRowIndex < dgvHSTC.Rows.Count)
            {
                string maHSTC = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colMaHSTC"].Value?.ToString() ?? "";
                string hoTen = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colHoTenHS"].Value?.ToString() ?? "";
                string gt = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colGioiTinhHS"].Value?.ToString() ?? "";
                string soDTKH = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colSoDTKhHSTC"].Value?.ToString() ?? "";
                var valNgaySinh = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colNgaySinhHS"].Value;
                string ns =
                  valNgaySinh is DateTime dt ? dt.ToString("dd-MM-yyyy") :
                   DateTime.TryParse(valNgaySinh?.ToString(), out var d) ? d.ToString("dd-MM-yyyy") :
                    "";
                string quanhe = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colQuanHeHS"].Value?.ToString() ?? "";
                string tenKH = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colHoTenKHHGHS"].Value?.ToString() ?? "";
                string maKH = dgvHSTC.Rows[selectedHSTCRowIndex].Cells["colMaKHHSTC"].Value?.ToString() ?? "";

                frmThemMuiTiem formTiem = new frmThemMuiTiem(maHSTC, hoTen, gt, ns,maKH, tenKH, quanhe,soDTKH);
                formTiem.ShowDialog();
            }
        }
    }
}

