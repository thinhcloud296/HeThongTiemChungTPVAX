namespace TPVAXWinform_GUI
{
    partial class frmThemNV
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpNgayVaoLam = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cboChucVu = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSoDT = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Location = new System.Drawing.Point(200, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(301, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÊM NHÂN VIÊN";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHoTen.Location = new System.Drawing.Point(180, 90);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(400, 34);
            this.txtHoTen.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(30, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Họ tên (*):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(30, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Giới tính:";
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Location = new System.Drawing.Point(180, 140);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(200, 36);
            this.cboGioiTinh.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(30, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ngày sinh:";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(180, 190);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(200, 34);
            this.dtpNgaySinh.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(30, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 28);
            this.label4.TabIndex = 7;
            this.label4.Text = "CCCD (*):";
            // 
            // txtCCCD
            // 
            this.txtCCCD.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCCCD.Location = new System.Drawing.Point(180, 240);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(400, 34);
            this.txtCCCD.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(30, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 28);
            this.label5.TabIndex = 9;
            this.label5.Text = "Ngày vào làm:";
            // 
            // dtpNgayVaoLam
            // 
            this.dtpNgayVaoLam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayVaoLam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayVaoLam.Location = new System.Drawing.Point(180, 290);
            this.dtpNgayVaoLam.Name = "dtpNgayVaoLam";
            this.dtpNgayVaoLam.Size = new System.Drawing.Size(200, 34);
            this.dtpNgayVaoLam.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.Location = new System.Drawing.Point(30, 343);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 28);
            this.label6.TabIndex = 11;
            this.label6.Text = "Chứ vụ:";
            // 
            // cboChucVu
            // 
            this.cboChucVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChucVu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboChucVu.FormattingEnabled = true;
            this.cboChucVu.Location = new System.Drawing.Point(180, 340);
            this.cboChucVu.Name = "cboChucVu";
            this.cboChucVu.Size = new System.Drawing.Size(200, 36);
            this.cboChucVu.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.Location = new System.Drawing.Point(30, 393);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 28);
            this.label7.TabIndex = 13;
            this.label7.Text = "Số ĐT (*):";
            // 
            // txtSoDT
            // 
            this.txtSoDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoDT.Location = new System.Drawing.Point(180, 390);
            this.txtSoDT.Name = "txtSoDT";
            this.txtSoDT.Size = new System.Drawing.Size(400, 34);
            this.txtSoDT.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(30, 443);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 28);
            this.label8.TabIndex = 15;
            this.label8.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiaChi.Location = new System.Drawing.Point(180, 440);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(400, 34);
            this.txtDiaChi.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.Location = new System.Drawing.Point(30, 493);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 28);
            this.label9.TabIndex = 17;
            this.label9.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(180, 490);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 34);
            this.txtEmail.TabIndex = 18;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(180, 550);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(150, 45);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(350, 550);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 45);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "H?y";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmThemNV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 620);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSoDT);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboChucVu);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpNgayVaoLam);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCCCD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboGioiTinh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemNV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm nhân viên";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

 #endregion

        private System.Windows.Forms.Label lblTitle;
   private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGioiTinh;
    private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
   private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpNgayVaoLam;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboChucVu;
      private System.Windows.Forms.Label label7;
     private System.Windows.Forms.TextBox txtSoDT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
