namespace TPVAXWinform_GUI.UserControls
{
    partial class HoaDonControl
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
            this.numGiaDen = new System.Windows.Forms.NumericUpDown();
            this.numGiaTu = new System.Windows.Forms.NumericUpDown();
            this.lblGiaDen = new System.Windows.Forms.Label();
            this.lblGiaTu = new System.Windows.Forms.Label();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.btnDatLai = new System.Windows.Forms.Button();
            this.txtSearchMaHD = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.contextMenuStripHoaDon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemXemChiTiet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemXacNhanThanhToan = new System.Windows.Forms.ToolStripMenuItem();
            this.colMaHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayLap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaKM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTieuDe.SuspendLayout();
            this.pnlLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaDen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.contextMenuStripHoaDon.SuspendLayout();
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
            this.lblTieuDe.Text = "QUẢN LÝ HÓA ĐƠN";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLoc
            // 
            this.pnlLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlLoc.Controls.Add(this.numGiaDen);
            this.pnlLoc.Controls.Add(this.numGiaTu);
            this.pnlLoc.Controls.Add(this.lblGiaDen);
            this.pnlLoc.Controls.Add(this.lblGiaTu);
            this.pnlLoc.Controls.Add(this.lblDen);
            this.pnlLoc.Controls.Add(this.dtpDenNgay);
            this.pnlLoc.Controls.Add(this.dtpTuNgay);
            this.pnlLoc.Controls.Add(this.lblTuNgay);
            this.pnlLoc.Controls.Add(this.btnDatLai);
            this.pnlLoc.Controls.Add(this.txtSearchMaHD);
            this.pnlLoc.Controls.Add(this.lblTimKiem);
            this.pnlLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoc.Location = new System.Drawing.Point(0, 108);
            this.pnlLoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlLoc.Size = new System.Drawing.Size(1800, 200);
            this.pnlLoc.TabIndex = 1;
            // 
            // numGiaDen
            // 
            this.numGiaDen.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numGiaDen.Increment = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numGiaDen.Location = new System.Drawing.Point(573, 138);
            this.numGiaDen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numGiaDen.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGiaDen.Name = "numGiaDen";
            this.numGiaDen.Size = new System.Drawing.Size(180, 37);
            this.numGiaDen.TabIndex = 14;
            this.numGiaDen.ThousandsSeparator = true;
            // 
            // numGiaTu
            // 
            this.numGiaTu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numGiaTu.Increment = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numGiaTu.Location = new System.Drawing.Point(294, 138);
            this.numGiaTu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numGiaTu.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGiaTu.Name = "numGiaTu";
            this.numGiaTu.Size = new System.Drawing.Size(180, 37);
            this.numGiaTu.TabIndex = 13;
            this.numGiaTu.ThousandsSeparator = true;
            // 
            // lblGiaDen
            // 
            this.lblGiaDen.AutoSize = true;
            this.lblGiaDen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGiaDen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblGiaDen.Location = new System.Drawing.Point(500, 143);
            this.lblGiaDen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGiaDen.Name = "lblGiaDen";
            this.lblGiaDen.Size = new System.Drawing.Size(50, 28);
            this.lblGiaDen.TabIndex = 12;
            this.lblGiaDen.Text = "Đến";
            // 
            // lblGiaTu
            // 
            this.lblGiaTu.AutoSize = true;
            this.lblGiaTu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGiaTu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblGiaTu.Location = new System.Drawing.Point(45, 143);
            this.lblGiaTu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGiaTu.Name = "lblGiaTu";
            this.lblGiaTu.Size = new System.Drawing.Size(236, 28);
            this.lblGiaTu.TabIndex = 11;
            this.lblGiaTu.Text = "Lọc theo khoảng giá từ:";
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblDen.Location = new System.Drawing.Point(500, 33);
            this.lblDen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(50, 28);
            this.lblDen.TabIndex = 10;
            this.lblDen.Text = "Đến";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(573, 28);
            this.dtpDenNgay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(180, 37);
            this.dtpDenNgay.TabIndex = 9;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(294, 28);
            this.dtpTuNgay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(180, 37);
            this.dtpTuNgay.TabIndex = 8;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTuNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTuNgay.Location = new System.Drawing.Point(45, 33);
            this.lblTuNgay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(217, 28);
            this.lblTuNgay.TabIndex = 7;
            this.lblTuNgay.Text = "Lọc theo thời gian từ:";
            // 
            // btnDatLai
            // 
            this.btnDatLai.BackColor = System.Drawing.Color.Gray;
            this.btnDatLai.FlatAppearance.BorderSize = 0;
            this.btnDatLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatLai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDatLai.ForeColor = System.Drawing.Color.White;
            this.btnDatLai.Location = new System.Drawing.Point(1656, 20);
            this.btnDatLai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDatLai.Name = "btnDatLai";
            this.btnDatLai.Size = new System.Drawing.Size(120, 55);
            this.btnDatLai.TabIndex = 6;
            this.btnDatLai.Text = "Đặt lại";
            this.btnDatLai.UseVisualStyleBackColor = false;
            this.btnDatLai.Click += new System.EventHandler(this.btnDatLai_Click);
            // 
            // txtSearchMaHD
            // 
            this.txtSearchMaHD.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearchMaHD.Location = new System.Drawing.Point(1248, 28);
            this.txtSearchMaHD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearchMaHD.Name = "txtSearchMaHD";
            this.txtSearchMaHD.Size = new System.Drawing.Size(373, 37);
            this.txtSearchMaHD.TabIndex = 5;
            this.txtSearchMaHD.TextChanged += new System.EventHandler(this.txtSearchMaHD_TextChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTimKiem.Location = new System.Drawing.Point(996, 33);
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(225, 28);
            this.lblTimKiem.TabIndex = 4;
            this.lblTimKiem.Text = "Tìm kiếm mã hóa đơn:";
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AllowUserToDeleteRows = false;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHoaDon.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHoaDon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHoaDon.ColumnHeadersHeight = 40;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaHD,
            this.colNgayLap,
            this.colTongTien,
            this.colTrangThai,
            this.colMaKH,
            this.colMaNV,
            this.colMaKM});
            this.dgvHoaDon.ContextMenuStrip = this.contextMenuStripHoaDon;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHoaDon.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHoaDon.EnableHeadersVisualStyles = false;
            this.dgvHoaDon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvHoaDon.Location = new System.Drawing.Point(0, 308);
            this.dgvHoaDon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.RowHeadersVisible = false;
            this.dgvHoaDon.RowHeadersWidth = 62;
            this.dgvHoaDon.RowTemplate.Height = 35;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.Size = new System.Drawing.Size(1800, 769);
            this.dgvHoaDon.TabIndex = 2;
            // 
            // contextMenuStripHoaDon
            // 
            this.contextMenuStripHoaDon.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripHoaDon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemXemChiTiet,
            this.toolStripMenuItemInHoaDon,
            this.toolStripSeparator1,
            this.toolStripMenuItemXacNhanThanhToan});
            this.contextMenuStripHoaDon.Name = "contextMenuStripHoaDon";
            this.contextMenuStripHoaDon.Size = new System.Drawing.Size(280, 106);
            this.contextMenuStripHoaDon.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripHoaDon_Opening);
            // 
            // toolStripMenuItemXemChiTiet
            // 
            this.toolStripMenuItemXemChiTiet.Name = "toolStripMenuItemXemChiTiet";
            this.toolStripMenuItemXemChiTiet.Size = new System.Drawing.Size(279, 32);
            this.toolStripMenuItemXemChiTiet.Text = "📋 Xem chi tiết hóa đơn";
            this.toolStripMenuItemXemChiTiet.Click += new System.EventHandler(this.toolStripMenuItemXemChiTiet_Click);
            // 
            // toolStripMenuItemInHoaDon
            // 
            this.toolStripMenuItemInHoaDon.Name = "toolStripMenuItemInHoaDon";
            this.toolStripMenuItemInHoaDon.Size = new System.Drawing.Size(279, 32);
            this.toolStripMenuItemInHoaDon.Text = "🖨️ In hóa đơn";
            this.toolStripMenuItemInHoaDon.Click += new System.EventHandler(this.toolStripMenuItemInHoaDon_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(276, 6);
            // 
            // toolStripMenuItemXacNhanThanhToan
            // 
            this.toolStripMenuItemXacNhanThanhToan.Name = "toolStripMenuItemXacNhanThanhToan";
            this.toolStripMenuItemXacNhanThanhToan.Size = new System.Drawing.Size(279, 32);
            this.toolStripMenuItemXacNhanThanhToan.Text = "💳 Xác nhận thanh toán";
            this.toolStripMenuItemXacNhanThanhToan.Click += new System.EventHandler(this.toolStripMenuItemXacNhanThanhToan_Click);
            // 
            // colMaHD
            // 
            this.colMaHD.DataPropertyName = "MaHD";
            this.colMaHD.FillWeight = 80F;
            this.colMaHD.HeaderText = "Mã hóa đơn";
            this.colMaHD.MinimumWidth = 8;
            this.colMaHD.Name = "colMaHD";
            this.colMaHD.ReadOnly = true;
            // 
            // colNgayLap
            // 
            this.colNgayLap.DataPropertyName = "NgayLap";
            this.colNgayLap.FillWeight = 90F;
            this.colNgayLap.HeaderText = "Ngày lập";
            this.colNgayLap.MinimumWidth = 8;
            this.colNgayLap.Name = "colNgayLap";
            this.colNgayLap.ReadOnly = true;
            // 
            // colTongTien
            // 
            this.colTongTien.DataPropertyName = "TongTien";
            this.colTongTien.FillWeight = 90F;
            this.colTongTien.HeaderText = "Tổng tiền";
            this.colTongTien.MinimumWidth = 8;
            this.colTongTien.Name = "colTongTien";
            this.colTongTien.ReadOnly = true;
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
            // colMaKH
            // 
            this.colMaKH.DataPropertyName = "MaKH";
            this.colMaKH.FillWeight = 80F;
            this.colMaKH.HeaderText = "Mã khách hàng";
            this.colMaKH.MinimumWidth = 8;
            this.colMaKH.Name = "colMaKH";
            this.colMaKH.ReadOnly = true;
            // 
            // colMaNV
            // 
            this.colMaNV.DataPropertyName = "MaNV";
            this.colMaNV.FillWeight = 80F;
            this.colMaNV.HeaderText = "Mã nhân viên";
            this.colMaNV.MinimumWidth = 8;
            this.colMaNV.Name = "colMaNV";
            this.colMaNV.ReadOnly = true;
            // 
            // colMaKM
            // 
            this.colMaKM.DataPropertyName = "MaKM";
            this.colMaKM.FillWeight = 80F;
            this.colMaKM.HeaderText = "Mã khuyến mãi";
            this.colMaKM.MinimumWidth = 8;
            this.colMaKM.Name = "colMaKM";
            this.colMaKM.ReadOnly = true;
            this.colMaKM.Visible = false;
            // 
            // HoaDonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvHoaDon);
            this.Controls.Add(this.pnlLoc);
            this.Controls.Add(this.pnlTieuDe);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "HoaDonControl";
            this.Size = new System.Drawing.Size(1800, 1077);
            this.Load += new System.EventHandler(this.HoaDonControl_Load);
            this.pnlTieuDe.ResumeLayout(false);
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaDen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.contextMenuStripHoaDon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.NumericUpDown numGiaDen;
        private System.Windows.Forms.NumericUpDown numGiaTu;
        private System.Windows.Forms.Label lblGiaDen;
        private System.Windows.Forms.Label lblGiaTu;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Button btnDatLai;
        private System.Windows.Forms.TextBox txtSearchMaHD;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripHoaDon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemXemChiTiet;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInHoaDon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemXacNhanThanhToan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayLap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKM;
    }
}
