namespace TPVAXWinform_GUI.Forms
{
    partial class frmThemSuaKhuyenMai
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
            this.lblTenKM = new System.Windows.Forms.Label();
            this.txtTenKM = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblLoaiKM = new System.Windows.Forms.Label();
            this.cboLoaiKM = new System.Windows.Forms.ComboBox();
            this.lblKieuGiam = new System.Windows.Forms.Label();
            this.cboKieuGiam = new System.Windows.Forms.ComboBox();
            this.lblGiaTriGiam = new System.Windows.Forms.Label();
            this.numGiaTriGiam = new System.Windows.Forms.NumericUpDown();
            this.lblNgayBatDau = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.lblNgayKetThuc = new System.Windows.Forms.Label();
            this.dtpNgayKetThuc = new System.Windows.Forms.DateTimePicker();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.grpSanPham = new System.Windows.Forms.GroupBox();
            this.btnThemSanPham = new System.Windows.Forms.Button();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.cboLoaiSanPham = new System.Windows.Forms.ComboBox();
            this.lblLoaiSanPham = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colLoaiSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaTriGiam)).BeginInit();
            this.grpSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTenKM
            // 
            this.lblTenKM.AutoSize = true;
            this.lblTenKM.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenKM.Location = new System.Drawing.Point(30, 30);
            this.lblTenKM.Name = "lblTenKM";
            this.lblTenKM.Size = new System.Drawing.Size(148, 28);
            this.lblTenKM.TabIndex = 0;
            this.lblTenKM.Text = "Tên khuyến mãi:";
            // 
            // txtTenKM
            // 
            this.txtTenKM.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenKM.Location = new System.Drawing.Point(220, 27);
            this.txtTenKM.Name = "txtTenKM";
            this.txtTenKM.Size = new System.Drawing.Size(500, 34);
            this.txtTenKM.TabIndex = 1;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMoTa.Location = new System.Drawing.Point(30, 80);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(69, 28);
            this.lblMoTa.TabIndex = 2;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMoTa.Location = new System.Drawing.Point(220, 77);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(500, 80);
            this.txtMoTa.TabIndex = 3;
            // 
            // lblLoaiKM
            // 
            this.lblLoaiKM.AutoSize = true;
            this.lblLoaiKM.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLoaiKM.Location = new System.Drawing.Point(30, 180);
            this.lblLoaiKM.Name = "lblLoaiKM";
            this.lblLoaiKM.Size = new System.Drawing.Size(88, 28);
            this.lblLoaiKM.TabIndex = 4;
            this.lblLoaiKM.Text = "Loại KM:";
            // 
            // cboLoaiKM
            // 
            this.cboLoaiKM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiKM.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiKM.FormattingEnabled = true;
            this.cboLoaiKM.Location = new System.Drawing.Point(220, 177);
            this.cboLoaiKM.Name = "cboLoaiKM";
            this.cboLoaiKM.Size = new System.Drawing.Size(250, 36);
            this.cboLoaiKM.TabIndex = 5;
            // 
            // lblKieuGiam
            // 
            this.lblKieuGiam.AutoSize = true;
            this.lblKieuGiam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKieuGiam.Location = new System.Drawing.Point(30, 230);
            this.lblKieuGiam.Name = "lblKieuGiam";
            this.lblKieuGiam.Size = new System.Drawing.Size(116, 28);
            this.lblKieuGiam.TabIndex = 6;
            this.lblKieuGiam.Text = "Kiểu giảm:";
            // 
            // cboKieuGiam
            // 
            this.cboKieuGiam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKieuGiam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKieuGiam.FormattingEnabled = true;
            this.cboKieuGiam.Location = new System.Drawing.Point(220, 227);
            this.cboKieuGiam.Name = "cboKieuGiam";
            this.cboKieuGiam.Size = new System.Drawing.Size(150, 36);
            this.cboKieuGiam.TabIndex = 7;
            // 
            // lblGiaTriGiam
            // 
            this.lblGiaTriGiam.AutoSize = true;
            this.lblGiaTriGiam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGiaTriGiam.Location = new System.Drawing.Point(400, 230);
            this.lblGiaTriGiam.Name = "lblGiaTriGiam";
            this.lblGiaTriGiam.Size = new System.Drawing.Size(126, 28);
            this.lblGiaTriGiam.TabIndex = 8;
            this.lblGiaTriGiam.Text = "Giá trị giảm:";
            // 
            // numGiaTriGiam
            // 
            this.numGiaTriGiam.DecimalPlaces = 0;
            this.numGiaTriGiam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numGiaTriGiam.Location = new System.Drawing.Point(540, 227);
            this.numGiaTriGiam.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGiaTriGiam.Name = "numGiaTriGiam";
            this.numGiaTriGiam.Size = new System.Drawing.Size(180, 34);
            this.numGiaTriGiam.TabIndex = 9;
            // 
            // lblNgayBatDau
            // 
            this.lblNgayBatDau.AutoSize = true;
            this.lblNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNgayBatDau.Location = new System.Drawing.Point(30, 280);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(142, 28);
            this.lblNgayBatDau.TabIndex = 10;
            this.lblNgayBatDau.Text = "Ngày bắt đầu:";
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(220, 277);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(200, 34);
            this.dtpNgayBatDau.TabIndex = 11;
            // 
            // lblNgayKetThuc
            // 
            this.lblNgayKetThuc.AutoSize = true;
            this.lblNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNgayKetThuc.Location = new System.Drawing.Point(30, 330);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(149, 28);
            this.lblNgayKetThuc.TabIndex = 12;
            this.lblNgayKetThuc.Text = "Ngày kết thúc:";
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(220, 327);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(200, 34);
            this.dtpNgayKetThuc.TabIndex = 13;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkTrangThai.Location = new System.Drawing.Point(220, 380);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(137, 32);
            this.chkTrangThai.TabIndex = 14;
            this.chkTrangThai.Text = "Hoạt động";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // grpSanPham
            // 
            this.grpSanPham.Controls.Add(this.btnThemSanPham);
            this.grpSanPham.Controls.Add(this.cboSanPham);
            this.grpSanPham.Controls.Add(this.lblSanPham);
            this.grpSanPham.Controls.Add(this.cboLoaiSanPham);
            this.grpSanPham.Controls.Add(this.lblLoaiSanPham);
            this.grpSanPham.Controls.Add(this.dgvChiTiet);
            this.grpSanPham.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpSanPham.Location = new System.Drawing.Point(30, 430);
            this.grpSanPham.Name = "grpSanPham";
            this.grpSanPham.Size = new System.Drawing.Size(690, 350);
            this.grpSanPham.TabIndex = 15;
            this.grpSanPham.TabStop = false;
            this.grpSanPham.Text = "Sản phẩm áp dụng";
            // 
            // btnThemSanPham
            // 
            this.btnThemSanPham.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThemSanPham.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemSanPham.ForeColor = System.Drawing.Color.White;
            this.btnThemSanPham.Location = new System.Drawing.Point(550, 80);
            this.btnThemSanPham.Name = "btnThemSanPham";
            this.btnThemSanPham.Size = new System.Drawing.Size(120, 40);
            this.btnThemSanPham.TabIndex = 5;
            this.btnThemSanPham.Text = "Thêm";
            this.btnThemSanPham.UseVisualStyleBackColor = false;
            this.btnThemSanPham.Click += new System.EventHandler(this.btnThemSanPham_Click);
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Location = new System.Drawing.Point(190, 82);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(350, 36);
            this.cboSanPham.TabIndex = 4;
            // 
            // lblSanPham
            // 
            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSanPham.Location = new System.Drawing.Point(20, 85);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.Size = new System.Drawing.Size(105, 28);
            this.lblSanPham.TabIndex = 3;
            this.lblSanPham.Text = "Sản phẩm:";
            // 
            // cboLoaiSanPham
            // 
            this.cboLoaiSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiSanPham.FormattingEnabled = true;
            this.cboLoaiSanPham.Location = new System.Drawing.Point(190, 32);
            this.cboLoaiSanPham.Name = "cboLoaiSanPham";
            this.cboLoaiSanPham.Size = new System.Drawing.Size(200, 36);
            this.cboLoaiSanPham.TabIndex = 2;
            this.cboLoaiSanPham.SelectedIndexChanged += new System.EventHandler(this.cboLoaiSanPham_SelectedIndexChanged);
            // 
            // lblLoaiSanPham
            // 
            this.lblLoaiSanPham.AutoSize = true;
            this.lblLoaiSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLoaiSanPham.Location = new System.Drawing.Point(20, 35);
            this.lblLoaiSanPham.Name = "lblLoaiSanPham";
            this.lblLoaiSanPham.Size = new System.Drawing.Size(146, 28);
            this.lblLoaiSanPham.TabIndex = 1;
            this.lblLoaiSanPham.Text = "Loại sản phẩm:";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLoaiSP,
            this.colMaSP,
            this.colTenSP,
            this.colXoa});
            this.dgvChiTiet.Location = new System.Drawing.Point(20, 140);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 62;
            this.dgvChiTiet.RowTemplate.Height = 28;
            this.dgvChiTiet.Size = new System.Drawing.Size(650, 190);
            this.dgvChiTiet.TabIndex = 0;
            this.dgvChiTiet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellContentClick);
            // 
            // colLoaiSP
            // 
            this.colLoaiSP.HeaderText = "Loại SP";
            this.colLoaiSP.MinimumWidth = 8;
            this.colLoaiSP.Name = "colLoaiSP";
            // 
            // colMaSP
            // 
            this.colMaSP.HeaderText = "Mã SP";
            this.colMaSP.MinimumWidth = 8;
            this.colMaSP.Name = "colMaSP";
            // 
            // colTenSP
            // 
            this.colTenSP.HeaderText = "Tên SP";
            this.colTenSP.MinimumWidth = 8;
            this.colTenSP.Name = "colTenSP";
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
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(430, 800);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(140, 50);
            this.btnLuu.TabIndex = 16;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(580, 800);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(140, 50);
            this.btnHuy.TabIndex = 17;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmThemSuaKhuyenMai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(750, 870);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.grpSanPham);
            this.Controls.Add(this.chkTrangThai);
            this.Controls.Add(this.dtpNgayKetThuc);
            this.Controls.Add(this.lblNgayKetThuc);
            this.Controls.Add(this.dtpNgayBatDau);
            this.Controls.Add(this.lblNgayBatDau);
            this.Controls.Add(this.numGiaTriGiam);
            this.Controls.Add(this.lblGiaTriGiam);
            this.Controls.Add(this.cboKieuGiam);
            this.Controls.Add(this.lblKieuGiam);
            this.Controls.Add(this.cboLoaiKM);
            this.Controls.Add(this.lblLoaiKM);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.txtTenKM);
            this.Controls.Add(this.lblTenKM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemSuaKhuyenMai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Khuyến Mãi";
            this.Load += new System.EventHandler(this.frmThemSuaKhuyenMai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numGiaTriGiam)).EndInit();
            this.grpSanPham.ResumeLayout(false);
            this.grpSanPham.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

#endregion
        private System.Windows.Forms.Label lblTenKM;
        private System.Windows.Forms.TextBox txtTenKM;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblLoaiKM;
        private System.Windows.Forms.ComboBox cboLoaiKM;
        private System.Windows.Forms.Label lblKieuGiam;
        private System.Windows.Forms.ComboBox cboKieuGiam;
        private System.Windows.Forms.Label lblGiaTriGiam;
        private System.Windows.Forms.NumericUpDown numGiaTriGiam;
        private System.Windows.Forms.Label lblNgayBatDau;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.Label lblNgayKetThuc;
        private System.Windows.Forms.DateTimePicker dtpNgayKetThuc;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.GroupBox grpSanPham;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.ComboBox cboLoaiSanPham;
        private System.Windows.Forms.Label lblLoaiSanPham;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblSanPham;
        private System.Windows.Forms.Button btnThemSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewButtonColumn colXoa;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}