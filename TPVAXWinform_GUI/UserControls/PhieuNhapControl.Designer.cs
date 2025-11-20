namespace TPVAXWinform_GUI.UserControls
{
    partial class PhieuNhapControl
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
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.btnDatLai = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.dgvPhieuNhap = new System.Windows.Forms.DataGridView();
            this.colMaPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayLap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemXemChiTiet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInPhieuNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemXacNhanNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTieuDe.SuspendLayout();
            this.pnlLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
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
            this.lblTieuDe.Text = "QUẢN LÝ PHIẾU NHẬP VACCINE";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLoc
            // 
            this.pnlLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlLoc.Controls.Add(this.btnThemMoi);
            this.pnlLoc.Controls.Add(this.dtpDenNgay);
            this.pnlLoc.Controls.Add(this.dtpTuNgay);
            this.pnlLoc.Controls.Add(this.lblDenNgay);
            this.pnlLoc.Controls.Add(this.lblTuNgay);
            this.pnlLoc.Controls.Add(this.btnDatLai);
            this.pnlLoc.Controls.Add(this.txtSearch);
            this.pnlLoc.Controls.Add(this.lblTimKiem);
            this.pnlLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoc.Location = new System.Drawing.Point(0, 108);
            this.pnlLoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlLoc.Size = new System.Drawing.Size(1800, 120);
            this.pnlLoc.TabIndex = 1;
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThemMoi.FlatAppearance.BorderSize = 0;
            this.btnThemMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemMoi.ForeColor = System.Drawing.Color.White;
            this.btnThemMoi.Location = new System.Drawing.Point(1458, 35);
            this.btnThemMoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(180, 55);
            this.btnThemMoi.TabIndex = 15;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.UseVisualStyleBackColor = false;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Checked = false;
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(455, 43);
            this.dtpDenNgay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.ShowCheckBox = true;
            this.dtpDenNgay.Size = new System.Drawing.Size(220, 37);
            this.dtpDenNgay.TabIndex = 14;
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.dtpDenNgay_ValueChanged);
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Checked = false;
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(120, 43);
            this.dtpTuNgay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.ShowCheckBox = true;
            this.dtpTuNgay.Size = new System.Drawing.Size(220, 37);
            this.dtpTuNgay.TabIndex = 13;
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.dtpTuNgay_ValueChanged);
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDenNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblDenNgay.Location = new System.Drawing.Point(348, 47);
            this.lblDenNgay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(107, 28);
            this.lblDenNgay.TabIndex = 12;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTuNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTuNgay.Location = new System.Drawing.Point(18, 47);
            this.lblTuNgay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(94, 28);
            this.lblTuNgay.TabIndex = 11;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // btnDatLai
            // 
            this.btnDatLai.BackColor = System.Drawing.Color.Gray;
            this.btnDatLai.FlatAppearance.BorderSize = 0;
            this.btnDatLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatLai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDatLai.ForeColor = System.Drawing.Color.White;
            this.btnDatLai.Location = new System.Drawing.Point(1646, 34);
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
            this.txtSearch.Location = new System.Drawing.Point(809, 43);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(629, 37);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTimKiem.Location = new System.Drawing.Point(696, 48);
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(105, 28);
            this.lblTimKiem.TabIndex = 4;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // dgvPhieuNhap
            // 
            this.dgvPhieuNhap.AllowUserToAddRows = false;
            this.dgvPhieuNhap.AllowUserToDeleteRows = false;
            this.dgvPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhieuNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhieuNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhieuNhap.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhieuNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPhieuNhap.ColumnHeadersHeight = 40;
            this.dgvPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPhieuNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaPN,
            this.colNgayLap,
            this.colTenNV,
            this.colTenNCC,
            this.colTrangThai});
            this.dgvPhieuNhap.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhieuNhap.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhieuNhap.EnableHeadersVisualStyles = false;
            this.dgvPhieuNhap.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvPhieuNhap.Location = new System.Drawing.Point(0, 228);
            this.dgvPhieuNhap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvPhieuNhap.Name = "dgvPhieuNhap";
            this.dgvPhieuNhap.ReadOnly = true;
            this.dgvPhieuNhap.RowHeadersVisible = false;
            this.dgvPhieuNhap.RowHeadersWidth = 62;
            this.dgvPhieuNhap.RowTemplate.Height = 35;
            this.dgvPhieuNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhieuNhap.Size = new System.Drawing.Size(1800, 849);
            this.dgvPhieuNhap.TabIndex = 3;
            this.dgvPhieuNhap.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPhieuNhap_CellMouseDown);
            // 
            // colMaPN
            // 
            this.colMaPN.FillWeight = 80F;
            this.colMaPN.HeaderText = "Mã Phiếu Nhập";
            this.colMaPN.MinimumWidth = 8;
            this.colMaPN.Name = "colMaPN";
            this.colMaPN.ReadOnly = true;
            // 
            // colNgayLap
            // 
            this.colNgayLap.FillWeight = 80F;
            this.colNgayLap.HeaderText = "Ngày Lập";
            this.colNgayLap.MinimumWidth = 8;
            this.colNgayLap.Name = "colNgayLap";
            this.colNgayLap.ReadOnly = true;
            // 
            // colTenNV
            // 
            this.colTenNV.HeaderText = "Tên Nhân Viên";
            this.colTenNV.MinimumWidth = 8;
            this.colTenNV.Name = "colTenNV";
            this.colTenNV.ReadOnly = true;
            // 
            // colTenNCC
            // 
            this.colTenNCC.HeaderText = "Tên Nhà Cung Cấp";
            this.colTenNCC.MinimumWidth = 8;
            this.colTenNCC.Name = "colTenNCC";
            this.colTenNCC.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.HeaderText = "Trạng Thái";
            this.colTrangThai.MinimumWidth = 8;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemXemChiTiet,
            this.toolStripMenuItemInPhieuNhap,
            this.toolStripMenuItemXacNhanNhap});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(237, 100);
            // 
            // toolStripMenuItemXemChiTiet
            // 
            this.toolStripMenuItemXemChiTiet.Name = "toolStripMenuItemXemChiTiet";
            this.toolStripMenuItemXemChiTiet.Size = new System.Drawing.Size(236, 32);
            this.toolStripMenuItemXemChiTiet.Text = "Xem chi tiết";
            this.toolStripMenuItemXemChiTiet.Click += new System.EventHandler(this.toolStripMenuItemXemChiTiet_Click);
            // 
            // toolStripMenuItemInPhieuNhap
            // 
            this.toolStripMenuItemInPhieuNhap.Name = "toolStripMenuItemInPhieuNhap";
            this.toolStripMenuItemInPhieuNhap.Size = new System.Drawing.Size(236, 32);
            this.toolStripMenuItemInPhieuNhap.Text = "In phiếu nhập";
            this.toolStripMenuItemInPhieuNhap.Click += new System.EventHandler(this.toolStripMenuItemInPhieuNhap_Click);
            // 
            // toolStripMenuItemXacNhanNhap
            // 
            this.toolStripMenuItemXacNhanNhap.Name = "toolStripMenuItemXacNhanNhap";
            this.toolStripMenuItemXacNhanNhap.Size = new System.Drawing.Size(236, 32);
            this.toolStripMenuItemXacNhanNhap.Text = "Xác nhận nhập kho";
            this.toolStripMenuItemXacNhanNhap.Click += new System.EventHandler(this.toolStripMenuItemXacNhanNhap_Click);
            // 
            // PhieuNhapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvPhieuNhap);
            this.Controls.Add(this.pnlLoc);
            this.Controls.Add(this.pnlTieuDe);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PhieuNhapControl";
            this.Size = new System.Drawing.Size(1800, 1077);
            this.Load += new System.EventHandler(this.PhieuNhapControl_Load);
            this.pnlTieuDe.ResumeLayout(false);
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

  private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnDatLai;
        private System.Windows.Forms.TextBox txtSearch;
   private System.Windows.Forms.Label lblTimKiem;
  private System.Windows.Forms.DataGridView dgvPhieuNhap;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
      private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPN;
 private System.Windows.Forms.DataGridViewTextBoxColumn colNgayLap;
   private System.Windows.Forms.DataGridViewTextBoxColumn colTenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNCC;
 private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
 private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemXemChiTiet;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInPhieuNhap;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemXacNhanNhap;
    }
}
