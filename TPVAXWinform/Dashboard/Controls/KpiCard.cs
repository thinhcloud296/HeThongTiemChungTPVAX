using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TPVAXWinform.Dashboard.Controls
{
    /// <summary>
    /// A reusable UserControl to display a Key Performance Indicator (KPI).
    /// </summary>
    public partial class KpiCard : UserControl
    {
        private string _title;
        private string _value;
        private string _subtitle;
        private Image _icon;

        public KpiCard()
        {
            InitializeComponent();
            // Enable double buffering to reduce flicker
            this.DoubleBuffered = true;
        }

        [Category("KPI Properties")]
        public string Title
        {
            get => _title;
            set { _title = value; lblTitle.Text = value; }
        }

        [Category("KPI Properties")]
        public string Value
        {
            get => _value;
            set { _value = value; lblValue.Text = value; }
        }

        [Category("KPI Properties")]
        public string Subtitle
        {
            get => _subtitle;
            set { _subtitle = value; lblSubtitle.Text = value; }
        }

        [Category("KPI Properties")]
        public Image Icon
        {
            get => _icon;
            set { _icon = value; picIcon.Image = value; }
        }

        

        

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw a rounded border
            using (var pen = new Pen(Color.FromArgb(224, 224, 224), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}
