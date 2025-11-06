# H??ng d?n s?a l?i encoding và t?ng kích th??c giao di?n

## ?? V?n ?? g?p ph?i

### 1. L?i Encoding (Ký t? ?, ?)
**Nguyên nhân:**
- Designer files (.Designer.cs) ?ang ???c l?u v?i encoding sai (ANSI ho?c Windows-1252)
- Ti?ng Vi?t có d?u b? mã hóa sai thành `?` ho?c `?`

**?nh h??ng:**
- T?t c? text có d?u hi?n th? l?i: "Tìm ki?m", "H? s? ti?m ch?ng"
- DataGridView headers b? l?i: "M?i s?", "Tr?ng thái"
- Button text b? l?i

### 2. Giao di?n quá nh?
- Font size quá nh? (9F, 10F)
- Controls quá nh? (button 100x35, textbox height 34px)
- Header panel quá th?p (60px)

---

## ? Gi?i pháp

### **Ph??ng pháp 1: Dùng Unicode Escape Sequences** (KHUY?N NGH?)

Thay vì vi?t:
```csharp
this.lblTitle.Text = "?? H? S? TIÊM CH?NG";
```

Dùng Unicode escapes:
```csharp
// ?? = \uD83D\uDC89 (emoji syringe)
// ? = \u00D4\u0300
// ? = \u01A0 ho?c \u01A1
// ? = \u01AF ho?c \u01B0

this.lblTitle.Text = "\uD83D\uDC89 H? S? TIÊM CH?NG"; // Hoàn toàn OK
```

**?u ?i?m:**
- ? Không b? l?i encoding
- ? Compile ???c trên m?i máy
- ? Cross-platform compatible

**Nh??c ?i?m:**
- ? Khó ??c trong code
- ? Khó maintain

### **Ph??ng pháp 2: Chuy?n File v? UTF-8 with BOM**

1. **Trong Visual Studio:**
   - File ? Advanced Save Options...
   - Ch?n: **Unicode (UTF-8 with signature) - Codepage 65001**
   - Save

2. **Dùng Notepad++:**
   - Encoding ? Convert to UTF-8-BOM
   - Save

**?u ?i?m:**
- ? Code d? ??c
- ? D? maintain

**Nh??c ?i?m:**
- ? Ph?i convert t?ng file
- ? Team member khác có th? l?i l?u sai encoding

### **Ph??ng pháp 3: Set text trong code-behind (KHUY?N NGH? CHO D? ÁN L?N)**

**Designer.cs** - Ch? ?? r?ng ho?c ASCII:
```csharp
this.lblTitle.Name = "lblTitle";
this.lblTitle.TabIndex = 0;
// KHÔNG set Text ? ?ây
```

**.cs (Code-behind)** - Set text trong constructor:
```csharp
public ImmunizationRecordControl()
{
  InitializeComponent();
    
    // Set text here v?i encoding ?úng
    lblTitle.Text = "?? H? S? TIÊM CH?NG";
    label1.Text = "T? ngày ~ ??n:";
    label2.Text = "Lo?i v?c xin:";
    label3.Text = "M?i s?:";
    label4.Text = "Tr?ng thái:";
    btnSearch.Text = "?? Tìm ki?m";
    btnReset.Text = "?? Reset";
    
    // DataGridView headers
    colRecordId.HeaderText = "Mã HS";
    colCustomerId.HeaderText = "Mã KH";
    colCustomerName.HeaderText = "H? tên";
    colVaccinationDate.HeaderText = "Ngày tiêm";
    colVaccineName.HeaderText = "V?c xin";
    colDoseNumber.HeaderText = "M?i s?";
    colLotNumber.HeaderText = "Lô";
    colEmployee.HeaderText = "NV th?c hi?n";
    colStatus.HeaderText = "Tr?ng thái";
    colPrintCert.HeaderText = "In ch?ng nh?n";
}
```

**?u ?i?m:**
- ? Code d? ??c
- ? Tách bi?t UI và text
- ? D? i18n (internationalization) sau này
- ? Không lo l?i encoding

**Nh??c ?i?m:**
- ? Nhi?u code h?n

---

## ?? Các thay ??i ?ã th?c hi?n

### 1. Fix Encoding Issues

#### ImmunizationRecordControl.Designer.cs
```csharp
// ? TR??C (B? l?i)
this.lblTitle.Text = "?? H? S? TI?M CH?NG";
this.label1.Text = "T? ngày ~ ??n:";
this.label2.Text = "Lo?i v?c xin:";
this.label3.Text = "M?i s?:";
this.label4.Text = "Tr?ng thái:";

// ? SAU (?ã s?a b?ng Unicode escape)
this.lblTitle.Text = "\uD83D\uDC89 H? S? TIÊM CH?NG";
this.label1.Text = "T? ngày ~ ??n:";
this.label2.Text = "Lo?i v?c xin:";
this.label3.Text = "M?i s?:";
this.label4.Text = "Tr?ng thái:";
```

#### DashboardControl.Designer.cs
```csharp
// ? TR??C
this.label6.Text = "Lu?t ti?m h?m nay";
this.label11.Text = "Kh?ch h?ng trong tu?n";
this.label14.Text = "?? T?m b?nh nh?n:";
this.label17.Text = "?? T?m vaccine:";

// ? SAU
this.label6.Text = "L??t tiêm hôm nay";
this.label11.Text = "Khách hàng trong tu?n";
this.label14.Text = "?? Tìm b?nh nhân:";
this.label17.Text = "?? Tìm vaccine:";
```

### 2. T?ng Kích Th??c Giao Di?n

#### T?ng Font Size
```csharp
// ? TR??C (Quá nh?)
this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold); // 16pt
this.label1.Font = new Font("Segoe UI", 9F); // 9pt
this.btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

// ? SAU (V?a m?t)
this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold); // 18pt
this.label1.Font = new Font("Segoe UI", 11F); // 11pt
this.btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // 10pt
```

#### T?ng Kích Th??c Controls
```csharp
// ? TR??C
this.btnSearch.Size = new Size(100, 35); // Nh?
this.txtSearchPatient.Size = new Size(130, 34); // Nh?
this.panelFilter.Size = new Size(1172, 100); // Th?p

// ? SAU
this.btnSearch.Size = new Size(120, 40); // L?n h?n
this.txtSearchPatient.Size = new Size(200, 38); // R?ng h?n
this.panelFilter.Size = new Size(1172, 120); // Cao h?n
```

#### T?ng Kích Th??c DataGridView
```csharp
// ? TR??C
this.dgvRecords.RowTemplate.Height = 28; // Nh?
this.dgvRecords.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

// ? SAU
this.dgvRecords.RowTemplate.Height = 35; // Cao h?n
this.dgvRecords.DefaultCellStyle.Font = new Font("Segoe UI", 10F); // Font l?n
this.dgvRecords.ColumnHeadersHeight = 40; // Header cao h?n
this.dgvRecords.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
```

#### T?ng Panel Header
```csharp
// ? TR??C
this.panelHeader.Size = new Size(1200, 60); // Th?p

// ? SAU
this.panelHeader.Size = new Size(1200, 80); // Cao h?n
this.lblTitle.Location = new Point(20, 20); // Centered t?t h?n
```

---

## ?? Code Template ?? s?a

### ImmunizationRecordControl.cs
```csharp
public ImmunizationRecordControl()
{
    InitializeComponent();
    SetupUIText(); // G?i method set text
}

private void SetupUIText()
{
    // Header
    lblTitle.Text = "?? H? S? TIÊM CH?NG";
    
    // Filter labels
    label1.Text = "T? ngày ~ ??n:";
    label2.Text = "Lo?i v?c xin:";
    label3.Text = "M?i s?:";
    label4.Text = "Tr?ng thái:";
    
    // Buttons
    btnSearch.Text = "?? Tìm ki?m";
    btnReset.Text = "?? Reset";
    
    // Tabs
    tabList.Text = "?? Danh sách h? s?";
    tabCreate.Text = "? Ghi m?i m?i";
    tabCertificate.Text = "?? Ch?ng nh?n";
    
    // ComboBox items
    cboVaccine.Items.Clear();
    cboVaccine.Items.AddRange(new object[] {
        "T?t c?",
        "Vaccine A",
        "Vaccine B",
        "Vaccine C",
        "Vaccine D"
    });
    
cboDoseNumber.Items.Clear();
  cboDoseNumber.Items.AddRange(new object[] {
        "T?t c?",
        "M?i 1",
        "M?i 2",
        "M?i 3",
  "M?i nh?c l?i"
    });
    
    cboStatus.Items.Clear();
    cboStatus.Items.AddRange(new object[] {
     "T?t c?",
        "Draft",
        "Done",
    "Cancelled"
  });
    
    // DataGridView headers
    colRecordId.HeaderText = "Mã HS";
    colCustomerId.HeaderText = "Mã KH";
    colCustomerName.HeaderText = "H? tên";
    colVaccinationDate.HeaderText = "Ngày tiêm";
    colVaccineName.HeaderText = "V?c xin";
    colDoseNumber.HeaderText = "M?i s?";
    colLotNumber.HeaderText = "Lô";
    colEmployee.HeaderText = "NV th?c hi?n";
    colStatus.HeaderText = "Tr?ng thái";
    colPrintCert.HeaderText = "In ch?ng nh?n";
    colPrintCert.Text = "??? In";
}
```

### DashboardControl.cs
```csharp
public DashboardControl()
{
    InitializeComponent();
    SetupUIText();
}

private void SetupUIText()
{
    // KPI Cards
    label6.Text = "L??t tiêm hôm nay";
    label8.Text = "T?ng 5% so v?i tháng tr??c";
    label11.Text = "Khách hàng trong tu?n";
    label9.Text = "T?ng 2% so v?i tu?n tr??c";
    
    // Search panels
    label14.Text = "?? Tìm b?nh nhân:";
    label17.Text = "?? Tìm vaccine:";
    
  // Buttons
    btnSearchPatient.Text = "?? Tìm";
 btnResetPatient.Text = "?? Reset";
    btnFilter.Text = "?? Tìm";
    btnReset.Text = "?? Reset";
}
```

---

## ?? CSS/Styling Improvements

### Font Sizes Standardized
```csharp
// Headers
Font headerFont = new Font("Segoe UI", 18F, FontStyle.Bold);

// Titles
Font titleFont = new Font("Segoe UI", 12F, FontStyle.Bold);

// Normal text
Font normalFont = new Font("Segoe UI", 10F);

// Small text
Font smallFont = new Font("Segoe UI", 9F);
```

### Control Heights
```csharp
const int BUTTON_HEIGHT = 40;
const int TEXTBOX_HEIGHT = 38;
const int COMBOBOX_HEIGHT = 38;
const int PANEL_FILTER_HEIGHT = 120;
const int PANEL_HEADER_HEIGHT = 80;
const int DATAGRID_ROW_HEIGHT = 35;
```

---

## ?? L?u ý quan tr?ng

### 1. Khi làm vi?c team
- **KHÔNG** edit Designer.cs tr?c ti?p
- **PH?I** set text trong code-behind (.cs)
- Commit c? 2 files: .Designer.cs và .cs

### 2. Khi add control m?i trong Designer
- Không set `Text` property trong Designer
- Set trong code-behind ngay sau `InitializeComponent()`

### 3. Git
- Add vào `.gitattributes`:
```
*.Designer.cs text eol=crlf
*.cs text eol=crlf
```

### 4. Visual Studio Settings
- Tools ? Options ? Environment ? Documents
- Check: "Save documents as Unicode when data cannot be saved in codepage"

---

## ?? Cách ki?m tra ?ã s?a ?úng ch?a

### 1. Check Encoding
```csharp
// Trong Designer.cs, tìm:
this.lblTitle.Text = // Ph?i có Unicode escape ho?c ASCII
// KHÔNG ???c có: ?, ?, ho?c ký t? l?
```

### 2. Test Runtime
- Run app (F5)
- Check t?t c? text hi?n th? ?úng
- Check font size ?? l?n, d? ??c
- Check buttons/controls ?? l?n ?? click

### 3. Check Source Code
```bash
# PowerShell - Check encoding c?a file
Get-Content "ImmunizationRecordControl.Designer.cs" -Encoding UTF8
# Không ???c có ký t? l?i
```

---

## ?? So sánh Before/After

| Feature | Before | After | Improvement |
|---------|--------|-------|-------------|
| lblTitle Font | 16pt | 18pt | +12.5% |
| Button Height | 35px | 40px | +14% |
| Filter Panel | 100px | 120px | +20% |
| DataGrid Row | 28px | 35px | +25% |
| Label Font | 9pt | 10-11pt | +11-22% |
| Text Encoding | ? L?i | ? ?úng | Fixed |

---

## ? Checklist

- [x] Fix encoding cho ImmunizationRecordControl
- [x] Fix encoding cho DashboardControl  
- [x] T?ng font size headers (18pt)
- [x] T?ng font size labels (11pt)
- [x] T?ng font size buttons (10pt)
- [x] T?ng button size (120x40)
- [x] T?ng textbox height (38px)
- [x] T?ng panel heights (+20%)
- [x] T?ng DataGridView row height (35px)
- [x] Test runtime

---

## ?? K?t qu?

**Tr??c khi s?a:**
```
? ?? H? S? TI?M CH?NG (Font 16pt, nh? + l?i)
? T? ngày ~ ??n: (Font 9pt, nh? + l?i)
? [100x35 button] (Quá nh?)
```

**Sau khi s?a:**
```
? ?? H? S? TIÊM CH?NG (Font 18pt, l?n + ??p)
? T? ngày ~ ??n: (Font 11pt, v?a + rõ)
? [120x40 button] (V?a tay, d? click)
```

---

## ?? Tài li?u tham kh?o

1. [Unicode in C# Strings](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/)
2. [File Encoding in Visual Studio](https://docs.microsoft.com/en-us/visualstudio/ide/encodings-and-line-breaks)
3. [WinForms Control Sizing Best Practices](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/)

**Chúc m?ng b?n ?ã fix xong! ??**
