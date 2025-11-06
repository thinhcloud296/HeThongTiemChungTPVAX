# H??ng d?n s? d?ng MenuButton Custom Control

## Tính n?ng c?a MenuButton

MenuButton là custom control d?a trên Button v?i các tính n?ng nâng cao:

### 1. **Hi?u ?ng màu s?c**
- Màu n?n m?c ??nh (BackgroundColor)
- Màu n?n khi hover chu?t (HoverBackgroundColor)
- Màu n?n khi nh?n (PressedBackgroundColor)
- Góc bo tròn (BorderRadius)
- Vi?n (BorderSize, BorderColor)

### 2. **Icons**
- H? tr? icon text t? font Segoe MDL2 Assets
- Màu icon tùy ch?nh (IconColor)
- Kích th??c icon tùy ch?nh (IconFont)

### 3. **Responsive**
- T? ??ng co dãn theo container
- Hi?u ?ng m??t mà v?i Anti-aliasing

## Cách s? d?ng

### Trong Designer (?ã ???c áp d?ng):

```csharp
this.button1.BackgroundColor = System.Drawing.Color.FromArgb(41, 128, 185);
this.button1.HoverBackgroundColor = System.Drawing.Color.FromArgb(52, 152, 219);
this.button1.PressedBackgroundColor = System.Drawing.Color.FromArgb(31, 97, 141);
this.button1.BorderRadius = 8;
this.button1.Text = "H? S? TIÊM CH?NG";
this.button1.IconText = ""; // Thêm icon n?u mu?n
```

### Trong Code:

```csharp
// T?o button m?i
var menuBtn = new MenuButton();
menuBtn.Text = "Button Text";
menuBtn.BackgroundColor = Color.FromArgb(41, 128, 185);
menuBtn.HoverBackgroundColor = Color.FromArgb(52, 152, 219);
menuBtn.BorderRadius = 10;

// Thêm icon (Segoe MDL2 Assets)
menuBtn.IconText = "\uE10F"; // Icon Home
menuBtn.IconFont = new Font("Segoe MDL2 Assets", 18F);
```

## M?t s? Icon ph? bi?n t? Segoe MDL2 Assets:

| Icon | Unicode | Mô t? |
|------|---------|-------|
| ?? | \uE10F | Home |
| ?? | \uE13D | Contact |
| ?? | \uE950 | Health |
| ?? | \uE160 | Document |
| ?? | \uE9D2 | Chart |
| ?? | \uE136 | Building |
| ? | \uE115 | Settings |
| ?? | \uEB8F | Gift/Promotion |

## B?ng màu ?? xu?t:

### Màu xanh d??ng (?ang dùng):
- Normal: RGB(41, 128, 185) - #2980b9
- Hover: RGB(52, 152, 219) - #3498db
- Pressed: RGB(31, 97, 141) - #1f618d

### Màu xanh lá:
- Normal: RGB(39, 174, 96) - #27ae60
- Hover: RGB(46, 204, 113) - #2ecc71
- Pressed: RGB(30, 132, 73) - #1e8449

### Màu cam:
- Normal: RGB(230, 126, 34) - #e67e22
- Hover: RGB(243, 156, 18) - #f39c12
- Pressed: RGB(175, 96, 26) - #af601a

### Màu ??:
- Normal: RGB(192, 57, 43) - #c0392b
- Hover: RGB(231, 76, 60) - #e74c3c
- Pressed: RGB(146, 43, 33) - #922b21

### Màu tím:
- Normal: RGB(142, 68, 173) - #8e44ad
- Hover: RGB(155, 89, 182) - #9b59b6
- Pressed: RGB(108, 52, 131) - #6c3483

## Ví d? áp d?ng cho các button:

```csharp
// Button 1 - H? s? tiêm ch?ng (màu xanh d??ng)
button1.BackgroundColor = Color.FromArgb(41, 128, 185);
button1.IconText = "\uE13D"; // Contact icon

// Button 2 - Khách hàng (màu xanh lá)
button2.BackgroundColor = Color.FromArgb(39, 174, 96);
button2.HoverBackgroundColor = Color.FromArgb(46, 204, 113);
button2.PressedBackgroundColor = Color.FromArgb(30, 132, 73);
button2.IconText = "\uE716"; // People icon

// Button 3 - Vaccine (màu cam)
button3.BackgroundColor = Color.FromArgb(230, 126, 34);
button3.HoverBackgroundColor = Color.FromArgb(243, 156, 18);
button3.PressedBackgroundColor = Color.FromArgb(175, 96, 26);
button3.IconText = "\uE950"; // Health icon

// Button 4 - Nhà cung c?p (màu tím)
button4.BackgroundColor = Color.FromArgb(142, 68, 173);
button4.HoverBackgroundColor = Color.FromArgb(155, 89, 182);
button4.PressedBackgroundColor = Color.FromArgb(108, 52, 131);
button4.IconText = "\uE136"; // Building icon

// Button 5 - Nhân viên (màu xanh d??ng nh?t)
button5.BackgroundColor = Color.FromArgb(52, 73, 94);
button5.IconText = "\uE716"; // People icon

// Button 6 - Hóa ??n (màu xanh ??m)
button6.BackgroundColor = Color.FromArgb(44, 62, 80);
button6.IconText = "\uE160"; // Document icon

// Button 7 - Khuy?n mãi (màu h?ng)
button7.BackgroundColor = Color.FromArgb(231, 76, 60);
button7.HoverBackgroundColor = Color.FromArgb(236, 112, 99);
button7.PressedBackgroundColor = Color.FromArgb(176, 58, 46);
button7.IconText = "\uEB8F"; // Gift icon

// Button 8 - Th?ng kê (màu xanh lá ??m)
button8.BackgroundColor = Color.FromArgb(22, 160, 133);
button8.HoverBackgroundColor = Color.FromArgb(26, 188, 156);
button8.PressedBackgroundColor = Color.FromArgb(17, 122, 101);
button8.IconText = "\uE9D2"; // Chart icon
```

## Ghi chú:
- T?t c? các buttons ?ã ???c thi?t l?p v?i Dock = Fill ?? t? ??ng co dãn
- Font ch?: Segoe UI, 10pt, Bold
- Font icon: Segoe MDL2 Assets, 16pt
- Border radius m?c ??nh: 8px
- Padding: 50px (left) ?? có ch? cho icon

B?n có th? tùy ch?nh thêm trong file Designer.cs ho?c trong code-behind!
