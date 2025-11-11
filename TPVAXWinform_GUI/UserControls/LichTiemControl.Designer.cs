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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTieuDe = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlLoc = new System.Windows.Forms.Panel();
            this.btnDatLai = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.dtpNgayHen = new System.Windows.Forms.DateTimePicker();
            this.lblChonNgay = new System.Windows.Forms.Label();
            this.pnlHanhDong = new System.Windows.Forms.Panel();
            this.btnThemLichHen = new System.Windows.Forms.Button();
            this.dgvLichTiem = new System.Windows.Forms.DataGridView();
            this.colNgayHen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHSTC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNguoiTiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTiemThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheckIn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colHuy = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlTieuDe.SuspendLayout();
            this.pnlLoc.SuspendLayout();
            this.pnlHanhDong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichTiem)).BeginInit();
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
            this.pnlLoc.Controls.Add(this.btnDatLai);
            this.pnlLoc.Controls.Add(this.btnTimKiem);
            this.pnlLoc.Controls.Add(this.txtSearch);
            this.pnlLoc.Controls.Add(this.lblTimKiem);
            this.pnlLoc.Controls.Add(this.cboTrangThai);
            this.pnlLoc.Controls.Add(this.lblTrangThai);
            this.pnlLoc.Controls.Add(this.dtpNgayHen);
            this.pnlLoc.Controls.Add(this.lblChonNgay);
            this.pnlLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoc.Location = new System.Drawing.Point(0, 108);
            this.pnlLoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlLoc.Size = new System.Drawing.Size(1800, 123);
            this.pnlLoc.TabIndex = 1;
            // 
            // btnDatLai
            // 
            this.btnDatLai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDatLai.FlatAppearance.BorderSize = 0;
            this.btnDatLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatLai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDatLai.ForeColor = System.Drawing.Color.White;
            this.btnDatLai.Location = new System.Drawing.Point(1650, 31);
            this.btnDatLai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDatLai.Name = "btnDatLai";
            this.btnDatLai.Size = new System.Drawing.Size(120, 62);
            this.btnDatLai.TabIndex = 7;
            this.btnDatLai.Text = "🔄 Đặt lại";
            this.btnDatLai.UseVisualStyleBackColor = false;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(1500, 31);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(135, 62);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "🔍 Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(1095, 40);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(373, 37);
            this.txtSearch.TabIndex = 5;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTimKiem.Location = new System.Drawing.Point(960, 45);
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(105, 28);
            this.lblTimKiem.TabIndex = 4;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Tất cả",
            "Chưa tiêm",
            "Đã tiêm",
            "Đã hủy"});
            this.cboTrangThai.Location = new System.Drawing.Point(645, 38);
            this.cboTrangThai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(268, 38);
            this.cboTrangThai.TabIndex = 3;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTrangThai.Location = new System.Drawing.Point(495, 45);
            this.lblTrangThai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(113, 28);
            this.lblTrangThai.TabIndex = 2;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // dtpNgayHen
            // 
            this.dtpNgayHen.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgayHen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHen.Location = new System.Drawing.Point(195, 38);
            this.dtpNgayHen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpNgayHen.Name = "dtpNgayHen";
            this.dtpNgayHen.Size = new System.Drawing.Size(268, 37);
            this.dtpNgayHen.TabIndex = 1;
            // 
            // lblChonNgay
            // 
            this.lblChonNgay.AutoSize = true;
            this.lblChonNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChonNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblChonNgay.Location = new System.Drawing.Point(45, 45);
            this.lblChonNgay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChonNgay.Name = "lblChonNgay";
            this.lblChonNgay.Size = new System.Drawing.Size(117, 28);
            this.lblChonNgay.TabIndex = 0;
            this.lblChonNgay.Text = "Chọn ngày:";
            // 
            // pnlHanhDong
            // 
            this.pnlHanhDong.BackColor = System.Drawing.Color.White;
            this.pnlHanhDong.Controls.Add(this.btnThemLichHen);
            this.pnlHanhDong.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHanhDong.Location = new System.Drawing.Point(0, 231);
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
            this.colNgayHen,
            this.colMaHSTC,
            this.colTenNguoiTiem,
            this.colTenKhachHang,
            this.colSoDT,
            this.colTrangThai,
            this.colNgayTiemThucTe,
            this.colCheckIn,
            this.colHuy});
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
            this.dgvLichTiem.Location = new System.Drawing.Point(0, 323);
            this.dgvLichTiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvLichTiem.Name = "dgvLichTiem";
            this.dgvLichTiem.ReadOnly = true;
            this.dgvLichTiem.RowHeadersVisible = false;
            this.dgvLichTiem.RowHeadersWidth = 62;
            this.dgvLichTiem.RowTemplate.Height = 35;
            this.dgvLichTiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichTiem.Size = new System.Drawing.Size(1800, 754);
            this.dgvLichTiem.TabIndex = 3;
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
            // colTenKhachHang
            // 
            this.colTenKhachHang.DataPropertyName = "TenKhachHang";
            this.colTenKhachHang.HeaderText = "Tên Khách hàng";
            this.colTenKhachHang.MinimumWidth = 8;
            this.colTenKhachHang.Name = "colTenKhachHang";
            this.colTenKhachHang.ReadOnly = true;
            // 
            // colSoDT
            // 
            this.colSoDT.DataPropertyName = "SoDT";
            this.colSoDT.FillWeight = 90F;
            this.colSoDT.HeaderText = "Số ĐT";
            this.colSoDT.MinimumWidth = 8;
            this.colSoDT.Name = "colSoDT";
            this.colSoDT.ReadOnly = true;
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
            this.pnlTieuDe.ResumeLayout(false);
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            this.pnlHanhDong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichTiem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.Label lblChonNgay;
        private System.Windows.Forms.DateTimePicker dtpNgayHen;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnDatLai;
        private System.Windows.Forms.Panel pnlHanhDong;
        private System.Windows.Forms.Button btnThemLichHen;
        private System.Windows.Forms.DataGridView dgvLichTiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayHen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHSTC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNguoiTiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTiemThucTe;
        private System.Windows.Forms.DataGridViewButtonColumn colCheckIn;
        private System.Windows.Forms.DataGridViewButtonColumn colHuy;
    }
}
