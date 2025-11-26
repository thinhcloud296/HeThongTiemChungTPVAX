using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPVAXWinform.UserControls
{
    public partial class BangDieuKhienControl : UserControl
    {
        public BangDieuKhienControl()
        {
            InitializeComponent();
        }
        private void BangDieuKhienControl_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }
            SetupLiveCharts();
        }
        private void SetupLiveCharts()
        {
            // ========== BIỂU ĐỒ CỘT (Column Chart) ==========
            var columnChart = new LiveCharts.WinForms.CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Series = new LiveCharts.SeriesCollection
    {
  new ColumnSeries
        {
     Title = "Lượt tiêm",
   Values = new ChartValues<double> { 120, 150, 200, 180, 220, 250 },
      Fill = System.Windows.Media.Brushes.RoyalBlue,
           DataLabels = true,
  LabelPoint = point => point.Y.ToString("N0")
    }
    },
                AxisX = new AxesCollection
         {
 new Axis
        {
          Title = "Tháng",
Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6" },
    Separator = new Separator { Step = 1 }
     }
       },
                AxisY = new AxesCollection
      {
            new Axis
    {
            Title = "Số lượt tiêm",
       LabelFormatter = value => value.ToString("N0")
        }
   },
                LegendLocation = LiveCharts.LegendLocation.Top
            };

            // Xóa chart1 cũ và thêm LiveChart mới
            if (chart1 != null && chart1.Parent != null)
            {
                var parent = chart1.Parent;
                var index = parent.Controls.IndexOf(chart1);
                parent.Controls.Remove(chart1);
                parent.Controls.Add(columnChart);
                parent.Controls.SetChildIndex(columnChart, index);
            }

            // ========== BIỂU ĐỒ TRÒN (Pie Chart) ==========
            var pieChart = new LiveCharts.WinForms.PieChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Series = new LiveCharts.SeriesCollection
    {
        new PieSeries
    {
      Title = "Vaccine A",
    Values = new ChartValues<double> { 30 },
     DataLabels = true,
    LabelPoint = point => $"{point.Y} ({point.Participation:P0})",
 Fill = System.Windows.Media.Brushes.Blue
 },
   new PieSeries
  {
             Title = "Vaccine B",
   Values = new ChartValues<double> { 25 },
    DataLabels = true,
      LabelPoint = point => $"{point.Y} ({point.Participation:P0})",
    Fill = System.Windows.Media.Brushes.Green
  },
new PieSeries
   {
        Title = "Vaccine C",
 Values = new ChartValues<double> { 20 },
      DataLabels = true,
        LabelPoint = point => $"{point.Y} ({point.Participation:P0})",
      Fill = System.Windows.Media.Brushes.Orange
        },
  new PieSeries
 {
    Title = "Vaccine D",
   Values = new ChartValues<double> { 15 },
     DataLabels = true,
 LabelPoint = point => $"{point.Y} ({point.Participation:P0})",
        Fill = System.Windows.Media.Brushes.Red
        },
     new PieSeries
     {
       Title = "Khác",
    Values = new ChartValues<double> { 10 },
 DataLabels = true,
       LabelPoint = point => $"{point.Y} ({point.Participation:P0})",
Fill = System.Windows.Media.Brushes.Purple
      }
          },
                LegendLocation = LiveCharts.LegendLocation.Right,
                InnerRadius = 20 // Tạo hiệu ứng donut chart
            };

            // Xóa chart2 cũ và thêm LiveChart mới
            if (chart2 != null && chart2.Parent != null)
            {
                var parent = chart2.Parent;
                var index = parent.Controls.IndexOf(chart2);
                parent.Controls.Remove(chart2);
                parent.Controls.Add(pieChart);
                parent.Controls.SetChildIndex(pieChart, index);
            }
        }

        // Public methods để cập nhật KPI từ bên ngoài
    }
}
