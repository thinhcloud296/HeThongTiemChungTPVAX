using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TPVAXWinform.Dashboard.Controls
{
    /// <summary>
    /// A button that can display a notification badge.
    /// </summary>
    public class BadgeButton : Button
    {
        private int _badgeCount;
        private bool _showBadge;

        public BadgeButton()
        {
            this.DoubleBuffered = true;
        }

        [Category("Badge Properties")]
        [DefaultValue(0)]
        public int BadgeCount
        {
            get => _badgeCount;
            set
            {
                if (_badgeCount != value)
                {
                    _badgeCount = value;
                    this.Invalidate(); // Redraw the control
                }
            }
        }

        [Category("Badge Properties")]
        [DefaultValue(false)]
        public bool ShowBadge
        {
            get => _showBadge;
            set
            {
                if (_showBadge != value)
                {
                    _showBadge = value;
                    this.Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (ShowBadge && BadgeCount > 0)
            {
                // High-quality graphics
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                string badgeText = BadgeCount > 99 ? "99+" : BadgeCount.ToString();
                Font badgeFont = new Font("Segoe UI", 7F, FontStyle.Bold);
                SizeF textSize = e.Graphics.MeasureString(badgeText, badgeFont);

                // Calculate badge position (top-right corner)
                float badgeWidth = textSize.Width + 8;
                float badgeHeight = textSize.Height + 4;
                RectangleF badgeRect = new RectangleF(
                    this.Width - badgeWidth - 2,
                    2,
                    badgeWidth,
                    badgeHeight);

                // Draw badge circle
                using (var brush = new SolidBrush(Color.Red))
                {
                    e.Graphics.FillEllipse(brush, badgeRect);
                }

                // Draw badge text
                using (var brush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(badgeText, badgeFont, brush, badgeRect, sf);
                }
            }
        }
    }
}
