using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace TPVAXWinform
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
     
      // Cải tiến chất lượng rendering
 SetHighQualityRendering();
   }

        private void SetHighQualityRendering()
        {
   // Bật double buffering để giảm flickering
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer | 
      ControlStyles.AllPaintingInWmPaint | 
         ControlStyles.UserPaint, true);
this.UpdateStyles();

   // Cải tiến chất lượng text rendering cho tất cả controls
          SetTextRenderingForControls(this.Controls);
        }

  private void SetTextRenderingForControls(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
            {
      // Cải tiến rendering cho Labels
            if (control is Label label)
       {
          label.UseCompatibleTextRendering = false;
          }
      
         // Cải tiến rendering cho Buttons
    if (control is Button button)
           {
        button.UseCompatibleTextRendering = false;
      }

                // Đệ quy cho các control con
                if (control.HasChildren)
       {
           SetTextRenderingForControls(control.Controls);
   }
            }
        }

        private void main_Load(object sender, EventArgs e)
   {
          SetupLiveCharts();
            InitializeSearchHandlers();
        }

 private void InitializeSearchHandlers()
        {
            // Wire up event handlers cho tìm kiếm bệnh nhân
            btnSearchPatient.Click += BtnSearchPatient_Click;
    btnResetPatient.Click += BtnResetPatient_Click;
   txtSearchPatient.KeyPress += TxtSearchPatient_KeyPress;
            
            // Wire up event handlers cho tìm kiếm vaccine
            btnFilter.Click += BtnSearchVaccine_Click;
   btnReset.Click += BtnResetVaccine_Click;
            txtSearchVaccine.KeyPress += TxtSearchVaccine_KeyPress;
     }

      private void TxtSearchPatient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
     {
      BtnSearchPatient_Click(sender, e);
                e.Handled = true;
            }
        }

        private void TxtSearchVaccine_KeyPress(object sender, KeyPressEventArgs e)
{
            if (e.KeyChar == (char)Keys.Enter)
            {
    BtnSearchVaccine_Click(sender, e);
            e.Handled = true;
       }
        }

private void BtnSearchPatient_Click(object sender, EventArgs e)
    {
     string searchText = txtSearchPatient.Text.Trim();
 
        if (string.IsNullOrEmpty(searchText))
            {
    MessageBox.Show("Vui lòng nhập tên hoặc mã bệnh nhân!", 
        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
       txtSearchPatient.Focus();
     return;
       }
            
  // TODO: Implement search logic
     MessageBox.Show($"Tìm kiếm bệnh nhân: {searchText}", 
      "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
 }

        private void BtnResetPatient_Click(object sender, EventArgs e)
        {
            txtSearchPatient.Clear();
  txtSearchPatient.Focus();
  
    // TODO: Reset chart data
   SetupLiveCharts();
        }

        private void BtnSearchVaccine_Click(object sender, EventArgs e)
        {
     string searchText = txtSearchVaccine.Text.Trim();
        
     if (string.IsNullOrEmpty(searchText))
    {
      MessageBox.Show("Vui lòng nhập tên vaccine!", 
         "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
     txtSearchVaccine.Focus();
                return;
      }
        
   // TODO: Implement search logic
            MessageBox.Show($"Tìm kiếm vaccine: {searchText}", 
     "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

        private void BtnResetVaccine_Click(object sender, EventArgs e)
        {
         txtSearchVaccine.Clear();
   txtSearchVaccine.Focus();
            
    // TODO: Reset chart data
            SetupLiveCharts();
        }
        private void SetupLiveCharts()
        {
            // ========== BIỂU ĐỒ CỘT (Column Chart) ==========
        var columnChart = new LiveCharts.WinForms.CartesianChart
            {
     Dock = DockStyle.Fill,
         BackColor = Color.White,
                Series = new SeriesCollection
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
    Series = new SeriesCollection
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

     private void textBox1_TextChanged(object sender, EventArgs e)
      {

     }

 private void label3_Click(object sender, EventArgs e)
      {

     }

      private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
