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
// THAY: Sử dụng Microsoft Office Interop cho Excel
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

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

        // ComboBox chọn khoảng thời gian thống kê
        private ComboBox cboThoiGian;
        private Label lblThoiGian;
        private Label lblChartTitle;

        // DataGridViews
        private DataGridView dgvDoanhThu;
        private DataGridView dgvXuatNhapTon;
        private DataGridView dgvSapHetHan;

        // THÊM: Nút xuất Excel
        private Button btnExportExcel;
        // THÊM: Nút xuất Excel cho các tab khác
        private Button btnExportExcelXNT;
        private Button btnExportExcelSapHetHan;

        // THÊM: Lưu dữ liệu để xuất Excel
        private DataTable dtDoanhThuData;
        private DataTable dtXuatNhapTonData;
        private DataTable dtSapHetHanData;

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
            LoadDataByTimeRange();
        }
        private void InitializeDashboard()
        {
            this.SuspendLayout();

            // ========== BỐ CỤC CHÍNH (4 DÒNG) ==========
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(10)
            };

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F)); // Filter Bar
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15F)); // KPI Cards
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F)); // Charts
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F)); // Details
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // ========== DÒNG 0: FILTER BAR ==========
            var filterPanel = CreateFilterSection();
            mainLayout.Controls.Add(filterPanel, 0, 0);

            // ========== DÒNG 1: KPI CARDS ==========
            var kpiPanel = CreateKPISection();
            mainLayout.Controls.Add(kpiPanel, 0, 1);

            // ========== DÒNG 2: BIỂU ĐỒ ==========
            var chartPanel = CreateChartSection();
            mainLayout.Controls.Add(chartPanel, 0, 2);

            // ========== DÒNG 3: CHI TIẾT (TABCONTROL) ==========
            var detailPanel = CreateDetailSection();
            mainLayout.Controls.Add(detailPanel, 0, 3);

            this.Controls.Add(mainLayout);
            this.ResumeLayout();
        }

        // ========== TẠO SECTION FILTER BAR ==========
        private Panel CreateFilterSection()
        {
            var filterPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10, 5, 10, 5)
            };

            lblThoiGian = new Label
            {
                Text = "📊 Khoảng thời gian thống kê:",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(15, 12)
            };
            filterPanel.Controls.Add(lblThoiGian);

            cboThoiGian = new ComboBox
            {
                Font = new Font("Segoe UI", 11F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 180,
                Height = 30,
                Location = new Point(230, 8)
            };
            cboThoiGian.Items.AddRange(new object[] { "Hôm nay", "7 ngày gần đây", "Tháng này" });
            cboThoiGian.SelectedIndex = 1; // Mặc định: 7 ngày gần đây
            cboThoiGian.SelectedIndexChanged += CboThoiGian_SelectedIndexChanged;
            filterPanel.Controls.Add(cboThoiGian);

            return filterPanel;
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

            lblChartTitle = new Label
            {
                Text = "DOANH THU 7 NGÀY GẦN NHẤT",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft
            };
            chartPanel1.Controls.Add(lblChartTitle);

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
            
            // THÊM: Panel chứa DataGridView và nút xuất Excel
            var panelDoanhThu = new Panel
            {
                Dock = DockStyle.Fill
            };
            
            dgvDoanhThu = CreateStyledDataGridView();
            tabDoanhThu.Controls.Add(dgvDoanhThu);
            panelDoanhThu.Controls.Add(dgvDoanhThu);
            
            // THÊM: Tạo nút xuất Excel
            btnExportExcel = new Button
            {
                Text = "📊 Xuất Excel",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 125, 50), // Màu xanh lá
                ForeColor = Color.White,
                Width = 150,
                Height = 65,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnExportExcel.FlatAppearance.BorderSize = 0;
            btnExportExcel.Click += BtnExportExcel_Click;
            
            panelDoanhThu.Controls.Add(btnExportExcel);
            
            tabDoanhThu.Controls.Add(panelDoanhThu);
            tabControl.TabPages.Add(tabDoanhThu);

            // Tab 2: Xuất Nhập Tồn
            var tabXuatNhapTon = new TabPage("Xuất Nhập Tồn");
            // Panel chứa DataGridView và nút xuất Excel
            var panelXuatNhapTon = new Panel { Dock = DockStyle.Fill };
            dgvXuatNhapTon = CreateStyledDataGridView();
            panelXuatNhapTon.Controls.Add(dgvXuatNhapTon);

            btnExportExcelXNT = new Button
            {
                Text = "📊 Xuất Excel",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                Width = 150,
                Height = 65,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnExportExcelXNT.FlatAppearance.BorderSize = 0;
            btnExportExcelXNT.Click += BtnExportExcel_XNT_Click;
            panelXuatNhapTon.Controls.Add(btnExportExcelXNT);
            tabXuatNhapTon.Controls.Add(panelXuatNhapTon);
            tabControl.TabPages.Add(tabXuatNhapTon);

            // Tab 3: Cảnh báo Hết hạn
            var tabSapHetHan = new TabPage("Cảnh báo Hết hạn");
            var panelSapHetHan = new Panel { Dock = DockStyle.Fill };
            dgvSapHetHan = CreateStyledDataGridView();
            panelSapHetHan.Controls.Add(dgvSapHetHan);

            btnExportExcelSapHetHan = new Button
            {
                Text = "📊 Xuất Excel",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                Width = 150,
                Height = 65,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnExportExcelSapHetHan.FlatAppearance.BorderSize = 0;
            btnExportExcelSapHetHan.Click += BtnExportExcel_SapHetHan_Click;
            panelSapHetHan.Controls.Add(btnExportExcelSapHetHan);
            tabSapHetHan.Controls.Add(panelSapHetHan);
            tabControl.TabPages.Add(tabSapHetHan);

            return tabControl;
        }

        // THÊM: Xử lý sự kiện click nút xuất Excel
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu
                if (dtDoanhThuData == null || dtDoanhThuData.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Tạo SaveFileDialog
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.Title = "Lưu báo cáo thống kê";
                    sfd.FileName = $"BaoCaoThongKe_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel(sfd.FileName);
                        
                        // Hỏi người dùng có muốn mở file không
                        var result = MessageBox.Show(
                            "Xuất file Excel thành công!\nBạn có muốn mở file ngay không?", 
                            "Thành công", 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý xuất Excel cho tab Xuất Nhập Tồn
        private void BtnExportExcel_XNT_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtXuatNhapTonData == null || dtXuatNhapTonData.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu Xuất Nhập Tồn để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.Title = "Lưu báo cáo Xuất Nhập Tồn";
                    sfd.FileName = $"BaoCaoXuatNhapTon_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportSingleTableToExcel(sfd.FileName, dtXuatNhapTonData, "XUẤT NHẬP TỒN KHO", "XUẤT NHẬP TỒN");
                        var result = MessageBox.Show(
                            "Xuất file Excel thành công!\nBạn có muốn mở file ngay không?",
                            "Thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý xuất Excel cho tab Cảnh báo Hết hạn
        private void BtnExportExcel_SapHetHan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtSapHetHanData == null || dtSapHetHanData.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu Cảnh báo Hết hạn để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.Title = "Lưu báo cáo Cảnh báo Hết hạn";
                    sfd.FileName = $"BaoCaoSapHetHan_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportSingleTableToExcel(sfd.FileName, dtSapHetHanData, "CẢNH BÁO HẾT HẠN", "VACCINE SẮP HẾT HẠN");
                        var result = MessageBox.Show(
                            "Xuất file Excel thành công!\nBạn có muốn mở file ngay không?",
                            "Thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // THAY: Hàm xuất dữ liệu ra Excel sử dụng Interop
        private void ExportToExcel(string filePath)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkBook = null;

            try
            {
                xlApp = new Excel.Application { Visible = false };
                xlWorkBook = xlApp.Workbooks.Add(Type.Missing);

                // ===== SHEET 1: TỔNG QUAN =====
                var wsTongQuan = (Excel.Worksheet)xlWorkBook.Sheets[1];
                wsTongQuan.Name = "Tổng Quan";

                // Tiêu đề
                wsTongQuan.Range["A1"].Value = "BÁO CÁO THỐNG KÊ TIÊM CHỦNG";
                wsTongQuan.Range["A1:D1"].Merge();
                wsTongQuan.Range["A1"].Font.Bold = true;
                wsTongQuan.Range["A1"].Font.Size = 16;

                wsTongQuan.Range["A2"].Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
                wsTongQuan.Range["A2:D2"].Merge();

                // KPI
                wsTongQuan.Range["A4"].Value = "CHỈ SỐ THÁNG NÀY";
                wsTongQuan.Range["A4"].Font.Bold = true;

                int row = 5;
                wsTongQuan.Range[$"A{row}"] .Value = "Doanh thu tháng:";
                wsTongQuan.Range[$"B{row}"] .Value = lblKPI_DoanhThu.Text;
                wsTongQuan.Range[$"B{row}"] .Font.Bold = true;

                row++;
                wsTongQuan.Range[$"A{row}"] .Value = "Lượt tiêm:";
                wsTongQuan.Range[$"B{row}"] .Value = lblKPI_LuotTiem.Text;

                row++;
                wsTongQuan.Range[$"A{row}"] .Value = "Khách hàng mới:";
                wsTongQuan.Range[$"B{row}"] .Value = lblKPI_KhachMoi.Text;

                row++;
                wsTongQuan.Range[$"A{row}"] .Value = "Vaccine sắp hết hạn:";
                wsTongQuan.Range[$"B{row}"] .Value = lblKPI_SapHetHan.Text;

                // ===== SHEET 2: CHI TIẾT DOANH THU =====
                var wsDoanhThu = (Excel.Worksheet)xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                wsDoanhThu.Name = "Chi Tiết Doanh Thu";
                AddDataTableToWorksheetInterop(wsDoanhThu, dtDoanhThuData, "CHI TIẾT DOANH THU");

                // ===== SHEET 3: XUẤT NHẬP TỒN =====
                if (dtXuatNhapTonData != null && dtXuatNhapTonData.Rows.Count > 0)
                {
                    var wsXuatNhapTon = (Excel.Worksheet)xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                    wsXuatNhapTon.Name = "Xuất Nhập Tồn";
                    AddDataTableToWorksheetInterop(wsXuatNhapTon, dtXuatNhapTonData, "XUẤT NHẬP TỒN KHO");
                }

                // ===== SHEET 4: SẮP HẾT HẠN =====
                if (dtSapHetHanData != null && dtSapHetHanData.Rows.Count > 0)
                {
                    var wsSapHetHan = (Excel.Worksheet)xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                    wsSapHetHan.Name = "Cảnh Báo Hết Hạn";
                    AddDataTableToWorksheetInterop(wsSapHetHan, dtSapHetHanData, "VACCINE SẮP HẾT HẠN");
                }

                // Lưu file
                xlWorkBook.SaveAs(Path.GetFullPath(filePath));
            }
            finally
            {
                // Đóng và giải phóng
                if (xlWorkBook != null)
                {
                    xlWorkBook.Close(false);
                    ReleaseComObject(xlWorkBook);
                }

                if (xlApp != null)
                {
                    xlApp.Quit();
                    ReleaseComObject(xlApp);
                }
            }
        }

        // THÊM: Helper để ghi DataTable vào worksheet sử dụng Interop
        private void AddDataTableToWorksheetInterop(Excel.Worksheet ws, DataTable dt, string title)
        {
            if (dt == null) return;

            // Tiêu đề
            ws.Range["A1"].Value = title;
            ws.Range["A1"].Font.Bold = true;
            ws.Range["A1"].Font.Size = 14;
            ws.Range[ws.Cells[1, 1], ws.Cells[1, dt.Columns.Count]].Merge();

            // Header
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                ws.Cells[3, c + 1] = dt.Columns[c].ColumnName;
                var headerRange = ws.Range[ws.Cells[3, c + 1], ws.Cells[3, c + 1]];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                headerRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            }

            // Data
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    var cell = ws.Cells[r + 4, c + 1];
                    var value = dt.Rows[r][c];

                    // Nếu là số, gán giá trị gốc để Excel nhận dạng là số
                    decimal num;
                    if (value != null && decimal.TryParse(value.ToString(), out num) && (dt.Columns[c].ColumnName.ToLower().Contains("tiền") || dt.Columns[c].ColumnName.ToLower().Contains("giá") || dt.Columns[c].DataType == typeof(decimal) || dt.Columns[c].DataType == typeof(double) || dt.Columns[c].DataType == typeof(int)))
                    {
                        cell.Value = num;
                        cell.NumberFormat = "#,##0";
                    }
                    else
                    {
                        cell.Value = value?.ToString();
                    }

                    var dataRange = ws.Range[ws.Cells[r + 4, c + 1], ws.Cells[r + 4, c + 1]];
                    dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }
            }

            // Autofit columns
            ws.Columns.AutoFit();
        }

        private void ReleaseComObject(object obj)
        {
            try
            {
                if (obj != null && Marshal.IsComObject(obj))
                {
                    Marshal.ReleaseComObject(obj);
                }
            }
            catch { }
            finally
            {
                obj = null;
            }
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

        // Export single DataTable to Excel (single sheet)
        private void ExportSingleTableToExcel(string filePath, DataTable dt, string sheetName, string title)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkBook = null;
            try
            {
                xlApp = new Excel.Application { Visible = false };
                xlWorkBook = xlApp.Workbooks.Add(Type.Missing);

                var ws = (Excel.Worksheet)xlWorkBook.Sheets[1];
                ws.Name = sheetName;

                AddDataTableToWorksheetInterop(ws, dt, title);

                xlWorkBook.SaveAs(Path.GetFullPath(filePath));
            }
            finally
            {
                if (xlWorkBook != null)
                {
                    xlWorkBook.Close(false);
                    ReleaseComObject(xlWorkBook);
                }
                if (xlApp != null)
                {
                    xlApp.Quit();
                    ReleaseComObject(xlApp);
                }
            }
        }

        // ========== XỬ LÝ CHUYỂN ĐỔI KHOẢNG THỜI GIAN ==========
        private void CboThoiGian_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataByTimeRange();
        }

        private void LoadDataByTimeRange()
        {
            try
            {
                int selectedIndex = cboThoiGian.SelectedIndex;
                // 0: Hôm nay, 1: 7 ngày gần đây, 2: Tháng này

                // 1. Load KPI theo khoảng thời gian
                var kpi = thongKeBLL.GetKPI(selectedIndex);
                UpdateKPI(kpi.DoanhThu, kpi.LuotTiem, kpi.KhachMoi, kpi.SapHetHan);

                // 2. Load Biểu đồ doanh thu
                LoadDoanhThuChart(selectedIndex);

                // 3. Load Chi tiết doanh thu
                dtDoanhThuData = thongKeBLL.GetDoanhThuChiTiet(selectedIndex);
                UpdateDataGrid("doanhthu", dtDoanhThuData);
                if (dgvDoanhThu.Columns["Tổng Tiền"] != null)
                {
                    dgvDoanhThu.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
                    dgvDoanhThu.Columns["Tổng Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // 4. Load Xuất Nhập Tồn
                dtXuatNhapTonData = thongKeBLL.GetXuatNhapTon(selectedIndex);
                UpdateDataGrid("xuatnhapton", dtXuatNhapTonData);

                // 5. Load Biểu đồ tỷ lệ (không thay đổi theo thời gian)
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

                // 6. Load Vaccine sắp hết hạn (không thay đổi theo thời gian)
                dtSapHetHanData = thongKeBLL.GetVaccineSapHetHan();
                UpdateDataGrid("saphethan", dtSapHetHanData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu thống kê: " + ex.Message);
            }
        }

        private void LoadDoanhThuChart(int timeRange)
        {
            try
            {
                List<double> values = new List<double>();
                List<string> labels = new List<string>();

                if (timeRange == 0) // Hôm nay
                {
                    lblChartTitle.Text = "DOANH THU HÔM NAY (THEO GIỜ)";
                    DataTable dtChart = thongKeBLL.GetDoanhThuHomNay();

                    foreach (DataRow row in dtChart.Rows)
                    {
                        values.Add(Convert.ToDouble(row["TongTien"]));
                        string gio = row["Gio"].ToString();
                        labels.Add(gio + "h");
                    }

                    // Nếu không có dữ liệu, hiển thị biểu đồ trống với các giờ mặc định
                    if (values.Count == 0)
                    {
                        for (int i = 8; i <= 17; i++)
                        {
                            values.Add(0);
                            labels.Add(i + "h");
                        }
                    }
                }
                else if (timeRange == 1) // 7 ngày gần đây
                {
                    lblChartTitle.Text = "DOANH THU 7 NGÀY GẦN NHẤT";
                    DataTable dtChart = thongKeBLL.GetDoanhThu7Ngay();

                    foreach (DataRow row in dtChart.Rows)
                    {
                        values.Add(Convert.ToDouble(row["TongTien"]));
                        DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                        labels.Add(ngay.ToString("dd/MM"));
                    }
                }
                else // Tháng này
                {
                    lblChartTitle.Text = "DOANH THU THÁNG NÀY (THEO NGÀY)";
                    DataTable dtChart = thongKeBLL.GetDoanhThuThangNay();

                    foreach (DataRow row in dtChart.Rows)
                    {
                        values.Add(Convert.ToDouble(row["TongTien"]));
                        DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                        labels.Add(ngay.ToString("dd"));
                    }
                }

                UpdateDoanhThuChart(values.ToArray(), labels.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải biểu đồ doanh thu: " + ex.Message);
            }
        }
    }
}
