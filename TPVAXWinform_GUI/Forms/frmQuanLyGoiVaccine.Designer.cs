namespace TPVAXWinform_GUI.Forms
{
    partial class frmQuanLyGoiVaccine
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTieuDe = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlLoc = new System.Windows.Forms.Panel();
            this.btnThemGoi = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.dgvGoiVaccine = new System.Windows.Forms.DataGridView();
            this.colMaGoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenGoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoiTuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaGoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.contextMenuGoi = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuXemChiTiet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSuaGoi = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTieuDe.SuspendLayout();
            this.pnlLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoiVaccine)).BeginInit();
            this.pnlButton.SuspendLayout();
            this.contextMenuGoi.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTieuDe
            // 
            this.pnlTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.pnlTieuDe.Controls.Add(this.lblTieuDe);
            this.pnlTieuDe.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTieuDe.Location = new System.Drawing.Point(0, 0);
            this.pnlTieuDe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlTieuDe.Name = "pnlTieuDe";
            this.pnlTieuDe.Size = new System.Drawing.Size(1350, 100);
            this.pnlTieuDe.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1350, 100);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ GÓI VACCINE";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLoc
            // 
            this.pnlLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlLoc.Controls.Add(this.btnThemGoi);
            this.pnlLoc.Controls.Add(this.txtTimKiem);
            this.pnlLoc.Controls.Add(this.lblTimKiem);
            this.pnlLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoc.Location = new System.Drawing.Point(0, 100);
            this.pnlLoc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Padding = new System.Windows.Forms.Padding(34, 19, 34, 19);
            this.pnlLoc.Size = new System.Drawing.Size(1350, 100);
            this.pnlLoc.TabIndex = 1;
            // 
            // btnThemGoi
            // 
            this.btnThemGoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemGoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnThemGoi.FlatAppearance.BorderSize = 0;
            this.btnThemGoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemGoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemGoi.ForeColor = System.Drawing.Color.White;
            this.btnThemGoi.Location = new System.Drawing.Point(1148, 25);
            this.btnThemGoi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThemGoi.Name = "btnThemGoi";
            this.btnThemGoi.Size = new System.Drawing.Size(169, 50);
            this.btnThemGoi.TabIndex = 2;
            this.btnThemGoi.Text = "+ Thêm gói";
            this.btnThemGoi.UseVisualStyleBackColor = false;
            this.btnThemGoi.Click += new System.EventHandler(this.btnThemGoi_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTimKiem.Location = new System.Drawing.Point(169, 28);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(450, 37);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTimKiem.Location = new System.Drawing.Point(37, 34);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(105, 28);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // dgvGoiVaccine
            // 
            this.dgvGoiVaccine.AllowUserToAddRows = false;
            this.dgvGoiVaccine.AllowUserToDeleteRows = false;
            this.dgvGoiVaccine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGoiVaccine.BackgroundColor = System.Drawing.Color.White;
            this.dgvGoiVaccine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGoiVaccine.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGoiVaccine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGoiVaccine.ColumnHeadersHeight = 45;
            this.dgvGoiVaccine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvGoiVaccine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaGoi,
            this.colTenGoi,
            this.colMoTa,
            this.colDoiTuong,
            this.colGiaGoi,
            this.colTrangThai});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGoiVaccine.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGoiVaccine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGoiVaccine.EnableHeadersVisualStyles = false;
            this.dgvGoiVaccine.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvGoiVaccine.Location = new System.Drawing.Point(0, 200);
            this.dgvGoiVaccine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvGoiVaccine.Name = "dgvGoiVaccine";
            this.dgvGoiVaccine.ReadOnly = true;
            this.dgvGoiVaccine.RowHeadersVisible = false;
            this.dgvGoiVaccine.RowHeadersWidth = 51;
            this.dgvGoiVaccine.RowTemplate.Height = 40;
            this.dgvGoiVaccine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGoiVaccine.Size = new System.Drawing.Size(1350, 612);
            this.dgvGoiVaccine.TabIndex = 2;
            this.dgvGoiVaccine.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvGoiVaccine_CellMouseClick);
            // 
            // colMaGoi
            // 
            this.colMaGoi.DataPropertyName = "MaGoi";
            this.colMaGoi.FillWeight = 70F;
            this.colMaGoi.HeaderText = "Mã gói";
            this.colMaGoi.MinimumWidth = 6;
            this.colMaGoi.Name = "colMaGoi";
            this.colMaGoi.ReadOnly = true;
            // 
            // colTenGoi
            // 
            this.colTenGoi.DataPropertyName = "TenGoi";
            this.colTenGoi.FillWeight = 150F;
            this.colTenGoi.HeaderText = "Tên gói";
            this.colTenGoi.MinimumWidth = 6;
            this.colTenGoi.Name = "colTenGoi";
            this.colTenGoi.ReadOnly = true;
            // 
            // colMoTa
            // 
            this.colMoTa.DataPropertyName = "MoTa";
            this.colMoTa.FillWeight = 150F;
            this.colMoTa.HeaderText = "Mô tả";
            this.colMoTa.MinimumWidth = 6;
            this.colMoTa.Name = "colMoTa";
            this.colMoTa.ReadOnly = true;
            // 
            // colDoiTuong
            // 
            this.colDoiTuong.DataPropertyName = "DoiTuongApDung";
            this.colDoiTuong.HeaderText = "Đối tượng áp dụng";
            this.colDoiTuong.MinimumWidth = 6;
            this.colDoiTuong.Name = "colDoiTuong";
            this.colDoiTuong.ReadOnly = true;
            // 
            // colGiaGoi
            // 
            this.colGiaGoi.DataPropertyName = "GiaGoi";
            this.colGiaGoi.FillWeight = 80F;
            this.colGiaGoi.HeaderText = "Giá gói";
            this.colGiaGoi.MinimumWidth = 6;
            this.colGiaGoi.Name = "colGiaGoi";
            this.colGiaGoi.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.DataPropertyName = "TrangThai";
            this.colTrangThai.FillWeight = 70F;
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 6;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.White;
            this.pnlButton.Controls.Add(this.btnDong);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 812);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(1350, 88);
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
            this.btnDong.Location = new System.Drawing.Point(591, 15);
            this.btnDong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(169, 56);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // contextMenuGoi
            // 
            this.contextMenuGoi.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuGoi.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuXemChiTiet,
            this.menuSuaGoi});
            this.contextMenuGoi.Name = "contextMenuGoi";
            this.contextMenuGoi.Size = new System.Drawing.Size(313, 72);
            // 
            // menuXemChiTiet
            // 
            this.menuXemChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuXemChiTiet.Name = "menuXemChiTiet";
            this.menuXemChiTiet.Size = new System.Drawing.Size(312, 34);
            this.menuXemChiTiet.Text = "📄 Xem thông tin gói vaccine";
            this.menuXemChiTiet.Click += new System.EventHandler(this.menuXemChiTiet_Click);
            // 
            // menuSuaGoi
            // 
            this.menuSuaGoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuSuaGoi.Name = "menuSuaGoi";
            this.menuSuaGoi.Size = new System.Drawing.Size(312, 34);
            this.menuSuaGoi.Text = "✏️ Sửa gói vaccine";
            this.menuSuaGoi.Click += new System.EventHandler(this.menuSuaGoi_Click);
            // 
            // frmQuanLyGoiVaccine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1350, 900);
            this.Controls.Add(this.dgvGoiVaccine);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.pnlLoc);
            this.Controls.Add(this.pnlTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuanLyGoiVaccine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý gói vaccine";
            this.Load += new System.EventHandler(this.frmQuanLyGoiVaccine_Load);
            this.pnlTieuDe.ResumeLayout(false);
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoiVaccine)).EndInit();
            this.pnlButton.ResumeLayout(false);
            this.contextMenuGoi.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.Button btnThemGoi;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.DataGridView dgvGoiVaccine;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.ContextMenuStrip contextMenuGoi;
        private System.Windows.Forms.ToolStripMenuItem menuXemChiTiet;
        private System.Windows.Forms.ToolStripMenuItem menuSuaGoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaGoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenGoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoiTuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaGoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
    }
}
