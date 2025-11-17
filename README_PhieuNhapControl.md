# H??ng d?n cài ??t và s? d?ng PhieuNhapControl

## T?ng quan
?ã t?o thành công các file sau ?? qu?n lý Phi?u Nh?p Vaccine:

### 1. Files ?ã t?o

#### User Control
- `TPVAXWinform_GUI\UserControls\PhieuNhapControl.cs`
- `TPVAXWinform_GUI\UserControls\PhieuNhapControl.Designer.cs`
- `TPVAXWinform_GUI\UserControls\PhieuNhapControl.resx`

#### Form Chi Ti?t
- `TPVAXWinform_GUI\Forms\frmChiTietPhieuNhap.cs`
- `TPVAXWinform_GUI\Forms\frmChiTietPhieuNhap.Designer.cs`
- `TPVAXWinform_GUI\Forms\frmChiTietPhieuNhap.resx`

#### Business Logic Layer
- `TPVAXWinform_BLL\ChiTietPhieuNhapBLL.cs` (?ã c?p nh?t)

#### Data Access Layer
- `TPVAXWinform_DAL\ChiTietPhieuNhapDAL.cs` (?ã c?p nh?t)

#### SQL Script
- `SQL_Scripts\usp_ChiTietPhieuNhap_GetByMaPN.sql`

## 2. Cài ??t Database

### B??c 1: Ch?y Stored Procedure
B?n c?n ch?y file SQL sau trong SQL Server Management Studio:

```sql
-- File: SQL_Scripts\usp_ChiTietPhieuNhap_GetByMaPN.sql

CREATE PROCEDURE dbo.usp_ChiTietPhieuNhap_GetByMaPN
    @MaPN NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ct.MaCTPN AS [Mã Chi Ti?t],
        ct.MaVC AS [Mã Vaccine],
        vc.TenVC AS [Tên Vaccine],
    ct.NuocSanXuat AS [N??c S?n Xu?t],
        ct.SoLuong AS [S? L??ng],
 ct.GiaNhap AS [Giá Nh?p],
ct.HanSuDung AS [H?n S? D?ng],
    (ct.SoLuong * ct.GiaNhap) AS [Thành Ti?n]
    FROM
        dbo.ChiTietPhieuNhap AS ct
    LEFT JOIN
      dbo.Vaccine AS vc ON ct.MaVC = vc.MaVC
    WHERE
      ct.MaPN = @MaPN
    ORDER BY
        ct.MaCTPN;
END
GO
```

### B??c 2: Ki?m tra Stored Procedure ?ã có
??m b?o stored procedure `dbo.usp_PhieuNhap_GetAllWithDetails` ?ã ???c t?o (theo yêu c?u c?a b?n).

## 3. Tính n?ng ?ã tri?n khai

### PhieuNhapControl (User Control)
? **Giao di?n t??ng t? VaccineControl:**
- Panel tiêu ?? màu xanh v?i title "QU?N LÝ PHI?U NH?P VACCINE"
- Panel l?c v?i các tính n?ng:
  - Tìm ki?m theo Mã Phi?u Nh?p, Tên Nhân Viên, Tên Nhà Cung C?p
  - L?c theo kho?ng th?i gian (T? ngày - ??n ngày) v?i DateTimePicker có checkbox
  - Nút "??t l?i" ?? reset b? l?c
- DataGridView hi?n th? danh sách phi?u nh?p v?i các c?t:
  - Mã Phi?u Nh?p
  - Ngày L?p
  - **Tên Nhân Viên** (thay vì MaNV)
  - **Tên Nhà Cung C?p** (thay vì MaNCC)

? **Ch?c n?ng chu?t ph?i xem chi ti?t:**
- Click chu?t ph?i vào b?t k? dòng phi?u nh?p nào
- Form chi ti?t phi?u nh?p s? hi?n th?

### frmChiTietPhieuNhap (Form)
? **Thông tin ??y ?? c?a phi?u nh?p:**
- Mã Phi?u Nh?p
- Ngày L?p
- Nhân Viên l?p phi?u
- Nhà Cung C?p
- T?ng Ti?n (tính t? ??ng)

? **Chi ti?t phi?u nh?p trong DataGridView:**
- Mã Chi Ti?t
- Mã Vaccine
- Tên Vaccine
- N??c S?n Xu?t
- S? L??ng
- Giá Nh?p (??nh d?ng ti?n t?)
- H?n S? D?ng
- Thành Ti?n (tính t? ??ng, ??nh d?ng ti?n t?)

## 4. Cách s? d?ng

### ?? s? d?ng PhieuNhapControl trong form chính:

```csharp
// Trong form chính c?a b?n (ví d?: frmMain)
private void ShowPhieuNhapControl()
{
    PhieuNhapControl phieuNhapControl = new PhieuNhapControl();
    phieuNhapControl.Dock = DockStyle.Fill;
    
    // Clear panel và add control
    yourPanel.Controls.Clear();
    yourPanel.Controls.Add(phieuNhapControl);
}
```

### Xem chi ti?t phi?u nh?p:
1. M? PhieuNhapControl
2. Click chu?t ph?i vào dòng phi?u nh?p mu?n xem
3. Form chi ti?t s? t? ??ng hi?n th? v?i ??y ?? thông tin

## 5. L?u ý k? thu?t

### Proc SQL:
- `dbo.usp_PhieuNhap_GetAllWithDetails`: L?y danh sách phi?u nh?p kèm tên NV và NCC
- `dbo.usp_ChiTietPhieuNhap_GetByMaPN`: L?y chi ti?t phi?u nh?p theo mã phi?u

### Tên c?t trong DataTable:
- Stored procedure tr? v? c?t có tên ti?ng Vi?t có d?u
- Ví d?: `[Mã Phi?u Nh?p]`, `[Tên Nhân Viên L?p]`, `[Tên Nhà Cung C?p]`
- DataPropertyName ph?i match chính xác v?i tên c?t này

### Format d? li?u:
- Giá ti?n: Format "N0" (ng?n cách hàng nghìn)
- Ngày tháng: Format "dd/MM/yyyy"
- Tính toán: Thành Ti?n = S? L??ng × Giá Nh?p

## 6. Màu s?c và Style

### Gi?ng v?i VaccineControl:
- **Màu tiêu ??:** RGB(41, 128, 185) - Xanh d??ng
- **Màu filter panel:** RGB(236, 240, 241) - Xám nh?t
- **Màu header DataGridView:** RGB(52, 73, 94) - Xám ??m
- **Màu nút "??t l?i":** Gray
- **Màu selection:** RGB(189, 195, 199)
- **Font:** Segoe UI

## 7. Ki?m tra hoàn thành

? Build thành công
? Giao di?n t??ng t? các UserControl khác
? Hi?n th? Tên Nhân Viên thay vì MaNV
? Hi?n th? Tên Nhà Cung C?p thay vì MaNCC
? Click chu?t ph?i hi?n th? chi ti?t
? Form chi ti?t có ??y ?? thông tin phi?u nh?p
? Chi ti?t phi?u nh?p hi?n th? trong DataGridView

## 8. Các b??c ti?p theo

1. Ch?y stored procedure `usp_ChiTietPhieuNhap_GetByMaPN` trong database
2. Thêm PhieuNhapControl vào menu/navigation c?a ?ng d?ng
3. Test ch?c n?ng click chu?t ph?i và xem chi ti?t
4. Ki?m tra hi?n th? d? li?u

Chúc b?n thành công! ??
