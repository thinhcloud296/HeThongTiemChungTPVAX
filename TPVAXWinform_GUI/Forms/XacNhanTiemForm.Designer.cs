namespace TPVAXWinform_GUI.Forms
{
    partial class XacNhanTiemForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblNgayTiemThucTeValue = new System.Windows.Forms.Label();
            this.lblNgayTiemThucTe = new System.Windows.Forms.Label();
            this.lblSoMuiValue = new System.Windows.Forms.Label();
            this.lblSoMui = new System.Windows.Forms.Label();
            this.lblNgayHenValue = new System.Windows.Forms.Label();
            this.lblNgayHen = new System.Windows.Forms.Label();
            this.lblTenVaccineValue = new System.Windows.Forms.Label();
            this.lblTenVaccine = new System.Windows.Forms.Label();
            this.lblTenNguoiTiemValue = new System.Windows.Forms.Label();
            this.lblTenNguoiTiem = new System.Windows.Forms.Label();
            this.lblMaHSTCValue = new System.Windows.Forms.Label();
            this.lblMaHSTC = new System.Windows.Forms.Label();
            this.lblMaLTValue = new System.Windows.Forms.Label();
            this.lblMaLT = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(700, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(700, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "XÁC NHẬN TIÊM CHỦNG";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.txtGhiChu);
            this.pnlContent.Controls.Add(this.lblGhiChu);
            this.pnlContent.Controls.Add(this.lblNgayTiemThucTeValue);
            this.pnlContent.Controls.Add(this.lblNgayTiemThucTe);
            this.pnlContent.Controls.Add(this.lblSoMuiValue);
            this.pnlContent.Controls.Add(this.lblSoMui);
            this.pnlContent.Controls.Add(this.lblNgayHenValue);
            this.pnlContent.Controls.Add(this.lblNgayHen);
            this.pnlContent.Controls.Add(this.lblTenVaccineValue);
            this.pnlContent.Controls.Add(this.lblTenVaccine);
            this.pnlContent.Controls.Add(this.lblTenNguoiTiemValue);
            this.pnlContent.Controls.Add(this.lblTenNguoiTiem);
            this.pnlContent.Controls.Add(this.lblMaHSTCValue);
            this.pnlContent.Controls.Add(this.lblMaHSTC);
            this.pnlContent.Controls.Add(this.lblMaLTValue);
            this.pnlContent.Controls.Add(this.lblMaLT);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 80);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlContent.Size = new System.Drawing.Size(700, 570);
            this.pnlContent.TabIndex = 1;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGhiChu.Location = new System.Drawing.Point(200, 410);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(450, 120);
            this.txtGhiChu.TabIndex = 15;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblGhiChu.Location = new System.Drawing.Point(50, 410);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(97, 30);
            this.lblGhiChu.TabIndex = 14;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // lblNgayTiemThucTeValue
            // 
            this.lblNgayTiemThucTeValue.AutoSize = true;
            this.lblNgayTiemThucTeValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayTiemThucTeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblNgayTiemThucTeValue.Location = new System.Drawing.Point(300, 350);
            this.lblNgayTiemThucTeValue.Name = "lblNgayTiemThucTeValue";
            this.lblNgayTiemThucTeValue.Size = new System.Drawing.Size(65, 30);
            this.lblNgayTiemThucTeValue.TabIndex = 13;
            this.lblNgayTiemThucTeValue.Text = "Value";
            // 
            // lblNgayTiemThucTe
            // 
            this.lblNgayTiemThucTe.AutoSize = true;
            this.lblNgayTiemThucTe.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNgayTiemThucTe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNgayTiemThucTe.Location = new System.Drawing.Point(50, 350);
            this.lblNgayTiemThucTe.Name = "lblNgayTiemThucTe";
            this.lblNgayTiemThucTe.Size = new System.Drawing.Size(208, 30);
            this.lblNgayTiemThucTe.TabIndex = 12;
            this.lblNgayTiemThucTe.Text = "Ngày tiêm thực tế:";
            // 
            // lblSoMuiValue
            // 
            this.lblSoMuiValue.AutoSize = true;
            this.lblSoMuiValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSoMuiValue.Location = new System.Drawing.Point(300, 300);
            this.lblSoMuiValue.Name = "lblSoMuiValue";
            this.lblSoMuiValue.Size = new System.Drawing.Size(65, 30);
            this.lblSoMuiValue.TabIndex = 11;
            this.lblSoMuiValue.Text = "Value";
            // 
            // lblSoMui
            // 
            this.lblSoMui.AutoSize = true;
            this.lblSoMui.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSoMui.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblSoMui.Location = new System.Drawing.Point(50, 300);
            this.lblSoMui.Name = "lblSoMui";
            this.lblSoMui.Size = new System.Drawing.Size(162, 30);
            this.lblSoMui.TabIndex = 10;
            this.lblSoMui.Text = "Số thứ tự mũi:";
            // 
            // lblNgayHenValue
            // 
            this.lblNgayHenValue.AutoSize = true;
            this.lblNgayHenValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayHenValue.Location = new System.Drawing.Point(300, 250);
            this.lblNgayHenValue.Name = "lblNgayHenValue";
            this.lblNgayHenValue.Size = new System.Drawing.Size(65, 30);
            this.lblNgayHenValue.TabIndex = 9;
            this.lblNgayHenValue.Text = "Value";
            // 
            // lblNgayHen
            // 
            this.lblNgayHen.AutoSize = true;
            this.lblNgayHen.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNgayHen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNgayHen.Location = new System.Drawing.Point(50, 250);
            this.lblNgayHen.Name = "lblNgayHen";
            this.lblNgayHen.Size = new System.Drawing.Size(118, 30);
            this.lblNgayHen.TabIndex = 8;
            this.lblNgayHen.Text = "Ngày hẹn:";
            // 
            // lblTenVaccineValue
            // 
            this.lblTenVaccineValue.AutoSize = true;
            this.lblTenVaccineValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTenVaccineValue.Location = new System.Drawing.Point(300, 200);
            this.lblTenVaccineValue.Name = "lblTenVaccineValue";
            this.lblTenVaccineValue.Size = new System.Drawing.Size(65, 30);
            this.lblTenVaccineValue.TabIndex = 7;
            this.lblTenVaccineValue.Text = "Value";
            // 
            // lblTenVaccine
            // 
            this.lblTenVaccine.AutoSize = true;
            this.lblTenVaccine.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTenVaccine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTenVaccine.Location = new System.Drawing.Point(50, 200);
            this.lblTenVaccine.Name = "lblTenVaccine";
            this.lblTenVaccine.Size = new System.Drawing.Size(139, 30);
            this.lblTenVaccine.TabIndex = 6;
            this.lblTenVaccine.Text = "Tên Vaccine:";
            // 
            // lblTenNguoiTiemValue
            // 
            this.lblTenNguoiTiemValue.AutoSize = true;
            this.lblTenNguoiTiemValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTenNguoiTiemValue.Location = new System.Drawing.Point(300, 150);
            this.lblTenNguoiTiemValue.Name = "lblTenNguoiTiemValue";
            this.lblTenNguoiTiemValue.Size = new System.Drawing.Size(65, 30);
            this.lblTenNguoiTiemValue.TabIndex = 5;
            this.lblTenNguoiTiemValue.Text = "Value";
            // 
            // lblTenNguoiTiem
            // 
            this.lblTenNguoiTiem.AutoSize = true;
            this.lblTenNguoiTiem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTenNguoiTiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTenNguoiTiem.Location = new System.Drawing.Point(50, 150);
            this.lblTenNguoiTiem.Name = "lblTenNguoiTiem";
            this.lblTenNguoiTiem.Size = new System.Drawing.Size(176, 30);
            this.lblTenNguoiTiem.TabIndex = 4;
            this.lblTenNguoiTiem.Text = "Tên người tiêm:";
            // 
            // lblMaHSTCValue
            // 
            this.lblMaHSTCValue.AutoSize = true;
            this.lblMaHSTCValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMaHSTCValue.Location = new System.Drawing.Point(300, 100);
            this.lblMaHSTCValue.Name = "lblMaHSTCValue";
            this.lblMaHSTCValue.Size = new System.Drawing.Size(65, 30);
            this.lblMaHSTCValue.TabIndex = 3;
            this.lblMaHSTCValue.Text = "Value";
            // 
            // lblMaHSTC
            // 
            this.lblMaHSTC.AutoSize = true;
            this.lblMaHSTC.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMaHSTC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblMaHSTC.Location = new System.Drawing.Point(50, 100);
            this.lblMaHSTC.Name = "lblMaHSTC";
            this.lblMaHSTC.Size = new System.Drawing.Size(113, 30);
            this.lblMaHSTC.TabIndex = 2;
            this.lblMaHSTC.Text = "Mã HSTC:";
            // 
            // lblMaLTValue
            // 
            this.lblMaLTValue.AutoSize = true;
            this.lblMaLTValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMaLTValue.Location = new System.Drawing.Point(300, 50);
            this.lblMaLTValue.Name = "lblMaLTValue";
            this.lblMaLTValue.Size = new System.Drawing.Size(65, 30);
            this.lblMaLTValue.TabIndex = 1;
            this.lblMaLTValue.Text = "Value";
            // 
            // lblMaLT
            // 
            this.lblMaLT.AutoSize = true;
            this.lblMaLT.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMaLT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblMaLT.Location = new System.Drawing.Point(50, 50);
            this.lblMaLT.Name = "lblMaLT";
            this.lblMaLT.Size = new System.Drawing.Size(147, 30);
            this.lblMaLT.TabIndex = 0;
            this.lblMaLT.Text = "Mã lịch tiêm:";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFooter.Controls.Add(this.btnHuy);
            this.pnlFooter.Controls.Add(this.btnXacNhan);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 650);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlFooter.Size = new System.Drawing.Size(700, 100);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(380, 25);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(150, 50);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnXacNhan.FlatAppearance.BorderSize = 0;
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(170, 25);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(180, 50);
            this.btnXacNhan.TabIndex = 0;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // XacNhanTiemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 750);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XacNhanTiemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xác Nhận Tiêm Chủng";
            this.pnlHeader.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

 }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnXacNhan;
     private System.Windows.Forms.Button btnHuy;
    private System.Windows.Forms.Label lblMaLT;
        private System.Windows.Forms.Label lblMaLTValue;
        private System.Windows.Forms.Label lblMaHSTCValue;
        private System.Windows.Forms.Label lblMaHSTC;
        private System.Windows.Forms.Label lblTenNguoiTiemValue;
      private System.Windows.Forms.Label lblTenNguoiTiem;
     private System.Windows.Forms.Label lblTenVaccineValue;
        private System.Windows.Forms.Label lblTenVaccine;
        private System.Windows.Forms.Label lblNgayHenValue;
 private System.Windows.Forms.Label lblNgayHen;
        private System.Windows.Forms.Label lblSoMuiValue;
      private System.Windows.Forms.Label lblSoMui;
    private System.Windows.Forms.Label lblNgayTiemThucTeValue;
     private System.Windows.Forms.Label lblNgayTiemThucTe;
    private System.Windows.Forms.TextBox txtGhiChu;
      private System.Windows.Forms.Label lblGhiChu;
    }
}
