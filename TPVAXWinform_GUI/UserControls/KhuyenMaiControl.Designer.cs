namespace TPVAXWinform_GUI.UserControls
{
    partial class KhuyenMaiControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTieuDe = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlLoc = new System.Windows.Forms.Panel();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.btnDatLai = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.pnlHanhDong = new System.Windows.Forms.Panel();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvKhuyenMai = new System.Windows.Forms.DataGridView();
            this.colMaKM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenKM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiKM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKieuGiam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaTriGiam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayBatDau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayKetThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTieuDe.SuspendLayout();
            this.pnlLoc.SuspendLayout();
            this.pnlHanhDong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuyenMai)).BeginInit();
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
            this.lblTieuDe.Text = "QUẢN LÝ KHUYẾN MÃI";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLoc
            // 
            this.pnlLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlLoc.Controls.Add(this.cboTrangThai);
            this.pnlLoc.Controls.Add(this.lblTrangThai);
            this.pnlLoc.Controls.Add(this.btnDatLai);
            this.pnlLoc.Controls.Add(this.txtSearch);
            this.pnlLoc.Controls.Add(this.lblTimKiem);
            this.pnlLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoc.Location = new System.Drawing.Point(0, 108);
            this.pnlLoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlLoc.Size = new System.Drawing.Size(1800, 100);
            this.pnlLoc.TabIndex = 1;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(215, 27);
            this.cboTrangThai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(250, 38);
            this.cboTrangThai.TabIndex = 11;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.cboTrangThai_SelectedIndexChanged);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTrangThai.Location = new System.Drawing.Point(45, 32);
            this.lblTrangThai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(113, 28);
            this.lblTrangThai.TabIndex = 10;
            this.lblTrangThai.Text = "Trạng thái:";
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
            this.txtSearch.Location = new System.Drawing.Point(700, 28);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(900, 37);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTimKiem.Location = new System.Drawing.Point(551, 33);
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(105, 28);
            this.lblTimKiem.TabIndex = 4;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // pnlHanhDong
            // 
            this.pnlHanhDong.BackColor = System.Drawing.Color.White;
            this.pnlHanhDong.Controls.Add(this.btnSua);
            this.pnlHanhDong.Controls.Add(this.btnThem);
            this.pnlHanhDong.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHanhDong.Location = new System.Drawing.Point(0, 208);
            this.pnlHanhDong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHanhDong.Name = "pnlHanhDong";
            this.pnlHanhDong.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlHanhDong.Size = new System.Drawing.Size(1800, 92);
            this.pnlHanhDong.TabIndex = 2;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(350, 20);
            this.btnSua.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(280, 50);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa Khuyến Mãi";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(50, 20);
            this.btnThem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(280, 50);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm Khuyến Mãi";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvKhuyenMai
            // 
            this.dgvKhuyenMai.AllowUserToAddRows = false;
            this.dgvKhuyenMai.AllowUserToDeleteRows = false;
            this.dgvKhuyenMai.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhuyenMai.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhuyenMai.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKhuyenMai.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKhuyenMai.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvKhuyenMai.ColumnHeadersHeight = 40;
            this.dgvKhuyenMai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvKhuyenMai.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaKM,
            this.colTenKM,
            this.colLoaiKM,
            this.colKieuGiam,
            this.colGiaTriGiam,
            this.colNgayBatDau,
            this.colNgayKetThuc,
            this.colTrangThai,
            this.colMoTa});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvKhuyenMai.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvKhuyenMai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhuyenMai.EnableHeadersVisualStyles = false;
            this.dgvKhuyenMai.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dgvKhuyenMai.Location = new System.Drawing.Point(0, 300);
            this.dgvKhuyenMai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvKhuyenMai.Name = "dgvKhuyenMai";
            this.dgvKhuyenMai.ReadOnly = true;
            this.dgvKhuyenMai.RowHeadersVisible = false;
            this.dgvKhuyenMai.RowHeadersWidth = 62;
            this.dgvKhuyenMai.RowTemplate.Height = 35;
            this.dgvKhuyenMai.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhuyenMai.Size = new System.Drawing.Size(1800, 777);
            this.dgvKhuyenMai.TabIndex = 3;
            // 
            // colMaKM
            // 
            this.colMaKM.FillWeight = 70F;
            this.colMaKM.HeaderText = "Mã KM";
            this.colMaKM.MinimumWidth = 8;
            this.colMaKM.Name = "colMaKM";
            this.colMaKM.ReadOnly = true;
            // 
            // colTenKM
            // 
            this.colTenKM.FillWeight = 120F;
            this.colTenKM.HeaderText = "Tên Khuyến Mãi";
            this.colTenKM.MinimumWidth = 8;
            this.colTenKM.Name = "colTenKM";
            this.colTenKM.ReadOnly = true;
            // 
            // colLoaiKM
            // 
            this.colLoaiKM.FillWeight = 90F;
            this.colLoaiKM.HeaderText = "Loại KM";
            this.colLoaiKM.MinimumWidth = 8;
            this.colLoaiKM.Name = "colLoaiKM";
            this.colLoaiKM.ReadOnly = true;
            // 
            // colKieuGiam
            // 
            this.colKieuGiam.FillWeight = 80F;
            this.colKieuGiam.HeaderText = "Kiểu Giảm";
            this.colKieuGiam.MinimumWidth = 8;
            this.colKieuGiam.Name = "colKieuGiam";
            this.colKieuGiam.ReadOnly = true;
            // 
            // colGiaTriGiam
            // 
            this.colGiaTriGiam.FillWeight = 80F;
            this.colGiaTriGiam.HeaderText = "Giá Trị Giảm";
            this.colGiaTriGiam.MinimumWidth = 8;
            this.colGiaTriGiam.Name = "colGiaTriGiam";
            this.colGiaTriGiam.ReadOnly = true;
            // 
            // colNgayBatDau
            // 
            this.colNgayBatDau.FillWeight = 90F;
            this.colNgayBatDau.HeaderText = "Ngày Bắt Đầu";
            this.colNgayBatDau.MinimumWidth = 8;
            this.colNgayBatDau.Name = "colNgayBatDau";
            this.colNgayBatDau.ReadOnly = true;
            // 
            // colNgayKetThuc
            // 
            this.colNgayKetThuc.FillWeight = 90F;
            this.colNgayKetThuc.HeaderText = "Ngày Kết Thúc";
            this.colNgayKetThuc.MinimumWidth = 8;
            this.colNgayKetThuc.Name = "colNgayKetThuc";
            this.colNgayKetThuc.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.FillWeight = 70F;
            this.colTrangThai.HeaderText = "Trạng Thái";
            this.colTrangThai.MinimumWidth = 8;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // colMoTa
            // 
            this.colMoTa.HeaderText = "Mô Tả";
            this.colMoTa.MinimumWidth = 8;
            this.colMoTa.Name = "colMoTa";
            this.colMoTa.ReadOnly = true;
            // 
            // KhuyenMaiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvKhuyenMai);
            this.Controls.Add(this.pnlHanhDong);
            this.Controls.Add(this.pnlLoc);
            this.Controls.Add(this.pnlTieuDe);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "KhuyenMaiControl";
            this.Size = new System.Drawing.Size(1800, 1077);
            this.Load += new System.EventHandler(this.KhuyenMaiControl_Load);
            this.pnlTieuDe.ResumeLayout(false);
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            this.pnlHanhDong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuyenMai)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvKhuyenMai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenKM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiKM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKieuGiam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaTriGiam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayBatDau;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayKetThuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTa;
    }
}