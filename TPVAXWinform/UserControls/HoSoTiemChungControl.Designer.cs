namespace TPVAXWinform.UserControls
{
    partial class HoSoTiemChungControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabList = new System.Windows.Forms.TabPage();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDoseNumber = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboVaccine = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.colRecordId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVaccinationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVaccineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoseNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrintCert = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabControl1.SuspendLayout();
            this.tabList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabList);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl1.Location = new System.Drawing.Point(0, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1200, 640);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "";
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.dgvRecords);
            this.tabList.Controls.Add(this.panelFilter);
            this.tabList.Location = new System.Drawing.Point(4, 37);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(10);
            this.tabList.Size = new System.Drawing.Size(1192, 599);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "Danh sách";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecords.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRecordId,
            this.colCustomerId,
            this.colCustomerName,
            this.colVaccinationDate,
            this.colVaccineName,
            this.colDoseNumber,
            this.colLotNumber,
            this.colEmployee,
            this.colStatus,
            this.colPrintCert});
            this.dgvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecords.Location = new System.Drawing.Point(10, 110);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.ReadOnly = true;
            this.dgvRecords.RowHeadersWidth = 62;
            this.dgvRecords.RowTemplate.Height = 28;
            this.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecords.Size = new System.Drawing.Size(1172, 479);
            this.dgvRecords.TabIndex = 1;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelFilter.Controls.Add(this.btnReset);
            this.panelFilter.Controls.Add(this.btnSearch);
            this.panelFilter.Controls.Add(this.cboStatus);
            this.panelFilter.Controls.Add(this.label4);
            this.panelFilter.Controls.Add(this.cboDoseNumber);
            this.panelFilter.Controls.Add(this.label3);
            this.panelFilter.Controls.Add(this.cboVaccine);
            this.panelFilter.Controls.Add(this.label2);
            this.panelFilter.Controls.Add(this.dtpToDate);
            this.panelFilter.Controls.Add(this.dtpFromDate);
            this.panelFilter.Controls.Add(this.label1);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(10, 10);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(10);
            this.panelFilter.Size = new System.Drawing.Size(1172, 100);
            this.panelFilter.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(1050, 50);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 35);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Đặt lại";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(930, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 35);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Tất cả",
            "Đã tiêm",
            "Đã hủy",
            "Chưa tiêm"});
            this.cboStatus.Location = new System.Drawing.Point(730, 54);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(150, 36);
            this.cboStatus.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(630, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 28);
            this.label4.TabIndex = 7;
            this.label4.Text = "Trạng thái";
            // 
            // cboDoseNumber
            // 
            this.cboDoseNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoseNumber.FormattingEnabled = true;
            this.cboDoseNumber.Items.AddRange(new object[] {
            "Tất cả",
            "Mũi 1",
            "Mũi 2",
            "Mũi 3"});
            this.cboDoseNumber.Location = new System.Drawing.Point(450, 54);
            this.cboDoseNumber.Name = "cboDoseNumber";
            this.cboDoseNumber.Size = new System.Drawing.Size(150, 36);
            this.cboDoseNumber.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(370, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mũi số";
            // 
            // cboVaccine
            // 
            this.cboVaccine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVaccine.FormattingEnabled = true;
            this.cboVaccine.Items.AddRange(new object[] {
            "Tất cả",
            "Vaccine A",
            "Vaccine B",
            "Vaccine C",
            "Vaccine D"});
            this.cboVaccine.Location = new System.Drawing.Point(130, 54);
            this.cboVaccine.Name = "cboVaccine";
            this.cboVaccine.Size = new System.Drawing.Size(200, 36);
            this.cboVaccine.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Loại Vaccine";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(320, 15);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(150, 34);
            this.dtpToDate.TabIndex = 2;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(130, 15);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(150, 34);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1200, 60);
            this.panelHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(368, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỒ SƠ TIÊM CHỦNG";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 37);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1192, 599);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // colRecordId
            // 
            this.colRecordId.HeaderText = "Mã HS";
            this.colRecordId.MinimumWidth = 8;
            this.colRecordId.Name = "colRecordId";
            this.colRecordId.ReadOnly = true;
            // 
            // colCustomerId
            // 
            this.colCustomerId.HeaderText = "Mã KH";
            this.colCustomerId.MinimumWidth = 8;
            this.colCustomerId.Name = "colCustomerId";
            this.colCustomerId.ReadOnly = true;
            // 
            // colCustomerName
            // 
            this.colCustomerName.HeaderText = "Họ tên";
            this.colCustomerName.MinimumWidth = 8;
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.ReadOnly = true;
            // 
            // colVaccinationDate
            // 
            this.colVaccinationDate.HeaderText = "Ngày tiêm";
            this.colVaccinationDate.MinimumWidth = 8;
            this.colVaccinationDate.Name = "colVaccinationDate";
            this.colVaccinationDate.ReadOnly = true;
            // 
            // colVaccineName
            // 
            this.colVaccineName.HeaderText = "Vaccine";
            this.colVaccineName.MinimumWidth = 8;
            this.colVaccineName.Name = "colVaccineName";
            this.colVaccineName.ReadOnly = true;
            // 
            // colDoseNumber
            // 
            this.colDoseNumber.HeaderText = "Mũi số";
            this.colDoseNumber.MinimumWidth = 8;
            this.colDoseNumber.Name = "colDoseNumber";
            this.colDoseNumber.ReadOnly = true;
            // 
            // colLotNumber
            // 
            this.colLotNumber.HeaderText = "Lô";
            this.colLotNumber.MinimumWidth = 8;
            this.colLotNumber.Name = "colLotNumber";
            this.colLotNumber.ReadOnly = true;
            // 
            // colEmployee
            // 
            this.colEmployee.HeaderText = "Tên Nhân Viên";
            this.colEmployee.MinimumWidth = 8;
            this.colEmployee.Name = "colEmployee";
            this.colEmployee.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.MinimumWidth = 8;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colPrintCert
            // 
            this.colPrintCert.HeaderText = "In hóa đơn";
            this.colPrintCert.MinimumWidth = 8;
            this.colPrintCert.Name = "colPrintCert";
            this.colPrintCert.ReadOnly = true;
            this.colPrintCert.Text = "In";
            this.colPrintCert.UseColumnTextForButtonValue = true;
            // 
            // HoSoTiemChungControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelHeader);
            this.Name = "HoSoTiemChungControl";
            this.Size = new System.Drawing.Size(1200, 700);
            this.Load += new System.EventHandler(this.HoSoTiemChungControl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

   }

 #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabList;
private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
      private System.Windows.Forms.Panel panelFilter;
     private System.Windows.Forms.DataGridView dgvRecords;
   private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.ComboBox cboVaccine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDoseNumber;
        private System.Windows.Forms.Label label3;
   private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVaccinationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVaccineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoseNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLotNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colPrintCert;
    }
}
