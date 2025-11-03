# H??ng d?n chuy?n ??i sang LiveCharts2

## ?? B??c 1: Cài ??t Package

### Cách 1: Visual Studio (KHUY?N NGH?)
1. Right-click project **TPVAXWinform** ? **Manage NuGet Packages**
2. Tab **Browse**, tìm: `LiveChartsCore.SkiaSharpView.WinForms`
3. Click **Install**
4. Accept license

### Cách 2: Package Manager Console
```powershell
Install-Package LiveChartsCore.SkiaSharpView.WinForms -ProjectName TPVAXWinform
```

### Package s? cài ??t:
- ? LiveChartsCore.SkiaSharpView.WinForms (v2.x)
- ? LiveChartsCore (dependencies)
- ? SkiaSharp (rendering engine)
- ? SkiaSharp.Views.WindowsForms

---

## ?? B??c 2: Thay ??i trong Designer

### Chart hi?n t?i:
```csharp
// C? - System.Windows.Forms.DataVisualization.Charting.Chart
private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
```

### LiveCharts2:
```csharp
// M?I - LiveChartsCore.SkiaSharpView.WinForms
private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChart;
private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieChart;
```

---

## ?? B??c 3: Code m?u

### A. Bi?u ?? C?t (Column Chart)

```csharp
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

public partial class frmMain : Form
{
    public frmMain()
    {
        InitializeComponent();
        SetupColumnChart();
  SetupPieChart();
    }

    private void SetupColumnChart()
    {
        // D? li?u m?u
        var monthlyData = new[]
        {
            new { Month = "Tháng 1", Value = 120 },
            new { Month = "Tháng 2", Value = 150 },
         new { Month = "Tháng 3", Value = 200 },
 new { Month = "Tháng 4", Value = 180 },
new { Month = "Tháng 5", Value = 220 },
      new { Month = "Tháng 6", Value = 250 }
        };

        // T?o series
    cartesianChart.Series = new ISeries[]
        {
      new ColumnSeries<int>
          {
     Name = "L??t tiêm",
       Values = monthlyData.Select(x => x.Value).ToArray(),
   Fill = new SolidColorPaint(SKColors.RoyalBlue),
    Stroke = null,
     MaxBarWidth = 50
      }
        };

        // Tùy ch?nh tr?c X
        cartesianChart.XAxes = new Axis[]
    {
       new Axis
         {
     Labels = monthlyData.Select(x => x.Month).ToArray(),
                LabelsRotation = 0,
      LabelsPaint = new SolidColorPaint(SKColors.Black),
      SeparatorsPaint = new SolidColorPaint(SKColors.LightGray)
    }
        };

        // Tùy ch?nh tr?c Y
    cartesianChart.YAxes = new Axis[]
        {
  new Axis
   {
    Name = "S? l??t",
   NamePaint = new SolidColorPaint(SKColors.Black),
        LabelsPaint = new SolidColorPaint(SKColors.Black),
      SeparatorsPaint = new SolidColorPaint(SKColors.LightGray)
            }
  };

        // Title
        cartesianChart.Title = new LiveChartsCore.SkiaSharpView.VisualElements.LabelVisual
        {
        Text = "L??t tiêm ch?ng hàng tháng",
   TextSize = 20,
            Paint = new SolidColorPaint(SKColors.Black)
  };
  }

    private void SetupPieChart()
    {
     // D? li?u m?u
        var vaccineData = new[]
        {
      new { Name = "Vaccine A", Value = 30 },
  new { Name = "Vaccine B", Value = 25 },
            new { Name = "Vaccine C", Value = 20 },
   new { Name = "Vaccine D", Value = 15 },
   new { Name = "Khác", Value = 10 }
        };

  // T?o series
        pieChart.Series = vaccineData.Select(item => 
            new PieSeries<int>
            {
    Name = item.Name,
    Values = new[] { item.Value },
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
              DataLabelsSize = 14,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle
            }
   ).ToArray();

        // Title
        pieChart.Title = new LiveChartsCore.SkiaSharpView.VisualElements.LabelVisual
 {
   Text = "T? l? các lo?i Vaccine",
            TextSize = 20,
   Paint = new SolidColorPaint(SKColors.Black)
   };

        // Legend position
        pieChart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
    }
}
```

---

## ?? B??c 4: Tùy ch?nh màu s?c

### B?ng màu Material Design:

```csharp
// Màu cho Column Chart
var colors = new[]
{
    SKColor.Parse("#2196F3"), // Blue
    SKColor.Parse("#4CAF50"), // Green
    SKColor.Parse("#FF9800"), // Orange
    SKColor.Parse("#F44336"), // Red
    SKColor.Parse("#9C27B0"), // Purple
    SKColor.Parse("#00BCD4")  // Cyan
};

// Áp d?ng
cartesianChart.Series = new ISeries[]
{
    new ColumnSeries<int>
{
        Values = values,
        Fill = new SolidColorPaint(colors[0]),
      Stroke = new SolidColorPaint(colors[0]) { StrokeThickness = 3 }
    }
};
```

---

## ? B??c 5: Animations & Tooltips

### B?t Animation:
```csharp
cartesianChart.AnimationsSpeed = TimeSpan.FromMilliseconds(800);
cartesianChart.EasingFunction = EasingFunctions.BounceOut;
```

### Tooltips:
```csharp
cartesianChart.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Top;
cartesianChart.TooltipBackgroundPaint = new SolidColorPaint(SKColors.White);
cartesianChart.TooltipTextPaint = new SolidColorPaint(SKColors.Black);
```

---

## ?? Các lo?i Chart khác

### Line Chart (Bi?u ?? ???ng):
```csharp
new LineSeries<int>
{
    Values = new[] { 120, 150, 200, 180, 220, 250 },
    Fill = null,
  Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 3 },
    GeometryFill = new SolidColorPaint(SKColors.Blue),
    GeometryStroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 3 },
    GeometrySize = 8
}
```

### Area Chart (Bi?u ?? vùng):
```csharp
new LineSeries<int>
{
    Values = values,
    Fill = new SolidColorPaint(SKColors.Blue.WithAlpha(100)),
    Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
    GeometrySize = 0
}
```

### Stacked Column Chart:
```csharp
cartesianChart.Series = new ISeries[]
{
    new StackedColumnSeries<int>
    {
Values = new[] { 3, 5, 3, 2, 5 },
        Stroke = null,
     Fill = new SolidColorPaint(SKColors.Blue)
    },
    new StackedColumnSeries<int>
    {
        Values = new[] { 4, 2, 3, 2, 3 },
   Stroke = null,
        Fill = new SolidColorPaint(SKColors.Red)
    }
};
```

---

## ?? Real-time Update

```csharp
// ObservableCollection cho real-time update
using System.Collections.ObjectModel;

private ObservableCollection<int> liveData;

public frmMain()
{
    InitializeComponent();
    
    liveData = new ObservableCollection<int> { 0, 0, 0, 0, 0 };
  
    cartesianChart.Series = new ISeries[]
    {
        new LineSeries<int>
     {
   Values = liveData,
GeometrySize = 0
        }
    };
}

// Update data
private void UpdateData()
{
    liveData.Add(random.Next(100, 300));
    if (liveData.Count > 20)
liveData.RemoveAt(0);
}
```

---

## ?? So sánh Chart c? vs LiveCharts2

| Feature | Chart c? | LiveCharts2 |
|---------|----------|-------------|
| Animations | ? Limited | ? Smooth |
| Modern Design | ? Old | ? Beautiful |
| Real-time | ? Difficult | ? Easy |
| Performance | ??? | ????? |
| Tooltips | ? Basic | ? Advanced |
| Customization | ??? | ????? |
| Touch Support | ? | ? |

---

## ?? Checklist Migration

- [ ] Cài ??t package LiveChartsCore.SkiaSharpView.WinForms
- [ ] Thay ??i type trong Designer.cs
- [ ] Update InitializeComponent()
- [ ] Chuy?n ??i data sang LiveCharts format
- [ ] Test hi?n th?
- [ ] Thêm animations
- [ ] Tùy ch?nh màu s?c
- [ ] Test trên nhi?u màn hình DPI

---

## ?? B??c ti?p theo

Sau khi cài ??t package xong, tôi s?:
1. ? T?o file code m?u hoàn ch?nh
2. ? Update Designer.cs
3. ? Update frmMain.cs
4. ? Test và verify

Hãy cài package và cho tôi bi?t khi nào xong nhé! ??
