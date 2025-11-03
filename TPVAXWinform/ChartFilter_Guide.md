# H??ng d?n s? d?ng ph?n tra c?u bi?u ??

## ? Các thay ??i ?ã th?c hi?n

### 1. **Layout m?i**
```
???????????????????????????????????????????????????
?  KPI Cards (3 panels - 2 columns)    ?
?  - L??t tiêm hôm nay             ?
?  - Khách hàng trong tu?n ?
?  - Vaccine trong kho        ?
???????????????????????????????????????????????????
?  Filter Panel (Full width)            ?
?  [T? ngày] [??n ngày] [Lo?i bi?u ??] [L?c] [Reset] ?
???????????????????????????????????????????????????
?   Column Chart       ?     Pie Chart   ?
?(50% width)    ?     (50% width)          ?
?    ?           ?
?          ?            ?
???????????????????????????????????????????????????
```

### 2. **C?u trúc TableLayoutPanel m?i**
- **2 c?t**: M?i c?t 50% width
- **4 rows**:
  - Row 1: KPI Cards (150px) - span 2 columns
  - Row 2: Vaccine panel (150px) - ? c?t 1
  - Row 3: Filter Panel (80px) - span 2 columns
  - Row 4: Charts (auto height) - m?i chart 1 c?t

---

## ?? Ph?n Tra c?u (Filter Panel)

### Controls

#### 1. **T? ngày (dtpFromDate)**
- Type: DateTimePicker
- Format: Short date (dd/MM/yyyy)
- M?c ??nh: 6 tháng tr??c
- Location: Left side

#### 2. **??n ngày (dtpToDate)**
- Type: DateTimePicker
- Format: Short date (dd/MM/yyyy)
- M?c ??nh: Hôm nay
- Location: Middle

#### 3. **Lo?i bi?u ?? (cboChartType)**
- Type: ComboBox
- Style: DropDownList
- Options:
  - Theo ngày
  - Theo tu?n
  - **Theo tháng** (m?c ??nh)
  - Theo n?m

#### 4. **Button L?c (btnFilter)**
- Color: #2980b9 (Blue)
- Text: "L?c"
- Action: L?c d? li?u theo date range và chart type

#### 5. **Button Reset (btnReset)**
- Color: #95a5a6 (Gray)
- Text: "Reset"
- Action: Reset v? giá tr? m?c ??nh

---

## ?? Charts Layout

### Column Chart (chart1)
- **Width**: 50% (698px)
- **Height**: Auto (432px)
- **Position**: Left column
- **Title**: "L??t tiêm ch?ng hàng tháng"
- **Dock**: Fill

### Pie Chart (chart2)
- **Width**: 50% (698px)
- **Height**: Auto (432px)
- **Position**: Right column
- **Title**: "T? l? các lo?i Vaccine"
- **Dock**: Fill

---

## ?? Code Usage

### Kh?i t?o
```csharp
private void InitializeFilters()
{
    // Set default dates (6 months ago to now)
    dtpFromDate.Value = DateTime.Now.AddMonths(-6);
    dtpToDate.Value = DateTime.Now;
    
    // Set default chart type
    cboChartType.SelectedIndex = 2; // "Theo tháng"
    
    // Wire up event handlers
    btnFilter.Click += BtnFilter_Click;
    btnReset.Click += BtnReset_Click;
}
```

### X? lý Filter
```csharp
private void BtnFilter_Click(object sender, EventArgs e)
{
// Validate dates
    if (dtpFromDate.Value > dtpToDate.Value)
    {
        MessageBox.Show("Ngày b?t ??u ph?i nh? h?n ngày k?t thúc!", 
            "L?i", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 return;
    }
    
    // Get selected chart type
    string chartType = cboChartType.SelectedItem.ToString();
    
    // Load data theo filter
    LoadChartData(dtpFromDate.Value, dtpToDate.Value, chartType);
}
```

### Load Data theo Filter
```csharp
private void LoadChartData(DateTime fromDate, DateTime toDate, string chartType)
{
    var columnChart = (LiveCharts.WinForms.CartesianChart)
        tableLayoutPanel2.Controls.OfType<LiveCharts.WinForms.CartesianChart>()
    .FirstOrDefault();
    
    if (columnChart != null)
    {
        var series = (ColumnSeries)columnChart.Series[0];
        series.Values.Clear();
        
        // TODO: Query database
        var data = GetVaccinationData(fromDate, toDate, chartType);
        
  foreach (var item in data)
        {
     series.Values.Add(item.Count);
   }
        
        // Update X-axis labels
        columnChart.AxisX[0].Labels = data.Select(x => x.Label).ToArray();
    }
}
```

---

## ??? K?t n?i Database

### Ví d? v?i SQL Server

```csharp
private List<ChartDataPoint> GetVaccinationData(DateTime fromDate, DateTime toDate, string chartType)
{
 var data = new List<ChartDataPoint>();
    
    using (var connection = new SqlConnection(connectionString))
  {
  string query = BuildQuery(chartType);
        
     using (var command = new SqlCommand(query, connection))
      {
   command.Parameters.AddWithValue("@FromDate", fromDate);
       command.Parameters.AddWithValue("@ToDate", toDate);
      
connection.Open();
       
     using (var reader = command.ExecuteReader())
  {
                while (reader.Read())
      {
           data.Add(new ChartDataPoint
         {
   Label = reader["Label"].ToString(),
     Count = Convert.ToInt32(reader["Count"])
         });
         }
            }
        }
    }
    
    return data;
}

private string BuildQuery(string chartType)
{
    switch (chartType)
    {
      case "Theo ngày":
   return @"
      SELECT CONVERT(VARCHAR(10), NgayTiem, 103) AS Label, 
    COUNT(*) AS Count
  FROM LichTiemChung
        WHERE NgayTiem BETWEEN @FromDate AND @ToDate
  GROUP BY CONVERT(VARCHAR(10), NgayTiem, 103)
                ORDER BY CONVERT(DATE, NgayTiem)";
      
        case "Theo tu?n":
   return @"
                SELECT 'Tu?n ' + CAST(DATEPART(WEEK, NgayTiem) AS VARCHAR) AS Label,
     COUNT(*) AS Count
           FROM LichTiemChung
           WHERE NgayTiem BETWEEN @FromDate AND @ToDate
           GROUP BY DATEPART(WEEK, NgayTiem)
    ORDER BY DATEPART(WEEK, NgayTiem)";
      
        case "Theo tháng":
 return @"
        SELECT 'Tháng ' + CAST(MONTH(NgayTiem) AS VARCHAR) AS Label,
   COUNT(*) AS Count
    FROM LichTiemChung
  WHERE NgayTiem BETWEEN @FromDate AND @ToDate
   GROUP BY MONTH(NgayTiem)
          ORDER BY MONTH(NgayTiem)";
        
        case "Theo n?m":
            return @"
         SELECT CAST(YEAR(NgayTiem) AS VARCHAR) AS Label,
       COUNT(*) AS Count
  FROM LichTiemChung
                WHERE NgayTiem BETWEEN @FromDate AND @ToDate
                GROUP BY YEAR(NgayTiem)
          ORDER BY YEAR(NgayTiem)";
    
        default:
       return string.Empty;
    }
}

// Data model
public class ChartDataPoint
{
    public string Label { get; set; }
    public int Count { get; set; }
}
```

---

## ?? Styling

### Filter Panel
```csharp
panelChartFilter.BackColor = Color.White;
panelChartFilter.Padding = new Padding(10);
```

### Buttons
```csharp
// Filter button
btnFilter.BackColor = Color.FromArgb(41, 128, 185); // Blue
btnFilter.ForeColor = Color.White;
btnFilter.FlatStyle = FlatStyle.Flat;

// Reset button
btnReset.BackColor = Color.FromArgb(149, 165, 166); // Gray
btnReset.ForeColor = Color.White;
btnReset.FlatStyle = FlatStyle.Flat;
```

### DateTimePickers
```csharp
dtpFromDate.Format = DateTimePickerFormat.Short;
dtpToDate.Format = DateTimePickerFormat.Short;
```

---

## ?? Features Nâng cao

### 1. **Export Chart to Image**
```csharp
private void ExportChart(Chart chart, string filename)
{
 chart.SaveImage(filename, ChartImageFormat.Png);
}
```

### 2. **Print Chart**
```csharp
private void PrintChart(Chart chart)
{
    chart.Printing.PrintDocument.Print();
}
```

### 3. **Auto-refresh**
```csharp
private Timer refreshTimer;

private void SetupAutoRefresh()
{
    refreshTimer = new Timer();
    refreshTimer.Interval = 60000; // 1 minute
  refreshTimer.Tick += (s, e) => LoadChartData(dtpFromDate.Value, dtpToDate.Value, cboChartType.Text);
    refreshTimer.Start();
}
```

### 4. **Custom Date Ranges**
```csharp
private void AddQuickFilters()
{
    var btnToday = new Button { Text = "Hôm nay" };
    btnToday.Click += (s, e) => 
{
        dtpFromDate.Value = DateTime.Today;
        dtpToDate.Value = DateTime.Today;
        BtnFilter_Click(s, e);
    };
 
    var btnThisWeek = new Button { Text = "Tu?n này" };
    btnThisWeek.Click += (s, e) => 
    {
   dtpFromDate.Value = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
dtpToDate.Value = DateTime.Today;
    BtnFilter_Click(s, e);
    };
    
    var btnThisMonth = new Button { Text = "Tháng này" };
    btnThisMonth.Click += (s, e) => 
    {
        dtpFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        dtpToDate.Value = DateTime.Today;
        BtnFilter_Click(s, e);
    };
}
```

---

## ?? Validation Rules

### Date Validation
```csharp
private bool ValidateDateRange()
{
    if (dtpFromDate.Value > dtpToDate.Value)
    {
 MessageBox.Show("Ngày b?t ??u ph?i nh? h?n ngày k?t thúc!");
        return false;
    }
    
    if ((dtpToDate.Value - dtpFromDate.Value).TotalDays > 365)
    {
MessageBox.Show("Kho?ng th?i gian không ???c v??t quá 1 n?m!");
        return false;
    }
    
    return true;
}
```

---

## ?? Best Practices

1. **Loading Indicator**: Hi?n th? loading khi ?ang load data
```csharp
private async void BtnFilter_Click(object sender, EventArgs e)
{
    btnFilter.Enabled = false;
    btnFilter.Text = "?ang t?i...";
    
    await Task.Run(() => LoadChartData(...));
    
    btnFilter.Text = "L?c";
    btnFilter.Enabled = true;
}
```

2. **Cache Data**: Cache d? li?u ?ã load ?? tránh query l?i
```csharp
private Dictionary<string, List<ChartDataPoint>> dataCache = new Dictionary<string, List<ChartDataPoint>>();
```

3. **Error Handling**: X? lý l?i khi query database
```csharp
try
{
    LoadChartData(...);
}
catch (Exception ex)
{
    MessageBox.Show($"L?i khi t?i d? li?u: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

---

## ?? K?t lu?n

B?n ?ã có:
- ? Layout 2 charts cân ??i (50%-50%)
- ? Ph?n tra c?u ??y ?? (date range + chart type)
- ? Buttons ?? filter và reset
- ? Code structure s?n sàng k?t n?i database
- ? Validation và error handling

**Next steps**:
1. K?t n?i database th?c t?
2. Implement query logic
3. Thêm loading indicator
4. Thêm export/print features
5. Optimize performance v?i caching

Chúc b?n code vui v?! ??
