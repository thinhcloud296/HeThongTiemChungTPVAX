namespace TPVAXWinform_GUI.Forms
{
 partial class frmChiTietPhieuNhap
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
            this.lblNhaCungCapValue = new System.Windows.Forms.Label();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.lblNhanVienValue = new System.Windows.Forms.Label();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.lblNgayLapValue = new System.Windows.Forms.Label();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblMaPNValue = new System.Windows.Forms.Label();
            this.lblMaPN = new System.Windows.Forms.Label();
            this.dgvChiTietPN = new System.Windows.Forms.DataGridView();
            this.colMaCTPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNuocSanXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHanSuDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlTieuDe.SuspendLayout();
            this.pnlThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPN)).BeginInit();
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
            this.pnlTieuDe.Size = new System.Drawing.Size(1400, 80);
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
            this.lblTieuDe.Size = new System.Drawing.Size(1400, 80);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "CHI TIẾT PHIẾU NHẬP VACCINE";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlThongTin.Controls.Add(this.lblTongTienValue);
            this.pnlThongTin.Controls.Add(this.lblTongTien);
            this.pnlThongTin.Controls.Add(this.lblNhaCungCapValue);
            this.pnlThongTin.Controls.Add(this.lblNhaCungCap);
            this.pnlThongTin.Controls.Add(this.lblNhanVienValue);
            this.pnlThongTin.Controls.Add(this.lblNhanVien);
            this.pnlThongTin.Controls.Add(this.lblNgayLapValue);
            this.pnlThongTin.Controls.Add(this.lblNgayLap);
            this.pnlThongTin.Controls.Add(this.lblMaPNValue);
            this.pnlThongTin.Controls.Add(this.lblMaPN);
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.Location = new System.Drawing.Point(0, 80);
            this.pnlThongTin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlThongTin.Size = new System.Drawing.Size(1400, 150);
            this.pnlThongTin.TabIndex = 1;
            // 
            // lblTongTienValue
            // 
            this.lblTongTienValue.AutoSize = true;
            this.lblTongTienValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTienValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblTongTienValue.Location = new System.Drawing.Point(975, 100);
            this.lblTongTienValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTongTienValue.Name = "lblTongTienValue";
            this.lblTongTienValue.Size = new System.Drawing.Size(24, 32);
            this.lblTongTienValue.TabIndex = 9;
            this.lblTongTienValue.Text = "-";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTongTien.Location = new System.Drawing.Point(825, 102);
            this.lblTongTien.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(112, 30);
            this.lblTongTien.TabIndex = 8;
            this.lblTongTien.Text = "Tổng tiền";
            // 
            // lblNhaCungCapValue
            // 
            this.lblNhaCungCapValue.AutoSize = true;
            this.lblNhaCungCapValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNhaCungCapValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNhaCungCapValue.Location = new System.Drawing.Point(975, 55);
            this.lblNhaCungCapValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNhaCungCapValue.Name = "lblNhaCungCapValue";
            this.lblNhaCungCapValue.Size = new System.Drawing.Size(22, 30);
            this.lblNhaCungCapValue.TabIndex = 7;
            this.lblNhaCungCapValue.Text = "-";
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNhaCungCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNhaCungCap.Location = new System.Drawing.Point(825, 55);
            this.lblNhaCungCap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(161, 30);
            this.lblNhaCungCap.TabIndex = 6;
            this.lblNhaCungCap.Text = "Nhà cung cấp:";
            // 
            // lblNhanVienValue
            // 
            this.lblNhanVienValue.AutoSize = true;
            this.lblNhanVienValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNhanVienValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNhanVienValue.Location = new System.Drawing.Point(975, 20);
            this.lblNhanVienValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNhanVienValue.Name = "lblNhanVienValue";
            this.lblNhanVienValue.Size = new System.Drawing.Size(22, 30);
            this.lblNhanVienValue.TabIndex = 5;
            this.lblNhanVienValue.Text = "-";
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNhanVien.Location = new System.Drawing.Point(825, 20);
            this.lblNhanVien.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(123, 30);
            this.lblNhanVien.TabIndex = 4;
            this.lblNhanVien.Text = "Nhân viên:";
            // 
            // lblNgayLapValue
            // 
            this.lblNgayLapValue.AutoSize = true;
            this.lblNgayLapValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayLapValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNgayLapValue.Location = new System.Drawing.Point(250, 55);
            this.lblNgayLapValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayLapValue.Name = "lblNgayLapValue";
            this.lblNgayLapValue.Size = new System.Drawing.Size(22, 30);
            this.lblNgayLapValue.TabIndex = 3;
            this.lblNgayLapValue.Text = "-";
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNgayLap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNgayLap.Location = new System.Drawing.Point(45, 55);
            this.lblNgayLap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(112, 30);
            this.lblNgayLap.TabIndex = 2;
            this.lblNgayLap.Text = "Ngày lập:";
            // 
            // lblMaPNValue
            // 
            this.lblMaPNValue.AutoSize = true;
            this.lblMaPNValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMaPNValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblMaPNValue.Location = new System.Drawing.Point(250, 20);
            this.lblMaPNValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaPNValue.Name = "lblMaPNValue";
            this.lblMaPNValue.Size = new System.Drawing.Size(24, 32);
            this.lblMaPNValue.TabIndex = 1;
            this.lblMaPNValue.Text = "-";
            // 
            // lblMaPN
            // 
            this.lblMaPN.AutoSize = true;
            this.lblMaPN.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMaPN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblMaPN.Location = new System.Drawing.Point(45, 20);
            this.lblMaPN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaPN.Name = "lblMaPN";
            this.lblMaPN.Size = new System.Drawing.Size(174, 30);
            this.lblMaPN.TabIndex = 0;
            this.lblMaPN.Text = "Mã phiếu nhập:";
            // 
            // dgvChiTietPN
            // 
            this.dgvChiTietPN.AllowUserToAddRows = false;
            this.dgvChiTietPN.AllowUserToDeleteRows = false;
            this.dgvChiTietPN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietPN.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietPN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTietPN.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTietPN.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTietPN.ColumnHeadersHeight = 40;
            this.dgvChiTietPN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvChiTietPN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaCTPN,
            this.colMaVC,
            this.colTenVC,
            this.colNuocSanXuat,
            this.colSoLuong,
            this.colGiaNhap,
            this.colHanSuDung,
            this.colThanhTien});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTietPN.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChiTietPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietPN.EnableHeadersVisualStyles = false;
            this.dgvChiTietPN.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvChiTietPN.Location = new System.Drawing.Point(0, 230);
            this.dgvChiTietPN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvChiTietPN.Name = "dgvChiTietPN";
            this.dgvChiTietPN.ReadOnly = true;
            this.dgvChiTietPN.RowHeadersVisible = false;
            this.dgvChiTietPN.RowHeadersWidth = 62;
            this.dgvChiTietPN.RowTemplate.Height = 35;
            this.dgvChiTietPN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietPN.Size = new System.Drawing.Size(1400, 420);
            this.dgvChiTietPN.TabIndex = 2;
            // 
            // colMaCTPN
            // 
            this.colMaCTPN.FillWeight = 80F;
            this.colMaCTPN.HeaderText = "Mã Chi Tiết";
            this.colMaCTPN.MinimumWidth = 8;
            this.colMaCTPN.Name = "colMaCTPN";
            this.colMaCTPN.ReadOnly = true;
            // 
            // colMaVC
            // 
            this.colMaVC.FillWeight = 70F;
            this.colMaVC.HeaderText = "Mã Vaccine";
            this.colMaVC.MinimumWidth = 8;
            this.colMaVC.Name = "colMaVC";
            this.colMaVC.ReadOnly = true;
            // 
            // colTenVC
            // 
            this.colTenVC.HeaderText = "Tên Vaccine";
            this.colTenVC.MinimumWidth = 8;
            this.colTenVC.Name = "colTenVC";
            this.colTenVC.ReadOnly = true;
            // 
            // colNuocSanXuat
            // 
            this.colNuocSanXuat.FillWeight = 90F;
            this.colNuocSanXuat.HeaderText = "Nước Sản Xuất";
            this.colNuocSanXuat.MinimumWidth = 8;
            this.colNuocSanXuat.Name = "colNuocSanXuat";
            this.colNuocSanXuat.ReadOnly = true;
            // 
            // colSoLuong
            // 
            this.colSoLuong.FillWeight = 60F;
            this.colSoLuong.HeaderText = "Số Lượng";
            this.colSoLuong.MinimumWidth = 8;
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            // 
            // colGiaNhap
            // 
            this.colGiaNhap.FillWeight = 80F;
            this.colGiaNhap.HeaderText = "Giá Nhập";
            this.colGiaNhap.MinimumWidth = 8;
            this.colGiaNhap.Name = "colGiaNhap";
            this.colGiaNhap.ReadOnly = true;
            // 
            // colHanSuDung
            // 
            this.colHanSuDung.FillWeight = 80F;
            this.colHanSuDung.HeaderText = "Hạn Sử Dụng";
            this.colHanSuDung.MinimumWidth = 8;
            this.colHanSuDung.Name = "colHanSuDung";
            this.colHanSuDung.ReadOnly = true;
            // 
            // colThanhTien
            // 
            this.colThanhTien.FillWeight = 90F;
            this.colThanhTien.HeaderText = "Thành Tiền";
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
            this.pnlButton.Size = new System.Drawing.Size(1400, 80);
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
            this.btnDong.Location = new System.Drawing.Point(625, 15);
            this.btnDong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(150, 50);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmChiTietPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1400, 730);
            this.Controls.Add(this.dgvChiTietPN);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.pnlThongTin);
            this.Controls.Add(this.pnlTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChiTietPhieuNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết phiếu nhập";
            this.Load += new System.EventHandler(this.frmChiTietPhieuNhap_Load);
            this.pnlTieuDe.ResumeLayout(false);
            this.pnlThongTin.ResumeLayout(false);
            this.pnlThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietPN)).EndInit();
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

   }

        #endregion

        private System.Windows.Forms.Panel pnlTieuDe;
private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblTongTienValue;
    private System.Windows.Forms.Label lblTongTien;
  private System.Windows.Forms.Label lblNhaCungCapValue;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.Label lblNhanVienValue;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label lblNgayLapValue;
   private System.Windows.Forms.Label lblNgayLap;
  private System.Windows.Forms.Label lblMaPNValue;
        private System.Windows.Forms.Label lblMaPN;
   private System.Windows.Forms.DataGridView dgvChiTietPN;
        private System.Windows.Forms.Panel pnlButton;
     private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaCTPN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNuocSanXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHanSuDung;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
    }
}
