USE QLTIEMCHUNG
GO

-- =================================================================================
-- KIỂM TRA VÀ SỬA LỖI LOAD DỮ LIỆU
-- =================================================================================

PRINT '=== BẮT ĐẦU KIỂM TRA ==='
GO

-- 1. Kiểm tra dữ liệu hiện tại
PRINT 'Kiểm tra dữ liệu GoiVaccine...'
SELECT COUNT(*) AS 'Số gói vaccine' FROM GoiVaccine
SELECT MaGoi, TenGoi, TrangThai, HinhAnh FROM GoiVaccine
GO

PRINT 'Kiểm tra dữ liệu KhuyenMai...'
SELECT COUNT(*) AS 'Số khuyến mãi' FROM KhuyenMai
SELECT MaKM, TenKM, TrangThai, HinhAnh FROM KhuyenMai
GO

-- 2. Kiểm tra cột HinhAnh
PRINT 'Kiểm tra cột HinhAnh...'
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('GoiVaccine') AND name = 'HinhAnh')
BEGIN
    PRINT '❌ Cột HinhAnh CHƯA TỒN TẠI trong GoiVaccine - ĐANG THÊM...'
    ALTER TABLE GoiVaccine ADD HinhAnh NVARCHAR(255) NULL
    PRINT '✅ Đã thêm cột HinhAnh vào GoiVaccine'
END
ELSE
BEGIN
    PRINT '✅ Cột HinhAnh đã tồn tại trong GoiVaccine'
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('KhuyenMai') AND name = 'HinhAnh')
BEGIN
    PRINT '❌ Cột HinhAnh CHƯA TỒN TẠI trong KhuyenMai - ĐANG THÊM...'
    ALTER TABLE KhuyenMai ADD HinhAnh NVARCHAR(255) NULL
    PRINT '✅ Đã thêm cột HinhAnh vào KhuyenMai'
END
ELSE
BEGIN
    PRINT '✅ Cột HinhAnh đã tồn tại trong KhuyenMai'
END
GO

-- 3. Cập nhật đường dẫn ảnh
PRINT 'Cập nhật đường dẫn ảnh...'

UPDATE GoiVaccine SET HinhAnh = 'GOI00001.jpg' WHERE MaGoi = 'GOI0000001'
UPDATE GoiVaccine SET HinhAnh = 'GOI00002.jpg' WHERE MaGoi = 'GOI0000002'
UPDATE GoiVaccine SET HinhAnh = 'GOI00003.jpg' WHERE MaGoi = 'GOI0000003'
UPDATE GoiVaccine SET HinhAnh = 'GOI00004.jpg' WHERE MaGoi = 'GOI0000004'

UPDATE KhuyenMai SET HinhAnh = 'KM000001.jpg' WHERE MaKM = 'KM00000001'
UPDATE KhuyenMai SET HinhAnh = 'KM000002.jpg' WHERE MaKM = 'KM00000002'

PRINT '✅ Đã cập nhật đường dẫn ảnh'
GO

-- 4. Kiểm tra lại sau khi update
PRINT ''
PRINT '=== KẾT QUẢ SAU KHI CẬP NHẬT ==='

PRINT 'Gói Vaccine:'
SELECT MaGoi, TenGoi, GiaGoi, TrangThai, HinhAnh 
FROM GoiVaccine
ORDER BY MaGoi

PRINT ''
PRINT 'Khuyến Mãi:'
SELECT MaKM, TenKM, LoaiKM, GiaTriGiam, KieuGiam, TrangThai, HinhAnh,
       NgayBatDau, NgayKetThuc
FROM KhuyenMai
ORDER BY MaKM

PRINT ''
PRINT '=== HOÀN TẤT ==='
PRINT 'Bây giờ hãy chạy lại website và kiểm tra trang /GoiVaccine và /KhuyenMai'
GO
