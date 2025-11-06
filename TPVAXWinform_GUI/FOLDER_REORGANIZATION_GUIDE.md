# H??ng d?n t? ch?c l?i c?u trúc th? m?c GUI

## ?? M?c tiêu

T? ch?c l?i code v?i folder `GUI` ?? qu?n lý t?t h?n:

```
TPVAXWinform/
??? GUI/
?   ??? Forms/      (Các form chính)
?   ??? UserControls/       (User controls)
?   ??? CustomControls/     (Custom controls)
??? Business/               (Business logic - t?o sau)
??? Data/    (Data access - t?o sau)
??? Models/                 (Data models - t?o sau)
??? Utils/            (Utilities - t?o sau)
```

---

## ?? Các b??c th?c hi?n trong Visual Studio

### B??c 1: T?o folder structure

1. **M? Solution Explorer**
2. **Right-click** vào project `TPVAXWinform`
3. **Add ? New Folder** ? ??t tên `GUI`
4. **Right-click** vào folder `GUI`
   - Add ? New Folder ? `Forms`
   - Add ? New Folder ? `UserControls`
   - Add ? New Folder ? `CustomControls`

---

### B??c 2: Di chuy?n Forms

#### **frmMain và các form khác**

1. **Trong Solution Explorer:**
   - Ch?n `frmMain.cs`
   - **Kéo th?** vào folder `GUI/Forms/`
   
   **Ho?c:**
 - Right-click `frmMain.cs` ? **Cut** (Ctrl+X)
   - Right-click `GUI/Forms/` ? **Paste** (Ctrl+V)

2. **L?p l?i** cho t?t c? các file liên quan:
   - `frmMain.cs`
   - `frmMain.Designer.cs`
   - `frmMain.resx`

**?? L?u ý:** Visual Studio s? t? ??ng c?p nh?t namespace!

---

### B??c 3: Di chuy?n UserControls

1. **Ch?n toàn b? folder `UserControls`** (n?u có)
2. **Kéo th?** vào `GUI/UserControls/`

**Các file c?n di chuy?n:**
- `DashboardControl.cs`
- `DashboardControl.Designer.cs`
- `UserControl_Architecture_Guide.md` (optional)

---

### B??c 4: Di chuy?n CustomControls

1. **Ch?n toàn b? folder `CustomControls`**
2. **Kéo th?** vào `GUI/CustomControls/`

**Các file c?n di chuy?n:**
- `MenuButton.cs`
- `MenuButton.Designer.cs` (n?u có)
- `MenuButton_Guide.md` (optional)

---

### B??c 5: C?p nh?t Namespace (T? ??ng)

Visual Studio s? t? ??ng h?i:
```
"Would you like to adjust the namespaces in the moved files?"
```

**? Click "Yes"**

Namespace s? ???c c?p nh?t t?:
```csharp
// C?
namespace TPVAXWinform
namespace TPVAXWinform.UserControls
namespace TPVAXWinform.CustomControls
```

Thành:
```csharp
// M?i
namespace TPVAXWinform.GUI.Forms
namespace TPVAXWinform.GUI.UserControls
namespace TPVAXWinform.GUI.CustomControls
```

---

### B??c 6: C?p nh?t using statements

N?u Visual Studio không t? ??ng fix, b?n c?n update manually:

#### **Program.cs**
```csharp
using TPVAXWinform.GUI.Forms; // Thêm dòng này

namespace TPVAXWinform
{
    static class Program
  {
   static void Main()
        {
            Application.Run(new frmMain()); // S? tìm th?y frmMain
    }
    }
}
```

#### **Các file s? d?ng UserControls**
```csharp
using TPVAXWinform.GUI.UserControls;
using TPVAXWinform.GUI.CustomControls;
```

---

### B??c 7: Rebuild Solution

1. **Build ? Rebuild Solution** (Ctrl+Shift+B)
2. **Ki?m tra l?i** trong Error List
3. **Fix** các using statements n?u c?n

---

## ?? N?u g?p l?i "Type or namespace not found"

### Fix 1: Thêm using statement
```csharp
using TPVAXWinform.GUI.Forms;
using TPVAXWinform.GUI.UserControls;
using TPVAXWinform.GUI.CustomControls;
```

### Fix 2: Fully qualified name
```csharp
// Thay vì
var form = new frmMain();

// Dùng
var form = new TPVAXWinform.GUI.Forms.frmMain();
```

### Fix 3: Check Designer.cs files
?ôi khi `.Designer.cs` không ???c update, c?n fix manually:

```csharp
// frmMain.Designer.cs
namespace TPVAXWinform.GUI.Forms // Update này
{
    partial class frmMain
{
     // Designer code
    }
}
```

---

## ?? C?u trúc cu?i cùng

```
TPVAXWinform/
?
??? GUI/
?   ??? Forms/
?   ?   ??? frmMain.cs
?   ?   ??? frmMain.Designer.cs
?   ?   ??? frmMain.resx
?   ?
?   ??? UserControls/
?   ?   ??? DashboardControl.cs
?   ?   ??? DashboardControl.Designer.cs
?   ?   ??? UserControl_Architecture_Guide.md
? ?
?   ??? CustomControls/
?       ??? MenuButton.cs
?       ??? MenuButton_Guide.md
?
??? Program.cs
?
??? Properties/
?   ??? AssemblyInfo.cs
? ??? Resources.Designer.cs
?   ??? Resources.resx
?   ??? Settings.Designer.cs
?   ??? Settings.settings
?
??? SqlServerTypes/
?   ??? Loader.cs
?
??? Dashboard/
?   ??? Controls/
? ??? BadgeButton.cs
?       ??? KpiCard.cs
? ??? KpiCard.Designer.cs
?
??? temp.cs
??? temp.Designer.cs
?
??? Guides/ (Optional - Move các file .md vào ?ây)
?   ??? LiveCharts_Migration_Guide.md
?   ??? LiveCharts_Usage_Guide.md
?   ??? ChartFilter_Guide.md
?   ??? SearchPanels_Guide.md
?
??? TPVAXWinform.csproj
```

---

## ?? Các folder khác nên t?o (T??ng lai)

### 1. **Business/** - Business Logic Layer
```csharp
TPVAXWinform/
??? Business/
?   ??? Services/
?   ?   ??? PatientService.cs
?   ?   ??? VaccineService.cs
?   ?   ??? InvoiceService.cs
?   ??? Validators/
?     ??? PatientValidator.cs
?    ??? VaccineValidator.cs
```

### 2. **Data/** - Data Access Layer
```csharp
TPVAXWinform/
??? Data/
?   ??? Repositories/
?   ?   ??? PatientRepository.cs
?   ?   ??? VaccineRepository.cs
?   ?   ??? InvoiceRepository.cs
?   ??? Context/
?       ??? DatabaseContext.cs
```

### 3. **Models/** - Data Models
```csharp
TPVAXWinform/
??? Models/
?   ??? Patient.cs
?   ??? Vaccine.cs
?   ??? Invoice.cs
?   ??? Employee.cs
```

### 4. **Utils/** - Utilities
```csharp
TPVAXWinform/
??? Utils/
?   ??? ConfigHelper.cs
?   ??? DateHelper.cs
?   ??? ValidationHelper.cs
?   ??? ExportHelper.cs
```

### 5. **Constants/** - Constants
```csharp
TPVAXWinform/
??? Constants/
?   ??? AppConstants.cs
?   ??? MessageConstants.cs
?   ??? ColorConstants.cs
```

---

## ? Checklist sau khi di chuy?n

- [ ] T?t c? file ?ã ???c di chuy?n ?úng folder
- [ ] Namespace ?ã ???c update
- [ ] Using statements ?ã ???c thêm
- [ ] Solution build thành công (0 errors)
- [ ] Ch?y ?ng d?ng và test
- [ ] Git commit changes

---

## ?? Troubleshooting

### L?i: "Could not find type 'frmMain'"

**Nguyên nhân:** Namespace không ?úng

**Fix:**
```csharp
// Program.cs
using TPVAXWinform.GUI.Forms;

Application.Run(new frmMain());
```

### L?i: "UserControl does not exist in current context"

**Nguyên nhân:** Thi?u using statement

**Fix:**
```csharp
// frmMain.cs
using TPVAXWinform.GUI.UserControls;
using TPVAXWinform.GUI.CustomControls;
```

### L?i: Designer không m? ???c

**Nguyên nhân:** Designer.cs có namespace sai

**Fix:**
1. M? file `.Designer.cs`
2. Check namespace ? ??u file
3. Update cho ?úng v?i `.cs` file

---

## ?? Tips

### 1. **Backup tr??c khi di chuy?n**
```bash
git commit -m "Backup before reorganizing folders"
```

### 2. **Di chuy?n t?ng folder m?t**
- Di chuy?n Forms ? Build ? Test
- Di chuy?n UserControls ? Build ? Test
- Di chuy?n CustomControls ? Build ? Test

### 3. **S? d?ng Find & Replace**
N?u có nhi?u file c?n update namespace:
- Ctrl+Shift+H (Find and Replace in Files)
- Find: `namespace TPVAXWinform`
- Replace: `namespace TPVAXWinform.GUI.Forms` (ho?c t??ng ?ng)

---

## ?? K?t qu?

Sau khi hoàn thành, b?n s? có:
- ? Code ???c t? ch?c rõ ràng theo layers
- ? D? dàng tìm ki?m và maintain
- ? Chu?n b? t?t cho vi?c m? r?ng
- ? Theo best practices c?a .NET

---

## ?? Next Steps

1. **T?o Business Layer**
- Services cho logic nghi?p v?
   - Validators cho validation

2. **T?o Data Layer**
   - Repositories cho database access
   - Context/Connection management

3. **T?o Models**
   - DTOs (Data Transfer Objects)
   - ViewModels cho binding

4. **T?o Utils**
   - Helper classes
   - Extension methods

**Chúc b?n t? ch?c code thành công!** ??
