namespace TPVAXWinform_GUI.UserControls
{
    partial class LichTiemControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
  /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
          this.pnlTieuDe = new System.Windows.Forms.Panel();
  this.lblTieuDe = new System.Windows.Forms.Label();
     this.pnlLoc = new System.Windows.Forms.Panel();
this.chkChuaTiem = new System.Windows.Forms.CheckBox();
        this.chkDaTiem = new System.Windows.Forms.CheckBox();
            this.lblDen = new System.Windows.Forms.Label();
this.dtpDenThang = new System.Windows.Forms.DateTimePicker();
     this.dtpTuThang = new System.Windows.Forms.DateTimePicker();
            this.lblTuThang = new System.Windows.Forms.Label();
       this.btnDatLai = new System.Windows.Forms.Button();
     this.txtSearch = new System.Windows.Forms.TextBox();
     this.cboLoaiTimKiem = new System.Windows.Forms.ComboBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
          this.pnlHanhDong = new System.Windows.Forms.Panel();
 this.btnThemLichHen = new System.Windows.Forms.Button();
    this.dgvLichTiem = new System.Windows.Forms.DataGridView();
this.colMaHSTC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNguoiTiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
   this.colNgayHen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTiemThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.colCheckIn = new System.Windows.Forms.DataGridViewButtonColumn();
   this.colHuy = new System.Windows.Forms.DataGridViewButtonColumn();
      this.contextMenuStripLichTiem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemXemThongTin = new System.Windows.Forms.ToolStripMenuItem();
this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
  this.toolStripMenuItemXacNhanTiem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHuyTiem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTieuDe.SuspendLayout();
      this.pnlLoc.SuspendLayout();
   this.pnlHanhDong.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dgvLichTiem)).BeginInit();
        this.contextMenuStripLichTiem.SuspendLayout();
      this.SuspendLayout();
            // 
            // pnlTieuDe
            // 
            this.pnlTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
      this.pnlTieuDe.Controls.Add(this.lblTieuDe);
            this.pnlTieuDe.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTieuDe.Location = new System.Drawing.Point(0, 0);
            this.pnlTieuDe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTieuDe.Name = "pnlTieuDe";
            this.pnlTieuDe.Size = new System.Drawing.Size(1800, 108);
            this.pnlTieuDe.TabIndex = 0;
       // 
       // lblTieuDe
  // 
       this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
        this.lblTieuDe.ForeColor = System.Drawing.Color.White;
    this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
   this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
    this.lblTieuDe.Size = new System.Drawing.Size(1800, 108);
 this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ LỊCH TIÊM";
    this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLoc
         // 
    this.pnlLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
    this.pnlLoc.Controls.Add(this.chkChuaTiem);
  this.pnlLoc.Controls.Add(this.chkDaTiem);
   this.pnlLoc.Controls.Add(this.lblDen);
   this.pnlLoc.Controls.Add(this.dtpDenThang);
 this.pnlLoc.Controls.Add(this.dtpTuThang);
   this.pnlLoc.Controls.Add(this.lblTuThang);
        this.pnlLoc.Controls.Add(this.btnDatLai);
    this.pnlLoc.Controls.Add(this.txtSearch);
     this.pnlLoc.Controls.Add(this.cboLoaiTimKiem);
       this.pnlLoc.Controls.Add(this.lblTimKiem);
        this.pnlLoc.Controls.Add(this.lblTrangThai);
        this.pnlLoc.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlLoc.Location = new System.Drawing.Point(0, 108);
            this.pnlLoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
     this.pnlLoc.Name = "pnlLoc";
       this.pnlLoc.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
      this.pnlLoc.Size = new System.Drawing.Size(1800, 160);
            this.pnlLoc.TabIndex = 1;
       // 
      // chkChuaTiem
   // 
            this.chkChuaTiem.AutoSize = true;
            this.chkChuaTiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkChuaTiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.chkChuaTiem.Location = new System.Drawing.Point(294, 108);
     this.chkChuaTiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
        this.chkChuaTiem.Name = "chkChuaTiem";
      this.chkChuaTiem.Size = new System.Drawing.Size(128, 32);
            this.chkChuaTiem.TabIndex = 14;
      this.chkChuaTiem.Text = "Chưa tiêm";
            this.chkChuaTiem.UseVisualStyleBackColor = true;
      this.chkChuaTiem.CheckedChanged += new System.EventHandler(this.chkTrangThai_CheckedChanged);
    // 
            // chkDaTiem
   // 
          this.chkDaTiem.AutoSize = true;
            this.chkDaTiem.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.chkDaTiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.chkDaTiem.Location = new System.Drawing.Point(165, 108);
            this.chkDaTiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.chkDaTiem.Name = "chkDaTiem";
            this.chkDaTiem.Size = new System.Drawing.Size(108, 32);
            this.chkDaTiem.TabIndex = 13;
   this.chkDaTiem.Text = "Đã tiêm";
            this.chkDaTiem.UseVisualStyleBackColor = true;
  this.chkDaTiem.CheckedChanged += new System.EventHandler(this.chkTrangThai_CheckedChanged);
            // 
      // lblDen
  // 
        this.lblDen.AutoSize = true;
            this.lblDen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
       this.lblDen.Location = new System.Drawing.Point(500, 33);
          this.lblDen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
this.lblDen.Name = "lblDen";
          this.lblDen.Size = new System.Drawing.Size(52, 28);
this.lblDen.TabIndex = 12;
      this.lblDen.Text = "Đến";
            // 
            // dtpDenThang
          // 
        this.dtpDenThang.CustomFormat = "MM/yyyy";
   this.dtpDenThang.Font = new System.Drawing.Font("Segoe UI", 11F);
     this.dtpDenThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
         this.dtpDenThang.Location = new System.Drawing.Point(573, 28);
            this.dtpDenThang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDenThang.Name = "dtpDenThang";
            this.dtpDenThang.Size = new System.Drawing.Size(180, 37);
          this.dtpDenThang.TabIndex = 11;
   this.dtpDenThang.ValueChanged += new System.EventHandler(this.dtpThang_ValueChanged);
      // 
  // dtpTuThang
     // 
      this.dtpTuThang.CustomFormat = "MM/yyyy";
      this.dtpTuThang.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpTuThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuThang.Location = new System.Drawing.Point(294, 28);
            this.dtpTuThang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
       this.dtpTuThang.Name = "dtpTuThang";
         this.dtpTuThang.Size = new System.Drawing.Size(180, 37);
    this.dtpTuThang.TabIndex = 10;
            this.dtpTuThang.ValueChanged += new System.EventHandler(this.dtpThang_ValueChanged);
     // 
        // lblTuThang
    // 
    this.lblTuThang.AutoSize = true;
       this.lblTuThang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTuThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTuThang.Location = new System.Drawing.Point(45, 33);
    this.lblTuThang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTuThang.Name = "lblTuThang";
     this.lblTuThang.Size = new System.Drawing.Size(205, 28);
            this.lblTuThang.TabIndex = 9;
         this.lblTuThang.Text = "Lọc theo thời gian từ:";
  // 
            // btnDatLai
         // 
            this.btnDatLai.BackColor = System.Drawing.Color.Gray;
            this.btnDatLai.FlatAppearance.BorderSize = 0;
      this.btnDatLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatLai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDatLai.ForeColor = System.Drawing.Color.White;
    this.btnDatLai.Location = new System.Drawing.Point(1650, 82);
     this.btnDatLai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
    this.btnDatLai.Name = "btnDatLai";
   this.btnDatLai.Size = new System.Drawing.Size(120, 55);
      this.btnDatLai.TabIndex = 7;
    this.btnDatLai.Text = "Đặt lại";
            this.btnDatLai.UseVisualStyleBackColor = false;
   this.btnDatLai.Click += new System.EventHandler(this.btnDatLai_Click);
       // 
        // txtSearch
// 
      this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
    this.txtSearch.Location = new System.Drawing.Point(1248, 28);
 this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
    this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(373, 37);
        this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
    // cboLoaiTimKiem
    // 
       this.cboLoaiTimKiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboLoaiTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F);
this.cboLoaiTimKiem.FormattingEnabled = true;
      this.cboLoaiTimKiem.Items.AddRange(new object[] {
          "Tên người tiêm",
            "Tên Vaccine"});
   this.cboLoaiTimKiem.Location = new System.Drawing.Point(939, 28);
     this.cboLoaiTimKiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
          this.cboLoaiTimKiem.Name = "cboLoaiTimKiem";
            this.cboLoaiTimKiem.Size = new System.Drawing.Size(280, 38);
 this.cboLoaiTimKiem.TabIndex = 4;
 // 
            // lblTimKiem
            // 
      this.lblTimKiem.AutoSize = true;
        this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
    this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTimKiem.Location = new System.Drawing.Point(790, 33);
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
    this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(128, 28);
         this.lblTimKiem.TabIndex = 4;
      this.lblTimKiem.Text = "Tìm kiếm từ:";
 // 
      // lblTrangThai
     // 
     this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
   this.lblTrangThai.Location = new System.Drawing.Point(45, 110);
    this.lblTrangThai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
this.lblTrangThai.Name = "lblTrangThai";
 this.lblTrangThai.Size = new System.Drawing.Size(113, 28);
        this.lblTrangThai.TabIndex = 2;
     this.lblTrangThai.Text = "Trạng thái:";
            // 
            // pnlHanhDong
     // 
       this.pnlHanhDong.BackColor = System.Drawing.Color.White;
     this.pnlHanhDong.Controls.Add(this.btnThemLichHen);
            this.pnlHanhDong.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlHanhDong.Location = new System.Drawing.Point(0, 268);
 this.pnlHanhDong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
          this.pnlHanhDong.Name = "pnlHanhDong";
            this.pnlHanhDong.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
    this.pnlHanhDong.Size = new System.Drawing.Size(1800, 92);
            this.pnlHanhDong.TabIndex = 2;
            // 
        // btnThemLichHen
            // 
            this.btnThemLichHen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnThemLichHen.FlatAppearance.BorderSize = 0;
     this.btnThemLichHen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemLichHen.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThemLichHen.ForeColor = System.Drawing.Color.White;
        this.btnThemLichHen.Location = new System.Drawing.Point(45, 15);
    this.btnThemLichHen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
          this.btnThemLichHen.Name = "btnThemLichHen";
 this.btnThemLichHen.Size = new System.Drawing.Size(300, 62);
        this.btnThemLichHen.TabIndex = 0;
            this.btnThemLichHen.Text = "➕ Thêm lịch hẹn mới";
    this.btnThemLichHen.UseVisualStyleBackColor = false;
       // 
   // dgvLichTiem
      // 
       this.dgvLichTiem.AllowUserToAddRows = false;
            this.dgvLichTiem.AllowUserToDeleteRows = false;
      this.dgvLichTiem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichTiem.BackgroundColor = System.Drawing.Color.White;
    this.dgvLichTiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
     this.dgvLichTiem.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
     dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
  this.dgvLichTiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
 this.dgvLichTiem.ColumnHeadersHeight = 40;
     this.dgvLichTiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLichTiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaHSTC,
    this.colTenNguoiTiem,
 this.colTenVC,
            this.colNgayHen,
       this.colTrangThai,
        this.colNgayTiemThucTe,
    this.colCheckIn,
            this.colHuy});
       this.dgvLichTiem.ContextMenuStrip = this.contextMenuStripLichTiem;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
    dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
       dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
    dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
     dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
          this.dgvLichTiem.DefaultCellStyle = dataGridViewCellStyle2;
         this.dgvLichTiem.Dock = System.Windows.Forms.DockStyle.Fill;
     this.dgvLichTiem.EnableHeadersVisualStyles = false;
            this.dgvLichTiem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
      this.dgvLichTiem.Location = new System.Drawing.Point(0, 360);
          this.dgvLichTiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.dgvLichTiem.Name = "dgvLichTiem";
     this.dgvLichTiem.ReadOnly = true;
         this.dgvLichTiem.RowHeadersVisible = false;
            this.dgvLichTiem.RowHeadersWidth = 62;
    this.dgvLichTiem.RowTemplate.Height = 35;
        this.dgvLichTiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichTiem.Size = new System.Drawing.Size(1800, 717);
      this.dgvLichTiem.TabIndex = 3;
            // 
    // colMaHSTC
         // 
            this.colMaHSTC.DataPropertyName = "MaHSTC";
       this.colMaHSTC.FillWeight = 80F;
   this.colMaHSTC.HeaderText = "Mã HSTC";
            this.colMaHSTC.MinimumWidth = 8;
            this.colMaHSTC.Name = "colMaHSTC";
  this.colMaHSTC.ReadOnly = true;
// 
 // colTenNguoiTiem
   // 
            this.colTenNguoiTiem.DataPropertyName = "TenNguoiTiem";
            this.colTenNguoiTiem.HeaderText = "Tên người tiêm";
      this.colTenNguoiTiem.MinimumWidth = 8;
this.colTenNguoiTiem.Name = "colTenNguoiTiem";
      this.colTenNguoiTiem.ReadOnly = true;
      // 
        // colTenVC
 // 
            this.colTenVC.HeaderText = "Tên Vaccine";
this.colTenVC.MinimumWidth = 8;
     this.colTenVC.Name = "colTenVC";
     this.colTenVC.ReadOnly = true;
        // 
          // colNgayHen
            // 
  this.colNgayHen.DataPropertyName = "NgayHen";
      this.colNgayHen.FillWeight = 80F;
         this.colNgayHen.HeaderText = "Ngày hẹn";
     this.colNgayHen.MinimumWidth = 8;
    this.colNgayHen.Name = "colNgayHen";
        this.colNgayHen.ReadOnly = true;
          // 
   // colTrangThai
         // 
    this.colTrangThai.DataPropertyName = "TrangThai";
    this.colTrangThai.FillWeight = 80F;
      this.colTrangThai.HeaderText = "Trạng thái";
  this.colTrangThai.MinimumWidth = 8;
            this.colTrangThai.Name = "colTrangThai";
 this.colTrangThai.ReadOnly = true;
   // 
    // colNgayTiemThucTe
          // 
  this.colNgayTiemThucTe.DataPropertyName = "NgayTiemThucTe";
    this.colNgayTiemThucTe.FillWeight = 90F;
            this.colNgayTiemThucTe.HeaderText = "Ngày tiêm thực tế";
this.colNgayTiemThucTe.MinimumWidth = 8;
            this.colNgayTiemThucTe.Name = "colNgayTiemThucTe";
    this.colNgayTiemThucTe.ReadOnly = true;
    // 
            // colCheckIn
            // 
            this.colCheckIn.FillWeight = 60F;
            this.colCheckIn.HeaderText = "Tiêm";
            this.colCheckIn.MinimumWidth = 8;
      this.colCheckIn.Name = "colCheckIn";
          this.colCheckIn.ReadOnly = true;
     this.colCheckIn.Text = "✔️ Tiêm";
         this.colCheckIn.UseColumnTextForButtonValue = true;
            // 
// colHuy
            // 
 this.colHuy.FillWeight = 60F;
      this.colHuy.HeaderText = "Hủy";
          this.colHuy.MinimumWidth = 8;
    this.colHuy.Name = "colHuy";
          this.colHuy.ReadOnly = true;
        this.colHuy.Text = "❌ Hủy";
      this.colHuy.UseColumnTextForButtonValue = true;
            // 
            // contextMenuStripLichTiem
        // 
            this.contextMenuStripLichTiem.ImageScalingSize = new System.Drawing.Size(24, 24);
        this.contextMenuStripLichTiem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemXemThongTin,
            this.toolStripSeparator1,
     this.toolStripMenuItemXacNhanTiem,
this.toolStripMenuItemHuyTiem});
            this.contextMenuStripLichTiem.Name = "contextMenuStripLichTiem";
this.contextMenuStripLichTiem.Size = new System.Drawing.Size(240, 106);
        this.contextMenuStripLichTiem.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripLichTiem_Opening);
         // 
            // toolStripMenuItemXemThongTin
            // 
            this.toolStripMenuItemXemThongTin.Name = "toolStripMenuItemXemThongTin";
      this.toolStripMenuItemXemThongTin.Size = new System.Drawing.Size(239, 32);
            this.toolStripMenuItemXemThongTin.Text = "📋 Xem thông tin mũi tiêm";
   this.toolStripMenuItemXemThongTin.Click += new System.EventHandler(this.toolStripMenuItemXemThongTin_Click);
            // 
 // toolStripSeparator1
      // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
          this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
            // 
 // toolStripMenuItemXacNhanTiem
        // 
    this.toolStripMenuItemXacNhanTiem.Name = "toolStripMenuItemXacNhanTiem";
  this.toolStripMenuItemXacNhanTiem.Size = new System.Drawing.Size(239, 32);
      this.toolStripMenuItemXacNhanTiem.Text = "✔️ Xác nhận tiêm";
   this.toolStripMenuItemXacNhanTiem.Click += new System.EventHandler(this.toolStripMenuItemXacNhanTiem_Click);
        // 
            // toolStripMenuItemHuyTiem
    // 
            this.toolStripMenuItemHuyTiem.Name = "toolStripMenuItemHuyTiem";
            this.toolStripMenuItemHuyTiem.Size = new System.Drawing.Size(239, 32);
        this.toolStripMenuItemHuyTiem.Text = "❌ Hủy tiêm";
         this.toolStripMenuItemHuyTiem.Click += new System.EventHandler(this.toolStripMenuItemHuyTiem_Click);
     // 
     // LichTiemControl
            // 
       this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.White;
          this.Controls.Add(this.dgvLichTiem);
            this.Controls.Add(this.pnlHanhDong);
            this.Controls.Add(this.pnlLoc);
            this.Controls.Add(this.pnlTieuDe);
   this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.Name = "LichTiemControl";
            this.Size = new System.Drawing.Size(1800, 1077);
     this.Load += new System.EventHandler(this.LichTiemControl_Load);
     this.pnlTieuDe.ResumeLayout(false);
    this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            this.pnlHanhDong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichTiem)).EndInit();
       this.contextMenuStripLichTiem.ResumeLayout(false);
 this.ResumeLayout(false);

        }

        #endregion

  private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.CheckBox chkChuaTiem;
        private System.Windows.Forms.CheckBox chkDaTiem;
        private System.Windows.Forms.Label lblDen;
     private System.Windows.Forms.DateTimePicker dtpDenThang;
        private System.Windows.Forms.DateTimePicker dtpTuThang;
        private System.Windows.Forms.Label lblTuThang;
    private System.Windows.Forms.Button btnDatLai;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboLoaiTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
   private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Panel pnlHanhDong;
     private System.Windows.Forms.Button btnThemLichHen;
        private System.Windows.Forms.DataGridView dgvLichTiem;
      private System.Windows.Forms.DataGridViewTextBoxColumn colMaHSTC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNguoiTiem;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTenVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayHen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTiemThucTe;
     private System.Windows.Forms.DataGridViewButtonColumn colCheckIn;
        private System.Windows.Forms.DataGridViewButtonColumn colHuy;
  private System.Windows.Forms.ContextMenuStrip contextMenuStripLichTiem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemXemThongTin;
private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemXacNhanTiem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHuyTiem;
    }
}
