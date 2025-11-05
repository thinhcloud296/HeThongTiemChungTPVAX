# H??ng d?n t?o Class Library Project "GUI"

## ?? M?c tiêu

T?o m?t **Class Library** project riêng tên `GUI` ?? ch?a t?t c? các thành ph?n giao di?n, tách bi?t kh?i business logic.

### Ki?n trúc Solution m?i:

```
HeThongTiemChungTPVAX (Solution)
??? TPVAXWinform (Windows Forms App - Main project)
?   ??? Program.cs
?   ??? app.manifest
?
??? GUI (Class Library - UI Components)
? ??? Forms/
?   ?   ??? frmMain.cs
?   ??? UserControls/
?   ?   ??? DashboardControl.cs
?   ??? CustomControls/
?   ??? MenuButton.cs
?
??? TPVAXWebsite (Web project - existing)
?
??? Business (Class Library - Business Logic) [T?o sau]
```

---

## ?? B??c 1: T?o Class Library Project "GUI"

### Trong Visual Studio:

1. **Right-click** vào Solution `HeThongTiemChungTPVAX` trong Solution Explorer
2. **Add ? New Project...**
3. Ch?n **"Class Library (.NET Framework)"**
4. **C?u hình:**
   - **Project name:** `GUI`
   - **Location:** `C:\srcKhoaLuan\HeThongTiemChungTPVAX\`
   - **Framework:** `.NET Framework 4.8.1` (gi?ng TPVAXWinform)
5. Click **Create**

### Ho?c dùng Command Line:

```powershell
cd C:\srcKhoaLuan\HeThongTiemChungTPVAX

# T?o Class Library project
dotnet new classlib --name GUI --framework net481 --output GUI

# Thêm vào solution
dotnet sln HeThongTiemChungTPVAX.sln add GUI/GUI.csproj
```

---

## ?? B??c 2: C?u hình GUI Project

### 2.1. Xóa Class1.cs m?c ??nh

1. Right-click `Class1.cs` trong project `GUI`
2. **Delete**

### 2.2. Thêm References c?n thi?t

**Right-click project `GUI` ? Add ? Reference...**

Thêm các references sau:

#### **System References:**
- ? `System.Windows.Forms`
- ? `System.Drawing`
- ? `System.Data`
- ? `System.Configuration`

#### **NuGet Packages:**
```powershell
# Trong Package Manager Console
Install-Package LiveCharts.WinForms -ProjectName GUI
Install-Package LiveCharts -ProjectName GUI
Install-Package LiveCharts.Wpf -ProjectName GUI
```

**Ho?c dùng NuGet Package Manager:**
1. Right-click `GUI` project ? **Manage NuGet Packages**
2. Browse tab ? Search `LiveCharts.WinForms`
3. Install

### 2.3. T?o folder structure

**Trong project `GUI`:**
1. Right-click `GUI` project ? **Add ? New Folder** ? `Forms`
2. Right-click `GUI` project ? **Add ? New Folder** ? `UserControls`
3. Right-click `GUI` project ? **Add ? New Folder** ? `CustomControls`

---

## ?? B??c 3: Di chuy?n Forms vào GUI Project

### 3.1. Di chuy?n frmMain

**Trong Visual Studio:**

1. **Trong project `TPVAXWinform`:**
   - Right-click `frmMain.cs` ? **Cut** (Ctrl+X)

2. **Trong project `GUI/Forms/`:**
   - Right-click folder `Forms` ? **Paste** (Ctrl+V)

3. **Visual Studio s? h?i:**
```
   "Would you like to adjust the namespaces in the moved files?"
   ```
   ? Click **Yes**

4. **Namespace s? ???c update:**
   ```csharp
   // T?:
   namespace TPVAXWinform
   
   // Thành:
   namespace GUI.Forms
   ```

### 3.2. Fix frmMain.Designer.cs

M? file `GUI/Forms/frmMain.Designer.cs` và ??m b?o:

```csharp
namespace GUI.Forms
{
  partial class frmMain
    {
        // ... designer code
}
}
```

### 3.3. Fix các using statements

**frmMain.cs:**
```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
// Các using khác...

namespace GUI.Forms
{
    public partial class frmMain : Form
    {
        // ... code
    }
}
```

---

## ?? B??c 4: Di chuy?n UserControls vào GUI Project

### 4.1. Di chuy?n DashboardControl

1. **Cut** toàn b? folder `UserControls` t? `TPVAXWinform`
2. **Paste** vào project `GUI`

3. **Update namespace:**
   ```csharp
   // DashboardControl.cs
   namespace GUI.UserControls
   {
       public partial class DashboardControl : UserControl
       {
 // ... code
       }
   }
   ```

### 4.2. Fix references trong DashboardControl

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace GUI.UserControls
{
    public partial class DashboardControl : UserControl
    {
   // ... implementation
    }
}
```

---

## ?? B??c 5: Di chuy?n CustomControls vào GUI Project

### 5.1. Di chuy?n MenuButton

1. **Cut** folder `CustomControls` t? `TPVAXWinform`
2. **Paste** vào project `GUI`

3. **Update namespace:**
   ```csharp
   // MenuButton.cs
   namespace GUI.CustomControls
   {
       public class MenuButton : Button
       {
    // ... code
     }
   }
   ```

---

## ?? B??c 6: Thêm Reference t? TPVAXWinform ??n GUI

### Trong TPVAXWinform project:

1. **Right-click `TPVAXWinform` project ? Add ? Reference...**
2. Tab **Projects**
3. Check ? `GUI`
4. Click **OK**

---

## ?? B??c 7: C?p nh?t Program.cs trong TPVAXWinform

```csharp
using System;
using System.Windows.Forms;
using GUI.Forms; // Thêm reference ??n GUI project

namespace TPVAXWinform
{
    internal static class Program
    {
  /// <summary>
        /// The main entry point for the application.
    /// </summary>
        [STAThread]
    static void Main()
   {
            // B?t High DPI support cho .NET Framework 4.7+
   if (Environment.OSVersion.Version.Major >= 6)
         {
 SetProcessDPIAware();
            }

       Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
     Application.Run(new frmMain()); // frmMain gi? t? GUI.Forms
  }

   // Import hàm t? user32.dll ?? b?t DPI awareness
    [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
```

---

## ?? B??c 8: Copy Resources (Images, Icons)

### 8.1. Copy Properties/Resources

N?u có images/icons trong `TPVAXWinform/Properties/Resources.resx`:

1. **Copy** file `Resources.resx` và folder `Resources` (n?u có)
2. **Paste** vào `GUI/Properties/`

### 8.2. Update Resource references

Trong các Form/UserControl, ??m b?o resources ???c tham chi?u ?úng:

```csharp
// T?:
this.BackgroundImage = TPVAXWinform.Properties.Resources.logo;

// Thành:
this.BackgroundImage = GUI.Properties.Resources.logo;
```

---

## ?? B??c 9: Rebuild Solution

```
Build ? Rebuild Solution (Ctrl+Shift+B)
```

### Ki?m tra:
- ? 0 Errors
- ?? Warnings là OK
- ? TPVAXWinform.exe ???c build thành công
- ? GUI.dll ???c build thành công

---

## ?? B??c 10: Test ?ng d?ng

1. **Set `TPVAXWinform` as Startup Project** (n?u ch?a)
   - Right-click `TPVAXWinform` ? **Set as Startup Project**

2. **Run** (F5)

3. **Test các tính n?ng:**
   - Form load ?úng
   - Dashboard hi?n th?
   - Charts ho?t ??ng
   - Search panels ho?t ??ng

---

## ?? C?u trúc Solution cu?i cùng

```
HeThongTiemChungTPVAX/
?
??? TPVAXWinform/           (Windows Forms App - Entry Point)
?   ??? Program.cs   (Main entry, reference GUI.Forms.frmMain)
?   ??? app.manifest (DPI settings)
?   ??? App.config
?   ??? Properties/
?
??? GUI/    (Class Library - UI Layer)
?   ??? Forms/
?   ?   ??? frmMain.cs
?   ?   ??? frmMain.Designer.cs
?   ?   ??? frmMain.resx
?   ?
?   ??? UserControls/
?   ?   ??? DashboardControl.cs
?   ?   ??? DashboardControl.Designer.cs
?   ?   ??? UserControl_Architecture_Guide.md
?   ?
?   ??? CustomControls/
?   ?   ??? MenuButton.cs
? ?
?   ??? Properties/
?   ?   ??? Resources.resx
?   ?   ??? Resources.Designer.cs
?   ?
?   ??? GUI.csproj
?
??? TPVAXWebsite/  (Web project - existing)
?
??? HeThongTiemChungTPVAX.sln
```

---

## ?? L?i ích c?a ki?n trúc này

### 1. **Separation of Concerns**
- UI logic tách bi?t kh?i entry point
- D? test t?ng layer riêng

### 2. **Reusability**
- GUI.dll có th? dùng cho nhi?u projects
- D? dàng t?o multiple entry points

### 3. **Maintainability**
- Thay ??i UI không ?nh h??ng TPVAXWinform
- Clear dependencies

### 4. **Scalability**
- D? dàng thêm projects khác (Business, Data)
- Chu?n N-tier architecture

---

## ?? Next Steps - T?o các Class Library khác

### 1. **Business Layer** (Business Logic)

```powershell
dotnet new classlib --name Business --framework net481
dotnet sln add Business/Business.csproj
```

**Structure:**
```
Business/
??? Services/
?   ??? PatientService.cs
???? VaccineService.cs
???? InvoiceService.cs
??? Validators/
?   ??? PatientValidator.cs
??? Interfaces/
    ??? IPatientService.cs
```

**References:**
- `GUI` ? `Business` (GUI calls Business services)
- `Business` ? `Data` (Business calls Data repositories)

### 2. **Data Layer** (Data Access)

```powershell
dotnet new classlib --name Data --framework net481
dotnet sln add Data/Data.csproj
```

**Structure:**
```
Data/
??? Repositories/
?   ??? PatientRepository.cs
?   ??? VaccineRepository.cs
??? Context/
?   ??? DatabaseContext.cs
??? Interfaces/
    ??? IPatientRepository.cs
```

**NuGet Packages:**
- Entity Framework (ho?c ADO.NET)
- SQL Server Client

### 3. **Models/Entities** (Shared Data Models)

```powershell
dotnet new classlib --name Models --framework net481
dotnet sln add Models/Models.csproj
```

**Structure:**
```
Models/
??? Entities/
?   ??? Patient.cs
?   ??? Vaccine.cs
?   ??? Invoice.cs
??? DTOs/
    ??? PatientDTO.cs
    ??? VaccineDTO.cs
```

---

## ?? Dependency Flow

```
TPVAXWinform (Entry Point)
    ? references
GUI (UI Layer)
    ? references
Business (Business Logic)
    ? references
Data (Data Access)
    ? references
Models (Shared)
```

**Rules:**
- ? Upper layers can reference lower layers
- ? Lower layers CANNOT reference upper layers
- ? All layers can reference Models

---

## ?? Troubleshooting

### L?i: "The type or namespace name 'frmMain' could not be found"

**Fix:**
```csharp
// Program.cs
using GUI.Forms; // Thêm using này

Application.Run(new frmMain());
```

### L?i: "Could not load file or assembly 'GUI'"

**Fix:**
1. Rebuild `GUI` project first
2. Rebuild `TPVAXWinform`
3. Check references

### L?i: Designer không m? ???c

**Fix:**
1. Close designer
2. Rebuild solution
3. Reopen designer

### L?i: Resources not found

**Fix:**
1. Copy `Properties/Resources.resx` sang `GUI` project
2. Set **Build Action** = **Embedded Resource**
3. Rebuild

---

## ? Checklist

- [ ] T?o GUI Class Library project
- [ ] Add references (System.Windows.Forms, System.Drawing)
- [ ] Install LiveCharts packages
- [ ] Di chuy?n Forms vào GUI/Forms/
- [ ] Di chuy?n UserControls vào GUI/UserControls/
- [ ] Di chuy?n CustomControls vào GUI/CustomControls/
- [ ] Add project reference t? TPVAXWinform ? GUI
- [ ] Update Program.cs v?i `using GUI.Forms;`
- [ ] Copy resources n?u c?n
- [ ] Rebuild solution (0 errors)
- [ ] Test ?ng d?ng ch?y OK
- [ ] Commit to Git

---

## ?? Hoàn thành!

Sau khi hoàn thành, b?n s? có:
- ? Architecture N-tier chu?n
- ? UI Layer tách bi?t hoàn toàn
- ? D? dàng thêm Business/Data layers sau
- ? Code organization t?t h?n
- ? Chu?n b? t?t cho team work

**Chúc b?n thành công!** ??
