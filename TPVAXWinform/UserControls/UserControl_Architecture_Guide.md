# H??ng d?n UserControl Architecture

## ? ?ã tách thành công Dashboard thành UserControl!

### ?? C?u trúc m?i

```
TPVAXWinform/
??? frmMain.cs (Main Form)
?   ??? panel3 (Container)
?     ??? DashboardControl (UserControl)
?
??? UserControls/
?   ??? DashboardControl.cs
?   ??? DashboardControl.Designer.cs
?
??? ... (các file khác)
```

---

## ?? L?i ích c?a ki?n trúc này

### 1. **Tái s? d?ng code**
- DashboardControl có th? dùng ? nhi?u form khác
- Copy/paste d? dàng vào project khác

### 2. **D? b?o trì**
- Code ???c tách riêng, d? tìm và s?a
- Thay ??i Dashboard không ?nh h??ng frmMain

### 3. **M? r?ng d? dàng**
- T?o thêm UserControl cho m?i ch?c n?ng
- Chuy?n ??i gi?a các màn hình b?ng cách thay UserControl

### 4. **Testing d? h?n**
- Test riêng t?ng UserControl
- Không c?n load toàn b? form

---

## ?? DashboardControl - Tính n?ng

### Controls bên trong
1. **2 KPI Cards**:
   - L??t tiêm hôm nay (panel4)
   - Khách hàng trong tu?n (panel5)

2. **2 Search Panels**:
   - Tìm ki?m b?nh nhân (panel6)
   - Tìm ki?m vaccine (panelChartFilter)

3. **2 LiveCharts**:
   - Column Chart (L??t tiêm theo tháng)
   - Pie Chart (T? l? vaccine)

### Public Methods
```csharp
// C?p nh?t KPI t? bên ngoài
dashboardControl1.UpdateVaccinationToday(1257, "T?ng 5% so v?i tháng tr??c");
dashboardControl1.UpdateWeeklyCustomers(542, "T?ng 2% so v?i tu?n tr??c");
```

---

## ?? Cách t?o UserControl m?i cho các ch?c n?ng khác

### B??c 1: T?o UserControl m?i

**Trong Visual Studio:**
1. Right-click folder `UserControls`
2. Add ? User Control
3. ??t tên: `[TenChucNang]Control.cs`
   - Ví d?: `PatientControl.cs`, `VaccineControl.cs`, `InvoiceControl.cs`

### B??c 2: Design giao di?n

```csharp
// PatientControl.Designer.cs
namespace TPVAXWinform.UserControls
{
    partial class PatientControl
    {
        private System.ComponentModel.IContainer components = null;
        
        // Controls c?a b?n
     private System.Windows.Forms.DataGridView dgvPatients;
     private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
    private System.Windows.Forms.Button btnDelete;
        
        private void InitializeComponent()
   {
 // Design code t?i ?ây
   }
    }
}
```

### B??c 3: Implement logic

```csharp
// PatientControl.cs
using System;
using System.Windows.Forms;

namespace TPVAXWinform.UserControls
{
    public partial class PatientControl : UserControl
    {
        public PatientControl()
        {
    InitializeComponent();
        }
        
  private void PatientControl_Load(object sender, EventArgs e)
        {
LoadPatients();
    }
        
        private void LoadPatients()
        {
 // Load data t? database
            // dgvPatients.DataSource = ...
      }
      
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Thêm b?nh nhân m?i
        }
     
        // ... các methods khác
        
        // Public method ?? refresh t? bên ngoài
  public void RefreshData()
     {
            LoadPatients();
        }
    }
}
```

### B??c 4: S? d?ng trong frmMain

```csharp
// frmMain.Designer.cs - Thêm khai báo
private UserControls.PatientControl patientControl1;
private UserControls.VaccineControl vaccineControl1;
private UserControls.InvoiceControl invoiceControl1;

// Trong InitializeComponent()
this.patientControl1 = new TPVAXWinform.UserControls.PatientControl();
this.vaccineControl1 = new TPVAXWinform.UserControls.VaccineControl();
this.invoiceControl1 = new TPVAXWinform.UserControls.InvoiceControl();

// Ban ??u ?n t?t c?
this.patientControl1.Visible = false;
this.vaccineControl1.Visible = false;
this.invoiceControl1.Visible = false;
```

### B??c 5: Chuy?n ??i gi?a các UserControl

```csharp
// frmMain.cs
private void ShowUserControl(UserControl control)
{
    // ?n t?t c? UserControl
    dashboardControl1.Visible = false;
    patientControl1.Visible = false;
    vaccineControl1.Visible = false;
    invoiceControl1.Visible = false;
    
// Hi?n control ???c ch?n
    control.Visible = true;
    control.BringToFront();
}

// Event handlers cho menu buttons
private void button1_Click(object sender, EventArgs e)
{
    ShowUserControl(dashboardControl1); // Trang ch?
}

private void button2_Click(object sender, EventArgs e)
{
    ShowUserControl(patientControl1); // Khách hàng
}

private void button3_Click(object sender, EventArgs e)
{
    ShowUserControl(vaccineControl1); // Vaccine
}

private void button6_Click(object sender, EventArgs e)
{
    ShowUserControl(invoiceControl1); // Hóa ??n
}
```

---

## ?? Template cho UserControl m?i

### File: `[TenChucNang]Control.cs`

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TPVAXWinform.UserControls
{
    public partial class [TenChucNang]Control : UserControl
    {
    public [TenChucNang]Control()
        {
      InitializeComponent();
        }

     private void [TenChucNang]Control_Load(object sender, EventArgs e)
        {
       InitializeData();
        SetupEventHandlers();
        }

     private void InitializeData()
  {
            // Load d? li?u ban ??u
}

      private void SetupEventHandlers()
        {
          // Wire up event handlers
        }

      // Public methods ?? t??ng tác t? bên ngoài
        public void RefreshData()
      {
InitializeData();
        }

        public void ClearForm()
    {
   // Clear các controls
        }
    }
}
```

---

## ?? Best Practices

### 1. **Naming Convention**
```
DashboardControl   ? (PascalCase + Control suffix)
dashboardControl1  ? (instance: camelCase + 1)
PatientControl     ?
patientCtrl        ? (không rõ ràng)
UC_Patient         ? (không theo chu?n .NET)
```

### 2. **Folder Structure**
```
UserControls/
??? DashboardControl.cs
??? DashboardControl.Designer.cs
??? PatientControl.cs
??? PatientControl.Designer.cs
??? VaccineControl.cs
??? VaccineControl.Designer.cs
??? Shared/           # Common controls
    ??? SearchPanel.cs
```

### 3. **Communication gi?a UserControl và Form**

#### **Cách 1: Public Methods** (??n gi?n)
```csharp
// T? frmMain g?i method c?a UserControl
patientControl1.RefreshData();
patientControl1.LoadPatient(patientId);
```

#### **Cách 2: Events** (Professional)
```csharp
// Trong UserControl
public event EventHandler<PatientEventArgs> PatientSelected;

protected virtual void OnPatientSelected(PatientEventArgs e)
{
    PatientSelected?.Invoke(this, e);
}

// Trong frmMain
patientControl1.PatientSelected += (s, e) => 
{
    MessageBox.Show($"?ã ch?n: {e.Patient.Name}");
};
```

#### **Cách 3: Delegate/Callback**
```csharp
// Trong UserControl
public Action<Patient> OnPatientAdded { get; set; }

// Khi thêm thành công
OnPatientAdded?.Invoke(newPatient);

// Trong frmMain
patientControl1.OnPatientAdded = (patient) =>
{
    RefreshDashboard();
    ShowNotification($"?ã thêm: {patient.Name}");
};
```

### 4. **Dispose Resources ?úng cách**
```csharp
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
     // Dispose managed resources
        if (components != null)
        components.Dispose();
        
      // Dispose database connections
    if (connection != null && connection.State == ConnectionState.Open)
  connection.Close();
    }
    base.Dispose(disposing);
}
```

### 5. **Loading State**
```csharp
private bool isLoading = false;

private async void LoadData()
{
    if (isLoading) return;
    
    try
    {
        isLoading = true;
        ShowLoadingIndicator();
      
        var data = await GetDataFromDatabase();
        dgv.DataSource = data;
    }
    finally
    {
        isLoading = false;
        HideLoadingIndicator();
    }
}
```

---

## ?? Advanced: Dependency Injection cho UserControl

```csharp
// Service interface
public interface IPatientService
{
    List<Patient> GetAllPatients();
void AddPatient(Patient patient);
}

// UserControl with DI
public partial class PatientControl : UserControl
{
    private readonly IPatientService _patientService;
    
    public PatientControl(IPatientService patientService)
    {
        InitializeComponent();
        _patientService = patientService;
    }
    
    private void LoadPatients()
    {
   var patients = _patientService.GetAllPatients();
        dgv.DataSource = patients;
    }
}

// Usage trong frmMain
var patientService = new PatientService(connectionString);
patientControl1 = new PatientControl(patientService);
```

---

## ?? Checklist khi t?o UserControl m?i

- [ ] Tên file theo convention: `[Feature]Control.cs`
- [ ] Namespace ?úng: `TPVAXWinform.UserControls`
- [ ] Implement `Load` event
- [ ] Public methods ?? refresh data
- [ ] Dispose resources properly
- [ ] Comment code ??y ??
- [ ] Test riêng UserControl tr??c khi integrate
- [ ] Handle exceptions
- [ ] Loading indicators cho async operations
- [ ] Validation ??u vào

---

## ?? K? ho?ch ti?p theo

B?n có th? t?o các UserControl cho:

1. **PatientControl** - Qu?n lý b?nh nhân
   - CRUD operations
   - Search & filter
   - Export to Excel

2. **VaccineControl** - Qu?n lý vaccine
   - Danh sách vaccine
   - Qu?n lý kho
   - H?n s? d?ng

3. **EmployeeControl** - Qu?n lý nhân viên
   - Thông tin nhân viên
   - Phân quy?n
   - L?ch làm vi?c

4. **InvoiceControl** - Qu?n lý hóa ??n
   - T?o hóa ??n
   - In hóa ??n
   - Báo cáo doanh thu

5. **StatisticsControl** - Th?ng kê báo cáo
   - Charts & graphs
   - Export reports
   - Date range filtering

---

## ?? K?t lu?n

B?n ?ã có:
- ? DashboardControl ??c l?p
- ? Ki?n trúc UserControl rõ ràng
- ? Template ?? t?o UserControl m?i
- ? Best practices và patterns
- ? Code d? maintain và m? r?ng

**Gi? b?n có th? t?o UserControl cho t?ng ch?c n?ng m?t cách có t? ch?c!** ??
