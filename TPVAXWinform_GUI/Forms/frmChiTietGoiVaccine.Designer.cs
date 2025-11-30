namespace TPVAXWinform_GUI.Forms
{
    partial class frmChiTietGoiVaccine
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTieuDe = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.lblTongGiaValue = new System.Windows.Forms.Label();
            this.lblTongGia = new System.Windows.Forms.Label();
            this.lblTenGoiValue = new System.Windows.Forms.Label();
            this.lblTenGoi = new System.Windows.Forms.Label();
            this.lblMaGoiValue = new System.Windows.Forms.Label();
            this.lblMaGoi = new System.Windows.Forms.Label();
            this.dgvChiTietGoi = new System.Windows.Forms.DataGridView();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.colMaVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiBenh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiVaccine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNuocSX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoMui = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTieuDe.SuspendLayout();
            this.pnlThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietGoi)).BeginInit();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTieuDe
            // 
            this.pnlTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlTieuDe.Controls.Add(this.lblTieuDe);
            this.pnlTieuDe.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTieuDe.Location = new System.Drawing.Point(0, 0);
            this.pnlTieuDe.Name = "pnlTieuDe";
            this.pnlTieuDe.Size = new System.Drawing.Size(1100, 70);
            this.pnlTieuDe.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1100, 70);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "CHI TIẾT GÓI VACCINE";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlThongTin.Controls.Add(this.lblTongGiaValue);
            this.pnlThongTin.Controls.Add(this.lblTongGia);
            this.pnlThongTin.Controls.Add(this.lblTenGoiValue);
            this.pnlThongTin.Controls.Add(this.lblTenGoi);
            this.pnlThongTin.Controls.Add(this.lblMaGoiValue);
            this.pnlThongTin.Controls.Add(this.lblMaGoi);
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.Location = new System.Drawing.Point(0, 70);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlThongTin.Size = new System.Drawing.Size(1100, 90);
            this.pnlThongTin.TabIndex = 1;
            // 
            // lblTongGiaValue
            // 
            this.lblTongGiaValue.AutoSize = true;
            this.lblTongGiaValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongGiaValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblTongGiaValue.Location = new System.Drawing.Point(920, 30);
            this.lblTongGiaValue.Name = "lblTongGiaValue";
            this.lblTongGiaValue.Size = new System.Drawing.Size(20, 28);
            this.lblTongGiaValue.TabIndex = 5;
            this.lblTongGiaValue.Text = "-";
            // 
            // lblTongGia
            // 
            this.lblTongGia.AutoSize = true;
            this.lblTongGia.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTongGia.Location = new System.Drawing.Point(800, 32);
            this.lblTongGia.Name = "lblTongGia";
            this.lblTongGia.Size = new System.Drawing.Size(104, 25);
            this.lblTongGia.TabIndex = 4;
            this.lblTongGia.Text = "Tổng giá:";
            // 
            // lblTenGoiValue
            // 
            this.lblTenGoiValue.AutoSize = true;
            this.lblTenGoiValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenGoiValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTenGoiValue.Location = new System.Drawing.Point(400, 30);
            this.lblTenGoiValue.Name = "lblTenGoiValue";
            this.lblTenGoiValue.Size = new System.Drawing.Size(20, 28);
            this.lblTenGoiValue.TabIndex = 3;
            this.lblTenGoiValue.Text = "-";
            // 
            // lblTenGoi
            // 
            this.lblTenGoi.AutoSize = true;
            this.lblTenGoi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTenGoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTenGoi.Location = new System.Drawing.Point(300, 32);
            this.lblTenGoi.Name = "lblTenGoi";
            this.lblTenGoi.Size = new System.Drawing.Size(90, 25);
            this.lblTenGoi.TabIndex = 2;
            this.lblTenGoi.Text = "Tên gói:";
            // 
            // lblMaGoiValue
            // 
            this.lblMaGoiValue.AutoSize = true;
            this.lblMaGoiValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMaGoiValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblMaGoiValue.Location = new System.Drawing.Point(130, 30);
            this.lblMaGoiValue.Name = "lblMaGoiValue";
            this.lblMaGoiValue.Size = new System.Drawing.Size(20, 28);
            this.lblMaGoiValue.TabIndex = 1;
            this.lblMaGoiValue.Text = "-";
            // 
            // lblMaGoi
            // 
            this.lblMaGoi.AutoSize = true;
            this.lblMaGoi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMaGoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblMaGoi.Location = new System.Drawing.Point(33, 32);
            this.lblMaGoi.Name = "lblMaGoi";
            this.lblMaGoi.Size = new System.Drawing.Size(84, 25);
            this.lblMaGoi.TabIndex = 0;
            this.lblMaGoi.Text = "Mã gói:";
            // 
            // dgvChiTietGoi
            // 
            this.dgvChiTietGoi.AllowUserToAddRows = false;
            this.dgvChiTietGoi.AllowUserToDeleteRows = false;
            this.dgvChiTietGoi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietGoi.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietGoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTietGoi.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTietGoi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTietGoi.ColumnHeadersHeight = 45;
            this.dgvChiTietGoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvChiTietGoi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaVC,
            this.colTenVC,
            this.colLoaiBenh,
            this.colLoaiVaccine,
            this.colNuocSX,
            this.colGiaBan,
            this.colSoMui,
            this.colGhiChu});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTietGoi.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChiTietGoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietGoi.EnableHeadersVisualStyles = false;
            this.dgvChiTietGoi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvChiTietGoi.Location = new System.Drawing.Point(0, 160);
            this.dgvChiTietGoi.Name = "dgvChiTietGoi";
            this.dgvChiTietGoi.ReadOnly = true;
            this.dgvChiTietGoi.RowHeadersVisible = false;
            this.dgvChiTietGoi.RowHeadersWidth = 51;
            this.dgvChiTietGoi.RowTemplate.Height = 40;
            this.dgvChiTietGoi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietGoi.Size = new System.Drawing.Size(1100, 430);
            this.dgvChiTietGoi.TabIndex = 2;
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.White;
            this.pnlButton.Controls.Add(this.btnDong);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 590);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(1100, 70);
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
            this.btnDong.Location = new System.Drawing.Point(475, 12);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(150, 45);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // colMaVC
            // 
            this.colMaVC.DataPropertyName = "MaVC";
            this.colMaVC.FillWeight = 70F;
            this.colMaVC.HeaderText = "Mã Vaccine";
            this.colMaVC.MinimumWidth = 6;
            this.colMaVC.Name = "colMaVC";
            this.colMaVC.ReadOnly = true;
            // 
            // colTenVC
            // 
            this.colTenVC.DataPropertyName = "TenVC";
            this.colTenVC.FillWeight = 130F;
            this.colTenVC.HeaderText = "Tên Vaccine";
            this.colTenVC.MinimumWidth = 6;
            this.colTenVC.Name = "colTenVC";
            this.colTenVC.ReadOnly = true;
            // 
            // colLoaiBenh
            // 
            this.colLoaiBenh.DataPropertyName = "CacBenhPhongNgua";
            this.colLoaiBenh.FillWeight = 120F;
            this.colLoaiBenh.HeaderText = "Loại bệnh";
            this.colLoaiBenh.MinimumWidth = 6;
            this.colLoaiBenh.Name = "colLoaiBenh";
            this.colLoaiBenh.ReadOnly = true;
            // 
            // colLoaiVaccine
            // 
            this.colLoaiVaccine.DataPropertyName = "TenLoaiVaccine";
            this.colLoaiVaccine.FillWeight = 90F;
            this.colLoaiVaccine.HeaderText = "Loại Vaccine";
            this.colLoaiVaccine.MinimumWidth = 6;
            this.colLoaiVaccine.Name = "colLoaiVaccine";
            this.colLoaiVaccine.ReadOnly = true;
            // 
            // colNuocSX
            // 
            this.colNuocSX.DataPropertyName = "Nước sản xuất";
            this.colNuocSX.FillWeight = 80F;
            this.colNuocSX.HeaderText = "Nước SX";
            this.colNuocSX.MinimumWidth = 6;
            this.colNuocSX.Name = "colNuocSX";
            this.colNuocSX.ReadOnly = true;
            // 
            // colGiaBan
            // 
            this.colGiaBan.DataPropertyName = "GiaBan";
            this.colGiaBan.FillWeight = 70F;
            this.colGiaBan.HeaderText = "Giá bán";
            this.colGiaBan.MinimumWidth = 6;
            this.colGiaBan.Name = "colGiaBan";
            this.colGiaBan.ReadOnly = true;
            // 
            // colSoMui
            // 
            this.colSoMui.DataPropertyName = "SoMui";
            this.colSoMui.FillWeight = 50F;
            this.colSoMui.HeaderText = "Số mũi";
            this.colSoMui.MinimumWidth = 6;
            this.colSoMui.Name = "colSoMui";
            this.colSoMui.ReadOnly = true;
            // 
            // colGhiChu
            // 
            this.colGhiChu.DataPropertyName = "GhiChu";
            this.colGhiChu.FillWeight = 90F;
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.MinimumWidth = 6;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.ReadOnly = true;
            // 
            // frmChiTietGoiVaccine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 660);
            this.Controls.Add(this.dgvChiTietGoi);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.pnlThongTin);
            this.Controls.Add(this.pnlTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChiTietGoiVaccine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết gói vaccine";
            this.Load += new System.EventHandler(this.frmChiTietGoiVaccine_Load);
            this.pnlTieuDe.ResumeLayout(false);
            this.pnlThongTin.ResumeLayout(false);
            this.pnlThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietGoi)).EndInit();
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblTongGiaValue;
        private System.Windows.Forms.Label lblTongGia;
        private System.Windows.Forms.Label lblTenGoiValue;
        private System.Windows.Forms.Label lblTenGoi;
        private System.Windows.Forms.Label lblMaGoiValue;
        private System.Windows.Forms.Label lblMaGoi;
        private System.Windows.Forms.DataGridView dgvChiTietGoi;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiBenh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiVaccine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNuocSX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoMui;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
    }
}
