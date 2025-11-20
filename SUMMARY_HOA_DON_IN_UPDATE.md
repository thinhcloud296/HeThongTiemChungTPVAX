# ? C?P NH?T THÀNH CÔNG: HÓA ??N IN HI?N TH? KHUY?N MÃI

## ?? Yêu c?u
Hi?n th? **Giá g?c**, **Giá sau khuy?n mãi**, và **Ti?n gi?m** trên hóa ??n in.

## ? ?ã th?c hi?n

### 1. Code Changes
- ? **HoaDonInDTO.cs**: Thêm 3 properties m?i
  - `GiaGoc` - Giá niêm y?t ban ??u
  - `DonGia` - Giá sau khuy?n mãi (giá th?c t?)
  - `TienGiam` - S? ti?n ???c gi?m

### 2. Files m?i t?o
- ? **usp_Report_GetHoaDonIn.sql**: Stored procedure tính giá
- ? **HUONG_DAN_HOA_DON_IN_KHUYEN_MAI.md**: H??ng d?n chi ti?t

### 3. Build Status
- ? Build successful - No errors

---

## ?? B??c ti?p theo (B?T BU?C)

### B??c 1: Ch?y SQL Script
```sql
-- M? file: usp_Report_GetHoaDonIn.sql
-- Execute trong SQL Server Management Studio
```

### B??c 2: C?p nh?t RDLC Report
1. M? `rptHoaDon.rdlc` trong Visual Studio
2. C?p nh?t Dataset (thêm 3 fields: GiaGoc, DonGia, TienGiam)
3. Thêm c?t "Gi?m giá" vào b?ng chi ti?t
4. Thêm dòng "T?ng gi?m giá" ? cu?i hóa ??n

**Chi ti?t**: Xem file `HUONG_DAN_HOA_DON_IN_KHUYEN_MAI.md`

---

## ?? Ví d? hi?n th?

**TR??C:**
```
S?n ph?m        | SL | ??n giá    | Thành ti?n
Vaccine HPV     | 2  | 2.816.750? | 5.633.500?
```

**SAU:**
```
S?n ph?m    | SL | Giá g?c    | Gi?m giá  | ??n giá    | Thành ti?n
Vaccine HPV     | 2  | 2.965.000? | -148.250? | 2.816.750? | 5.633.500?
      ?????????????????????????????????
            T?ng gi?m giá: -296.500?
  T?ng c?ng:   5.633.500?
```

---

## ?? Test
1. T?o hóa ??n cho vaccine có khuy?n mãi
2. In hóa ??n
3. Ki?m tra hi?n th? ?úng: Giá g?c, Gi?m giá, ??n giá

---

## ?? Tài li?u tham kh?o
- **H??ng d?n chi ti?t**: `HUONG_DAN_HOA_DON_IN_KHUYEN_MAI.md`
- **SQL Script**: `usp_Report_GetHoaDonIn.sql`
- **DTO**: `TPVAXWinform_DTO\HoaDonInDTO.cs`

---

? **Status**: Code changes completed. Next: Update RDLC report manually.
