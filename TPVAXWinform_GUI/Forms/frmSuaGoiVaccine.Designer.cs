namespace TPVAXWinform_GUI.Forms
{
    partial class frmSuaGoiVaccine
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTieuDe = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlVaccineList = new System.Windows.Forms.Panel();
            this.dgvVaccine = new System.Windows.Forms.DataGridView();
            this.colMaVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiBenh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoMuiToiDa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlVaccineHeader = new System.Windows.Forms.Panel();
            this.btnThemVaoDS = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.lblDanhSachVC = new System.Windows.Forms.Label();
            this.pnlGoiVaccine = new System.Windows.Forms.Panel();
            this.dgvDanhSachChon = new System.Windows.Forms.DataGridView();
            this.colChonMaVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChonTenVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChonGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChonSoMui = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChonGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlGoiHeader = new System.Windows.Forms.Panel();
            this.btnXoaKhoiDS = new System.Windows.Forms.Button();
            this.lblTongGiaValue = new System.Windows.Forms.Label();
            this.lblTongGia = new System.Windows.Forms.Label();
            this.lblDanhSachChon = new System.Windows.Forms.Label();
            this.pnlThongTinGoi = new System.Windows.Forms.Panel();
            this.txtDoiTuong = new System.Windows.Forms.TextBox();
            this.lblDoiTuong = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtTenGoi = new System.Windows.Forms.TextBox();
            this.lblTenGoi = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuuGoi = new System.Windows.Forms.Button();
            this.pnlTieuDe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlVaccineList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccine)).BeginInit();
            this.pnlVaccineHeader.SuspendLayout();
            this.pnlGoiVaccine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachChon)).BeginInit();
            this.pnlGoiHeader.SuspendLayout();
            this.pnlThongTinGoi.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTieuDe
            // 
            this.pnlTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.pnlTieuDe.Controls.Add(this.lblTieuDe);
            this.pnlTieuDe.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTieuDe.Location = new System.Drawing.Point(0, 0);
            this.pnlTieuDe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlTieuDe.Name = "pnlTieuDe";
            this.pnlTieuDe.Size = new System.Drawing.Size(1575, 88);
            this.pnlTieuDe.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1575, 88);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "SỬA GÓI VACCINE";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 88);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pnlVaccineList);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlGoiVaccine);
            this.splitContainer.Size = new System.Drawing.Size(1575, 724);
            this.splitContainer.SplitterDistance = 787;
            this.splitContainer.TabIndex = 1;
            // 
            // pnlVaccineList
            // 
            this.pnlVaccineList.Controls.Add(this.dgvVaccine);
            this.pnlVaccineList.Controls.Add(this.pnlVaccineHeader);
            this.pnlVaccineList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVaccineList.Location = new System.Drawing.Point(0, 0);
            this.pnlVaccineList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlVaccineList.Name = "pnlVaccineList";
            this.pnlVaccineList.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.pnlVaccineList.Size = new System.Drawing.Size(787, 724);
            this.pnlVaccineList.TabIndex = 0;
            // 
            // dgvVaccine
            // 
            this.dgvVaccine.AllowUserToAddRows = false;
            this.dgvVaccine.AllowUserToDeleteRows = false;
            this.dgvVaccine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVaccine.BackgroundColor = System.Drawing.Color.White;
            this.dgvVaccine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvVaccine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvVaccine.ColumnHeadersHeight = 35;
            this.dgvVaccine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaVC,
            this.colTenVC,
            this.colLoaiBenh,
            this.colGiaBan,
            this.colSoMuiToiDa});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVaccine.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvVaccine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVaccine.EnableHeadersVisualStyles = false;
            this.dgvVaccine.Location = new System.Drawing.Point(11, 100);
            this.dgvVaccine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvVaccine.Name = "dgvVaccine";
            this.dgvVaccine.ReadOnly = true;
            this.dgvVaccine.RowHeadersVisible = false;
            this.dgvVaccine.RowHeadersWidth = 62;
            this.dgvVaccine.RowTemplate.Height = 30;
            this.dgvVaccine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVaccine.Size = new System.Drawing.Size(765, 612);
            this.dgvVaccine.TabIndex = 1;
            // 
            // colMaVC
            // 
            this.colMaVC.DataPropertyName = "MaVC";
            this.colMaVC.FillWeight = 60F;
            this.colMaVC.HeaderText = "Mã VC";
            this.colMaVC.MinimumWidth = 8;
            this.colMaVC.Name = "colMaVC";
            this.colMaVC.ReadOnly = true;
            // 
            // colTenVC
            // 
            this.colTenVC.DataPropertyName = "TenVC";
            this.colTenVC.FillWeight = 120F;
            this.colTenVC.HeaderText = "Tên Vaccine";
            this.colTenVC.MinimumWidth = 8;
            this.colTenVC.Name = "colTenVC";
            this.colTenVC.ReadOnly = true;
            // 
            // colLoaiBenh
            // 
            this.colLoaiBenh.DataPropertyName = "CacBenhPhongNgua";
            this.colLoaiBenh.HeaderText = "Loại bệnh";
            this.colLoaiBenh.MinimumWidth = 8;
            this.colLoaiBenh.Name = "colLoaiBenh";
            this.colLoaiBenh.ReadOnly = true;
            // 
            // colGiaBan
            // 
            this.colGiaBan.DataPropertyName = "GiaBan";
            this.colGiaBan.FillWeight = 60F;
            this.colGiaBan.HeaderText = "Giá bán";
            this.colGiaBan.MinimumWidth = 8;
            this.colGiaBan.Name = "colGiaBan";
            this.colGiaBan.ReadOnly = true;
            // 
            // colSoMuiToiDa
            // 
            this.colSoMuiToiDa.DataPropertyName = "SoMuiToiDa";
            this.colSoMuiToiDa.FillWeight = 50F;
            this.colSoMuiToiDa.HeaderText = "Số mũi";
            this.colSoMuiToiDa.MinimumWidth = 8;
            this.colSoMuiToiDa.Name = "colSoMuiToiDa";
            this.colSoMuiToiDa.ReadOnly = true;
            // 
            // pnlVaccineHeader
            // 
            this.pnlVaccineHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlVaccineHeader.Controls.Add(this.btnThemVaoDS);
            this.pnlVaccineHeader.Controls.Add(this.txtTimKiem);
            this.pnlVaccineHeader.Controls.Add(this.lblTimKiem);
            this.pnlVaccineHeader.Controls.Add(this.lblDanhSachVC);
            this.pnlVaccineHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVaccineHeader.Location = new System.Drawing.Point(11, 12);
            this.pnlVaccineHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlVaccineHeader.Name = "pnlVaccineHeader";
            this.pnlVaccineHeader.Size = new System.Drawing.Size(765, 88);
            this.pnlVaccineHeader.TabIndex = 0;
            // 
            // btnThemVaoDS
            // 
            this.btnThemVaoDS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemVaoDS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnThemVaoDS.FlatAppearance.BorderSize = 0;
            this.btnThemVaoDS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoDS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemVaoDS.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoDS.Location = new System.Drawing.Point(630, 25);
            this.btnThemVaoDS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThemVaoDS.Name = "btnThemVaoDS";
            this.btnThemVaoDS.Size = new System.Drawing.Size(124, 44);
            this.btnThemVaoDS.TabIndex = 3;
            this.btnThemVaoDS.Text = "Thêm >>";
            this.btnThemVaoDS.UseVisualStyleBackColor = false;
            this.btnThemVaoDS.Click += new System.EventHandler(this.btnThemVaoDS_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.Location = new System.Drawing.Point(315, 28);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(292, 34);
            this.txtTimKiem.TabIndex = 2;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.Location = new System.Drawing.Point(225, 34);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(95, 25);
            this.lblTimKiem.TabIndex = 1;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // lblDanhSachVC
            // 
            this.lblDanhSachVC.AutoSize = true;
            this.lblDanhSachVC.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDanhSachVC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblDanhSachVC.Location = new System.Drawing.Point(11, 29);
            this.lblDanhSachVC.Name = "lblDanhSachVC";
            this.lblDanhSachVC.Size = new System.Drawing.Size(240, 30);
            this.lblDanhSachVC.TabIndex = 0;
            this.lblDanhSachVC.Text = "DANH SÁCH VACCINE";
            // 
            // pnlGoiVaccine
            // 
            this.pnlGoiVaccine.Controls.Add(this.dgvDanhSachChon);
            this.pnlGoiVaccine.Controls.Add(this.pnlGoiHeader);
            this.pnlGoiVaccine.Controls.Add(this.pnlThongTinGoi);
            this.pnlGoiVaccine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGoiVaccine.Location = new System.Drawing.Point(0, 0);
            this.pnlGoiVaccine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGoiVaccine.Name = "pnlGoiVaccine";
            this.pnlGoiVaccine.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.pnlGoiVaccine.Size = new System.Drawing.Size(784, 724);
            this.pnlGoiVaccine.TabIndex = 0;
            // 
            // dgvDanhSachChon
            // 
            this.dgvDanhSachChon.AllowUserToAddRows = false;
            this.dgvDanhSachChon.AllowUserToDeleteRows = false;
            this.dgvDanhSachChon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachChon.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSachChon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvDanhSachChon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDanhSachChon.ColumnHeadersHeight = 35;
            this.dgvDanhSachChon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChonMaVC,
            this.colChonTenVC,
            this.colChonGiaBan,
            this.colChonSoMui,
            this.colChonGhiChu});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhSachChon.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDanhSachChon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachChon.EnableHeadersVisualStyles = false;
            this.dgvDanhSachChon.Location = new System.Drawing.Point(11, 275);
            this.dgvDanhSachChon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvDanhSachChon.Name = "dgvDanhSachChon";
            this.dgvDanhSachChon.RowHeadersVisible = false;
            this.dgvDanhSachChon.RowHeadersWidth = 62;
            this.dgvDanhSachChon.RowTemplate.Height = 30;
            this.dgvDanhSachChon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachChon.Size = new System.Drawing.Size(762, 437);
            this.dgvDanhSachChon.TabIndex = 2;
            this.dgvDanhSachChon.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachChon_CellEndEdit);
            // 
            // colChonMaVC
            // 
            this.colChonMaVC.DataPropertyName = "MaVC";
            this.colChonMaVC.FillWeight = 60F;
            this.colChonMaVC.HeaderText = "Mã VC";
            this.colChonMaVC.MinimumWidth = 8;
            this.colChonMaVC.Name = "colChonMaVC";
            this.colChonMaVC.ReadOnly = true;
            // 
            // colChonTenVC
            // 
            this.colChonTenVC.DataPropertyName = "TenVC";
            this.colChonTenVC.FillWeight = 120F;
            this.colChonTenVC.HeaderText = "Tên Vaccine";
            this.colChonTenVC.MinimumWidth = 8;
            this.colChonTenVC.Name = "colChonTenVC";
            this.colChonTenVC.ReadOnly = true;
            // 
            // colChonGiaBan
            // 
            this.colChonGiaBan.DataPropertyName = "GiaBan";
            this.colChonGiaBan.FillWeight = 60F;
            this.colChonGiaBan.HeaderText = "Giá bán";
            this.colChonGiaBan.MinimumWidth = 8;
            this.colChonGiaBan.Name = "colChonGiaBan";
            this.colChonGiaBan.ReadOnly = true;
            // 
            // colChonSoMui
            // 
            this.colChonSoMui.DataPropertyName = "SoMui";
            this.colChonSoMui.FillWeight = 40F;
            this.colChonSoMui.HeaderText = "Số mũi";
            this.colChonSoMui.MinimumWidth = 8;
            this.colChonSoMui.Name = "colChonSoMui";
            // 
            // colChonGhiChu
            // 
            this.colChonGhiChu.DataPropertyName = "GhiChu";
            this.colChonGhiChu.FillWeight = 80F;
            this.colChonGhiChu.HeaderText = "Ghi chú";
            this.colChonGhiChu.MinimumWidth = 8;
            this.colChonGhiChu.Name = "colChonGhiChu";
            // 
            // pnlGoiHeader
            // 
            this.pnlGoiHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlGoiHeader.Controls.Add(this.btnXoaKhoiDS);
            this.pnlGoiHeader.Controls.Add(this.lblTongGiaValue);
            this.pnlGoiHeader.Controls.Add(this.lblTongGia);
            this.pnlGoiHeader.Controls.Add(this.lblDanhSachChon);
            this.pnlGoiHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGoiHeader.Location = new System.Drawing.Point(11, 200);
            this.pnlGoiHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGoiHeader.Name = "pnlGoiHeader";
            this.pnlGoiHeader.Size = new System.Drawing.Size(762, 75);
            this.pnlGoiHeader.TabIndex = 1;
            // 
            // btnXoaKhoiDS
            // 
            this.btnXoaKhoiDS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoaKhoiDS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnXoaKhoiDS.FlatAppearance.BorderSize = 0;
            this.btnXoaKhoiDS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaKhoiDS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaKhoiDS.ForeColor = System.Drawing.Color.White;
            this.btnXoaKhoiDS.Location = new System.Drawing.Point(628, 15);
            this.btnXoaKhoiDS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXoaKhoiDS.Name = "btnXoaKhoiDS";
            this.btnXoaKhoiDS.Size = new System.Drawing.Size(124, 44);
            this.btnXoaKhoiDS.TabIndex = 3;
            this.btnXoaKhoiDS.Text = "Xóa";
            this.btnXoaKhoiDS.UseVisualStyleBackColor = false;
            this.btnXoaKhoiDS.Click += new System.EventHandler(this.btnXoaKhoiDS_Click);
            // 
            // lblTongGiaValue
            // 
            this.lblTongGiaValue.AutoSize = true;
            this.lblTongGiaValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongGiaValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblTongGiaValue.Location = new System.Drawing.Point(428, 22);
            this.lblTongGiaValue.Name = "lblTongGiaValue";
            this.lblTongGiaValue.Size = new System.Drawing.Size(46, 30);
            this.lblTongGiaValue.TabIndex = 2;
            this.lblTongGiaValue.Text = "0 đ";
            // 
            // lblTongGia
            // 
            this.lblTongGia.AutoSize = true;
            this.lblTongGia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTongGia.Location = new System.Drawing.Point(315, 25);
            this.lblTongGia.Name = "lblTongGia";
            this.lblTongGia.Size = new System.Drawing.Size(100, 28);
            this.lblTongGia.TabIndex = 1;
            this.lblTongGia.Text = "Tổng giá:";
            // 
            // lblDanhSachChon
            // 
            this.lblDanhSachChon.AutoSize = true;
            this.lblDanhSachChon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDanhSachChon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.lblDanhSachChon.Location = new System.Drawing.Point(11, 22);
            this.lblDanhSachChon.Name = "lblDanhSachChon";
            this.lblDanhSachChon.Size = new System.Drawing.Size(234, 30);
            this.lblDanhSachChon.TabIndex = 0;
            this.lblDanhSachChon.Text = "VACCINE TRONG GÓI";
            // 
            // pnlThongTinGoi
            // 
            this.pnlThongTinGoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlThongTinGoi.Controls.Add(this.txtDoiTuong);
            this.pnlThongTinGoi.Controls.Add(this.lblDoiTuong);
            this.pnlThongTinGoi.Controls.Add(this.txtMoTa);
            this.pnlThongTinGoi.Controls.Add(this.lblMoTa);
            this.pnlThongTinGoi.Controls.Add(this.txtTenGoi);
            this.pnlThongTinGoi.Controls.Add(this.lblTenGoi);
            this.pnlThongTinGoi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTinGoi.Location = new System.Drawing.Point(11, 12);
            this.pnlThongTinGoi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlThongTinGoi.Name = "pnlThongTinGoi";
            this.pnlThongTinGoi.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.pnlThongTinGoi.Size = new System.Drawing.Size(762, 188);
            this.pnlThongTinGoi.TabIndex = 0;
            // 
            // txtDoiTuong
            // 
            this.txtDoiTuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDoiTuong.Location = new System.Drawing.Point(146, 131);
            this.txtDoiTuong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDoiTuong.Name = "txtDoiTuong";
            this.txtDoiTuong.Size = new System.Drawing.Size(596, 34);
            this.txtDoiTuong.TabIndex = 5;
            // 
            // lblDoiTuong
            // 
            this.lblDoiTuong.AutoSize = true;
            this.lblDoiTuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDoiTuong.Location = new System.Drawing.Point(15, 135);
            this.lblDoiTuong.Name = "lblDoiTuong";
            this.lblDoiTuong.Size = new System.Drawing.Size(114, 28);
            this.lblDoiTuong.TabIndex = 4;
            this.lblDoiTuong.Text = "Đối tượng:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMoTa.Location = new System.Drawing.Point(146, 75);
            this.txtMoTa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(596, 34);
            this.txtMoTa.TabIndex = 3;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMoTa.Location = new System.Drawing.Point(15, 79);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(73, 28);
            this.lblMoTa.TabIndex = 2;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // txtTenGoi
            // 
            this.txtTenGoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenGoi.Location = new System.Drawing.Point(146, 19);
            this.txtTenGoi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenGoi.Name = "txtTenGoi";
            this.txtTenGoi.Size = new System.Drawing.Size(596, 34);
            this.txtTenGoi.TabIndex = 1;
            // 
            // lblTenGoi
            // 
            this.lblTenGoi.AutoSize = true;
            this.lblTenGoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenGoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblTenGoi.Location = new System.Drawing.Point(15, 22);
            this.lblTenGoi.Name = "lblTenGoi";
            this.lblTenGoi.Size = new System.Drawing.Size(101, 28);
            this.lblTenGoi.TabIndex = 0;
            this.lblTenGoi.Text = "Tên gói: *";
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.White;
            this.pnlButton.Controls.Add(this.btnHuy);
            this.pnlButton.Controls.Add(this.btnLuuGoi);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 812);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(1575, 88);
            this.pnlButton.TabIndex = 2;
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(878, 15);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(169, 56);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuuGoi
            // 
            this.btnLuuGoi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLuuGoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnLuuGoi.FlatAppearance.BorderSize = 0;
            this.btnLuuGoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuGoi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuuGoi.ForeColor = System.Drawing.Color.White;
            this.btnLuuGoi.Location = new System.Drawing.Point(529, 15);
            this.btnLuuGoi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLuuGoi.Name = "btnLuuGoi";
            this.btnLuuGoi.Size = new System.Drawing.Size(169, 56);
            this.btnLuuGoi.TabIndex = 0;
            this.btnLuuGoi.Text = "Lưu thay đổi";
            this.btnLuuGoi.UseVisualStyleBackColor = false;
            this.btnLuuGoi.Click += new System.EventHandler(this.btnLuuGoi_Click);
            // 
            // frmSuaGoiVaccine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1575, 900);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.pnlTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSuaGoiVaccine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa gói vaccine";
            this.Load += new System.EventHandler(this.frmSuaGoiVaccine_Load);
            this.pnlTieuDe.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlVaccineList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccine)).EndInit();
            this.pnlVaccineHeader.ResumeLayout(false);
            this.pnlVaccineHeader.PerformLayout();
            this.pnlGoiVaccine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachChon)).EndInit();
            this.pnlGoiHeader.ResumeLayout(false);
            this.pnlGoiHeader.PerformLayout();
            this.pnlThongTinGoi.ResumeLayout(false);
            this.pnlThongTinGoi.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlVaccineList;
        private System.Windows.Forms.DataGridView dgvVaccine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiBenh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoMuiToiDa;
        private System.Windows.Forms.Panel pnlVaccineHeader;
        private System.Windows.Forms.Button btnThemVaoDS;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Label lblDanhSachVC;
        private System.Windows.Forms.Panel pnlGoiVaccine;
        private System.Windows.Forms.DataGridView dgvDanhSachChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChonMaVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChonTenVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChonGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChonSoMui;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChonGhiChu;
        private System.Windows.Forms.Panel pnlGoiHeader;
        private System.Windows.Forms.Button btnXoaKhoiDS;
        private System.Windows.Forms.Label lblTongGiaValue;
        private System.Windows.Forms.Label lblTongGia;
        private System.Windows.Forms.Label lblDanhSachChon;
        private System.Windows.Forms.Panel pnlThongTinGoi;
        private System.Windows.Forms.TextBox txtDoiTuong;
        private System.Windows.Forms.Label lblDoiTuong;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtTenGoi;
        private System.Windows.Forms.Label lblTenGoi;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuuGoi;
    }
}
