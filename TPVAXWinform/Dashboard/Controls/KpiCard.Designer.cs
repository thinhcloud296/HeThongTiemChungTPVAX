namespace TPVAXWinform.Dashboard.Controls
{
    partial class KpiCard
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTitle.Location = new System.Drawing.Point(60, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "L?ch h?n hôm nay";
            // 
            // lblValue
            // 
            this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(60, 30);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(125, 40);
            this.lblValue.TabIndex = 1;
            this.lblValue.Text = "56";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(60, 70);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(111, 13);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "??n: 24 • V?ng: 3";
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(10, 10);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(40, 40);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 3;
            this.picIcon.TabStop = false;
            // 
            // KpiCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblTitle);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "KpiCard";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(200, 100);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.PictureBox picIcon;
    }
}
