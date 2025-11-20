USE QLTIEMCHUNG
GO

-- =================================================================================
-- SỬA TÊN ẢNH ĐỂ KHỚP VỚI FILE THỰC TẾ
-- =================================================================================

PRINT '=== BẮT ĐẦU SỬA TÊN ẢNH ==='
GO

-- Kiểm tra bảng GoiVaccine có cột HinhAnh chưa
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('GoiVaccine') AND name = 'HinhAnh')
BEGIN
    PRINT 'Thêm cột HinhAnh vào bảng GoiVaccine...'
    ALTER TABLE GoiVaccine ADD HinhAnh NVARCHAR(255) NULL
    PRINT '-> Đã thêm cột HinhAnh'
END
ELSE
BEGIN
    PRINT '-> Cột HinhAnh đã tồn tại'
END
GO

-- Kiểm tra bảng KhuyenMai có cột HinhAnh chưa
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('KhuyenMai') AND name = 'HinhAnh')
BEGIN
    PRINT 'Thêm cột HinhAnh vào bảng KhuyenMai...'
    ALTER TABLE KhuyenMai ADD HinhAnh NVARCHAR(255) NULL
    PRINT '-> Đã thêm cột HinhAnh'
END
ELSE
BEGIN
    PRINT '-> Cột HinhAnh đã tồn tại'
END
GO

-- =================================================================================
-- CẬP NHẬT ĐƯỜNG DẪN ẢNH CHO GÓI VACCINE
-- Tên file thực tế: GOI00001.jpg, GOI00002.jpg, GOI00003.jpg, GOI00004.jpg
-- =================================================================================

PRINT 'Cập nhật đường dẫn ảnh cho Gói Vaccine...'

UPDATE GoiVaccine SET HinhAnh = 'GOI00001.jpg' WHERE MaGoi = 'GOI0000001'
UPDATE GoiVaccine SET HinhAnh = 'GOI00002.jpg' WHERE MaGoi = 'GOI0000002'
UPDATE GoiVaccine SET HinhAnh = 'GOI00003.jpg' WHERE MaGoi = 'GOI0000003'
UPDATE GoiVaccine SET HinhAnh = 'GOI00004.jpg' WHERE MaGoi = 'GOI0000004'

PRINT '-> Đã cập nhật ảnh cho 4 gói vaccine'
GO

-- =================================================================================
-- CẬP NHẬT ĐƯỜNG DẪN ẢNH CHO KHUYẾN MÃI
-- Tên file thực tế: KM000001.jpg, KM000002.jpg
-- =================================================================================

PRINT 'Cập nhật đường dẫn ảnh cho Khuyến Mãi...'

UPDATE KhuyenMai SET HinhAnh = 'KM000001.jpg' WHERE MaKM = 'KM00000001'
UPDATE KhuyenMai SET HinhAnh = 'KM000002.jpg' WHERE MaKM = 'KM00000002'

PRINT '-> Đã cập nhật ảnh cho 2 chương trình khuyến mãi'
GO

-- =================================================================================
-- KIỂM TRA KẾT QUẢ
-- =================================================================================

PRINT ''
PRINT '=== KIỂM TRA KẾT QUẢ ==='

SELECT MaGoi, TenGoi, HinhAnh 
FROM GoiVaccine
ORDER BY MaGoi

PRINT ''

SELECT MaKM, TenKM, HinhAnh 
FROM KhuyenMai
ORDER BY MaKM

PRINT ''
PRINT '=== HOÀN TẤT ==='
GO
