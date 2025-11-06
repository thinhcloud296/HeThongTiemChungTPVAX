# H??ng d?n s? d?ng Search Panels

## ? Layout m?i (?ã s?a)

```
???????????????????????????????????????????????
?  L??t tiêm hôm nay   ? Khách hàng trong tu?n?
?      (Panel 4)       ?    (Panel 5)       ?
???????????????????????????????????????????????
?          Vaccine trong kho (Panel 6)        ?
? [?? Tìm b?nh nhân: ________] [??Tìm] [Reset]?
???????????????????????????????????????????????
?   [?? Tìm vaccine:] ? ?
?   [________]    ?           ?
?   [??Tìm] [Reset]   ?        ?
???????????????????????????????????????????????
?   Column Chart       ?     Pie Chart        ?
?   (50% width)        ?   (50% width)      ?
?            ?       ?
?         ?          ?
???????????????????????????????????????????????
```

## ?? Layout Structure

### TableLayoutPanel2
- **2 c?t**: M?i c?t 50% width
- **3 rows**:
  - Row 1 (150px): KPI Cards - Panel 4 (col 1), Panel 5 (col 2)
  - Row 2 (80px): Search Panels - Panel 6 (col 1), panelChartFilter (col 2)
  - Row 3 (Auto): Charts - chart1 (col 1), chart2 (col 2)

---

## ?? Panel 6 - Tìm ki?m b?nh nhân

### Controls

#### 1. **Label (label14)**
- Text: "?? Tìm b?nh nhân:"
- Font: Segoe UI, 10pt, Bold
- Color: #2980b9 (Blue)

#### 2. **TextBox (txtSearchPatient)**
- Font: Segoe UI, 10pt
- Width: 230px
- Purpose: Nh?p tên ho?c mã b?nh nhân

#### 3. **Button Search (btnSearchPatient)**
- Text: "?? Tìm"
- BackColor: #2980b9 (Blue)
- ForeColor: White
- Size: 100x35px
- Action: Tìm ki?m b?nh nhân

#### 4. **Button Reset (btnResetPatient)**
- Text: "?? Reset"
- BackColor: #95a5a6 (Gray)
- ForeColor: White
- Size: 100x35px
- Action: Xóa tìm ki?m và reset

---

## ?? panelChartFilter - Tìm ki?m vaccine

### Controls

#### 1. **Label (label17)**
- Text: "?? Tìm vaccine:"
- Font: Segoe UI, 10pt, Bold
- Color: #2980b9 (Blue)

#### 2. **TextBox (txtSearchVaccine)**
- Font: Segoe UI, 10pt
- Width: 260px
- Purpose: Nh?p tên vaccine

#### 3. **Button Search (btnFilter)**
- Text: "?? Tìm"
- BackColor: #2980b9 (Blue)
- ForeColor: White
- Size: 100x35px
- Action: Tìm ki?m vaccine

#### 4. **Button Reset (btnReset)**
- Text: "?? Reset"
- BackColor: #95a5a6 (Gray)
- ForeColor: White
- Size: 100x35px
- Action: Xóa tìm ki?m và reset

---

## ?? Code Implementation

### Kh?i t?o Event Handlers

```csharp
private void InitializeSearchHandlers()
{
    // Tìm ki?m b?nh nhân
    btnSearchPatient.Click += BtnSearchPatient_Click;
    btnResetPatient.Click += BtnResetPatient_Click;
    txtSearchPatient.KeyPress += TxtSearchPatient_KeyPress; // Enter to search
    
    // Tìm ki?m vaccine
    btnFilter.Click += BtnSearchVaccine_Click;
    btnReset.Click += BtnResetVaccine_Click;
    txtSearchVaccine.KeyPress += TxtSearchVaccine_KeyPress; // Enter to search
}
```

### Enter Key ?? tìm ki?m

```csharp
private void TxtSearchPatient_KeyPress(object sender, KeyPressEventArgs e)
{
 if (e.KeyChar == (char)Keys.Enter)
    {
        BtnSearchPatient_Click(sender, e);
        e.Handled = true; // Prevent beep sound
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
```

### Tìm ki?m B?nh nhân

```csharp
private void BtnSearchPatient_Click(object sender, EventArgs e)
{
    string searchText = txtSearchPatient.Text.Trim();
    
    // Validate
    if (string.IsNullOrEmpty(searchText))
    {
        MessageBox.Show("Vui lòng nh?p tên ho?c mã b?nh nhân!", 
            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    txtSearchPatient.Focus();
        return;
    }
    
    // Search in database
    var patients = SearchPatients(searchText);
    
    if (patients.Count == 0)
    {
     MessageBox.Show($"Không tìm th?y b?nh nhân: {searchText}", 
            "K?t qu?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }
    
    // Update chart with filtered data
    UpdateChartWithPatients(patients);
}

private List<Patient> SearchPatients(string keyword)
{
    var patients = new List<Patient>();
    
  using (var connection = new SqlConnection(connectionString))
    {
   string query = @"
          SELECT * FROM BenhNhan 
     WHERE HoTen LIKE @Keyword 
               OR MaBenhNhan LIKE @Keyword
            ORDER BY HoTen";
        
        using (var command = new SqlCommand(query, connection))
        {
   command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

     connection.Open();
   using (var reader = command.ExecuteReader())
            {
   while (reader.Read())
              {
          patients.Add(new Patient
   {
       MaBenhNhan = reader["MaBenhNhan"].ToString(),
             HoTen = reader["HoTen"].ToString(),
       NgaySinh = Convert.ToDateTime(reader["NgaySinh"])
     });
      }
 }
        }
    }
    
    return patients;
}
```

### Reset B?nh nhân

```csharp
private void BtnResetPatient_Click(object sender, EventArgs e)
{
 txtSearchPatient.Clear();
    txtSearchPatient.Focus();
    
    // Reload default chart data
    SetupLiveCharts();
}
```

### Tìm ki?m Vaccine

```csharp
private void BtnSearchVaccine_Click(object sender, EventArgs e)
{
    string searchText = txtSearchVaccine.Text.Trim();
    
    // Validate
    if (string.IsNullOrEmpty(searchText))
    {
        MessageBox.Show("Vui lòng nh?p tên vaccine!", 
"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
  txtSearchVaccine.Focus();
        return;
    }
    
    // Search in database
    var vaccines = SearchVaccines(searchText);
  
    if (vaccines.Count == 0)
    {
    MessageBox.Show($"Không tìm th?y vaccine: {searchText}", 
      "K?t qu?", MessageBoxButtons.OK, MessageBoxIcon.Information);
      return;
    }
    
    // Update pie chart with filtered data
    UpdatePieChartWithVaccines(vaccines);
}

private List<Vaccine> SearchVaccines(string keyword)
{
    var vaccines = new List<Vaccine>();
    
    using (var connection = new SqlConnection(connectionString))
    {
   string query = @"
         SELECT v.*, COUNT(lt.MaLichTiem) as SoLuotTiem
            FROM Vaccine v
            LEFT JOIN LichTiemChung lt ON v.MaVaccine = lt.MaVaccine
       WHERE v.TenVaccine LIKE @Keyword
            GROUP BY v.MaVaccine, v.TenVaccine, v.XuatXu, v.GiaTien
 ORDER BY SoLuotTiem DESC";
        
        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
      
       connection.Open();
            using (var reader = command.ExecuteReader())
  {
           while (reader.Read())
          {
         vaccines.Add(new Vaccine
       {
               MaVaccine = reader["MaVaccine"].ToString(),
        TenVaccine = reader["TenVaccine"].ToString(),
             SoLuotTiem = Convert.ToInt32(reader["SoLuotTiem"])
       });
     }
            }
   }
    }
    
    return vaccines;
}
```

### Reset Vaccine

```csharp
private void BtnResetVaccine_Click(object sender, EventArgs e)
{
    txtSearchVaccine.Clear();
    txtSearchVaccine.Focus();
    
    // Reload default chart data
  SetupLiveCharts();
}
```

---

## ?? Update Charts v?i d? li?u tìm ki?m

### Update Column Chart (B?nh nhân)

```csharp
private void UpdateChartWithPatients(List<Patient> patients)
{
    var columnChart = (LiveCharts.WinForms.CartesianChart)
   tableLayoutPanel2.Controls.OfType<LiveCharts.WinForms.CartesianChart>()
        .FirstOrDefault();
    
    if (columnChart != null)
    {
    var series = (ColumnSeries)columnChart.Series[0];
        series.Values.Clear();
        
        // Group by month
        var monthlyData = patients
 .GroupBy(p => p.NgaySinh.Month)
  .Select(g => new { Month = g.Key, Count = g.Count() })
     .OrderBy(x => x.Month)
            .ToList();
        
 foreach (var item in monthlyData)
      {
         series.Values.Add(item.Count);
        }
        
    // Update labels
  columnChart.AxisX[0].Labels = monthlyData
 .Select(x => $"Tháng {x.Month}")
        .ToArray();
  
        // Update title
  columnChart.Series[0].Title = $"K?t qu?: {patients.Count} b?nh nhân";
    }
}
```

### Update Pie Chart (Vaccine)

```csharp
private void UpdatePieChartWithVaccines(List<Vaccine> vaccines)
{
    var pieChart = (LiveCharts.WinForms.PieChart)
      tableLayoutPanel2.Controls.OfType<LiveCharts.WinForms.PieChart>()
        .FirstOrDefault();
    
  if (pieChart != null)
    {
  pieChart.Series.Clear();
        
   foreach (var vaccine in vaccines)
        {
            pieChart.Series.Add(new PieSeries
            {
      Title = vaccine.TenVaccine,
           Values = new ChartValues<int> { vaccine.SoLuotTiem },
         DataLabels = true,
          LabelPoint = point => $"{vaccine.TenVaccine}\n{point.Y} ({point.Participation:P0})"
            });
        }
    }
}
```

---

## ?? Styling

### TextBox Style
```csharp
txtSearchPatient.Font = new Font("Segoe UI", 10F);
txtSearchPatient.BorderStyle = BorderStyle.FixedSingle;

// Thêm ForeColor khi focus
txtSearchPatient.Enter += (s, e) => txtSearchPatient.BackColor = Color.FromArgb(240, 248, 255);
txtSearchPatient.Leave += (s, e) => txtSearchPatient.BackColor = Color.White;
```

### Button Hover Effect
```csharp
btnSearchPatient.MouseEnter += (s, e) => 
{
    btnSearchPatient.BackColor = Color.FromArgb(52, 152, 219); // Lighter blue
};

btnSearchPatient.MouseLeave += (s, e) => 
{
    btnSearchPatient.BackColor = Color.FromArgb(41, 128, 185); // Original blue
};
```

---

## ?? Features Nâng cao

### 1. **Auto-complete cho TextBox**

```csharp
private AutoCompleteStringCollection GetPatientNames()
{
    var collection = new AutoCompleteStringCollection();
    
    // Load t? database
    var patients = LoadAllPatients();
    collection.AddRange(patients.Select(p => p.HoTen).ToArray());
    
    return collection;
}

// Setup
txtSearchPatient.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
txtSearchPatient.AutoCompleteSource = AutoCompleteSource.CustomSource;
txtSearchPatient.AutoCompleteCustomSource = GetPatientNames();
```

### 2. **Loading Indicator**

```csharp
private async void BtnSearchPatient_Click(object sender, EventArgs e)
{
    btnSearchPatient.Enabled = false;
    btnSearchPatient.Text = "? ?ang tìm...";
 
    var searchText = txtSearchPatient.Text.Trim();
    var patients = await Task.Run(() => SearchPatients(searchText));
 
    UpdateChartWithPatients(patients);
    
    btnSearchPatient.Text = "?? Tìm";
    btnSearchPatient.Enabled = true;
}
```

### 3. **Search History**

```csharp
private List<string> searchHistory = new List<string>();

private void SaveSearchHistory(string keyword)
{
    if (!searchHistory.Contains(keyword))
    {
     searchHistory.Insert(0, keyword);
        if (searchHistory.Count > 10)
     searchHistory.RemoveAt(10);
    }
}

// Hi?n th? history trong context menu
private void ShowSearchHistory()
{
    var contextMenu = new ContextMenuStrip();
    
    foreach (var item in searchHistory)
    {
contextMenu.Items.Add(item, null, (s, e) => 
        {
   txtSearchPatient.Text = item;
            BtnSearchPatient_Click(s, e);
    });
    }
    
    txtSearchPatient.ContextMenuStrip = contextMenu;
}
```

---

## ?? Best Practices

### 1. **Input Validation**
```csharp
private bool ValidateSearchInput(string input)
{
    // Minimum length
    if (input.Length < 2)
    {
        MessageBox.Show("Vui lòng nh?p ít nh?t 2 ký t?!");
        return false;
    }
    
    // No special characters
    if (Regex.IsMatch(input, @"[<>{}[\]\\\/]"))
    {
        MessageBox.Show("Không ???c ch?a ký t? ??c bi?t!");
  return false;
    }
    
    return true;
}
```

### 2. **Error Handling**
```csharp
try
{
    var patients = SearchPatients(searchText);
    UpdateChartWithPatients(patients);
}
catch (SqlException ex)
{
    MessageBox.Show($"L?i database: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
catch (Exception ex)
{
    MessageBox.Show($"L?i không xác ??nh: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

### 3. **Performance Optimization**
```csharp
// Cache results
private Dictionary<string, List<Patient>> searchCache = new Dictionary<string, List<Patient>>();

private List<Patient> SearchPatients(string keyword)
{
    if (searchCache.ContainsKey(keyword))
        return searchCache[keyword];
    
    var patients = SearchFromDatabase(keyword);
    searchCache[keyword] = patients;
    
    return patients;
}
```

---

## ?? Data Models

```csharp
public class Patient
{
    public string MaBenhNhan { get; set; }
    public string HoTen { get; set; }
    public DateTime NgaySinh { get; set; }
    public string SoDienThoai { get; set; }
 public string DiaChi { get; set; }
}

public class Vaccine
{
    public string MaVaccine { get; set; }
    public string TenVaccine { get; set; }
    public string XuatXu { get; set; }
    public decimal GiaTien { get; set; }
    public int SoLuotTiem { get; set; }
}
```

---

## ?? K?t lu?n

B?n ?ã có:
- ? 2 charts n?m cùng 1 hàng (50% - 50%)
- ? Panel tìm ki?m b?nh nhân (bên trái)
- ? Panel tìm ki?m vaccine (bên ph?i)
- ? Enter key ?? tìm ki?m nhanh
- ? Validation và error handling
- ? Reset functionality
- ? Ready ?? k?t n?i database

**Ch?y th? ngay!** ??
