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
            this.dgvMuiTiemCho = new System.Windows.Forms.DataGridView();
            this.colTenVaccine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNguoiTiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.grpNhapLieu = new System.Windows.Forms.GroupBox();
            this.btnThemVaoDanhSach = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.dtpNgayTiem = new System.Windows.Forms.DateTimePicker();
            this.lblNgayTiem = new System.Windows.Forms.Label();
            this.cboVaccine = new System.Windows.Forms.ComboBox();
            this.lblVaccine = new System.Windows.Forms.Label();
            this.grpThongTinHoSo = new System.Windows.Forms.GroupBox();
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
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLuuTatCa = new System.Windows.Forms.Button();
            this.ss = new System.Windows.Forms.Label();
            this.lblMaHSTC = new System.Windows.Forms.Label();
            this.lblSoDTValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.grpDanhSachCho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuiTiemCho)).BeginInit();
            this.grpNhapLieu.SuspendLayout();
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
            this.pnlMain.Size = new System.Drawing.Size(1147, 768);
            this.pnlMain.TabIndex = 0;
            // 
            // grpDanhSachCho
            // 
            this.grpDanhSachCho.Controls.Add(this.dgvMuiTiemCho);
            this.grpDanhSachCho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDanhSachCho.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpDanhSachCho.Location = new System.Drawing.Point(15, 382);
            this.grpDanhSachCho.Name = "grpDanhSachCho";
            this.grpDanhSachCho.Padding = new System.Windows.Forms.Padding(10);
            this.grpDanhSachCho.Size = new System.Drawing.Size(1117, 301);
            this.grpDanhSachCho.TabIndex = 3;
            this.grpDanhSachCho.TabStop = false;
            this.grpDanhSachCho.Text = "Danh sách mũi tiêm chờ lưu";
            // 
            // dgvMuiTiemCho
            // 
            this.dgvMuiTiemCho.AllowUserToAddRows = false;
            this.dgvMuiTiemCho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMuiTiemCho.BackgroundColor = System.Drawing.Color.White;
            this.dgvMuiTiemCho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMuiTiemCho.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTenVaccine,
            this.colMaPN,
            this.colNgayTiem,
            this.colTenNguoiTiem,
            this.colXoa});
            this.dgvMuiTiemCho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMuiTiemCho.Location = new System.Drawing.Point(10, 37);
            this.dgvMuiTiemCho.Name = "dgvMuiTiemCho";
            this.dgvMuiTiemCho.RowHeadersWidth = 62;
            this.dgvMuiTiemCho.RowTemplate.Height = 28;
            this.dgvMuiTiemCho.Size = new System.Drawing.Size(1097, 254);
            this.dgvMuiTiemCho.TabIndex = 0;
            // 
            // colTenVaccine
            // 
            this.colTenVaccine.DataPropertyName = "TenVaccine";
            this.colTenVaccine.HeaderText = "Tên Vaccine";
            this.colTenVaccine.MinimumWidth = 8;
            this.colTenVaccine.Name = "colTenVaccine";
            this.colTenVaccine.ReadOnly = true;
            // 
            // colMaPN
            // 
            this.colMaPN.DataPropertyName = "MaPN";
            this.colMaPN.HeaderText = "Mã Phiếu Nhập";
            this.colMaPN.MinimumWidth = 8;
            this.colMaPN.Name = "colMaPN";
            this.colMaPN.ReadOnly = true;
            // 
            // colNgayTiem
            // 
            this.colNgayTiem.DataPropertyName = "NgayTiem";
            this.colNgayTiem.HeaderText = "Ngày tiêm";
            this.colNgayTiem.MinimumWidth = 8;
            this.colNgayTiem.Name = "colNgayTiem";
            this.colNgayTiem.ReadOnly = true;
            // 
            // colTenNguoiTiem
            // 
            this.colTenNguoiTiem.DataPropertyName = "TenNguoiTiem";
            this.colTenNguoiTiem.HeaderText = "Tên nhân viên tiêm";
            this.colTenNguoiTiem.MinimumWidth = 8;
            this.colTenNguoiTiem.Name = "colTenNguoiTiem";
            this.colTenNguoiTiem.ReadOnly = true;
            // 
            // colXoa
            // 
            this.colXoa.FillWeight = 50F;
            this.colXoa.HeaderText = "Xóa";
            this.colXoa.MinimumWidth = 8;
            this.colXoa.Name = "colXoa";
            this.colXoa.Text = "Xóa";
            this.colXoa.UseColumnTextForButtonValue = true;
            // 
            // grpNhapLieu
            // 
            this.grpNhapLieu.Controls.Add(this.btnThemVaoDanhSach);
            this.grpNhapLieu.Controls.Add(this.txtGhiChu);
            this.grpNhapLieu.Controls.Add(this.lblGhiChu);
            this.grpNhapLieu.Controls.Add(this.dtpNgayTiem);
            this.grpNhapLieu.Controls.Add(this.lblNgayTiem);
            this.grpNhapLieu.Controls.Add(this.cboVaccine);
            this.grpNhapLieu.Controls.Add(this.lblVaccine);
            this.grpNhapLieu.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpNhapLieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpNhapLieu.Location = new System.Drawing.Point(15, 182);
            this.grpNhapLieu.Name = "grpNhapLieu";
            this.grpNhapLieu.Padding = new System.Windows.Forms.Padding(10);
            this.grpNhapLieu.Size = new System.Drawing.Size(1117, 200);
            this.grpNhapLieu.TabIndex = 2;
            this.grpNhapLieu.TabStop = false;
            this.grpNhapLieu.Text = "Thêm Mũi tiêm";
            // 
            // btnThemVaoDanhSach
            // 
            this.btnThemVaoDanhSach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThemVaoDanhSach.FlatAppearance.BorderSize = 0;
            this.btnThemVaoDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoDanhSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemVaoDanhSach.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoDanhSach.Location = new System.Drawing.Point(730, 145);
            this.btnThemVaoDanhSach.Name = "btnThemVaoDanhSach";
            this.btnThemVaoDanhSach.Size = new System.Drawing.Size(220, 40);
            this.btnThemVaoDanhSach.TabIndex = 8;
            this.btnThemVaoDanhSach.Text = "Thêm vào danh sách";
            this.btnThemVaoDanhSach.UseVisualStyleBackColor = false;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(180, 145);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(520, 40);
            this.txtGhiChu.TabIndex = 7;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.Location = new System.Drawing.Point(30, 148);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(89, 28);
            this.lblGhiChu.TabIndex = 6;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // dtpNgayTiem
            // 
            this.dtpNgayTiem.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayTiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayTiem.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayTiem.Location = new System.Drawing.Point(730, 55);
            this.dtpNgayTiem.Name = "dtpNgayTiem";
            this.dtpNgayTiem.Size = new System.Drawing.Size(285, 34);
            this.dtpNgayTiem.TabIndex = 3;
            // 
            // lblNgayTiem
            // 
            this.lblNgayTiem.AutoSize = true;
            this.lblNgayTiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNgayTiem.Location = new System.Drawing.Point(567, 58);
            this.lblNgayTiem.Name = "lblNgayTiem";
            this.lblNgayTiem.Size = new System.Drawing.Size(157, 28);
            this.lblNgayTiem.TabIndex = 2;
            this.lblNgayTiem.Text = "Ngày hẹn tiêm:";
            // 
            // cboVaccine
            // 
            this.cboVaccine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVaccine.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboVaccine.FormattingEnabled = true;
            this.cboVaccine.Location = new System.Drawing.Point(180, 55);
            this.cboVaccine.Name = "cboVaccine";
            this.cboVaccine.Size = new System.Drawing.Size(330, 36);
            this.cboVaccine.TabIndex = 1;
            // 
            // lblVaccine
            // 
            this.lblVaccine.AutoSize = true;
            this.lblVaccine.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVaccine.Location = new System.Drawing.Point(30, 58);
            this.lblVaccine.Name = "lblVaccine";
            this.lblVaccine.Size = new System.Drawing.Size(88, 28);
            this.lblVaccine.TabIndex = 0;
            this.lblVaccine.Text = "Vaccine:";
            // 
            // grpThongTinHoSo
            // 
            this.grpThongTinHoSo.Controls.Add(this.lblSoDTValue);
            this.grpThongTinHoSo.Controls.Add(this.label2);
            this.grpThongTinHoSo.Controls.Add(this.lblMaHSTC);
            this.grpThongTinHoSo.Controls.Add(this.ss);
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
            this.grpThongTinHoSo.Size = new System.Drawing.Size(1117, 167);
            this.grpThongTinHoSo.TabIndex = 1;
            this.grpThongTinHoSo.TabStop = false;
            this.grpThongTinHoSo.Text = "Thông tin Hồ sơ";
            // 
            // lblQuanHeValue
            // 
            this.lblQuanHeValue.AutoSize = true;
            this.lblQuanHeValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuanHeValue.Location = new System.Drawing.Point(553, 117);
            this.lblQuanHeValue.Name = "lblQuanHeValue";
            this.lblQuanHeValue.Size = new System.Drawing.Size(24, 28);
            this.lblQuanHeValue.TabIndex = 9;
            this.lblQuanHeValue.Text = "...";
            // 
            // lblQuanHe
            // 
            this.lblQuanHe.AutoSize = true;
            this.lblQuanHe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblQuanHe.Location = new System.Drawing.Point(425, 117);
            this.lblQuanHe.Name = "lblQuanHe";
            this.lblQuanHe.Size = new System.Drawing.Size(96, 28);
            this.lblQuanHe.TabIndex = 8;
            this.lblQuanHe.Text = "Quan hệ:";
            // 
            // lblTenKhachHangValue
            // 
            this.lblTenKhachHangValue.AutoSize = true;
            this.lblTenKhachHangValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTenKhachHangValue.Location = new System.Drawing.Point(242, 117);
            this.lblTenKhachHangValue.Name = "lblTenKhachHangValue";
            this.lblTenKhachHangValue.Size = new System.Drawing.Size(24, 28);
            this.lblTenKhachHangValue.TabIndex = 7;
            this.lblTenKhachHangValue.Text = "...";
            // 
            // lblTenKhachHang
            // 
            this.lblTenKhachHang.AutoSize = true;
            this.lblTenKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenKhachHang.Location = new System.Drawing.Point(34, 117);
            this.lblTenKhachHang.Name = "lblTenKhachHang";
            this.lblTenKhachHang.Size = new System.Drawing.Size(196, 28);
            this.lblTenKhachHang.TabIndex = 6;
            this.lblTenKhachHang.Text = "Họ tên khách hàng:";
            // 
            // lblGioiTinhValue
            // 
            this.lblGioiTinhValue.AutoSize = true;
            this.lblGioiTinhValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGioiTinhValue.Location = new System.Drawing.Point(843, 77);
            this.lblGioiTinhValue.Name = "lblGioiTinhValue";
            this.lblGioiTinhValue.Size = new System.Drawing.Size(24, 28);
            this.lblGioiTinhValue.TabIndex = 5;
            this.lblGioiTinhValue.Text = "...";
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGioiTinh.Location = new System.Drawing.Point(725, 77);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(100, 28);
            this.lblGioiTinh.TabIndex = 4;
            this.lblGioiTinh.Text = "Giới tính:";
            // 
            // lblNgaySinhValue
            // 
            this.lblNgaySinhValue.AutoSize = true;
            this.lblNgaySinhValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgaySinhValue.Location = new System.Drawing.Point(553, 77);
            this.lblNgaySinhValue.Name = "lblNgaySinhValue";
            this.lblNgaySinhValue.Size = new System.Drawing.Size(24, 28);
            this.lblNgaySinhValue.TabIndex = 3;
            this.lblNgaySinhValue.Text = "...";
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.Location = new System.Drawing.Point(425, 77);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(112, 28);
            this.lblNgaySinh.TabIndex = 2;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // lblTenNguoiTiemValue
            // 
            this.lblTenNguoiTiemValue.AutoSize = true;
            this.lblTenNguoiTiemValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTenNguoiTiemValue.Location = new System.Drawing.Point(242, 77);
            this.lblTenNguoiTiemValue.Name = "lblTenNguoiTiemValue";
            this.lblTenNguoiTiemValue.Size = new System.Drawing.Size(24, 28);
            this.lblTenNguoiTiemValue.TabIndex = 1;
            this.lblTenNguoiTiemValue.Text = "...";
            this.lblTenNguoiTiemValue.Click += new System.EventHandler(this.lblTenNguoiTiemValue_Click);
            // 
            // lblTenNguoiTiem
            // 
            this.lblTenNguoiTiem.AutoSize = true;
            this.lblTenNguoiTiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenNguoiTiem.Location = new System.Drawing.Point(34, 77);
            this.lblTenNguoiTiem.Name = "lblTenNguoiTiem";
            this.lblTenNguoiTiem.Size = new System.Drawing.Size(192, 28);
            this.lblTenNguoiTiem.TabIndex = 0;
            this.lblTenNguoiTiem.Text = "Họ tên người tiêm:";
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnDong);
            this.pnlActions.Controls.Add(this.btnLuuTatCa);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(15, 683);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlActions.Size = new System.Drawing.Size(1117, 70);
            this.pnlActions.TabIndex = 0;
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(520, 13);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(200, 45);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            // 
            // btnLuuTatCa
            // 
            this.btnLuuTatCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnLuuTatCa.FlatAppearance.BorderSize = 0;
            this.btnLuuTatCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuTatCa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuuTatCa.ForeColor = System.Drawing.Color.White;
            this.btnLuuTatCa.Location = new System.Drawing.Point(280, 13);
            this.btnLuuTatCa.Name = "btnLuuTatCa";
            this.btnLuuTatCa.Size = new System.Drawing.Size(200, 45);
            this.btnLuuTatCa.TabIndex = 0;
            this.btnLuuTatCa.Text = "Lưu tất cả";
            this.btnLuuTatCa.UseVisualStyleBackColor = false;
            // 
            // ss
            // 
            this.ss.AutoSize = true;
            this.ss.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ss.Location = new System.Drawing.Point(34, 37);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(105, 28);
            this.ss.TabIndex = 12;
            this.ss.Text = "Mã hồ sơ:";
            // 
            // lblMaHSTC
            // 
            this.lblMaHSTC.AutoSize = true;
            this.lblMaHSTC.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaHSTC.Location = new System.Drawing.Point(242, 37);
            this.lblMaHSTC.Name = "lblMaHSTC";
            this.lblMaHSTC.Size = new System.Drawing.Size(24, 28);
            this.lblMaHSTC.TabIndex = 13;
            this.lblMaHSTC.Text = "...";
            // 
            // lblSoDTValue
            // 
            this.lblSoDTValue.AutoSize = true;
            this.lblSoDTValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoDTValue.Location = new System.Drawing.Point(843, 117);
            this.lblSoDTValue.Name = "lblSoDTValue";
            this.lblSoDTValue.Size = new System.Drawing.Size(24, 28);
            this.lblSoDTValue.TabIndex = 15;
            this.lblSoDTValue.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(725, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 28);
            this.label2.TabIndex = 14;
            this.label2.Text = "Số ĐT:";
            // 
            // frmThemMuiTiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 768);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemMuiTiem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Mũi Tiêm";
            this.pnlMain.ResumeLayout(false);
            this.grpDanhSachCho.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuiTiemCho)).EndInit();
            this.grpNhapLieu.ResumeLayout(false);
            this.grpNhapLieu.PerformLayout();
            this.grpThongTinHoSo.ResumeLayout(false);
            this.grpThongTinHoSo.PerformLayout();
            this.pnlActions.ResumeLayout(false);
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
  private System.Windows.Forms.GroupBox grpNhapLieu;
        private System.Windows.Forms.Label lblVaccine;
        private System.Windows.Forms.ComboBox cboVaccine;
        private System.Windows.Forms.Label lblNgayTiem;
        private System.Windows.Forms.DateTimePicker dtpNgayTiem;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
  private System.Windows.Forms.Button btnThemVaoDanhSach;
        private System.Windows.Forms.GroupBox grpDanhSachCho;
        private System.Windows.Forms.DataGridView dgvMuiTiemCho;
        private System.Windows.Forms.Panel pnlActions;
      private System.Windows.Forms.Button btnLuuTatCa;
     private System.Windows.Forms.Button btnDong;
      private System.Windows.Forms.DataGridViewTextBoxColumn colTenVaccine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTiem;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTenNguoiTiem;
   private System.Windows.Forms.DataGridViewButtonColumn colXoa;
        private System.Windows.Forms.Label lblMaHSTC;
        private System.Windows.Forms.Label ss;
        private System.Windows.Forms.Label lblSoDTValue;
        private System.Windows.Forms.Label label2;
    }
}