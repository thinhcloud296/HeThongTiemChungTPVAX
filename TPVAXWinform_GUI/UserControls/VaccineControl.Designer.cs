namespace TPVAXWinform_GUI.UserControls
{
    partial class VaccineControl
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
            this.numGiaMax = new System.Windows.Forms.NumericUpDown();
            this.numGiaMin = new System.Windows.Forms.NumericUpDown();
            this.lblDen = new System.Windows.Forms.Label();
            this.lblKhoangGia = new System.Windows.Forms.Label();
            this.cboLoaiBenh = new System.Windows.Forms.ComboBox();
            this.lblLoaiBenh = new System.Windows.Forms.Label();
            this.cboLoaiVaccine = new System.Windows.Forms.ComboBox();
            this.lblLoaiVaccine = new System.Windows.Forms.Label();
            this.btnDatLai = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.pnlHanhDong = new System.Windows.Forms.Panel();
            this.btnQuanLyDanhMuc = new System.Windows.Forms.Button();
            this.dgvVaccine = new System.Windows.Forms.DataGridView();
            this.colMaVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiBenh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongTon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoMuiToiDa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTieuDe.SuspendLayout();
            this.pnlLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaMin)).BeginInit();
            this.pnlHanhDong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccine)).BeginInit();
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
            this.lblTieuDe.Text = "QUẢN LÝ DANH MỤC VACCINE";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLoc
            // 
            this.pnlLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlLoc.Controls.Add(this.numGiaMax);
            this.pnlLoc.Controls.Add(this.numGiaMin);
            this.pnlLoc.Controls.Add(this.lblDen);
            this.pnlLoc.Controls.Add(this.lblKhoangGia);
            this.pnlLoc.Controls.Add(this.cboLoaiBenh);
            this.pnlLoc.Controls.Add(this.lblLoaiBenh);
            this.pnlLoc.Controls.Add(this.cboLoaiVaccine);
            this.pnlLoc.Controls.Add(this.lblLoaiVaccine);
            this.pnlLoc.Controls.Add(this.btnDatLai);
            this.pnlLoc.Controls.Add(this.txtSearch);
            this.pnlLoc.Controls.Add(this.lblTimKiem);
            this.pnlLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoc.Location = new System.Drawing.Point(0, 108);
            this.pnlLoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlLoc.Size = new System.Drawing.Size(1800, 160);
            this.pnlLoc.TabIndex = 1;
            // 
            // numGiaMax
            // 
            this.numGiaMax.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numGiaMax.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numGiaMax.Location = new System.Drawing.Point(665, 106);
            this.numGiaMax.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numGiaMax.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGiaMax.Name = "numGiaMax";
            this.numGiaMax.Size = new System.Drawing.Size(180, 37);
            this.numGiaMax.TabIndex = 16;
            this.numGiaMax.ThousandsSeparator = true;
            this.numGiaMax.ValueChanged += new System.EventHandler(this.numGia_ValueChanged);
            // 
            // numGiaMin
            // 
            this.numGiaMin.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numGiaMin.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numGiaMin.Location = new System.Drawing.Point(294, 106);
            this.numGiaMin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numGiaMin.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGiaMin.Name = "numGiaMin";
            this.numGiaMin.Size = new System.Drawing.Size(180, 37);
            this.numGiaMin.TabIndex = 15;
            this.numGiaMin.ThousandsSeparator = true;
            this.numGiaMin.ValueChanged += new System.EventHandler(this.numGia_ValueChanged);
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblDen.Location = new System.Drawing.Point(600, 111);
            this.lblDen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(50, 28);
            this.lblDen.TabIndex = 14;
            this.lblDen.Text = "Đến";
            // 
            // lblKhoangGia
            // 
            this.lblKhoangGia.AutoSize = true;
            this.lblKhoangGia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKhoangGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblKhoangGia.Location = new System.Drawing.Point(45, 111);
            this.lblKhoangGia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKhoangGia.Name = "lblKhoangGia";
            this.lblKhoangGia.Size = new System.Drawing.Size(124, 28);
            this.lblKhoangGia.TabIndex = 12;
            this.lblKhoangGia.Text = "Khoảng giá:";
            // 
            // cboLoaiBenh
            // 
            this.cboLoaiBenh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiBenh.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboLoaiBenh.FormattingEnabled = true;
            this.cboLoaiBenh.Location = new System.Drawing.Point(480, 27);
            this.cboLoaiBenh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboLoaiBenh.Name = "cboLoaiBenh";
            this.cboLoaiBenh.Size = new System.Drawing.Size(280, 38);
            this.cboLoaiBenh.TabIndex = 11;
            this.cboLoaiBenh.SelectedIndexChanged += new System.EventHandler(this.cboLoaiBenh_SelectedIndexChanged);
            // 
            // lblLoaiBenh
            // 
            this.lblLoaiBenh.AutoSize = true;
            this.lblLoaiBenh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLoaiBenh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblLoaiBenh.Location = new System.Drawing.Point(354, 32);
            this.lblLoaiBenh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoaiBenh.Name = "lblLoaiBenh";
            this.lblLoaiBenh.Size = new System.Drawing.Size(109, 28);
            this.lblLoaiBenh.TabIndex = 10;
            this.lblLoaiBenh.Text = "Loại bệnh:";
            // 
            // cboLoaiVaccine
            // 
            this.cboLoaiVaccine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiVaccine.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboLoaiVaccine.FormattingEnabled = true;
            this.cboLoaiVaccine.Location = new System.Drawing.Point(50, 27);
            this.cboLoaiVaccine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboLoaiVaccine.Name = "cboLoaiVaccine";
            this.cboLoaiVaccine.Size = new System.Drawing.Size(280, 38);
            this.cboLoaiVaccine.TabIndex = 9;
            this.cboLoaiVaccine.SelectedIndexChanged += new System.EventHandler(this.cboLoaiVaccine_SelectedIndexChanged);
            // 
            // lblLoaiVaccine
            // 
            this.lblLoaiVaccine.AutoSize = true;
            this.lblLoaiVaccine.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLoaiVaccine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblLoaiVaccine.Location = new System.Drawing.Point(45, -6);
            this.lblLoaiVaccine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoaiVaccine.Name = "lblLoaiVaccine";
            this.lblLoaiVaccine.Size = new System.Drawing.Size(133, 28);
            this.lblLoaiVaccine.TabIndex = 8;
            this.lblLoaiVaccine.Text = "Loại Vaccine:";
            // 
            // btnDatLai
            // 
            this.btnDatLai.BackColor = System.Drawing.Color.Gray;
            this.btnDatLai.FlatAppearance.BorderSize = 0;
            this.btnDatLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatLai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDatLai.ForeColor = System.Drawing.Color.White;
            this.btnDatLai.Location = new System.Drawing.Point(1629, 19);
            this.btnDatLai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDatLai.Name = "btnDatLai";
            this.btnDatLai.Size = new System.Drawing.Size(150, 55);
            this.btnDatLai.TabIndex = 7;
            this.btnDatLai.Text = "Đặt lại";
            this.btnDatLai.UseVisualStyleBackColor = false;
            this.btnDatLai.Click += new System.EventHandler(this.btnDatLai_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(939, 28);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(682, 37);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTimKiem.Location = new System.Drawing.Point(790, 33);
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(105, 28);
            this.lblTimKiem.TabIndex = 4;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // pnlHanhDong
            // 
            this.pnlHanhDong.BackColor = System.Drawing.Color.White;
            this.pnlHanhDong.Controls.Add(this.btnQuanLyDanhMuc);
            this.pnlHanhDong.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHanhDong.Location = new System.Drawing.Point(0, 268);
            this.pnlHanhDong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHanhDong.Name = "pnlHanhDong";
            this.pnlHanhDong.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlHanhDong.Size = new System.Drawing.Size(1800, 92);
            this.pnlHanhDong.TabIndex = 2;
            // 
            // btnQuanLyDanhMuc
            // 
            this.btnQuanLyDanhMuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnQuanLyDanhMuc.FlatAppearance.BorderSize = 0;
            this.btnQuanLyDanhMuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnQuanLyDanhMuc.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyDanhMuc.Location = new System.Drawing.Point(50, 20);
            this.btnQuanLyDanhMuc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQuanLyDanhMuc.Name = "btnQuanLyDanhMuc";
            this.btnQuanLyDanhMuc.Size = new System.Drawing.Size(312, 50);
            this.btnQuanLyDanhMuc.TabIndex = 0;
            this.btnQuanLyDanhMuc.Text = "Quản lý Danh mục Vaccine";
            this.btnQuanLyDanhMuc.UseVisualStyleBackColor = false;
            this.btnQuanLyDanhMuc.Visible = false;
            this.btnQuanLyDanhMuc.Click += new System.EventHandler(this.btnQuanLyDanhMuc_Click);
            // 
            // dgvVaccine
            // 
            this.dgvVaccine.AllowUserToAddRows = false;
            this.dgvVaccine.AllowUserToDeleteRows = false;
            this.dgvVaccine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVaccine.BackgroundColor = System.Drawing.Color.White;
            this.dgvVaccine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVaccine.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVaccine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVaccine.ColumnHeadersHeight = 40;
            this.dgvVaccine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVaccine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaVC,
            this.colTenVC,
            this.colLoaiBenh,
            this.colGiaBan,
            this.colSoLuongTon,
            this.colMaLoai,
            this.colSoMuiToiDa,
            this.colMoTa});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVaccine.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVaccine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVaccine.EnableHeadersVisualStyles = false;
            this.dgvVaccine.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvVaccine.Location = new System.Drawing.Point(0, 360);
            this.dgvVaccine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvVaccine.Name = "dgvVaccine";
            this.dgvVaccine.ReadOnly = true;
            this.dgvVaccine.RowHeadersVisible = false;
            this.dgvVaccine.RowHeadersWidth = 62;
            this.dgvVaccine.RowTemplate.Height = 35;
            this.dgvVaccine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVaccine.Size = new System.Drawing.Size(1800, 717);
            this.dgvVaccine.TabIndex = 3;
            this.dgvVaccine.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvVaccine_CellFormatting);
            // 
            // colMaVC
            // 
            this.colMaVC.FillWeight = 60F;
            this.colMaVC.HeaderText = "Mã Vaccine";
            this.colMaVC.MinimumWidth = 8;
            this.colMaVC.Name = "colMaVC";
            this.colMaVC.ReadOnly = true;
            // 
            // colTenVC
            // 
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
            this.colGiaBan.HeaderText = "Giá bán";
            this.colGiaBan.MinimumWidth = 8;
            this.colGiaBan.Name = "colGiaBan";
            this.colGiaBan.ReadOnly = true;
            // 
            // colSoLuongTon
            // 
            this.colSoLuongTon.FillWeight = 70F;
            this.colSoLuongTon.HeaderText = "Số lượng tồn";
            this.colSoLuongTon.MinimumWidth = 8;
            this.colSoLuongTon.Name = "colSoLuongTon";
            this.colSoLuongTon.ReadOnly = true;
            // 
            // colMaLoai
            // 
            this.colMaLoai.FillWeight = 80F;
            this.colMaLoai.HeaderText = "Loại Vaccine";
            this.colMaLoai.MinimumWidth = 8;
            this.colMaLoai.Name = "colMaLoai";
            this.colMaLoai.ReadOnly = true;
            // 
            // colSoMuiToiDa
            // 
            this.colSoMuiToiDa.DataPropertyName = "SoMuiToiDa";
            this.colSoMuiToiDa.FillWeight = 60F;
            this.colSoMuiToiDa.HeaderText = "Số mũi";
            this.colSoMuiToiDa.MinimumWidth = 8;
            this.colSoMuiToiDa.Name = "colSoMuiToiDa";
            this.colSoMuiToiDa.ReadOnly = true;
            this.colSoMuiToiDa.Visible = false;
            // 
            // colMoTa
            // 
            this.colMoTa.DataPropertyName = "MoTa";
            this.colMoTa.HeaderText = "Mô tả";
            this.colMoTa.MinimumWidth = 8;
            this.colMoTa.Name = "colMoTa";
            this.colMoTa.ReadOnly = true;
            // 
            // VaccineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvVaccine);
            this.Controls.Add(this.pnlHanhDong);
            this.Controls.Add(this.pnlLoc);
            this.Controls.Add(this.pnlTieuDe);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "VaccineControl";
            this.Size = new System.Drawing.Size(1800, 1077);
            this.Load += new System.EventHandler(this.VaccineControl_Load);
            this.pnlTieuDe.ResumeLayout(false);
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaMin)).EndInit();
            this.pnlHanhDong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaccine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.Button btnDatLai;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Panel pnlHanhDong;
        private System.Windows.Forms.DataGridView dgvVaccine;
        private System.Windows.Forms.ComboBox cboLoaiVaccine;
        private System.Windows.Forms.Label lblLoaiVaccine;
        private System.Windows.Forms.ComboBox cboLoaiBenh;
        private System.Windows.Forms.Label lblLoaiBenh;
        private System.Windows.Forms.Label lblKhoangGia;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.NumericUpDown numGiaMin;
        private System.Windows.Forms.NumericUpDown numGiaMax;
        private System.Windows.Forms.Button btnQuanLyDanhMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiBenh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongTon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoMuiToiDa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTa;
    }
}
