namespace TPVAXWinform.UserControls
{
    partial class BangDieuKhienControl
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
       System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
  this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
 this.panel4 = new System.Windows.Forms.Panel();
     this.label8 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
          this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panelMiddle = new System.Windows.Forms.Panel();
    this.labelMiddleTitle = new System.Windows.Forms.Label();
        this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
     this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
   this.tableLayoutPanel2.SuspendLayout();
       this.panel4.SuspendLayout();
 this.panel5.SuspendLayout();
   this.panelMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
    this.SuspendLayout();
            // 
            // tableLayoutPanel2
   // 
       this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
     this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelMiddle, 0, 1);
     this.tableLayoutPanel2.Controls.Add(this.chart1, 0, 2);
      this.tableLayoutPanel2.Controls.Add(this.chart2, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1200, 700);
    this.tableLayoutPanel2.TabIndex = 0;
    // 
            // panel4
 // 
     this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel4.Controls.Add(this.label8);
  this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(594, 144);
       this.panel4.TabIndex = 0;
      // 
   // label8
   // 
    this.label8.AutoSize = true;
        this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.ForeColor = System.Drawing.Color.White;
    this.label8.Location = new System.Drawing.Point(23, 103);
      this.label8.Name = "label8";
    this.label8.Size = new System.Drawing.Size(250, 25);
            this.label8.TabIndex = 2;
       this.label8.Text = "Tăng 5% so với tháng trước";
         // 
          // label6
            // 
            this.label6.AutoSize = true;
     this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
 this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(21, 19);
    this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 29);
            this.label6.TabIndex = 0;
     this.label6.Text = "Lịch tiêm hôm nay";
       // 
     // panel5
     // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(162)))), ((int)(((byte)(235)))));
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label11);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
  this.panel5.Location = new System.Drawing.Point(603, 3);
       this.panel5.Name = "panel5";
  this.panel5.Size = new System.Drawing.Size(594, 144);
         this.panel5.TabIndex = 1;
 // 
       // label9
        // 
this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.ForeColor = System.Drawing.Color.White;
    this.label9.Location = new System.Drawing.Point(23, 103);
            this.label9.Name = "label9";
 this.label9.Size = new System.Drawing.Size(250, 25);
    this.label9.TabIndex = 2;
     this.label9.Text = "Tăng 2% so với tháng trước";
            // 
            // label11
          // 
     this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label11.ForeColor = System.Drawing.Color.White;
       this.label11.Location = new System.Drawing.Point(21, 19);
   this.label11.Name = "label11";
          this.label11.Size = new System.Drawing.Size(272, 29);
          this.label11.TabIndex = 0;
  this.label11.Text = "Khách hàng trong tuần";
   // 
  // panelMiddle
            // 
        this.panelMiddle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
        this.tableLayoutPanel2.SetColumnSpan(this.panelMiddle, 2);
  this.panelMiddle.Controls.Add(this.labelMiddleTitle);
      this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panelMiddle.Location = new System.Drawing.Point(3, 153);
      this.panelMiddle.Name = "panelMiddle";
     this.panelMiddle.Size = new System.Drawing.Size(1194, 74);
    this.panelMiddle.TabIndex = 2;
     // 
     // labelMiddleTitle
        // 
        this.labelMiddleTitle.Dock = System.Windows.Forms.DockStyle.Fill;
     this.labelMiddleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.labelMiddleTitle.ForeColor = System.Drawing.Color.White;
            this.labelMiddleTitle.Location = new System.Drawing.Point(0, 0);
            this.labelMiddleTitle.Name = "labelMiddleTitle";
  this.labelMiddleTitle.Size = new System.Drawing.Size(1194, 74);
       this.labelMiddleTitle.TabIndex = 0;
      this.labelMiddleTitle.Text = "Thống kê tổng quan";
this.labelMiddleTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
 // 
     // chart1
       // 
            chartArea3.Name = "ChartArea1";
 this.chart1.ChartAreas.Add(chartArea3);
      this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
    legend3.Name = "Legend1";
       this.chart1.Legends.Add(legend3);
 this.chart1.Location = new System.Drawing.Point(3, 233);
            this.chart1.Name = "chart1";
         series3.ChartArea = "ChartArea1";
        series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
          this.chart1.Size = new System.Drawing.Size(594, 464);
  this.chart1.TabIndex = 4;
         title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
     title3.Name = "Title1";
  this.chart1.Titles.Add(title3);
  // 
            // chart2
   // 
   chartArea4.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea4);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
       legend4.Name = "Legend1";
   this.chart2.Legends.Add(legend4);
     this.chart2.Location = new System.Drawing.Point(603, 233);
  this.chart2.Name = "chart2";
            series4.ChartArea = "ChartArea1";
   series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series4.Legend = "Legend1";
      series4.Name = "Series1";
            this.chart2.Series.Add(series4);
     this.chart2.Size = new System.Drawing.Size(594, 464);
       this.chart2.TabIndex = 5;
   title4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
        title4.Name = "Title1";
            this.chart2.Titles.Add(title4);
            // 
   // BangDieuKhienControl
            // 
  this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.BackColor = System.Drawing.Color.White;
          this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "BangDieuKhienControl";
       this.Size = new System.Drawing.Size(1200, 700);
       this.Load += new System.EventHandler(this.BangDieuKhienControl_Load);
  this.tableLayoutPanel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
 this.panel5.ResumeLayout(false);
  this.panel5.PerformLayout();
       this.panelMiddle.ResumeLayout(false);
    ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
this.ResumeLayout(false);

      }

   #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
      private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.Label labelMiddleTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}
