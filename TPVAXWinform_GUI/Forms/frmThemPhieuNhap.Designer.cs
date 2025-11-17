namespace TPVAXWinform_GUI.Forms
{
    partial class frmThemPhieuNhap
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBoxThongTin = new System.Windows.Forms.GroupBox();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.groupBoxChiTiet = new System.Windows.Forms.GroupBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colMaVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNuocSanXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHanSuDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelChiTiet = new System.Windows.Forms.Panel();
            this.btnThemVaccine = new System.Windows.Forms.Button();
            this.dtpHanSuDung = new System.Windows.Forms.DateTimePicker();
            this.numGiaNhap = new System.Windows.Forms.NumericUpDown();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.txtNuocSanXuat = new System.Windows.Forms.TextBox();
            this.cboVaccine = new System.Windows.Forms.ComboBox();
            this.lblHanSuDung = new System.Windows.Forms.Label();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblNuocSanXuat = new System.Windows.Forms.Label();
            this.lblVaccine = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.groupBoxThongTin.SuspendLayout();
            this.groupBoxChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.panelChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1800, 77);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÊM PHIẾU NHẬP";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxThongTin
            // 
            this.groupBoxThongTin.Controls.Add(this.dtpNgayLap);
            this.groupBoxThongTin.Controls.Add(this.cboNhanVien);
            this.groupBoxThongTin.Controls.Add(this.cboNhaCungCap);
            this.groupBoxThongTin.Controls.Add(this.lblNgayLap);
            this.groupBoxThongTin.Controls.Add(this.lblNhanVien);
            this.groupBoxThongTin.Controls.Add(this.lblNhaCungCap);
            this.groupBoxThongTin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupBoxThongTin.Location = new System.Drawing.Point(30, 92);
            this.groupBoxThongTin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxThongTin.Name = "groupBoxThongTin";
            this.groupBoxThongTin.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxThongTin.Size = new System.Drawing.Size(1740, 185);
            this.groupBoxThongTin.TabIndex = 1;
            this.groupBoxThongTin.TabStop = false;
            this.groupBoxThongTin.Text = "Thông tin phi?u nh?p";
            // 
            // dtpNgayLap
            // 
            this.dtpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayLap.Location = new System.Drawing.Point(225, 46);
            this.dtpNgayLap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(373, 34);
            this.dtpNgayLap.TabIndex = 1;
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(225, 108);
            this.cboNhanVien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(598, 36);
            this.cboNhanVien.TabIndex = 3;
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhaCungCap.FormattingEnabled = true;
            this.cboNhaCungCap.Location = new System.Drawing.Point(1050, 108);
            this.cboNhaCungCap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(598, 36);
            this.cboNhaCungCap.TabIndex = 5;
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(30, 51);
            this.lblNgayLap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(94, 28);
            this.lblNgayLap.TabIndex = 0;
            this.lblNgayLap.Text = "Ngày l?p:";
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location = new System.Drawing.Point(30, 112);
            this.lblNhanVien.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(104, 28);
            this.lblNhanVien.TabIndex = 2;
            this.lblNhanVien.Text = "Nhân viên:";
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Location = new System.Drawing.Point(870, 112);
            this.lblNhaCungCap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(135, 28);
            this.lblNhaCungCap.TabIndex = 4;
            this.lblNhaCungCap.Text = "Nhà cung c?p:";
            // 
            // groupBoxChiTiet
            // 
            this.groupBoxChiTiet.Controls.Add(this.dgvChiTiet);
            this.groupBoxChiTiet.Controls.Add(this.panelChiTiet);
            this.groupBoxChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupBoxChiTiet.Location = new System.Drawing.Point(30, 292);
            this.groupBoxChiTiet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxChiTiet.Name = "groupBoxChiTiet";
            this.groupBoxChiTiet.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxChiTiet.Size = new System.Drawing.Size(1740, 692);
            this.groupBoxChiTiet.TabIndex = 2;
            this.groupBoxChiTiet.TabStop = false;
            this.groupBoxChiTiet.Text = "Chi ti?t vaccine";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaVC,
            this.colTenVC,
            this.colNuocSanXuat,
            this.colSoLuong,
            this.colGiaNhap,
            this.colHanSuDung,
            this.colXoa});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(4, 214);
            this.dgvChiTiet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersWidth = 62;
            this.dgvChiTiet.Size = new System.Drawing.Size(1732, 473);
            this.dgvChiTiet.TabIndex = 1;
            // 
            // colMaVC
            // 
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
            this.colNuocSanXuat.HeaderText = "N??c s?n xu?t";
            this.colNuocSanXuat.MinimumWidth = 8;
            this.colNuocSanXuat.Name = "colNuocSanXuat";
            this.colNuocSanXuat.ReadOnly = true;
            // 
            // colSoLuong
            // 
            this.colSoLuong.HeaderText = "S? l??ng";
            this.colSoLuong.MinimumWidth = 8;
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            // 
            // colGiaNhap
            // 
            this.colGiaNhap.HeaderText = "Giá nh?p";
            this.colGiaNhap.MinimumWidth = 8;
            this.colGiaNhap.Name = "colGiaNhap";
            this.colGiaNhap.ReadOnly = true;
            // 
            // colHanSuDung
            // 
            this.colHanSuDung.HeaderText = "H?n s? d?ng";
            this.colHanSuDung.MinimumWidth = 8;
            this.colHanSuDung.Name = "colHanSuDung";
            this.colHanSuDung.ReadOnly = true;
            // 
            // colXoa
            // 
            this.colXoa.HeaderText = "Xóa";
            this.colXoa.MinimumWidth = 8;
            this.colXoa.Name = "colXoa";
            this.colXoa.ReadOnly = true;
            this.colXoa.Text = "Xóa";
            this.colXoa.UseColumnTextForButtonValue = true;
            // 
            // panelChiTiet
            // 
            this.panelChiTiet.Controls.Add(this.btnThemVaccine);
            this.panelChiTiet.Controls.Add(this.dtpHanSuDung);
            this.panelChiTiet.Controls.Add(this.numGiaNhap);
            this.panelChiTiet.Controls.Add(this.numSoLuong);
            this.panelChiTiet.Controls.Add(this.txtNuocSanXuat);
            this.panelChiTiet.Controls.Add(this.cboVaccine);
            this.panelChiTiet.Controls.Add(this.lblHanSuDung);
            this.panelChiTiet.Controls.Add(this.lblGiaNhap);
            this.panelChiTiet.Controls.Add(this.lblSoLuong);
            this.panelChiTiet.Controls.Add(this.lblNuocSanXuat);
            this.panelChiTiet.Controls.Add(this.lblVaccine);
            this.panelChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChiTiet.Location = new System.Drawing.Point(4, 32);
            this.panelChiTiet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelChiTiet.Name = "panelChiTiet";
            this.panelChiTiet.Size = new System.Drawing.Size(1732, 182);
            this.panelChiTiet.TabIndex = 0;
            // 
            // btnThemVaccine
            // 
            this.btnThemVaccine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThemVaccine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaccine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemVaccine.ForeColor = System.Drawing.Color.White;
            this.btnThemVaccine.Location = new System.Drawing.Point(1485, 92);
            this.btnThemVaccine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThemVaccine.Name = "btnThemVaccine";
            this.btnThemVaccine.Size = new System.Drawing.Size(180, 54);
            this.btnThemVaccine.TabIndex = 10;
            this.btnThemVaccine.Text = "Thêm vaccine";
            this.btnThemVaccine.UseVisualStyleBackColor = false;
            this.btnThemVaccine.Click += new System.EventHandler(this.btnThemVaccine_Click);
            // 
            // dtpHanSuDung
            // 
            this.dtpHanSuDung.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHanSuDung.Location = new System.Drawing.Point(1230, 100);
            this.dtpHanSuDung.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpHanSuDung.Name = "dtpHanSuDung";
            this.dtpHanSuDung.Size = new System.Drawing.Size(223, 34);
            this.dtpHanSuDung.TabIndex = 9;
            // 
            // numGiaNhap
            // 
            this.numGiaNhap.Location = new System.Drawing.Point(855, 100);
            this.numGiaNhap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numGiaNhap.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numGiaNhap.Name = "numGiaNhap";
            this.numGiaNhap.Size = new System.Drawing.Size(225, 34);
            this.numGiaNhap.TabIndex = 7;
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(480, 100);
            this.numSoLuong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numSoLuong.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(225, 34);
            this.numSoLuong.TabIndex = 5;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtNuocSanXuat
            // 
            this.txtNuocSanXuat.Location = new System.Drawing.Point(1230, 23);
            this.txtNuocSanXuat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNuocSanXuat.Name = "txtNuocSanXuat";
            this.txtNuocSanXuat.Size = new System.Drawing.Size(433, 34);
            this.txtNuocSanXuat.TabIndex = 3;
            // 
            // cboVaccine
            // 
            this.cboVaccine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVaccine.FormattingEnabled = true;
            this.cboVaccine.Location = new System.Drawing.Point(180, 23);
            this.cboVaccine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboVaccine.Name = "cboVaccine";
            this.cboVaccine.Size = new System.Drawing.Size(748, 36);
            this.cboVaccine.TabIndex = 1;
            // 
            // lblHanSuDung
            // 
            this.lblHanSuDung.AutoSize = true;
            this.lblHanSuDung.Location = new System.Drawing.Point(1095, 105);
            this.lblHanSuDung.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHanSuDung.Name = "lblHanSuDung";
            this.lblHanSuDung.Size = new System.Drawing.Size(121, 28);
            this.lblHanSuDung.TabIndex = 8;
            this.lblHanSuDung.Text = "H?n s? d?ng:";
            // 
            // lblGiaNhap
            // 
            this.lblGiaNhap.AutoSize = true;
            this.lblGiaNhap.Location = new System.Drawing.Point(735, 105);
            this.lblGiaNhap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGiaNhap.Name = "lblGiaNhap";
            this.lblGiaNhap.Size = new System.Drawing.Size(93, 28);
            this.lblGiaNhap.TabIndex = 6;
            this.lblGiaNhap.Text = "Giá nh?p:";
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(360, 105);
            this.lblSoLuong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(87, 28);
            this.lblSoLuong.TabIndex = 4;
            this.lblSoLuong.Text = "S? l??ng:";
            // 
            // lblNuocSanXuat
            // 
            this.lblNuocSanXuat.AutoSize = true;
            this.lblNuocSanXuat.Location = new System.Drawing.Point(1065, 28);
            this.lblNuocSanXuat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNuocSanXuat.Name = "lblNuocSanXuat";
            this.lblNuocSanXuat.Size = new System.Drawing.Size(132, 28);
            this.lblNuocSanXuat.TabIndex = 2;
            this.lblNuocSanXuat.Text = "N??c s?n xu?t:";
            // 
            // lblVaccine
            // 
            this.lblVaccine.AutoSize = true;
            this.lblVaccine.Location = new System.Drawing.Point(30, 28);
            this.lblVaccine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVaccine.Name = "lblVaccine";
            this.lblVaccine.Size = new System.Drawing.Size(81, 28);
            this.lblVaccine.TabIndex = 0;
            this.lblVaccine.Text = "Vaccine:";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnLuu);
            this.panelButtons.Controls.Add(this.btnHuy);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 1000);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1800, 92);
            this.panelButtons.TabIndex = 3;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(1425, 15);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(165, 62);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "L?u";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(1605, 15);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(165, 62);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "H?y";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmThemPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 1092);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.groupBoxChiTiet);
            this.Controls.Add(this.groupBoxThongTin);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmThemPhieuNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm phi?u nh?p";
            this.Load += new System.EventHandler(this.frmThemPhieuNhap_Load);
            this.groupBoxThongTin.ResumeLayout(false);
            this.groupBoxThongTin.PerformLayout();
            this.groupBoxChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.panelChiTiet.ResumeLayout(false);
            this.panelChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBoxThongTin;
        private System.Windows.Forms.DateTimePicker dtpNgayLap;
 private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Label lblNgayLap;
 private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label lblNhaCungCap;
private System.Windows.Forms.GroupBox groupBoxChiTiet;
        private System.Windows.Forms.DataGridView dgvChiTiet;
 private System.Windows.Forms.Panel panelChiTiet;
        private System.Windows.Forms.Button btnThemVaccine;
        private System.Windows.Forms.DateTimePicker dtpHanSuDung;
      private System.Windows.Forms.NumericUpDown numGiaNhap;
   private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.TextBox txtNuocSanXuat;
        private System.Windows.Forms.ComboBox cboVaccine;
        private System.Windows.Forms.Label lblHanSuDung;
        private System.Windows.Forms.Label lblGiaNhap;
 private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblNuocSanXuat;
        private System.Windows.Forms.Label lblVaccine;
      private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnLuu;
    private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenVC;
  private System.Windows.Forms.DataGridViewTextBoxColumn colNuocSanXuat;
   private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHanSuDung;
        private System.Windows.Forms.DataGridViewButtonColumn colXoa;
    }
}
