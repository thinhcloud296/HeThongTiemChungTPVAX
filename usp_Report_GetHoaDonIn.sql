-- =============================================
-- Stored Procedure: L?y d? li?u in hóa ??n
-- Bao g?m: Giá g?c, Giá sau KM, Ti?n gi?m
-- =============================================
CREATE OR ALTER PROCEDURE dbo.usp_Report_GetHoaDonIn
    @MaHD CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        -- Thông tin hóa ??n
      hd.MaHD,
    hd.NgayLap,
  hd.TongTien AS TongTienHoaDon,
        
        -- Thông tin khách hàng
        kh.HoTen AS TenKhachHang,
        kh.SoDT AS SDTKhachHang,
        kh.DiaChi AS DiaChiKhachHang,
      
   -- Thông tin thu ngân
        nv.HoTen AS TenThuNgan,
        
   -- Thông tin chi ti?t s?n ph?m
      CASE 
          WHEN cthd.LoaiSanPham = 'VACCINE' THEN vc.TenVC
      WHEN cthd.LoaiSanPham = 'GOIVACCINE' THEN gvc.TenGoi
 ELSE N'Không xác ??nh'
     END AS TenSanPham,
        
        cthd.SoLuong,
        
        -- === THÊM M?I: Tính giá g?c và giá sau khuy?n mãi ===
        
      -- 1. GiaGoc: L?y giá g?c t? b?ng Vaccine ho?c GoiVaccine
        CASE 
         WHEN cthd.LoaiSanPham = 'VACCINE' THEN vc.GiaBan
            WHEN cthd.LoaiSanPham = 'GOIVACCINE' THEN gvc.GiaGoi
    ELSE 0
        END AS GiaGoc,
        
        -- 2. DonGia: Giá ?Ã ???C L?U trong ChiTietHoaDon (giá sau KM)
      cthd.DonGia AS DonGia,
        
        -- 3. TienGiam: S? ti?n ???c gi?m = (GiaGoc - DonGia)
        (
    CASE 
   WHEN cthd.LoaiSanPham = 'VACCINE' THEN vc.GiaBan
         WHEN cthd.LoaiSanPham = 'GOIVACCINE' THEN gvc.GiaGoi
   ELSE 0
            END - cthd.DonGia
   ) AS TienGiam,
        
  -- === K?T THÚC THÊM M?I ===
      
   -- 4. ThanhTien: Tính t? giá sau KM * s? l??ng
        (cthd.SoLuong * cthd.DonGia) AS ThanhTien
   
    FROM 
        dbo.HoaDon hd
    
    -- Join b?ng khách hàng
  LEFT JOIN dbo.KhachHang kh ON hd.MaKH = kh.MaKH
    
    -- Join b?ng nhân viên (thu ngân)
    LEFT JOIN dbo.NhanVien nv ON hd.MaNV = nv.MaNV
    
    -- Join chi ti?t hóa ??n
    INNER JOIN dbo.ChiTietHoaDon cthd ON hd.MaHD = cthd.MaHD
    
    -- Join Vaccine (n?u là vaccine l?)
    LEFT JOIN dbo.Vaccine vc ON cthd.LoaiSanPham = 'VACCINE' 
        AND cthd.MaSanPham = vc.MaVC
    
    -- Join GoiVaccine (n?u là gói)
    LEFT JOIN dbo.GoiVaccine gvc ON cthd.LoaiSanPham = 'GOIVACCINE' 
        AND cthd.MaSanPham = gvc.MaGoi
    
    WHERE 
        hd.MaHD = @MaHD
    
    ORDER BY 
        cthd.MaCTHD; -- S?p x?p theo th? t? chi ti?t hóa ??n
END
GO

-- =============================================
-- TEST STORED PROCEDURE
-- =============================================
-- EXEC dbo.usp_Report_GetHoaDonIn @MaHD = 'HDON000001'
