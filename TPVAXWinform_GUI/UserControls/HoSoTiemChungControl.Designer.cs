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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearchRecordId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSearchCustomerId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.xo = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.colRecordId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xo)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabList);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(0, 70);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1308, 1116);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "";
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.tableLayoutPanel1);
            this.tabList.Location = new System.Drawing.Point(4, 39);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(10);
            this.tabList.Size = new System.Drawing.Size(1300, 1073);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "Danh sách";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelFilter, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.xo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1280, 1053);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.Controls.Add(this.txtSearchName);
            this.panelFilter.Controls.Add(this.label5);
            this.panelFilter.Controls.Add(this.txtSearchRecordId);
            this.panelFilter.Controls.Add(this.label6);
            this.panelFilter.Controls.Add(this.txtSearchCustomerId);
            this.panelFilter.Controls.Add(this.label7);
            this.panelFilter.Controls.Add(this.btnReset);
            this.panelFilter.Controls.Add(this.btnSearch);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(3, 3);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(15);
            this.panelFilter.Size = new System.Drawing.Size(1274, 80);
            this.panelFilter.TabIndex = 3;
            // 
            // txtSearchName
            // 
            this.txtSearchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchName.Location = new System.Drawing.Point(130, 25);
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(200, 34);
            this.txtSearchName.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label5.Location = new System.Drawing.Point(15, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "🔍 Họ tên:";
            // 
            // txtSearchRecordId
            // 
            this.txtSearchRecordId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchRecordId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchRecordId.Location = new System.Drawing.Point(450, 25);
            this.txtSearchRecordId.Name = "txtSearchRecordId";
            this.txtSearchRecordId.Size = new System.Drawing.Size(150, 34);
            this.txtSearchRecordId.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label6.Location = new System.Drawing.Point(340, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "📋 Mã HS:";
            // 
            // txtSearchCustomerId
            // 
            this.txtSearchCustomerId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchCustomerId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchCustomerId.Location = new System.Drawing.Point(730, 25);
            this.txtSearchCustomerId.Name = "txtSearchCustomerId";
            this.txtSearchCustomerId.Size = new System.Drawing.Size(150, 34);
            this.txtSearchCustomerId.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label7.Location = new System.Drawing.Point(620, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 25);
            this.label7.TabIndex = 12;
            this.label7.Text = "👤 Mã KH:";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(1020, 23);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 38);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "🔄 Đặt lại";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(890, 23);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 38);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "🔍 Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // xo
            // 
            this.xo.AllowUserToAddRows = false;
            this.xo.AllowUserToDeleteRows = false;
            this.xo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.xo.BackgroundColor = System.Drawing.Color.White;
            this.xo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.xo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRecordId,
            this.colCustomerId,
            this.colCustomerName,
            this.colGender,
            this.colBirthDate});
            this.xo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xo.Location = new System.Drawing.Point(3, 89);
            this.xo.Name = "xo";
            this.xo.ReadOnly = true;
            this.xo.RowHeadersWidth = 62;
            this.xo.RowTemplate.Height = 28;
            this.xo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.xo.Size = new System.Drawing.Size(1274, 466);
            this.xo.TabIndex = 4;
            this.xo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecords_CellClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 561);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1274, 489);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(186, 54);
            this.button1.TabIndex = 9;
            this.button1.Text = "Thêm hồ sơ";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(195, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(189, 54);
            this.button2.TabIndex = 8;
            this.button2.Text = "Thêm mũi tiêm";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(390, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(234, 54);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Chỉnh sửa thông tin";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1308, 70);
            this.panelHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(660, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 QUẢN LÝ HỒ SƠ TIÊM CHỦNG";
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
            // colGender
            // 
            this.colGender.HeaderText = "Giới tính";
            this.colGender.MinimumWidth = 8;
            this.colGender.Name = "colGender";
            this.colGender.ReadOnly = true;
            // 
            // colBirthDate
            // 
            this.colBirthDate.HeaderText = "Ngày sinh";
            this.colBirthDate.MinimumWidth = 8;
            this.colBirthDate.Name = "colBirthDate";
            this.colBirthDate.ReadOnly = true;
            // 
            // HoSoTiemChungControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelHeader);
            this.Name = "HoSoTiemChungControl";
            this.Size = new System.Drawing.Size(1308, 1186);
            this.Load += new System.EventHandler(this.HoSoTiemChungControl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xo)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

   }

 #endregion

 private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabList;
private System.Windows.Forms.Panel panelHeader;
   private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.TextBox txtSearchName;
  private System.Windows.Forms.Label label5;
  private System.Windows.Forms.TextBox txtSearchRecordId;
 private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSearchCustomerId;
   private System.Windows.Forms.Label label7;
     private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView xo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
     private System.Windows.Forms.Button btnEdit;
  private System.Windows.Forms.Button button2;
      private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBirthDate;
    }
}
