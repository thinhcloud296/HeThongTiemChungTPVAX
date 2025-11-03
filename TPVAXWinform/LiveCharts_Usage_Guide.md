# H??ng d?n s? d?ng LiveCharts trong TPVAXWinform

## ? ?ã hoàn thành

B?n ?ã tích h?p thành công **LiveCharts.WinForms** vào d? án!

---

## ?? Charts hi?n t?i

### 1. **Column Chart** (Bi?u ?? c?t)
- Hi?n th? l??t tiêm ch?ng theo tháng
- Màu xanh d??ng (RoyalBlue)
- Hi?n th? giá tr? trên t?ng c?t
- V? trí: C?t 1-2, dòng 2 c?a tableLayoutPanel2

### 2. **Pie Chart** (Bi?u ?? tròn)
- Hi?n th? t? l? các lo?i vaccine
- 5 màu khác nhau cho 5 lo?i
- Hi?n th? % và giá tr?
- Donut chart (InnerRadius = 20)
- V? trí: C?t 3, dòng 2 c?a tableLayoutPanel2

---

## ?? Tùy ch?nh màu s?c

### Column Chart
```csharp
new ColumnSeries
{
    Fill = System.Windows.Media.Brushes.RoyalBlue, // Màu xanh
    // Ho?c custom color:
    Fill = new System.Windows.Media.SolidColorBrush(
        System.Windows.Media.Color.FromRgb(41, 128, 185)
    )
}
```

### Pie Chart
```csharp
new PieSeries
{
    Fill = System.Windows.Media.Brushes.Blue, // Ho?c màu khác
    // Available colors:
    // - Red, Green, Blue, Orange, Purple, Yellow
    // - Cyan, Magenta, Pink, Brown, Gray
}
```

---

## ?? C?p nh?t d? li?u ??ng

### Cách 1: Thay ??i Values tr?c ti?p
```csharp
var columnChart = (LiveCharts.WinForms.CartesianChart)tableLayoutPanel2.Controls
    .OfType<LiveCharts.WinForms.CartesianChart>().FirstOrDefault();

if (columnChart != null)
{
    var series = columnChart.Series[0].Values;
    series.Clear();
    series.Add(100);
    series.Add(150);
    series.Add(200);
    // Chart t? ??ng update!
}
```

### Cách 2: S? d?ng ChartValues Observable
```csharp
// Khai báo ? class level
private ChartValues<double> vaccinationData = new ChartValues<double>();

// Trong constructor
vaccinationData.AddRange(new[] { 120.0, 150.0, 200.0, 180.0, 220.0, 250.0 });

// Trong SetupLiveCharts()
new ColumnSeries
{
    Values = vaccinationData // Bind vào collection
}

// Update data anywhere:
vaccinationData.Add(300); // T? ??ng refresh chart!
vaccinationData[0] = 130; // Update giá tr? c?
```

---

## ?? Các lo?i Chart khác

### Line Chart (Bi?u ?? ???ng)
```csharp
new LineSeries
{
    Title = "Xu h??ng",
    Values = new ChartValues<double> { 120, 150, 200, 180, 220, 250 },
    Fill = System.Windows.Media.Brushes.Transparent, // Không fill
    Stroke = System.Windows.Media.Brushes.Blue, // Màu ???ng
  StrokeThickness = 3,
    PointGeometry = DefaultGeometries.Circle,
    PointGeometrySize = 10
}
```

### Area Chart (Bi?u ?? vùng)
```csharp
new LineSeries
{
    Title = "Di?n tích",
    Values = new ChartValues<double> { 120, 150, 200, 180, 220, 250 },
    Fill = System.Windows.Media.Brushes.LightBlue, // Fill màu
    Stroke = System.Windows.Media.Brushes.Blue,
    StrokeThickness = 2,
  PointGeometrySize = 0 // Không hi?n ?i?m
}
```

### Stacked Column Chart
```csharp
Series = new SeriesCollection
{
    new StackedColumnSeries
    {
        Title = "Nam",
        Values = new ChartValues<double> { 60, 70, 90, 80, 100, 120 }
    },
    new StackedColumnSeries
 {
    Title = "N?",
        Values = new ChartValues<double> { 60, 80, 110, 100, 120, 130 }
    }
}
```

---

##  Tooltips & Formatting

### Custom Tooltip
```csharp
new ColumnSeries
{
    LabelPoint = point => $"{point.Y:N0} l??t", // Format: "150 l??t"
    DataLabels = true
}
```

### Custom Axis Format
```csharp
AxisY = new AxesCollection
{
    new Axis
    {
        Title = "S? l??t tiêm",
        LabelFormatter = value => value.ToString("N0"), // 1,000
        MinValue = 0, // Giá tr? min
     MaxValue = 300 // Giá tr? max (optional)
    }
}
```

---

## ?? Animation

LiveCharts t? ??ng có animation m??t mà! ?? tùy ch?nh:

```csharp
// T?t animation
LiveCharts.Charting.For<System.Windows.Forms.Control>().DisableAnimations = true;

// Ho?c ?i?u ch?nh t?c ??
var columnChart = new LiveCharts.WinForms.CartesianChart
{
    AnimationsSpeed = TimeSpan.FromMilliseconds(500) // 0.5 giây
};
```

---

## ?? K?t n?i Database

### Ví d?: Load data t? SQL
```csharp
private async Task LoadChartDataFromDatabase()
{
 var data = await GetVaccinationDataFromDB();
    
    var columnChart = (LiveCharts.WinForms.CartesianChart)tableLayoutPanel2.Controls
        .OfType<LiveCharts.WinForms.CartesianChart>().FirstOrDefault();
    
    if (columnChart != null)
    {
        var series = (ColumnSeries)columnChart.Series[0];
        series.Values.Clear();
        
        foreach (var item in data)
        {
            series.Values.Add(item.Count);
    }
    }
}

private async Task<List<VaccinationData>> GetVaccinationDataFromDB()
{
    // Your database logic here
    return new List<VaccinationData>();
}
```

---

## ?? Themes & Styling

### Dark Theme
```csharp
var columnChart = new LiveCharts.WinForms.CartesianChart
{
    BackColor = Color.FromArgb(45, 45, 48), // Dark background
    ForeColor = Color.White
};

// Axis colors
AxisX = new AxesCollection
{
    new Axis
    {
        Foreground = System.Windows.Media.Brushes.White
    }
}
```

### Gradient Fill
```csharp
new ColumnSeries
{
    Fill = new System.Windows.Media.LinearGradientBrush
    {
        StartPoint = new System.Windows.Point(0, 0),
        EndPoint = new System.Windows.Point(0, 1),
 GradientStops = new System.Windows.Media.GradientStopCollection
        {
new System.Windows.Media.GradientStop(
      System.Windows.Media.Colors.Blue, 0),
  new System.Windows.Media.GradientStop(
System.Windows.Media.Colors.LightBlue, 1)
    }
    }
}
```

---

## ?? Best Practices

### 1. **Performance v?i d? li?u l?n**
```csharp
// S? d?ng ChartValues thay vì List
private ChartValues<double> data = new ChartValues<double>();

// Batch update
data.AddRange(largeDataSet); // Thay vì nhi?u l?n Add()
```

### 2. **Memory Management**
```csharp
// Dispose charts khi không dùng
protected override void OnFormClosing(FormClosingEventArgs e)
{
    columnChart?.Dispose();
  pieChart?.Dispose();
    base.OnFormClosing(e);
}
```

### 3. **Thread-Safe Updates**
```csharp
private void UpdateChartFromBackgroundThread(double[] newData)
{
    if (this.InvokeRequired)
    {
        this.Invoke(new Action(() => UpdateChartFromBackgroundThread(newData)));
        return;
    }
    
    // Safe to update UI here
    var series = columnChart.Series[0].Values;
    series.Clear();
    series.AddRange(newData);
}
```

---

## ?? Ví d? th?c t? cho h? th?ng tiêm ch?ng

### Dashboard t?ng quan
```csharp
private void LoadDashboardData()
{
    // Load t? database
    var monthlyVaccinations = GetMonthlyVaccinations();
    var vaccineTypes = GetVaccineTypeDistribution();
    
    // Update Column Chart
    UpdateColumnChart(monthlyVaccinations);
    
    // Update Pie Chart
    UpdatePieChart(vaccineTypes);
}

private void UpdateColumnChart(Dictionary<string, int> data)
{
    var chart = (LiveCharts.WinForms.CartesianChart)
        tableLayoutPanel2.Controls.OfType<LiveCharts.WinForms.CartesianChart>()
        .FirstOrDefault();
    
    if (chart != null)
    {
 var series = (ColumnSeries)chart.Series[0];
     series.Values.Clear();
        
        foreach (var item in data.Values)
      {
        series.Values.Add(item);
    }
        
        // Update labels
        chart.AxisX[0].Labels = data.Keys.ToArray();
    }
}
```

---

## ?? Tài li?u tham kh?o

- **GitHub**: https://github.com/Live-Charts/Live-Charts
- **Documentation**: https://lvcharts.net/
- **Examples**: https://lvcharts.net/App/examples/v1/start

---

## ?? Troubleshooting

### L?i: Chart không hi?n th?
- Ki?m tra `Dock = DockStyle.Fill`
- Ki?m tra `Visible = true`
- Ki?m tra parent container có size > 0

### L?i: Data không update
- S? d?ng `ChartValues<T>` thay vì `List<T>`
- G?i `chart.Update(true)` n?u c?n force update

### L?i: Namespace conflict
- Dùng `LiveCharts.WinForms.CartesianChart` thay vì `CartesianChart`
- Dùng `LiveCharts.Wpf.ColumnSeries` cho series

---

## ?? K?t lu?n

B?n ?ã tích h?p thành công LiveCharts! Bi?u ?? gi? ?ây:
- ? ??p h?n và hi?n ??i
- ? Smooth animations
- ? Easy to update
- ? Highly customizable
- ? Better performance

Chúc b?n code vui v?! ??
