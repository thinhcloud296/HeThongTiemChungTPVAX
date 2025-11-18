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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();
            this.colMaKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinhKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioiTinhKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCCCDKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoDTKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCCCDKH = new System.Windows.Forms.TextBox();
            this.txtSearchPhone = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvHSTC = new System.Windows.Forms.DataGridView();
            this.colMaHSTC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTenHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioiTinhHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinhHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCCCDHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTenKHHGHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuanHeHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoDTKhHSTC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaKHHSTC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnThemMuiTiem = new System.Windows.Forms.Button();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearchRecordId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSearchCustomerId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHSTC)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.dgvKhachHang, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dgvHSTC, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelFilter, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1539, 1281);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnThemMoi);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 1215);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1533, 69);
            this.flowLayoutPanel1.TabIndex = 13;
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThemMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnThemMoi.ForeColor = System.Drawing.Color.White;
            this.btnThemMoi.Location = new System.Drawing.Point(3, 3);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(203, 63);
            this.btnThemMoi.TabIndex = 9;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.UseVisualStyleBackColor = false;
            this.btnThemMoi.Visible = false;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // dgvKhachHang
            // 
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.AllowUserToDeleteRows = false;
            this.dgvKhachHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhachHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaKH,
            this.colHoTenKH,
            this.colNgaySinhKH,
            this.colGioiTinhKH,
            this.colCCCDKH,
            this.colSoDTKH});
            this.dgvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhachHang.Location = new System.Drawing.Point(3, 795);
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.RowHeadersWidth = 62;
            this.dgvKhachHang.RowTemplate.Height = 28;
            this.dgvKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.Size = new System.Drawing.Size(1533, 414);
            this.dgvKhachHang.TabIndex = 12;
            this.dgvKhachHang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhachHang_CellContentClick);
            // 
            // colMaKH
            // 
            this.colMaKH.HeaderText = "Mã KH";
            this.colMaKH.MinimumWidth = 8;
            this.colMaKH.Name = "colMaKH";
            this.colMaKH.ReadOnly = true;
            // 
            // colHoTenKH
            // 
            this.colHoTenKH.HeaderText = "Họ tên";
            this.colHoTenKH.MinimumWidth = 8;
            this.colHoTenKH.Name = "colHoTenKH";
            this.colHoTenKH.ReadOnly = true;
            // 
            // colNgaySinhKH
            // 
            this.colNgaySinhKH.HeaderText = "Ngày sinh";
            this.colNgaySinhKH.MinimumWidth = 8;
            this.colNgaySinhKH.Name = "colNgaySinhKH";
            this.colNgaySinhKH.ReadOnly = true;
            // 
            // colGioiTinhKH
            // 
            this.colGioiTinhKH.HeaderText = "Giới tính";
            this.colGioiTinhKH.MinimumWidth = 8;
            this.colGioiTinhKH.Name = "colGioiTinhKH";
            this.colGioiTinhKH.ReadOnly = true;
            // 
            // colCCCDKH
            // 
            this.colCCCDKH.HeaderText = "CCCD";
            this.colCCCDKH.MinimumWidth = 8;
            this.colCCCDKH.Name = "colCCCDKH";
            this.colCCCDKH.ReadOnly = true;
            // 
            // colSoDTKH
            // 
            this.colSoDTKH.HeaderText = "Số ĐT";
            this.colSoDTKH.MinimumWidth = 8;
            this.colSoDTKH.Name = "colSoDTKH";
            this.colSoDTKH.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtCCCDKH);
            this.panel2.Controls.Add(this.txtSearchPhone);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 709);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(15);
            this.panel2.Size = new System.Drawing.Size(1533, 80);
            this.panel2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label4.Location = new System.Drawing.Point(950, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 25);
            this.label4.TabIndex = 18;
            this.label4.Text = "👤 CCCD:";
            // 
            // txtCCCDKH
            // 
            this.txtCCCDKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCCCDKH.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCCCDKH.Location = new System.Drawing.Point(1048, 25);
            this.txtCCCDKH.Name = "txtCCCDKH";
            this.txtCCCDKH.Size = new System.Drawing.Size(174, 34);
            this.txtCCCDKH.TabIndex = 20;
            // 
            // txtSearchPhone
            // 
            this.txtSearchPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchPhone.Location = new System.Drawing.Point(770, 25);
            this.txtSearchPhone.Name = "txtSearchPhone";
            this.txtSearchPhone.Size = new System.Drawing.Size(150, 34);
            this.txtSearchPhone.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label8.Location = new System.Drawing.Point(660, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 25);
            this.label8.TabIndex = 18;
            this.label8.Text = "📞 Số ĐT:";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBox1.Location = new System.Drawing.Point(421, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 34);
            this.textBox1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label2.Location = new System.Drawing.Point(310, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "🔍 Họ tên:";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBox2.Location = new System.Drawing.Point(125, 25);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(150, 34);
            this.textBox2.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label3.Location = new System.Drawing.Point(15, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "👤 Mã KH:";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1369, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 38);
            this.button3.TabIndex = 10;
            this.button3.Text = "Đặt lại";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(1239, 22);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 38);
            this.button4.TabIndex = 9;
            this.button4.Text = "Tìm kiếm";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 609);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1533, 94);
            this.panel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(403, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(477, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "QUẢN LÝ KHÁCH HÀNG";
            // 
            // dgvHSTC
            // 
            this.dgvHSTC.AllowUserToAddRows = false;
            this.dgvHSTC.AllowUserToDeleteRows = false;
            this.dgvHSTC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHSTC.BackgroundColor = System.Drawing.Color.White;
            this.dgvHSTC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHSTC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaHSTC,
            this.colHoTenHS,
            this.colGioiTinhHS,
            this.colNgaySinhHS,
            this.colCCCDHS,
            this.colHoTenKHHGHS,
            this.colQuanHeHS,
            this.colSoDTKhHSTC,
            this.colMaKHHSTC});
            this.dgvHSTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHSTC.Location = new System.Drawing.Point(3, 189);
            this.dgvHSTC.Name = "dgvHSTC";
            this.dgvHSTC.ReadOnly = true;
            this.dgvHSTC.RowHeadersWidth = 62;
            this.dgvHSTC.RowTemplate.Height = 28;
            this.dgvHSTC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHSTC.Size = new System.Drawing.Size(1533, 414);
            this.dgvHSTC.TabIndex = 8;
            this.dgvHSTC.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHSTC_CellContentClick);
            // 
            // colMaHSTC
            // 
            this.colMaHSTC.HeaderText = "Mã HS";
            this.colMaHSTC.MinimumWidth = 8;
            this.colMaHSTC.Name = "colMaHSTC";
            this.colMaHSTC.ReadOnly = true;
            // 
            // colHoTenHS
            // 
            this.colHoTenHS.HeaderText = "Họ tên";
            this.colHoTenHS.MinimumWidth = 8;
            this.colHoTenHS.Name = "colHoTenHS";
            this.colHoTenHS.ReadOnly = true;
            // 
            // colGioiTinhHS
            // 
            this.colGioiTinhHS.HeaderText = "Giới tính";
            this.colGioiTinhHS.MinimumWidth = 8;
            this.colGioiTinhHS.Name = "colGioiTinhHS";
            this.colGioiTinhHS.ReadOnly = true;
            // 
            // colNgaySinhHS
            // 
            this.colNgaySinhHS.HeaderText = "Ngày sinh";
            this.colNgaySinhHS.MinimumWidth = 8;
            this.colNgaySinhHS.Name = "colNgaySinhHS";
            this.colNgaySinhHS.ReadOnly = true;
            // 
            // colCCCDHS
            // 
            this.colCCCDHS.HeaderText = "CCCD";
            this.colCCCDHS.MinimumWidth = 8;
            this.colCCCDHS.Name = "colCCCDHS";
            this.colCCCDHS.ReadOnly = true;
            // 
            // colHoTenKHHGHS
            // 
            this.colHoTenKHHGHS.HeaderText = "Họ tên KH";
            this.colHoTenKHHGHS.MinimumWidth = 8;
            this.colHoTenKHHGHS.Name = "colHoTenKHHGHS";
            this.colHoTenKHHGHS.ReadOnly = true;
            // 
            // colQuanHeHS
            // 
            this.colQuanHeHS.HeaderText = "Quan hệ";
            this.colQuanHeHS.MinimumWidth = 8;
            this.colQuanHeHS.Name = "colQuanHeHS";
            this.colQuanHeHS.ReadOnly = true;
            // 
            // colSoDTKhHSTC
            // 
            this.colSoDTKhHSTC.HeaderText = "Số ĐT khách hàng";
            this.colSoDTKhHSTC.MinimumWidth = 8;
            this.colSoDTKhHSTC.Name = "colSoDTKhHSTC";
            this.colSoDTKhHSTC.ReadOnly = true;
            this.colSoDTKhHSTC.Visible = false;
            // 
            // colMaKHHSTC
            // 
            this.colMaKHHSTC.HeaderText = "Mã KH của HSTC";
            this.colMaKHHSTC.MinimumWidth = 8;
            this.colMaKHHSTC.Name = "colMaKHHSTC";
            this.colMaKHHSTC.ReadOnly = true;
            this.colMaKHHSTC.Visible = false;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.Controls.Add(this.btnThemMuiTiem);
            this.panelFilter.Controls.Add(this.txtSearchName);
            this.panelFilter.Controls.Add(this.label5);
            this.panelFilter.Controls.Add(this.txtSearchRecordId);
            this.panelFilter.Controls.Add(this.label6);
            this.panelFilter.Controls.Add(this.txtSearchCustomerId);
            this.panelFilter.Controls.Add(this.label7);
            this.panelFilter.Controls.Add(this.btnReset);
            this.panelFilter.Controls.Add(this.btnSearch);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFilter.Location = new System.Drawing.Point(3, 103);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(15);
            this.panelFilter.Size = new System.Drawing.Size(1533, 80);
            this.panelFilter.TabIndex = 7;
            // 
            // btnThemMuiTiem
            // 
            this.btnThemMuiTiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btnThemMuiTiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemMuiTiem.ForeColor = System.Drawing.Color.White;
            this.btnThemMuiTiem.Location = new System.Drawing.Point(1168, 23);
            this.btnThemMuiTiem.Name = "btnThemMuiTiem";
            this.btnThemMuiTiem.Size = new System.Drawing.Size(191, 39);
            this.btnThemMuiTiem.TabIndex = 18;
            this.btnThemMuiTiem.Text = "Thêm mũi tiêm";
            this.btnThemMuiTiem.UseVisualStyleBackColor = false;
            this.btnThemMuiTiem.Visible = false;
            this.btnThemMuiTiem.Click += new System.EventHandler(this.btnThemMuiTiem_Click);
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
            this.label7.Size = new System.Drawing.Size(92, 25);
            this.label7.TabIndex = 12;
            this.label7.Text = "👤 CCCD:";
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
            this.btnReset.Text = "Đặt lại";
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
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(3, 3);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1533, 94);
            this.panelHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(351, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(593, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ HỒ SƠ TIÊM CHỦNG";
            // 
            // HoSoTiemChungControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HoSoTiemChungControl";
            this.Size = new System.Drawing.Size(1539, 1281);
            this.Load += new System.EventHandler(this.HoSoTiemChungControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHSTC)).EndInit();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

   }

 #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSearchRecordId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSearchCustomerId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvHSTC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSearchPhone;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dgvKhachHang;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinhKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioiTinhKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCCCDKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoDTKH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCCCDKH;
        private System.Windows.Forms.Button btnThemMuiTiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHSTC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTenHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioiTinhHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinhHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCCCDHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTenKHHGHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuanHeHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoDTKhHSTC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKHHSTC;
    }
}
