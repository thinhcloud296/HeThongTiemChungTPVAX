namespace TPVAXWinform_GUI
{
    partial class frmEditHSTC
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtMaHSTC = new System.Windows.Forms.TextBox();
            this.lblMaHSTC = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(600, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(150, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(343, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHỈNH SỬA HỒ SƠ";
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.chkTrangThai);
            this.panelContent.Controls.Add(this.lblTrangThai);
            this.panelContent.Controls.Add(this.txtGhiChu);
            this.panelContent.Controls.Add(this.lblGhiChu);
            this.panelContent.Controls.Add(this.dtpNgaySinh);
            this.panelContent.Controls.Add(this.lblNgaySinh);
            this.panelContent.Controls.Add(this.cboGioiTinh);
            this.panelContent.Controls.Add(this.lblGioiTinh);
            this.panelContent.Controls.Add(this.txtCCCD);
            this.panelContent.Controls.Add(this.lblCCCD);
            this.panelContent.Controls.Add(this.txtHoTen);
            this.panelContent.Controls.Add(this.lblHoTen);
            this.panelContent.Controls.Add(this.txtMaHSTC);
            this.panelContent.Controls.Add(this.lblMaHSTC);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 70);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(600, 450);
            this.panelContent.TabIndex = 1;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkTrangThai.Location = new System.Drawing.Point(170, 330);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(133, 32);
            this.chkTrangThai.TabIndex = 13;
            this.chkTrangThai.Text = "Hoạt động";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTrangThai.Location = new System.Drawing.Point(30, 330);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(124, 30);
            this.lblTrangThai.TabIndex = 12;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(170, 255);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(380, 60);
            this.txtGhiChu.TabIndex = 11;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblGhiChu.Location = new System.Drawing.Point(30, 255);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(97, 30);
            this.lblGhiChu.TabIndex = 10;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgaySinh.Location = new System.Drawing.Point(170, 165);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(380, 34);
            this.dtpNgaySinh.TabIndex = 7;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblNgaySinh.Location = new System.Drawing.Point(30, 165);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(122, 30);
            this.lblNgaySinh.TabIndex = 6;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Items.AddRange(new object[] {
            "Nam",
            "Nử",
            "Khác"});
            this.cboGioiTinh.Location = new System.Drawing.Point(170, 120);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(380, 36);
            this.cboGioiTinh.TabIndex = 5;
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGioiTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblGioiTinh.Location = new System.Drawing.Point(30, 120);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(108, 30);
            this.lblGioiTinh.TabIndex = 4;
            this.lblGioiTinh.Text = "Giới tính:";
            // 
            // txtCCCD
            // 
            this.txtCCCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCCCD.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCCCD.Location = new System.Drawing.Point(170, 210);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(380, 34);
            this.txtCCCD.TabIndex = 9;
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCCCD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblCCCD.Location = new System.Drawing.Point(30, 210);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(75, 30);
            this.lblCCCD.TabIndex = 8;
            this.lblCCCD.Text = "CCCD:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHoTen.Location = new System.Drawing.Point(170, 75);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(380, 34);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblHoTen.Location = new System.Drawing.Point(30, 75);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(89, 30);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // txtMaHSTC
            // 
            this.txtMaHSTC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtMaHSTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaHSTC.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaHSTC.Location = new System.Drawing.Point(170, 30);
            this.txtMaHSTC.Name = "txtMaHSTC";
            this.txtMaHSTC.ReadOnly = true;
            this.txtMaHSTC.Size = new System.Drawing.Size(380, 34);
            this.txtMaHSTC.TabIndex = 1;
            // 
            // lblMaHSTC
            // 
            this.lblMaHSTC.AutoSize = true;
            this.lblMaHSTC.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMaHSTC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblMaHSTC.Location = new System.Drawing.Point(30, 30);
            this.lblMaHSTC.Name = "lblMaHSTC";
            this.lblMaHSTC.Size = new System.Drawing.Size(114, 30);
            this.lblMaHSTC.TabIndex = 0;
            this.lblMaHSTC.Text = "Mã hồ sơ:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(200, 450);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 40);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(330, 450);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmEditHSTC
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(600, 520);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditHSTC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chỉnh sửa thông tin hồ sơ";
            this.Load += new System.EventHandler(this.frmEditHSTC_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblMaHSTC;
        private System.Windows.Forms.TextBox txtMaHSTC;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
    }
}