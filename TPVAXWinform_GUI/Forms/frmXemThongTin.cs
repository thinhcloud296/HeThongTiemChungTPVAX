using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TPVAXWinform_GUI.Forms
{
    public partial class frmXemThongTin : Form
    {
        private Color primaryColor = Color.FromArgb(41, 128, 185);
        private Color accentColor = Color.FromArgb(52, 152, 219);
        private Color cardBgColor = Color.FromArgb(248, 249, 250);
        private Color textPrimaryColor = Color.FromArgb(44, 62, 80);
        private Color textSecondaryColor = Color.FromArgb(127, 140, 141);

        public frmXemThongTin(string tieuDe, List<KeyValuePair<string, string>> thongTin)
        {
            InitializeComponent();
            lblTieuDe.Text = tieuDe;
            BuildInfoPanel(thongTin);
            AddHoverEffects();
        }

        private void BuildInfoPanel(List<KeyValuePair<string, string>> thongTin)
        {
            int columns = 2;
            int cardWidth = 235;
            int cardHeight = 80;
            int marginX = 15;
            int marginY = 12;
            int startX = 15;
            int startY = 15;

            int col = 0;
            int row = 0;

            foreach (var item in thongTin)
            {
                int x = startX + col * (cardWidth + marginX);
                int y = startY + row * (cardHeight + marginY);

                Panel card = CreateInfoCard(item.Key, item.Value, cardWidth, cardHeight);
                card.Location = new Point(x, y);
                pnlContent.Controls.Add(card);

                col++;
                if (col >= columns)
                {
                    col = 0;
                    row++;
                }
            }

            // Tính toán kích thước form
            int totalRows = (int)Math.Ceiling((double)thongTin.Count / columns);
            int contentHeight = startY + totalRows * (cardHeight + marginY) + startY;
            int formWidth = startX * 2 + columns * cardWidth + (columns - 1) * marginX;
            
            // Header 60 + Content + Footer 70
            int headerHeight = 60;
            int footerHeight = 70;
            int formHeight = headerHeight + contentHeight + footerHeight;

            this.ClientSize = new Size(formWidth, formHeight);
            
            // Cập nhật lại vị trí footer
            pnlFooter.Location = new Point(0, headerHeight + contentHeight);
            pnlFooter.Size = new Size(formWidth, footerHeight);
            
            // Căn giữa button
            btnDong.Location = new Point((formWidth - btnDong.Width) / 2, 12);
        }

        private Panel CreateInfoCard(string fieldName, string fieldValue, int width, int height)
        {
            Panel card = new Panel
            {
                Size = new Size(width, height),
                BackColor = cardBgColor,
                Cursor = Cursors.Hand
            };

            // Bo góc cho card
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(card.ClientRectangle, 8))
                {
                    card.Region = new Region(path);
                }
            };

            // Indicator bên trái
            Panel indicator = new Panel
            {
                Size = new Size(4, height - 16),
                Location = new Point(0, 8),
                BackColor = primaryColor
            };

            // Label tên trường
            Label lblField = new Label
            {
                Text = fieldName,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = textSecondaryColor,
                Location = new Point(14, 8),
                Size = new Size(width - 20, 18),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Label giá trị
            Label lblValue = new Label
            {
                Text = string.IsNullOrEmpty(fieldValue) ? "-" : fieldValue,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = textPrimaryColor,
                Location = new Point(14, 30),
                Size = new Size(width - 20, 30),
                TextAlign = ContentAlignment.TopLeft,
                AutoEllipsis = true
            };

            // Hover effect
            EventHandler enterHandler = (s, e) =>
            {
                card.BackColor = Color.FromArgb(232, 245, 253);
                indicator.BackColor = accentColor;
            };
            EventHandler leaveHandler = (s, e) =>
            {
                card.BackColor = cardBgColor;
                indicator.BackColor = primaryColor;
            };

            card.MouseEnter += enterHandler;
            card.MouseLeave += leaveHandler;
            lblField.MouseEnter += enterHandler;
            lblField.MouseLeave += leaveHandler;
            lblValue.MouseEnter += enterHandler;
            lblValue.MouseLeave += leaveHandler;

            card.Controls.Add(indicator);
            card.Controls.Add(lblField);
            card.Controls.Add(lblValue);

            return card;
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void AddHoverEffects()
        {
            btnDong.MouseEnter += (s, e) => btnDong.BackColor = accentColor;
            btnDong.MouseLeave += (s, e) => btnDong.BackColor = primaryColor;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
