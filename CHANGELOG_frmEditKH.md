# Tóm t?t thay ??i - Form S?a Khách Hàng (frmEditKH)

## 1. C?i thi?n Giao Di?n (Designer)
- **Kích th??c Form**: 800x600 pixels
- **C?n gi?a màn hình**: `StartPosition = FormStartPosition.CenterScreen`
- **Header**: Thanh tiêu ?? xanh (RGB: 41, 128, 185) v?i text "S?A THÔNG TIN KHÁCH HÀNG"
- **Layout**: S? d?ng `TableLayoutPanel` (2 c?t: 30% label, 70% input)
- **Các field**:
  - Mã KH (readonly)
- H? tên (textbox r?ng)
  - CCCD (textbox)
  - Ngày sinh (DateTimePicker, ??nh d?ng dd/MM/yyyy)
  - Gi?i tính (ComboBox: Nam/N?/Khác)
  - ??a ch? (TextBox multiline)
  - S? ?i?n tho?i (textbox)
  - Email (textbox)
  - Mã TK (readonly)
- **Nút hành ??ng**: C?p nh?t (xanh) và H?y (xám), c?n gi?a d??i cùng

## 2. Ch?c n?ng (Code-behind)
- **LoadKhachHangData(string maKH)**: 
  - L?y d? li?u khách hàng t? `KhachHangBLL.GetData()`
  - Tìm khách hàng theo MaKH
  - Hi?n th? t?t c? thông tin vào form
  - L?u vào `currentKhachHang` DTO
  
- **BtnUpdate_Click**:
  - Ki?m tra d? li?u h?p l? (H? tên, Gi?i tính)
  - C?p nh?t DTO t? các textbox
  - G?i `KhachHangBLL.Edit()` ?? l?u vào DB
  - Hi?n th? thông báo và ?óng form v?i `DialogResult.OK`
  
- **BtnCancel_Click**:
  - ?óng form v?i `DialogResult.Cancel`

## 3. Tích h?p vào HoSoTiemChungControl.cs
- **EditInfo_Click_KH** (Context menu - S?a thông tin):
  - L?y MaKH t? dòng ???c ch?n
  - T?o instance `frmEditKH`
  - G?i `LoadKhachHangData(maKH)`
  - G?i `ShowDialog()`
  - N?u `DialogResult == OK`: refresh danh sách khách hàng

## 4. Quy trình s? d?ng
1. Nhân viên click chu?t ph?i vào khách hàng trong dgvKhachHang
2. Ch?n "S?a thông tin" t? context menu
3. Form frmEditKH m? v?i d? li?u khách hàng ???c t?i
4. Nhân viên thay ??i thông tin c?n thi?t
5. Click "C?p nh?t" ?? l?u ho?c "H?y" ?? thoát
6. Danh sách khách hàng t? ??ng refresh

## 5. Công ngh? s? d?ng
- C# 7.3
- .NET Framework 4.8.1
- Windows Forms
- TPVAXWinform_BLL (KhachHangBLL)
- TPVAXWinform_DTO (KhachHangDTO)
