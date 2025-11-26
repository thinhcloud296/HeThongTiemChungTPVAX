using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using WinFormsCharts = LiveCharts.WinForms;

namespace TPVAXWinform_GUI.UserControls
{
    public partial class ThongKeControl : UserControl
    {
        private ThongKeBLL thongKeBLL = new ThongKeBLL();
        // KPI Labels
        private Label lblKPI_DoanhThu;
        private Label lblKPI_LuotTiem;
        private Label lblKPI_KhachMoi;
        private Label lblKPI_SapHetHan;

        // Charts
        private WinFormsCharts.CartesianChart chartDoanhThu;
        private WinFormsCharts.PieChart chartTyLe;

        // DataGridViews
        private DataGridView dgvDoanhThu;
        private DataGridView dgvXuatNhapTon;
        private DataGridView dgvSapHetHan;

        public ThongKeControl()
        {
            InitializeComponent();
            InitializeDashboard();
            this.Load += ThongKeControl_Load;
        }
        public void RefreshData()
        {
            LoadData();
        }
        private void ThongKeControl_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }
            LoadData();
        }
        public void LoadData()
        {
            try
            {
                // 1. Load KPI
                var kpi = thongKeBLL.GetKPI();
                UpdateKPI(kpi.DoanhThu, kpi.LuotTiem, kpi.KhachMoi, kpi.SapHetHan);

                // 2. Load Biểu đồ cột (Doanh thu 7 ngày)
                DataTable dtChart1 = thongKeBLL.GetDoanhThu7Ngay();
                List<double> values1 = new List<double>();
                List<string> labels1 = new List<string>();

                foreach (DataRow row in dtChart1.Rows)
                {
                    values1.Add(Convert.ToDouble(row["TongTien"]));
                    DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                    labels1.Add(ngay.ToString("dd/MM"));
                }
                UpdateDoanhThuChart(values1.ToArray(), labels1.ToArray());

                // 3. Load Biểu đồ tròn (Tỷ lệ)
                DataTable dtChart2 = thongKeBLL.GetTyLeDoanhThu();
                double goiVal = 0, leVal = 0;
                foreach (DataRow row in dtChart2.Rows)
                {
                    string type = row["LoaiHinh"].ToString();
                    double val = Convert.ToDouble(row["TongGiaTri"]);
                    if (type == "Gói Vaccine") goiVal = val;
                    else if (type == "Vaccine Lẻ") leVal = val;
                }
                UpdateTyLeChart(goiVal, leVal);

                // 4. Load Grid Sắp hết hạn
                DataTable dtSapHetHan = thongKeBLL.GetVaccineSapHetHan();
                UpdateDataGrid("saphethan", dtSapHetHan);

                // --- THÊM MỚI: Load Grid Doanh Thu ---
                DataTable dtDoanhThu = thongKeBLL.GetDoanhThuChiTiet();
                UpdateDataGrid("doanhthu", dtDoanhThu);
                // Định dạng tiền tệ cho cột "Tổng Tiền" (nếu có)
                if (dgvDoanhThu.Columns["Tổng Tiền"] != null)
                {
                    dgvDoanhThu.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
                    dgvDoanhThu.Columns["Tổng Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // --- THÊM MỚI: Load Grid Xuất Nhập Tồn ---
                DataTable dtXuatNhapTon = thongKeBLL.GetXuatNhapTon();
                UpdateDataGrid("xuatnhapton", dtXuatNhapTon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu thống kê: " + ex.Message);
            }
        }
        private void InitializeDashboard()
        {
            this.SuspendLayout();

            // ========== BỐ CỤC CHÍNH (3 DÒNG) ==========
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(10)
            };

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15F)); // KPI Cards
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F)); // Charts
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F)); // Details
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // ========== DÒNG 1: KPI CARDS ==========
            var kpiPanel = CreateKPISection();
            mainLayout.Controls.Add(kpiPanel, 0, 0);

            // ========== DÒNG 2: BIỂU ĐỒ ==========
            var chartPanel = CreateChartSection();
            mainLayout.Controls.Add(chartPanel, 0, 1);

            // ========== DÒNG 3: CHI TIẾT (TABCONTROL) ==========
            var detailPanel = CreateDetailSection();
            mainLayout.Controls.Add(detailPanel, 0, 2);

            this.Controls.Add(mainLayout);
            this.ResumeLayout();
        }

        // ========== TẠO SECTION KPI CARDS ==========
        private TableLayoutPanel CreateKPISection()
        {
            var kpiLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Padding = new Padding(5)
            };

            for (int i = 0; i < 4; i++)
            {
                kpiLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }
            kpiLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // KPI 1: Doanh thu tháng
            var kpi1 = CreateKPICard("DOANH THU THÁNG", "0 VNĐ", Color.SeaGreen, out lblKPI_DoanhThu);
            kpiLayout.Controls.Add(kpi1, 0, 0);

            // KPI 2: Lượt tiêm
            var kpi2 = CreateKPICard("LƯỢT TIÊM", "0", Color.RoyalBlue, out lblKPI_LuotTiem);
            kpiLayout.Controls.Add(kpi2, 1, 0);

            // KPI 3: Khách mới
            var kpi3 = CreateKPICard("KHÁCH MỚI", "0", Color.Orange, out lblKPI_KhachMoi);
            kpiLayout.Controls.Add(kpi3, 2, 0);

            // KPI 4: Sắp hết hạn
            var kpi4 = CreateKPICard("SẮP HẾT HẠN", "0", Color.Crimson, out lblKPI_SapHetHan);
            kpiLayout.Controls.Add(kpi4, 3, 0);

            return kpiLayout;
        }

        // ========== TẠO 1 KPI CARD ==========
        private Panel CreateKPICard(string title, string value, Color bgColor, out Label valueLabel)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = bgColor,
                Margin = new Padding(5),
                Padding = new Padding(15, 10, 15, 10)
            };

            // Label tiêu đề
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 25,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Label giá trị
            valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel.Controls.Add(valueLabel);
            panel.Controls.Add(lblTitle);

            return panel;
        }

        // ========== TẠO SECTION BIỂU ĐỒ ==========
        private TableLayoutPanel CreateChartSection()
        {
            var chartLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(5)
            };

            chartLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            chartLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // ========== BIỂU ĐỒ CỘT (DOANH THU 7 NGÀY) ==========
            chartDoanhThu = new WinFormsCharts.CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                LegendLocation = LegendLocation.Bottom
            };

            // Khởi tạo Series cho biểu đồ cột
            chartDoanhThu.Series = new SeriesCollection
            {
      new ColumnSeries
     {
     Title = "Doanh thu",
   Values = new ChartValues<double> { 0, 0, 0, 0, 0, 0, 0 },
   Fill = System.Windows.Media.Brushes.SeaGreen,
 DataLabels = true,
         LabelPoint = point => point.Y.ToString("N0")
 }
    };

            // Cấu hình trục X
            chartDoanhThu.AxisX = new AxesCollection
  {
        new Axis
  {
   Title = "Ngày",
Labels = new[] { "CN", "T2", "T3", "T4", "T5", "T6", "T7" },
      Separator = new Separator { Step = 1 }
         }
            };

            // Cấu hình trục Y
            chartDoanhThu.AxisY = new AxesCollection
       {
  new Axis
        {
    Title = "Doanh thu (VNĐ)",
      LabelFormatter = value => value.ToString("N0")
     }
 };

            var chartPanel1 = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            chartPanel1.Controls.Add(chartDoanhThu);

            var lblChart1Title = new Label
            {
                Text = "DOANH THU 7 NGÀY GẦN NHẤT",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft
            };
            chartPanel1.Controls.Add(lblChart1Title);

            chartLayout.Controls.Add(chartPanel1, 0, 0);

            // ========== BIỂU ĐỒ TRÒN (TỶ LỆ GÓI/LẺ) ==========
            chartTyLe = new WinFormsCharts.PieChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                LegendLocation = LegendLocation.Right,
                InnerRadius = 30 // Donut chart
            };

            // Khởi tạo Series cho biểu đồ tròn
            chartTyLe.Series = new SeriesCollection
{
      new PieSeries
       {
  Title = "Tiêm theo gói",
    Values = new ChartValues<double> { 0 },
       DataLabels = true,
     LabelPoint = point => $"{point.Y} ({point.Participation:P0})",
 Fill = System.Windows.Media.Brushes.RoyalBlue
     },
       new PieSeries
  {
     Title = "Tiêm lẻ",
  Values = new ChartValues<double> { 0 },
    DataLabels = true,
          LabelPoint = point => $"{point.Y} ({point.Participation:P0})",
   Fill = System.Windows.Media.Brushes.Orange
     }
     };

            var chartPanel2 = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            chartPanel2.Controls.Add(chartTyLe);

            var lblChart2Title = new Label
            {
                Text = "TỶ LỆ TIÊM GÓI / LẺ",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft
            };
            chartPanel2.Controls.Add(lblChart2Title);

            chartLayout.Controls.Add(chartPanel2, 1, 0);

            return chartLayout;
        }

        // ========== TẠO SECTION CHI TIẾT (TABCONTROL) ==========
        private TabControl CreateDetailSection()
        {
            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F)
            };

            // Tab 1: Chi tiết Doanh thu
            var tabDoanhThu = new TabPage("Chi tiết Doanh thu");
            dgvDoanhThu = CreateStyledDataGridView();
            tabDoanhThu.Controls.Add(dgvDoanhThu);
            tabControl.TabPages.Add(tabDoanhThu);

            // Tab 2: Xuất Nhập Tồn
            var tabXuatNhapTon = new TabPage("Xuất Nhập Tồn");
            dgvXuatNhapTon = CreateStyledDataGridView();
            tabXuatNhapTon.Controls.Add(dgvXuatNhapTon);
            tabControl.TabPages.Add(tabXuatNhapTon);

            // Tab 3: Cảnh báo Hết hạn
            var tabSapHetHan = new TabPage("Cảnh báo Hết hạn");
            dgvSapHetHan = CreateStyledDataGridView();
            tabSapHetHan.Controls.Add(dgvSapHetHan);
            tabControl.TabPages.Add(tabSapHetHan);

            return tabControl;
        }

        // ========== TẠO DATAGRIDVIEW VỚI STYLE ĐỒNG BỘ ==========
        private DataGridView CreateStyledDataGridView()
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 35,
                RowTemplate = { Height = 30 },
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Style cho header
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Style cho selection
            dgv.DefaultCellStyle.SelectionBackColor = Color.RoyalBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Style cho alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            dgv.EnableHeadersVisualStyles = false;

            return dgv;
        }

        // ========== PHƯƠNG THỨC PUBLIC ĐỂ CẬP NHẬT DỮ LIỆU ==========

        /// <summary>
        /// Cập nhật giá trị KPI
        /// </summary>
        public void UpdateKPI(decimal doanhThu, int luotTiem, int khachMoi, int sapHetHan)
        {
            lblKPI_DoanhThu.Text = doanhThu.ToString("N0") + " VNĐ";
            lblKPI_LuotTiem.Text = luotTiem.ToString("N0");
            lblKPI_KhachMoi.Text = khachMoi.ToString("N0");
            lblKPI_SapHetHan.Text = sapHetHan.ToString("N0");
        }

        /// <summary>
        /// Cập nhật biểu đồ doanh thu
        /// </summary>
        public void UpdateDoanhThuChart(double[] values, string[] labels)
        {
            if (chartDoanhThu.Series.Count > 0)
            {
                chartDoanhThu.Series[0].Values.Clear();
                foreach (var val in values)
                {
                    chartDoanhThu.Series[0].Values.Add(val);
                }

                if (labels != null && chartDoanhThu.AxisX.Count > 0)
                {
                    chartDoanhThu.AxisX[0].Labels = labels;
                }
            }
        }

        /// <summary>
        /// Cập nhật biểu đồ tỷ lệ gói/lẻ
        /// </summary>
        public void UpdateTyLeChart(double goiValue, double leValue)
        {
            if (chartTyLe.Series.Count >= 2)
            {
                chartTyLe.Series[0].Values.Clear();
                chartTyLe.Series[0].Values.Add(goiValue);

                chartTyLe.Series[1].Values.Clear();
                chartTyLe.Series[1].Values.Add(leValue);
            }
        }

        /// <summary>
        /// Cập nhật DataGridView
        /// </summary>
        public void UpdateDataGrid(string gridName, DataTable data)
        {
            switch (gridName.ToLower())
            {
                case "doanhthu":
                    dgvDoanhThu.DataSource = data;
                    break;
                case "xuatnhapton":
                    dgvXuatNhapTon.DataSource = data;
                    break;
                case "saphethan":
                    dgvSapHetHan.DataSource = data;
                    break;
            }
        }

        
    }
}
