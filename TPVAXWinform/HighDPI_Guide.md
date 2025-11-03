# H??ng d?n c?i thi?n ch?t l??ng hi?n th? WinForms

## V?n ??
Text và hình ?nh b? m?, không s?c nét khi ch?y ?ng d?ng WinForms trên màn hình có DPI cao (High DPI).

## Gi?i pháp ?ã áp d?ng

### 1. **B?t DPI Awareness trong Program.cs**
```csharp
[System.Runtime.InteropServices.DllImport("user32.dll")]
private static extern bool SetProcessDPIAware();

static void Main()
{
    if (Environment.OSVersion.Version.Major >= 6)
    {
        SetProcessDPIAware();
    }
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.Run(new frmMain());
}
```

**L?i ích**: Cho phép ?ng d?ng nh?n bi?t và x? lý DPI c?a màn hình.

### 2. **Thêm app.manifest**
File `app.manifest` ?ã ???c t?o v?i c?u hình:
```xml
<dpiAwareness>PerMonitorV2</dpiAwareness>
<dpiAware>true</dpiAware>
```

**L?i ích**: 
- `PerMonitorV2`: H? tr? DPI khác nhau trên nhi?u màn hình (Windows 10 Creators Update+)
- `dpiAware`: T??ng thích ng??c v?i Windows 7/8

### 3. **C?i thi?n rendering trong frmMain.cs**

#### a. Double Buffering
```csharp
this.SetStyle(ControlStyles.OptimizedDoubleBuffer | 
ControlStyles.AllPaintingInWmPaint | 
      ControlStyles.UserPaint, true);
```
**L?i ích**: Gi?m flickering, làm m??t chuy?n ??ng.

#### b. Text Rendering
```csharp
label.UseCompatibleTextRendering = false;
button.UseCompatibleTextRendering = false;
```
**L?i ích**: S? d?ng GDI+ ?? render text rõ nét h?n.

#### c. Chart Anti-Aliasing
```csharp
chart1.AntiAliasing = AntiAliasingStyles.All;
chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
```
**L?i ích**: Làm m??t các ???ng v? và text trong bi?u ??.

## Cách áp d?ng vào project

### B??c 1: Thêm manifest vào project
1. M? Visual Studio
2. Right-click vào project `TPVAXWinform` trong Solution Explorer
3. Ch?n **Add** ? **Existing Item**
4. Ch?n file `app.manifest` ?ã t?o
5. Ho?c: Right-click project ? **Properties** ? **Application** tab
6. Trong ph?n **Resources**, ch?n **Manifest** ? ch?n `app.manifest`

### B??c 2: Rebuild project
```
Build ? Rebuild Solution
```

### B??c 3: Test trên các ?? phân gi?i khác nhau
- Màn hình 100% DPI (1920x1080)
- Màn hình 125% DPI
- Màn hình 150% DPI
- Màn hình 4K (3840x2160)

## K?t qu? mong ??i

? **Text s?c nét**: Font ch? rõ ràng, không b? m?
? **Icons rõ nét**: Logo và icons không b? pixelated
? **Charts ??p**: Bi?u ?? ???c làm m??t v?i anti-aliasing
? **Buttons ??p**: MenuButton custom control hi?n th? bo góc m??t mà
? **No flickering**: Không b? nh?p nháy khi resize

## L?u ý quan tr?ng

### 1. **Ki?m tra Windows version**
- Windows 10 Creators Update tr? lên: Dùng `PerMonitorV2`
- Windows 7/8/8.1: Fallback v? `dpiAware`

### 2. **Font ch?n l?a**
Nên s? d?ng các font ???c t?i ?u cho màn hình:
- ? Segoe UI (?ang dùng)
- ? Calibri
- ? Arial
- ? Tránh: Times New Roman, Comic Sans

### 3. **Image Resources**
Nên có nhi?u kích th??c cho cùng 1 ?nh:
- `logo.png` (100%)
- `logo@1.5x.png` (150%)
- `logo@2x.png` (200%)
- `logo@3x.png` (300% - cho 4K)

### 4. **Icon Fonts**
Segoe MDL2 Assets ???c t?i ?u t?t cho High DPI.

## Troubleshooting

### V?n ??: Text v?n b? m?
**Gi?i pháp**:
1. ??m b?o `UseCompatibleTextRendering = false`
2. Ki?m tra Windows DPI settings: 
 - Settings ? System ? Display ? Scale and layout
3. Restart Visual Studio sau khi thêm manifest

### V?n ??: Controls b? resize sai
**Gi?i pháp**:
1. S? d?ng `TableLayoutPanel` thay vì hard-code position
2. S? d?ng `Dock` và `Anchor` properties
3. Tránh dùng absolute positioning

### V?n ??: Charts b? v? layout
**Gi?i pháp**:
```csharp
chart.Dock = DockStyle.Fill;
chart.ChartAreas[0].RecalculateAxesScale();
```

## Thêm c?i ti?n (Optional)

### 1. Thêm ClearType font smoothing
```csharp
[System.Runtime.InteropServices.DllImport("gdi32.dll")]
private static extern int SetTextRenderingHint(IntPtr hdc, int mode);
```

### 2. T?i ?u cho màn hình 4K
```csharp
if (this.DeviceDpi > 96) // 96 DPI = 100%
{
    // Scale custom graphics
    float scaleFactor = this.DeviceDpi / 96f;
    // Apply scaling...
}
```

### 3. Smooth scrolling
```csharp
protected override CreateParams CreateParams
{
    get
    {
        CreateParams cp = base.CreateParams;
     cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
     return cp;
    }
}
```

## Tài li?u tham kh?o
- [High DPI Desktop Application Development on Windows](https://docs.microsoft.com/en-us/windows/win32/hidpi/high-dpi-desktop-application-development-on-windows)
- [WinForms High DPI Support](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/high-dpi-support-in-windows-forms)
- [Chart Control Anti-Aliasing](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.datavisualization.charting.chart)

## Checklist hoàn thành

- ? SetProcessDPIAware() trong Program.cs
- ? app.manifest v?i DPI awareness
- ? Double buffering cho form
- ? UseCompatibleTextRendering = false
- ? Chart anti-aliasing
- ? TableLayoutPanel cho responsive layout
- ? Segoe UI font
- ? Custom MenuButton v?i smooth rendering

?ng d?ng gi? ?ây s? hi?n th? s?c nét và ??p m?t trên m?i ?? phân gi?i màn hình! ??
