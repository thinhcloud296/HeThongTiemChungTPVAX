namespace TPVAXWinform_GUI
{
    partial class frmThemMuiTiem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
        if (disposing && (components != null))
{
     components.Dispose();
  }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grpDanhSachCho = new System.Windows.Forms.GroupBox();
            this.dgvVaccineWait = new System.Windows.Forms.DataGridView();
            this.colMaVCW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenVCW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiBenhW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiVCW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTiemW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNuocSXW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaBanW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChuW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpNhapLieu = new System.Windows.Forms.GroupBox();
            this.dgvVaccine = new System.Windows.Forms.DataGridView();
            this.colMaVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiBenh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNuocSX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlVaccineFilter = new System.Windows.Forms.Panel();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnThemMuiTiem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblNgayTiem = new System.Windows.Forms.Label();
            this.dtpNgayTiem = new System.Windows.Forms.DateTimePicker();
            this.pnlThongTinTiem = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.btnTimKiemVaccine = new System.Windows.Forms.Button();
            this.cboLoaiBenh = new System.Windows.Forms.ComboBox();
            this.lblLoaiBenh = new System.Windows.Forms.Label();
            this.cboLoaiVaccine = new System.Windows.Forms.ComboBox();
            this.lblLoaiVaccine = new System.Windows.Forms.Label();
            this.grpThongTinHoSo = new System.Windows.Forms.GroupBox();
            this.lbMaHSTC = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSoDTValue = new System.Windows.Forms.Label();
            this.lblSoDT = new System.Windows.Forms.Label();
            this.lblQuanHeValue = new System.Windows.Forms.Label();
            this.lblQuanHe = new System.Windows.Forms.Label();
            this.lblTenKhachHangValue = new System.Windows.Forms.Label();
            this.lblTenKhachHang = new System.Windows.Forms.Label();
            this.lblGioiTinhValue = new System.Windows.Forms.Label();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.lblNgaySinhValue = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblTenNguoiTiemValue = new System.Windows.Forms.Label();
            this.lblTenNguoiTiem = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lbTongSoMui = new System.Windows.Forms.Label();
            this.lbTongGia = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLuuTatCa = new System.Windows.Forms.Button();
            this.colMaVaccineWait = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenVaccineWait = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongWait = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTiemWait = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNuocSXWait = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.grpDanhSachCho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccineWait)).BeginInit();
            this.grpNhapLieu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccine)).BeginInit();
            this.pnlVaccineFilter.SuspendLayout();
            this.pnlThongTinTiem.SuspendLayout();
            this.grpThongTinHoSo.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlMain.Controls.Add(this.grpDanhSachCho);
            this.pnlMain.Controls.Add(this.grpNhapLieu);
            this.pnlMain.Controls.Add(this.grpThongTinHoSo);
            this.pnlMain.Controls.Add(this.pnlActions);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMain.Size = new System.Drawing.Size(2014, 1410);
            this.pnlMain.TabIndex = 0;
            // 
            // grpDanhSachCho
            // 
            this.grpDanhSachCho.Controls.Add(this.dgvVaccineWait);
            this.grpDanhSachCho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDanhSachCho.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpDanhSachCho.Location = new System.Drawing.Point(15, 675);
            this.grpDanhSachCho.Name = "grpDanhSachCho";
            this.grpDanhSachCho.Padding = new System.Windows.Forms.Padding(10);
            this.grpDanhSachCho.Size = new System.Drawing.Size(1984, 628);
            this.grpDanhSachCho.TabIndex = 3;
            this.grpDanhSachCho.TabStop = false;
            this.grpDanhSachCho.Text = "Danh sách mũi tiêm chờ lưu";
            // 
            // dgvVaccineWait
            // 
            this.dgvVaccineWait.AllowUserToAddRows = false;
            this.dgvVaccineWait.AllowUserToDeleteRows = false;
            this.dgvVaccineWait.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVaccineWait.BackgroundColor = System.Drawing.Color.White;
            this.dgvVaccineWait.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVaccineWait.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvVaccineWait.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVaccineWait.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaVCW,
            this.colTenVCW,
            this.colLoaiBenhW,
            this.colLoaiVCW,
            this.colNgayTiemW,
            this.colSoLuongW,
            this.colNuocSXW,
            this.colGiaBanW,
            this.colGhiChuW});
            this.dgvVaccineWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVaccineWait.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvVaccineWait.Location = new System.Drawing.Point(10, 37);
            this.dgvVaccineWait.Name = "dgvVaccineWait";
            this.dgvVaccineWait.ReadOnly = true;
            this.dgvVaccineWait.RowHeadersVisible = false;
            this.dgvVaccineWait.RowHeadersWidth = 62;
            this.dgvVaccineWait.RowTemplate.Height = 40;
            this.dgvVaccineWait.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVaccineWait.Size = new System.Drawing.Size(1964, 581);
            this.dgvVaccineWait.TabIndex = 3;
            this.dgvVaccineWait.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvVaccineWait_RowsAdded);
            this.dgvVaccineWait.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvVaccineWait_RowsRemoved);
            // 
            // colMaVCW
            // 
            this.colMaVCW.DataPropertyName = "MaVC";
            this.colMaVCW.FillWeight = 60F;
            this.colMaVCW.HeaderText = "Mã Vaccine";
            this.colMaVCW.MinimumWidth = 8;
            this.colMaVCW.Name = "colMaVCW";
            this.colMaVCW.ReadOnly = true;
            // 
            // colTenVCW
            // 
            this.colTenVCW.DataPropertyName = "TenVC";
            this.colTenVCW.FillWeight = 120F;
            this.colTenVCW.HeaderText = "Tên Vaccine";
            this.colTenVCW.MinimumWidth = 8;
            this.colTenVCW.Name = "colTenVCW";
            this.colTenVCW.ReadOnly = true;
            // 
            // colLoaiBenhW
            // 
            this.colLoaiBenhW.DataPropertyName = "LoaiBenh";
            this.colLoaiBenhW.FillWeight = 80F;
            this.colLoaiBenhW.HeaderText = "Loại bệnh";
            this.colLoaiBenhW.MinimumWidth = 8;
            this.colLoaiBenhW.Name = "colLoaiBenhW";
            this.colLoaiBenhW.ReadOnly = true;
            // 
            // colLoaiVCW
            // 
            this.colLoaiVCW.DataPropertyName = "LoaiVC";
            this.colLoaiVCW.FillWeight = 80F;
            this.colLoaiVCW.HeaderText = "Loại Vaccine";
            this.colLoaiVCW.MinimumWidth = 8;
            this.colLoaiVCW.Name = "colLoaiVCW";
            this.colLoaiVCW.ReadOnly = true;
            // 
            // colNgayTiemW
            // 
            this.colNgayTiemW.HeaderText = "Ngày tiêm";
            this.colNgayTiemW.MinimumWidth = 8;
            this.colNgayTiemW.Name = "colNgayTiemW";
            this.colNgayTiemW.ReadOnly = true;
            // 
            // colSoLuongW
            // 
            this.colSoLuongW.HeaderText = "Số Lượng";
            this.colSoLuongW.MinimumWidth = 8;
            this.colSoLuongW.Name = "colSoLuongW";
            this.colSoLuongW.ReadOnly = true;
            // 
            // colNuocSXW
            // 
            this.colNuocSXW.DataPropertyName = "NuocSX";
            this.colNuocSXW.FillWeight = 70F;
            this.colNuocSXW.HeaderText = "Nước sản xuất";
            this.colNuocSXW.MinimumWidth = 8;
            this.colNuocSXW.Name = "colNuocSXW";
            this.colNuocSXW.ReadOnly = true;
            // 
            // colGiaBanW
            // 
            this.colGiaBanW.DataPropertyName = "GiaBan";
            this.colGiaBanW.FillWeight = 70F;
            this.colGiaBanW.HeaderText = "Giá bán";
            this.colGiaBanW.MinimumWidth = 8;
            this.colGiaBanW.Name = "colGiaBanW";
            this.colGiaBanW.ReadOnly = true;
            // 
            // colGhiChuW
            // 
            this.colGhiChuW.HeaderText = "Ghi Chú";
            this.colGhiChuW.MinimumWidth = 8;
            this.colGhiChuW.Name = "colGhiChuW";
            this.colGhiChuW.ReadOnly = true;
            // 
            // grpNhapLieu
            // 
            this.grpNhapLieu.Controls.Add(this.dgvVaccine);
            this.grpNhapLieu.Controls.Add(this.pnlVaccineFilter);
            this.grpNhapLieu.Controls.Add(this.pnlThongTinTiem);
            this.grpNhapLieu.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpNhapLieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpNhapLieu.Location = new System.Drawing.Point(15, 190);
            this.grpNhapLieu.Name = "grpNhapLieu";
            this.grpNhapLieu.Padding = new System.Windows.Forms.Padding(10);
            this.grpNhapLieu.Size = new System.Drawing.Size(1984, 485);
            this.grpNhapLieu.TabIndex = 2;
            this.grpNhapLieu.TabStop = false;
            this.grpNhapLieu.Text = "Thêm Mũi tiêm";
            // 
            // dgvVaccine
            // 
            this.dgvVaccine.AllowUserToAddRows = false;
            this.dgvVaccine.AllowUserToDeleteRows = false;
            this.dgvVaccine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVaccine.BackgroundColor = System.Drawing.Color.White;
            this.dgvVaccine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVaccine.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvVaccine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVaccine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaVC,
            this.colTenVC,
            this.colLoaiBenh,
            this.colLoaiVC,
            this.colNuocSX,
            this.colGiaBan});
            this.dgvVaccine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVaccine.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvVaccine.Location = new System.Drawing.Point(10, 149);
            this.dgvVaccine.Name = "dgvVaccine";
            this.dgvVaccine.ReadOnly = true;
            this.dgvVaccine.RowHeadersVisible = false;
            this.dgvVaccine.RowHeadersWidth = 62;
            this.dgvVaccine.RowTemplate.Height = 40;
            this.dgvVaccine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVaccine.Size = new System.Drawing.Size(1964, 326);
            this.dgvVaccine.TabIndex = 2;
            // 
            // colMaVC
            // 
            this.colMaVC.DataPropertyName = "MaVC";
            this.colMaVC.FillWeight = 60F;
            this.colMaVC.HeaderText = "Mã Vaccine";
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
            this.colLoaiBenh.DataPropertyName = "LoaiBenh";
            this.colLoaiBenh.FillWeight = 80F;
            this.colLoaiBenh.HeaderText = "Loại bệnh";
            this.colLoaiBenh.MinimumWidth = 8;
            this.colLoaiBenh.Name = "colLoaiBenh";
            this.colLoaiBenh.ReadOnly = true;
            // 
            // colLoaiVC
            // 
            this.colLoaiVC.DataPropertyName = "LoaiVC";
            this.colLoaiVC.FillWeight = 80F;
            this.colLoaiVC.HeaderText = "Loại Vaccine";
            this.colLoaiVC.MinimumWidth = 8;
            this.colLoaiVC.Name = "colLoaiVC";
            this.colLoaiVC.ReadOnly = true;
            // 
            // colNuocSX
            // 
            this.colNuocSX.DataPropertyName = "NuocSX";
            this.colNuocSX.FillWeight = 70F;
            this.colNuocSX.HeaderText = "Nước sản xuất";
            this.colNuocSX.MinimumWidth = 8;
            this.colNuocSX.Name = "colNuocSX";
            this.colNuocSX.ReadOnly = true;
            // 
            // colGiaBan
            // 
            this.colGiaBan.DataPropertyName = "GiaBan";
            this.colGiaBan.FillWeight = 70F;
            this.colGiaBan.HeaderText = "Giá bán";
            this.colGiaBan.MinimumWidth = 8;
            this.colGiaBan.Name = "colGiaBan";
            this.colGiaBan.ReadOnly = true;
            // 
            // pnlVaccineFilter
            // 
            this.pnlVaccineFilter.BackColor = System.Drawing.Color.White;
            this.pnlVaccineFilter.Controls.Add(this.txtSoLuong);
            this.pnlVaccineFilter.Controls.Add(this.label5);
            this.pnlVaccineFilter.Controls.Add(this.btnThemMuiTiem);
            this.pnlVaccineFilter.Controls.Add(this.label2);
            this.pnlVaccineFilter.Controls.Add(this.txtGhiChu);
            this.pnlVaccineFilter.Controls.Add(this.lblGhiChu);
            this.pnlVaccineFilter.Controls.Add(this.lblNgayTiem);
            this.pnlVaccineFilter.Controls.Add(this.dtpNgayTiem);
            this.pnlVaccineFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVaccineFilter.Location = new System.Drawing.Point(10, 89);
            this.pnlVaccineFilter.Name = "pnlVaccineFilter";
            this.pnlVaccineFilter.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.pnlVaccineFilter.Size = new System.Drawing.Size(1964, 60);
            this.pnlVaccineFilter.TabIndex = 1;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoLuong.Location = new System.Drawing.Point(1624, 15);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(43, 34);
            this.txtSoLuong.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label5.Location = new System.Drawing.Point(1519, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Số lượng:";
            // 
            // btnThemMuiTiem
            // 
            this.btnThemMuiTiem.BackColor = System.Drawing.Color.ForestGreen;
            this.btnThemMuiTiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemMuiTiem.FlatAppearance.BorderSize = 0;
            this.btnThemMuiTiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemMuiTiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemMuiTiem.ForeColor = System.Drawing.Color.White;
            this.btnThemMuiTiem.Location = new System.Drawing.Point(1767, 13);
            this.btnThemMuiTiem.Name = "btnThemMuiTiem";
            this.btnThemMuiTiem.Size = new System.Drawing.Size(184, 38);
            this.btnThemMuiTiem.TabIndex = 15;
            this.btnThemMuiTiem.Text = "Thêm mũi tiêm";
            this.btnThemMuiTiem.UseVisualStyleBackColor = false;
            this.btnThemMuiTiem.Click += new System.EventHandler(this.btnThemMuiTiem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 25);
            this.label2.TabIndex = 14;
            this.label2.Text = "Thông tin mũi tiêm";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(795, 15);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(677, 34);
            this.txtGhiChu.TabIndex = 5;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblGhiChu.Location = new System.Drawing.Point(705, 20);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(84, 25);
            this.lblGhiChu.TabIndex = 4;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // lblNgayTiem
            // 
            this.lblNgayTiem.AutoSize = true;
            this.lblNgayTiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNgayTiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNgayTiem.Location = new System.Drawing.Point(319, 20);
            this.lblNgayTiem.Name = "lblNgayTiem";
            this.lblNgayTiem.Size = new System.Drawing.Size(108, 25);
            this.lblNgayTiem.TabIndex = 0;
            this.lblNgayTiem.Text = "Ngày tiêm:";
            // 
            // dtpNgayTiem
            // 
            this.dtpNgayTiem.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayTiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayTiem.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayTiem.Location = new System.Drawing.Point(433, 15);
            this.dtpNgayTiem.Name = "dtpNgayTiem";
            this.dtpNgayTiem.Size = new System.Drawing.Size(161, 34);
            this.dtpNgayTiem.TabIndex = 1;
            // 
            // pnlThongTinTiem
            // 
            this.pnlThongTinTiem.BackColor = System.Drawing.Color.White;
            this.pnlThongTinTiem.Controls.Add(this.label1);
            this.pnlThongTinTiem.Controls.Add(this.btnResetFilter);
            this.pnlThongTinTiem.Controls.Add(this.btnTimKiemVaccine);
            this.pnlThongTinTiem.Controls.Add(this.cboLoaiBenh);
            this.pnlThongTinTiem.Controls.Add(this.lblLoaiBenh);
            this.pnlThongTinTiem.Controls.Add(this.cboLoaiVaccine);
            this.pnlThongTinTiem.Controls.Add(this.lblLoaiVaccine);
            this.pnlThongTinTiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTinTiem.Location = new System.Drawing.Point(10, 37);
            this.pnlThongTinTiem.Name = "pnlThongTinTiem";
            this.pnlThongTinTiem.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.pnlThongTinTiem.Size = new System.Drawing.Size(1964, 52);
            this.pnlThongTinTiem.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 25);
            this.label1.TabIndex = 13;
            this.label1.Text = "Bộ lọc";
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnResetFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetFilter.FlatAppearance.BorderSize = 0;
            this.btnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnResetFilter.ForeColor = System.Drawing.Color.White;
            this.btnResetFilter.Location = new System.Drawing.Point(1624, 8);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(120, 38);
            this.btnResetFilter.TabIndex = 12;
            this.btnResetFilter.Text = "Đặt lại";
            this.btnResetFilter.UseVisualStyleBackColor = false;
            // 
            // btnTimKiemVaccine
            // 
            this.btnTimKiemVaccine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnTimKiemVaccine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiemVaccine.FlatAppearance.BorderSize = 0;
            this.btnTimKiemVaccine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiemVaccine.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimKiemVaccine.ForeColor = System.Drawing.Color.White;
            this.btnTimKiemVaccine.Location = new System.Drawing.Point(1494, 8);
            this.btnTimKiemVaccine.Name = "btnTimKiemVaccine";
            this.btnTimKiemVaccine.Size = new System.Drawing.Size(120, 38);
            this.btnTimKiemVaccine.TabIndex = 11;
            this.btnTimKiemVaccine.Text = "Tìm kiếm";
            this.btnTimKiemVaccine.UseVisualStyleBackColor = false;
            // 
            // cboLoaiBenh
            // 
            this.cboLoaiBenh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiBenh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiBenh.FormattingEnabled = true;
            this.cboLoaiBenh.Location = new System.Drawing.Point(1172, 9);
            this.cboLoaiBenh.Name = "cboLoaiBenh";
            this.cboLoaiBenh.Size = new System.Drawing.Size(300, 36);
            this.cboLoaiBenh.TabIndex = 10;
            // 
            // lblLoaiBenh
            // 
            this.lblLoaiBenh.AutoSize = true;
            this.lblLoaiBenh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLoaiBenh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblLoaiBenh.Location = new System.Drawing.Point(1062, 15);
            this.lblLoaiBenh.Name = "lblLoaiBenh";
            this.lblLoaiBenh.Size = new System.Drawing.Size(104, 25);
            this.lblLoaiBenh.TabIndex = 9;
            this.lblLoaiBenh.Text = "Loại bệnh:";
            // 
            // cboLoaiVaccine
            // 
            this.cboLoaiVaccine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiVaccine.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiVaccine.FormattingEnabled = true;
            this.cboLoaiVaccine.Location = new System.Drawing.Point(734, 9);
            this.cboLoaiVaccine.Name = "cboLoaiVaccine";
            this.cboLoaiVaccine.Size = new System.Drawing.Size(270, 36);
            this.cboLoaiVaccine.TabIndex = 8;
            // 
            // lblLoaiVaccine
            // 
            this.lblLoaiVaccine.AutoSize = true;
            this.lblLoaiVaccine.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLoaiVaccine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblLoaiVaccine.Location = new System.Drawing.Point(597, 15);
            this.lblLoaiVaccine.Name = "lblLoaiVaccine";
            this.lblLoaiVaccine.Size = new System.Drawing.Size(124, 25);
            this.lblLoaiVaccine.TabIndex = 7;
            this.lblLoaiVaccine.Text = "Loại vaccine:";
            // 
            // grpThongTinHoSo
            // 
            this.grpThongTinHoSo.Controls.Add(this.lbMaHSTC);
            this.grpThongTinHoSo.Controls.Add(this.label3);
            this.grpThongTinHoSo.Controls.Add(this.lblSoDTValue);
            this.grpThongTinHoSo.Controls.Add(this.lblSoDT);
            this.grpThongTinHoSo.Controls.Add(this.lblQuanHeValue);
            this.grpThongTinHoSo.Controls.Add(this.lblQuanHe);
            this.grpThongTinHoSo.Controls.Add(this.lblTenKhachHangValue);
            this.grpThongTinHoSo.Controls.Add(this.lblTenKhachHang);
            this.grpThongTinHoSo.Controls.Add(this.lblGioiTinhValue);
            this.grpThongTinHoSo.Controls.Add(this.lblGioiTinh);
            this.grpThongTinHoSo.Controls.Add(this.lblNgaySinhValue);
            this.grpThongTinHoSo.Controls.Add(this.lblNgaySinh);
            this.grpThongTinHoSo.Controls.Add(this.lblTenNguoiTiemValue);
            this.grpThongTinHoSo.Controls.Add(this.lblTenNguoiTiem);
            this.grpThongTinHoSo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinHoSo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThongTinHoSo.Location = new System.Drawing.Point(15, 15);
            this.grpThongTinHoSo.Name = "grpThongTinHoSo";
            this.grpThongTinHoSo.Padding = new System.Windows.Forms.Padding(10);
            this.grpThongTinHoSo.Size = new System.Drawing.Size(1984, 175);
            this.grpThongTinHoSo.TabIndex = 1;
            this.grpThongTinHoSo.TabStop = false;
            this.grpThongTinHoSo.Text = "Thông tin Hồ sơ";
            // 
            // lbMaHSTC
            // 
            this.lbMaHSTC.AutoSize = true;
            this.lbMaHSTC.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbMaHSTC.Location = new System.Drawing.Point(670, 37);
            this.lbMaHSTC.Name = "lbMaHSTC";
            this.lbMaHSTC.Size = new System.Drawing.Size(24, 28);
            this.lbMaHSTC.TabIndex = 14;
            this.lbMaHSTC.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(555, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 28);
            this.label3.TabIndex = 13;
            this.label3.Text = "Mã hồ sơ:";
            // 
            // lblSoDTValue
            // 
            this.lblSoDTValue.AutoSize = true;
            this.lblSoDTValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoDTValue.Location = new System.Drawing.Point(1361, 123);
            this.lblSoDTValue.Name = "lblSoDTValue";
            this.lblSoDTValue.Size = new System.Drawing.Size(24, 28);
            this.lblSoDTValue.TabIndex = 11;
            this.lblSoDTValue.Text = "...";
            // 
            // lblSoDT
            // 
            this.lblSoDT.AutoSize = true;
            this.lblSoDT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSoDT.Location = new System.Drawing.Point(1178, 123);
            this.lblSoDT.Name = "lblSoDT";
            this.lblSoDT.Size = new System.Drawing.Size(177, 28);
            this.lblSoDT.TabIndex = 10;
            this.lblSoDT.Text = "Số điện thoại KH:";
            // 
            // lblQuanHeValue
            // 
            this.lblQuanHeValue.AutoSize = true;
            this.lblQuanHeValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuanHeValue.Location = new System.Drawing.Point(987, 123);
            this.lblQuanHeValue.Name = "lblQuanHeValue";
            this.lblQuanHeValue.Size = new System.Drawing.Size(24, 28);
            this.lblQuanHeValue.TabIndex = 9;
            this.lblQuanHeValue.Text = "...";
            // 
            // lblQuanHe
            // 
            this.lblQuanHe.AutoSize = true;
            this.lblQuanHe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblQuanHe.Location = new System.Drawing.Point(869, 123);
            this.lblQuanHe.Name = "lblQuanHe";
            this.lblQuanHe.Size = new System.Drawing.Size(96, 28);
            this.lblQuanHe.TabIndex = 8;
            this.lblQuanHe.Text = "Quan hệ:";
            // 
            // lblTenKhachHangValue
            // 
            this.lblTenKhachHangValue.AutoSize = true;
            this.lblTenKhachHangValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTenKhachHangValue.Location = new System.Drawing.Point(670, 123);
            this.lblTenKhachHangValue.Name = "lblTenKhachHangValue";
            this.lblTenKhachHangValue.Size = new System.Drawing.Size(24, 28);
            this.lblTenKhachHangValue.TabIndex = 7;
            this.lblTenKhachHangValue.Text = "...";
            // 
            // lblTenKhachHang
            // 
            this.lblTenKhachHang.AutoSize = true;
            this.lblTenKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenKhachHang.Location = new System.Drawing.Point(464, 123);
            this.lblTenKhachHang.Name = "lblTenKhachHang";
            this.lblTenKhachHang.Size = new System.Drawing.Size(196, 28);
            this.lblTenKhachHang.TabIndex = 6;
            this.lblTenKhachHang.Text = "Họ tên khách hàng:";
            // 
            // lblGioiTinhValue
            // 
            this.lblGioiTinhValue.AutoSize = true;
            this.lblGioiTinhValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGioiTinhValue.Location = new System.Drawing.Point(1361, 80);
            this.lblGioiTinhValue.Name = "lblGioiTinhValue";
            this.lblGioiTinhValue.Size = new System.Drawing.Size(24, 28);
            this.lblGioiTinhValue.TabIndex = 5;
            this.lblGioiTinhValue.Text = "...";
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGioiTinh.Location = new System.Drawing.Point(1255, 80);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(100, 28);
            this.lblGioiTinh.TabIndex = 4;
            this.lblGioiTinh.Text = "Giới tính:";
            // 
            // lblNgaySinhValue
            // 
            this.lblNgaySinhValue.AutoSize = true;
            this.lblNgaySinhValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgaySinhValue.Location = new System.Drawing.Point(987, 80);
            this.lblNgaySinhValue.Name = "lblNgaySinhValue";
            this.lblNgaySinhValue.Size = new System.Drawing.Size(24, 28);
            this.lblNgaySinhValue.TabIndex = 3;
            this.lblNgaySinhValue.Text = "...";
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.Location = new System.Drawing.Point(869, 80);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(112, 28);
            this.lblNgaySinh.TabIndex = 2;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // lblTenNguoiTiemValue
            // 
            this.lblTenNguoiTiemValue.AutoSize = true;
            this.lblTenNguoiTiemValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTenNguoiTiemValue.Location = new System.Drawing.Point(670, 80);
            this.lblTenNguoiTiemValue.Name = "lblTenNguoiTiemValue";
            this.lblTenNguoiTiemValue.Size = new System.Drawing.Size(24, 28);
            this.lblTenNguoiTiemValue.TabIndex = 1;
            this.lblTenNguoiTiemValue.Text = "...";
            // 
            // lblTenNguoiTiem
            // 
            this.lblTenNguoiTiem.AutoSize = true;
            this.lblTenNguoiTiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenNguoiTiem.Location = new System.Drawing.Point(468, 80);
            this.lblTenNguoiTiem.Name = "lblTenNguoiTiem";
            this.lblTenNguoiTiem.Size = new System.Drawing.Size(192, 28);
            this.lblTenNguoiTiem.TabIndex = 0;
            this.lblTenNguoiTiem.Text = "Họ tên người tiêm:";
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.label6);
            this.pnlActions.Controls.Add(this.lbTongSoMui);
            this.pnlActions.Controls.Add(this.lbTongGia);
            this.pnlActions.Controls.Add(this.label4);
            this.pnlActions.Controls.Add(this.btnDong);
            this.pnlActions.Controls.Add(this.btnLuuTatCa);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(15, 1303);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlActions.Size = new System.Drawing.Size(1984, 92);
            this.pnlActions.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1683, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 37);
            this.label6.TabIndex = 5;
            this.label6.Text = "Số mũi:";
            // 
            // lbTongSoMui
            // 
            this.lbTongSoMui.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbTongSoMui.AutoSize = true;
            this.lbTongSoMui.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lbTongSoMui.Location = new System.Drawing.Point(1810, 31);
            this.lbTongSoMui.Name = "lbTongSoMui";
            this.lbTongSoMui.Size = new System.Drawing.Size(99, 32);
            this.lbTongSoMui.TabIndex = 4;
            this.lbTongSoMui.Text = "25 Mũi";
            // 
            // lbTongGia
            // 
            this.lbTongGia.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbTongGia.AutoSize = true;
            this.lbTongGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lbTongGia.Location = new System.Drawing.Point(1427, 31);
            this.lbTongGia.Name = "lbTongGia";
            this.lbTongGia.Size = new System.Drawing.Size(224, 32);
            this.lbTongGia.TabIndex = 3;
            this.lbTongGia.Text = "15.000.000 VNĐ";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1275, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 37);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tổng giá:";
            // 
            // btnDong
            // 
            this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(675, 13);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(223, 73);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            // 
            // btnLuuTatCa
            // 
            this.btnLuuTatCa.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLuuTatCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnLuuTatCa.FlatAppearance.BorderSize = 0;
            this.btnLuuTatCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuTatCa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuuTatCa.ForeColor = System.Drawing.Color.White;
            this.btnLuuTatCa.Location = new System.Drawing.Point(1022, 13);
            this.btnLuuTatCa.Name = "btnLuuTatCa";
            this.btnLuuTatCa.Size = new System.Drawing.Size(223, 73);
            this.btnLuuTatCa.TabIndex = 0;
            this.btnLuuTatCa.Text = "Lưu tất cả";
            this.btnLuuTatCa.UseVisualStyleBackColor = false;
            this.btnLuuTatCa.Click += new System.EventHandler(this.btnLuuTatCa_Click);
            // 
            // colMaVaccineWait
            // 
            this.colMaVaccineWait.DataPropertyName = "MaVC";
            this.colMaVaccineWait.HeaderText = "Mã Vaccine";
            this.colMaVaccineWait.MinimumWidth = 8;
            this.colMaVaccineWait.Name = "colMaVaccineWait";
            this.colMaVaccineWait.Width = 150;
            // 
            // colTenVaccineWait
            // 
            this.colTenVaccineWait.DataPropertyName = "TenVC";
            this.colTenVaccineWait.HeaderText = "Tên Vaccine";
            this.colTenVaccineWait.MinimumWidth = 8;
            this.colTenVaccineWait.Name = "colTenVaccineWait";
            this.colTenVaccineWait.ReadOnly = true;
            this.colTenVaccineWait.Width = 150;
            // 
            // colSoLuongWait
            // 
            this.colSoLuongWait.DataPropertyName = "SoLuong";
            this.colSoLuongWait.HeaderText = "Số lượng";
            this.colSoLuongWait.MinimumWidth = 8;
            this.colSoLuongWait.Name = "colSoLuongWait";
            this.colSoLuongWait.ReadOnly = true;
            this.colSoLuongWait.Width = 150;
            // 
            // colNgayTiemWait
            // 
            this.colNgayTiemWait.DataPropertyName = "NgayTiem";
            this.colNgayTiemWait.HeaderText = "Ngày tiêm";
            this.colNgayTiemWait.MinimumWidth = 8;
            this.colNgayTiemWait.Name = "colNgayTiemWait";
            this.colNgayTiemWait.ReadOnly = true;
            this.colNgayTiemWait.Width = 150;
            // 
            // colNuocSXWait
            // 
            this.colNuocSXWait.DataPropertyName = "NuocSanXuat";
            this.colNuocSXWait.HeaderText = "Nước Sản Xuất";
            this.colNuocSXWait.MinimumWidth = 8;
            this.colNuocSXWait.Name = "colNuocSXWait";
            this.colNuocSXWait.ReadOnly = true;
            this.colNuocSXWait.Width = 150;
            // 
            // frmThemMuiTiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2014, 1410);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemMuiTiem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Mũi Tiêm";
            this.Load += new System.EventHandler(this.frmThemMuiTiem_Load);
            this.pnlMain.ResumeLayout(false);
            this.grpDanhSachCho.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccineWait)).EndInit();
            this.grpNhapLieu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccine)).EndInit();
            this.pnlVaccineFilter.ResumeLayout(false);
            this.pnlVaccineFilter.PerformLayout();
            this.pnlThongTinTiem.ResumeLayout(false);
            this.pnlThongTinTiem.PerformLayout();
            this.grpThongTinHoSo.ResumeLayout(false);
            this.grpThongTinHoSo.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
private System.Windows.Forms.GroupBox grpThongTinHoSo;
        private System.Windows.Forms.Label lblTenNguoiTiem;
        private System.Windows.Forms.Label lblTenNguoiTiemValue;
        private System.Windows.Forms.Label lblNgaySinh;
   private System.Windows.Forms.Label lblNgaySinhValue;
        private System.Windows.Forms.Label lblGioiTinh;
   private System.Windows.Forms.Label lblGioiTinhValue;
 private System.Windows.Forms.Label lblTenKhachHang;
        private System.Windows.Forms.Label lblTenKhachHangValue;
      private System.Windows.Forms.Label lblQuanHe;
        private System.Windows.Forms.Label lblQuanHeValue;
        private System.Windows.Forms.Label lblSoDT;
        private System.Windows.Forms.Label lblSoDTValue;
        private System.Windows.Forms.GroupBox grpNhapLieu;
     private System.Windows.Forms.Panel pnlThongTinTiem;
        private System.Windows.Forms.Label lblNgayTiem;
        private System.Windows.Forms.DateTimePicker dtpNgayTiem;
        private System.Windows.Forms.Label lblGhiChu;
  private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel pnlVaccineFilter;
     private System.Windows.Forms.DataGridView dgvVaccine;
      private System.Windows.Forms.GroupBox grpDanhSachCho;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Button btnTimKiemVaccine;
        private System.Windows.Forms.ComboBox cboLoaiBenh;
        private System.Windows.Forms.Label lblLoaiBenh;
        private System.Windows.Forms.ComboBox cboLoaiVaccine;
        private System.Windows.Forms.Label lblLoaiVaccine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaVaccineWait;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenVaccineWait;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongWait;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTiemWait;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNuocSXWait;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiBenh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNuocSX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaBan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbMaHSTC;
        private System.Windows.Forms.DataGridView dgvVaccineWait;
        private System.Windows.Forms.Button btnThemMuiTiem;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnLuuTatCa;
        private System.Windows.Forms.Label lbTongGia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbTongSoMui;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaVCW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenVCW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiBenhW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiVCW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTiemW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNuocSXW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaBanW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChuW;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label label5;
    }
}