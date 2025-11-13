namespace TPVAXWinform_GUI
{
    partial class frmChiTietHoaDon
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTieuDe = new System.Windows.Forms.Panel();
   this.lblTieuDe = new System.Windows.Forms.Label();
  this.pnlThongTin = new System.Windows.Forms.Panel();
  this.lblTongTienValue = new System.Windows.Forms.Label();
   this.lblTongTien = new System.Windows.Forms.Label();
  this.lblMaHDValue = new System.Windows.Forms.Label();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.dgvChiTietHD = new System.Windows.Forms.DataGridView();
   this.colMaCTHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMaSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
 this.colTenSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
     this.colLoaiSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
    this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
       this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
   this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
     this.pnlButton = new System.Windows.Forms.Panel();
     this.btnDong = new System.Windows.Forms.Button();
    this.pnlTieuDe.SuspendLayout();
            this.pnlThongTin.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHD)).BeginInit();
    this.pnlButton.SuspendLayout();
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
  this.pnlTieuDe.Size = new System.Drawing.Size(1200, 80);
            this.pnlTieuDe.TabIndex = 0;
            // 
    // lblTieuDe
            // 
 this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
     this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
    this.lblTieuDe.ForeColor = System.Drawing.Color.White;
       this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
    this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
       this.lblTieuDe.Name = "lblTieuDe";
     this.lblTieuDe.Size = new System.Drawing.Size(1200, 80);
 this.lblTieuDe.TabIndex = 0;
    this.lblTieuDe.Text = "CHI TI?T HÓA ??N";
  this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
   // pnlThongTin
            // 
       this.pnlThongTin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
    this.pnlThongTin.Controls.Add(this.lblTongTienValue);
            this.pnlThongTin.Controls.Add(this.lblTongTien);
     this.pnlThongTin.Controls.Add(this.lblMaHDValue);
 this.pnlThongTin.Controls.Add(this.lblMaHD);
 this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
         this.pnlThongTin.Location = new System.Drawing.Point(0, 80);
            this.pnlThongTin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlThongTin.Name = "pnlThongTin";
      this.pnlThongTin.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
         this.pnlThongTin.Size = new System.Drawing.Size(1200, 100);
            this.pnlThongTin.TabIndex = 1;
  // 
   // lblTongTienValue
   // 
         this.lblTongTienValue.AutoSize = true;
   this.lblTongTienValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
   this.lblTongTienValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblTongTienValue.Location = new System.Drawing.Point(800, 35);
     this.lblTongTienValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
   this.lblTongTienValue.Name = "lblTongTienValue";
      this.lblTongTienValue.Size = new System.Drawing.Size(27, 32);
     this.lblTongTienValue.TabIndex = 3;
   this.lblTongTienValue.Text = "-";
            // 
       // lblTongTien
         // 
       this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
    this.lblTongTien.Location = new System.Drawing.Point(650, 37);
    this.lblTongTien.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
   this.lblTongTien.Name = "lblTongTien";
          this.lblTongTien.Size = new System.Drawing.Size(127, 30);
     this.lblTongTien.TabIndex = 2;
    this.lblTongTien.Text = "T?ng ti?n:";
       // 
 // lblMaHDValue
            // 
     this.lblMaHDValue.AutoSize = true;
     this.lblMaHDValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblMaHDValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
    this.lblMaHDValue.Location = new System.Drawing.Point(250, 35);
 this.lblMaHDValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaHDValue.Name = "lblMaHDValue";
            this.lblMaHDValue.Size = new System.Drawing.Size(27, 32);
 this.lblMaHDValue.TabIndex = 1;
    this.lblMaHDValue.Text = "-";
            // 
   // lblMaHD
      // 
this.lblMaHD.AutoSize = true;
     this.lblMaHD.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
this.lblMaHD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
     this.lblMaHD.Location = new System.Drawing.Point(45, 37);
   this.lblMaHD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
       this.lblMaHD.Name = "lblMaHD";
       this.lblMaHD.Size = new System.Drawing.Size(156, 30);
       this.lblMaHD.TabIndex = 0;
      this.lblMaHD.Text = "Mã hóa ??n:";
            // 
       // dgvChiTietHD
   // 
     this.dgvChiTietHD.AllowUserToAddRows = false;
   this.dgvChiTietHD.AllowUserToDeleteRows = false;
  this.dgvChiTietHD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietHD.BackgroundColor = System.Drawing.Color.White;
  this.dgvChiTietHD.BorderStyle = System.Windows.Forms.BorderStyle.None;
   this.dgvChiTietHD.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
         dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
 dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
         dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
   dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
       this.dgvChiTietHD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTietHD.ColumnHeadersHeight = 40;
         this.dgvChiTietHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
 this.dgvChiTietHD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
   this.colMaCTHD,
            this.colMaSanPham,
  this.colTenSanPham,
  this.colLoaiSanPham,
   this.colSoLuong,
       this.colDonGia,
     this.colThanhTien});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
   dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
         dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
    dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTietHD.DefaultCellStyle = dataGridViewCellStyle2;
this.dgvChiTietHD.Dock = System.Windows.Forms.DockStyle.Fill;
       this.dgvChiTietHD.EnableHeadersVisualStyles = false;
       this.dgvChiTietHD.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvChiTietHD.Location = new System.Drawing.Point(0, 180);
     this.dgvChiTietHD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
    this.dgvChiTietHD.Name = "dgvChiTietHD";
   this.dgvChiTietHD.ReadOnly = true;
     this.dgvChiTietHD.RowHeadersVisible = false;
   this.dgvChiTietHD.RowHeadersWidth = 62;
            this.dgvChiTietHD.RowTemplate.Height = 35;
        this.dgvChiTietHD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
    this.dgvChiTietHD.Size = new System.Drawing.Size(1200, 470);
     this.dgvChiTietHD.TabIndex = 2;
    // 
    // colMaCTHD
        // 
        this.colMaCTHD.DataPropertyName = "MaCTHD";
    this.colMaCTHD.FillWeight = 80F;
this.colMaCTHD.HeaderText = "Mã CTHD";
       this.colMaCTHD.MinimumWidth = 8;
       this.colMaCTHD.Name = "colMaCTHD";
        this.colMaCTHD.ReadOnly = true;
            // 
       // colMaSanPham
  // 
        this.colMaSanPham.DataPropertyName = "MaSanPham";
      this.colMaSanPham.FillWeight = 80F;
       this.colMaSanPham.HeaderText = "Mã s?n ph?m";
   this.colMaSanPham.MinimumWidth = 8;
        this.colMaSanPham.Name = "colMaSanPham";
            this.colMaSanPham.ReadOnly = true;
       // 
            // colTenSanPham
     // 
     this.colTenSanPham.DataPropertyName = "TenSanPham";
       this.colTenSanPham.HeaderText = "Tên s?n ph?m";
            this.colTenSanPham.MinimumWidth = 8;
            this.colTenSanPham.Name = "colTenSanPham";
        this.colTenSanPham.ReadOnly = true;
     // 
          // colLoaiSanPham
       // 
      this.colLoaiSanPham.DataPropertyName = "LoaiSanPham";
  this.colLoaiSanPham.FillWeight = 80F;
     this.colLoaiSanPham.HeaderText = "Lo?i s?n ph?m";
    this.colLoaiSanPham.MinimumWidth = 8;
     this.colLoaiSanPham.Name = "colLoaiSanPham";
        this.colLoaiSanPham.ReadOnly = true;
            // 
            // colSoLuong
    // 
        this.colSoLuong.DataPropertyName = "SoLuong";
    this.colSoLuong.FillWeight = 60F;
            this.colSoLuong.HeaderText = "S? l??ng";
          this.colSoLuong.MinimumWidth = 8;
            this.colSoLuong.Name = "colSoLuong";
     this.colSoLuong.ReadOnly = true;
            // 
            // colDonGia
         // 
      this.colDonGia.DataPropertyName = "DonGia";
     this.colDonGia.FillWeight = 80F;
  this.colDonGia.HeaderText = "??n giá";
       this.colDonGia.MinimumWidth = 8;
     this.colDonGia.Name = "colDonGia";
     this.colDonGia.ReadOnly = true;
// 
       // colThanhTien
       // 
     this.colThanhTien.DataPropertyName = "ThanhTien";
            this.colThanhTien.FillWeight = 90F;
            this.colThanhTien.HeaderText = "Thành ti?n";
            this.colThanhTien.MinimumWidth = 8;
        this.colThanhTien.Name = "colThanhTien";
  this.colThanhTien.ReadOnly = true;
// 
            // pnlButton
          // 
     this.pnlButton.BackColor = System.Drawing.Color.White;
 this.pnlButton.Controls.Add(this.btnDong);
    this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
  this.pnlButton.Location = new System.Drawing.Point(0, 650);
       this.pnlButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
    this.pnlButton.Name = "pnlButton";
       this.pnlButton.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
      this.pnlButton.Size = new System.Drawing.Size(1200, 80);
        this.pnlButton.TabIndex = 3;
      // 
     // btnDong
       // 
  this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDong.BackColor = System.Drawing.Color.Gray;
         this.btnDong.FlatAppearance.BorderSize = 0;
       this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnDong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
  this.btnDong.ForeColor = System.Drawing.Color.White;
 this.btnDong.Location = new System.Drawing.Point(525, 15);
        this.btnDong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
       this.btnDong.Name = "btnDong";
       this.btnDong.Size = new System.Drawing.Size(150, 50);
 this.btnDong.TabIndex = 0;
   this.btnDong.Text = "?óng";
            this.btnDong.UseVisualStyleBackColor = false;
       this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
      // 
       // frmChiTietHoaDon
     // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.White;
this.ClientSize = new System.Drawing.Size(1200, 730);
            this.Controls.Add(this.dgvChiTietHD);
       this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.pnlThongTin);
            this.Controls.Add(this.pnlTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
       this.MinimizeBox = false;
      this.Name = "frmChiTietHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
       this.Text = "Chi ti?t hóa ??n";
            this.Load += new System.EventHandler(this.frmChiTietHoaDon_Load);
            this.pnlTieuDe.ResumeLayout(false);
    this.pnlThongTin.ResumeLayout(false);
     this.pnlThongTin.PerformLayout();
  ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHD)).EndInit();
 this.pnlButton.ResumeLayout(false);
     this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
  private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblTongTienValue;
        private System.Windows.Forms.Label lblTongTien;
private System.Windows.Forms.Label lblMaHDValue;
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.DataGridView dgvChiTietHD;
  private System.Windows.Forms.DataGridViewTextBoxColumn colMaCTHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSanPham;
  private System.Windows.Forms.DataGridViewTextBoxColumn colTenSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiSanPham;
     private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
    private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnDong;
    }
}
