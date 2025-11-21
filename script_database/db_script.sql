
DROP TABLE IF EXISTS ChiTietHoaDon;
DROP TABLE IF EXISTS ChiTietKhuyenMai;
DROP TABLE IF EXISTS ChiTietPhieuNhap;
DROP TABLE IF EXISTS ChiTietGoiVaccine;
DROP TABLE IF EXISTS VaccinePhongBenh;
DROP TABLE IF EXISTS GioHang;
DROP TABLE IF EXISTS HoaDon;
DROP TABLE IF EXISTS PhieuNhapVaccine;
DROP TABLE IF EXISTS LichTiem;
DROP TABLE IF EXISTS LienKetHoSo;
DROP TABLE IF EXISTS Vaccine;
DROP TABLE IF EXISTS HoSoTiemChung;
DROP TABLE IF EXISTS GoiVaccine;
DROP TABLE IF EXISTS KhuyenMai;
DROP TABLE IF EXISTS LoaiVaccine;
DROP TABLE IF EXISTS LoaiBenh;
DROP TABLE IF EXISTS NhaCungCap;
DROP TABLE IF EXISTS NhanVien;
DROP TABLE IF EXISTS KhachHang;
DROP TABLE IF EXISTS TaiKhoan;
GO

-- =================================================================================
-- BẢNG CƠ SỞ 
-- =================================================================================

-- 10. Bảng TaiKhoan
CREATE TABLE TaiKhoan (
    MaTK CHAR(10) PRIMARY KEY,
    MatKhau VARCHAR(255) NOT NULL
);
-- 1. Bảng KhachHang
CREATE TABLE KhachHang (
    MaKH CHAR(10) PRIMARY KEY, -- KHHG123456
    HoTen NVARCHAR(100) NOT NULL,
    CCCD CHAR(12) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(500),
    SoDT VARCHAR(10) NOT NULL,
    Email VARCHAR(100),
    MaTK CHAR(10),
    CONSTRAINT FK_KhachHang_TaiKhoan FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

-- 2. Bảng NhanVien
CREATE TABLE NhanVien (
    MaNV CHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    CCCD CHAR(12),
    NgayVaoLam DATE NOT NULL,
    SoDT VARCHAR(10),
    DiaChi NVARCHAR(500),
    Email VARCHAR(100),
    ChucVu INT,
    TrangThai NVARCHAR DEFAULT '0',
    MaTK CHAR(10),
    CONSTRAINT FK_NhanVien_TaiKhoan FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

-- 3. Bảng NhaCungCap
CREATE TABLE NhaCungCap (
    MaNCC CHAR(10) PRIMARY KEY,
    TenNCC NVARCHAR(255) NOT NULL,
    DiaChi NVARCHAR(500),
    Email VARCHAR(100),
    SoDT VARCHAR(10),
    TenNganHang NVARCHAR(100),
    SoTK VARCHAR(30)
);

-- 4. Bảng KhuyenMai
CREATE TABLE KhuyenMai (
    MaKM CHAR(10) PRIMARY KEY,
    TenKM NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX),
    
    -- Loại KM: Dùng để phân loại quản lý (vd: "Lễ Tết", "Sinh Nhật")
    LoaiKM NVARCHAR(100), 
    
    -- Kiểu Giảm: 'PhanTram' (ví dụ giảm 10%) hoặc 'SoTien' (ví dụ giảm 50k)
    KieuGiam NVARCHAR(50) NOT NULL, 
    
    -- Giá trị: Lưu số 10 (nếu là %) hoặc 50000 (nếu là tiền)
    GiaTriGiam DECIMAL(18, 2) NOT NULL, 
    
    NgayBatDau DATETIME NOT NULL,
    NgayKetThuc DATETIME NOT NULL,
    TrangThai BIT DEFAULT (1) -- 1: Đang chạy, 0: Tạm dừng/Hết hạn
);

-- 5. Bảng LoaiVaccine
CREATE TABLE LoaiVaccine (
    MaLoai CHAR(10) PRIMARY KEY,
    TenLoai NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX)
);

-- 6. Bảng LoaiBenh
CREATE TABLE LoaiBenh (
    MaLoaiBenh CHAR(10) PRIMARY KEY,
    TenBenh NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX),
    NhomDoiTuong NVARCHAR(255)
);

-- 7. Bảng GoiVaccine
CREATE TABLE GoiVaccine (
    MaGoi CHAR(10) PRIMARY KEY,
    TenGoi NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX),
    DoiTuongApDung NVARCHAR(255),
    GiaGoi DECIMAL(18, 0) NOT NULL,
    TrangThai NVARCHAR(50)
);

-- =================================================================================
-- BẢNG CÓ KHÓA NGOẠI
-- =================================================================================

-- 8. Bảng Vaccine
CREATE TABLE Vaccine (
    MaVC CHAR(10) PRIMARY KEY, -- VCCN0001
    TenVC NVARCHAR(255) NOT NULL,
    GiaBan DECIMAL(18, 0) NOT NULL,
    SoLuong INT NOT NULL DEFAULT 0,
    SoMuiToiDa INT,
    SoThangCho INT,
    MaLoai CHAR(10),
    MoTa NVARCHAR(MAX),
    HinhAnh VARCHAR(255),
    CONSTRAINT FK_Vaccine_LoaiVaccine FOREIGN KEY (MaLoai) REFERENCES LoaiVaccine(MaLoai)
);


-- 9. Bảng HoSoTiemChung
CREATE TABLE HoSoTiemChung (
    MaHSTC CHAR(10) PRIMARY KEY,-- HSTM123456
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    NgaySinh DATE NOT NULL,
    CCCD VARCHAR(12) NOT NULL,
    GhiChu NVARCHAR(MAX),
    TrangThai BIT DEFAULT (1)
);


-- 11. Bảng LienKetHoSo
CREATE TABLE LienKetHoSo (
    MaLK     CHAR(10)      NOT NULL,
    VaiTro   NVARCHAR(100) NULL,    
    NgayLienKet DATETIME2(3) NOT NULL CONSTRAINT DF_LienKetHoSo_CreatedAt DEFAULT SYSUTCDATETIME(),
    MaKH     CHAR(10)      NOT NULL,
    MaHSTC   CHAR(10)      NOT NULL,
    CONSTRAINT PK_LienKetHoSo PRIMARY KEY (MaLK),
    CONSTRAINT FK_LienKet_KhachHang
        FOREIGN KEY (MaKH)   REFERENCES dbo.KhachHang(MaKH),
    CONSTRAINT FK_LienKet_HoSoTiemChung
        FOREIGN KEY (MaHSTC) REFERENCES dbo.HoSoTiemChung(MaHSTC)
);


-- 12. Bảng PhieuNhapVaccine
CREATE TABLE PhieuNhapVaccine (
    MaPN CHAR(10) PRIMARY KEY,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    MaNV CHAR(10),
    MaNCC CHAR(10),
    TrangThai BIT DEFAULT 0,
    CONSTRAINT FK_PhieuNhap_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_PhieuNhap_NhaCungCap FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

-- 13. Bảng HoaDon
CREATE TABLE HoaDon (
    MaHD CHAR(10) PRIMARY KEY,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18, 0) NOT NULL,
    TrangThai BIT,
    MaKH CHAR(10),
    MaNV CHAR(10),
    MaKM CHAR(10),
    CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_HoaDon_KhuyenMai FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM)
);

-- 14. Bảng LichTiem
CREATE TABLE LichTiem (
    MaLT CHAR(10) PRIMARY KEY,
    NgayHenTiem DATETIME NOT NULL,
    NgayTiemThucTe DATETIME,
    SoMui INT, -- Tiêm mũi thứ mấy
    TrangThai NVARCHAR(50) DEFAULT N'Chưa tiêm', 
    GhiChu NVARCHAR(MAX),
    MaHSTC CHAR(10) NOT NULL,
    MaVC CHAR(10) NULL, 
    MaNV CHAR(10) NULL, 
    CONSTRAINT FK_LichTiem_HoSoTiemChung FOREIGN KEY (MaHSTC) REFERENCES HoSoTiemChung(MaHSTC),
    CONSTRAINT FK_LichTiem_Vaccine FOREIGN KEY (MaVC) REFERENCES Vaccine(MaVC),
    CONSTRAINT FK_LichTiem_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

-- =================================================================================
-- BẢNG LIÊN KẾT 
-- =================================================================================

-- 15. Bảng VaccinePhongBenh
CREATE TABLE VaccinePhongBenh (
    MaVC CHAR(10) NOT NULL,
    MaLoaiBenh CHAR(10) NOT NULL,
    GhiChu NVARCHAR(MAX),
    PRIMARY KEY (MaVC, MaLoaiBenh),
    CONSTRAINT FK_VPB_Vaccine FOREIGN KEY (MaVC) REFERENCES Vaccine(MaVC),
    CONSTRAINT FK_VPB_LoaiBenh FOREIGN KEY (MaLoaiBenh) REFERENCES LoaiBenh(MaLoaiBenh)
);

-- 16. Bảng ChiTietGoiVaccine
CREATE TABLE ChiTietGoiVaccine (
    MaCTGoi CHAR(10) PRIMARY KEY, -- CTGV0001
    SoMui INT, -- Phác đồ
    GhiChu NVARCHAR(MAX),
    MaGoi CHAR(10) NOT NULL,
    MaVC CHAR(10) NOT NULL,
    CONSTRAINT FK_CTGV_GoiVaccine FOREIGN KEY (MaGoi) REFERENCES GoiVaccine(MaGoi),
    CONSTRAINT FK_CTGV_Vaccine FOREIGN KEY (MaVC) REFERENCES Vaccine(MaVC)
);

-- 17. Bảng ChiTietPhieuNhap
CREATE TABLE ChiTietPhieuNhap (
    MaCTPN CHAR(10) PRIMARY KEY,
    NuocSanXuat NVARCHAR(100),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    SoLuongTonKho INT NOT NULL CHECK (SoLuongTonKho >= 0),
    GiaNhap DECIMAL(18, 0) NOT NULL,
    HanSuDung DATE,
    MaPN CHAR(10) NOT NULL,
    MaVC CHAR(10) NOT NULL,
    CONSTRAINT FK_CTPN_PhieuNhap FOREIGN KEY (MaPN) REFERENCES PhieuNhapVaccine(MaPN),
    CONSTRAINT FK_CTPN_Vaccine FOREIGN KEY (MaVC) REFERENCES Vaccine(MaVC)
);

-- 18. GioHang
CREATE TABLE GioHang (
    MaGH INT PRIMARY KEY IDENTITY(1,1),
    MaKH CHAR(10) NOT NULL,
    MaSanPham CHAR(10) NOT NULL, --  MaGoi hoặc MaVC
    LoaiSanPham NVARCHAR(20) NOT NULL, -- 'GOIVACCINE' hoặc 'VACCINE'
    SoLuong INT NOT NULL DEFAULT 1,
    CONSTRAINT FK_GioHang_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- 19. ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaCTHD CHAR(10) PRIMARY KEY,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18, 0) NOT NULL,
    MaSanPham CHAR(10) NOT NULL, --  MaGoi hoặc MaVC
    LoaiSanPham NVARCHAR(20) NOT NULL, -- 'GOIVACCINE' hoặc 'VACCINE'
    MaHD CHAR(10) NOT NULL,
    CONSTRAINT FK_CTHD_HoaDon FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD)
);

-- 20. ChiTietKhuyenMai
CREATE TABLE ChiTietKhuyenMai (
    MaCTKM INT PRIMARY KEY IDENTITY(1,1),
    MaKM CHAR(10) NOT NULL,
    
    -- Loại sản phẩm: 'VACCINE' hoặc 'GOIVACCINE'
    LoaiSanPham NVARCHAR(50) NOT NULL, 
    
    -- Mã sản phẩm: Lưu MaVC hoặc MaGoi tùy theo LoaiSanPham
    MaSanPham CHAR(10) NOT NULL, 
    
    CONSTRAINT FK_CTKM_KhuyenMai FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM)
    -- Lưu ý: Không thể tạo FK trực tiếp tới Vaccine/GoiVaccine vì cột này động.
);
GO

-- =================================================================================
-- UPDATE HÌNH ẢNH
-- =================================================================================

-- Cập nhật đường dẫn hình ảnh cho các vaccine
UPDATE Vaccine SET HinhAnh = 'VC000001.jpg' WHERE MaVC = 'VC00000001';
UPDATE Vaccine SET HinhAnh = 'VC000002.jpg' WHERE MaVC = 'VC00000002';
UPDATE Vaccine SET HinhAnh = 'VC000003.jpg' WHERE MaVC = 'VC00000003';
UPDATE Vaccine SET HinhAnh = 'VC000004.jpg' WHERE MaVC = 'VC00000004';
UPDATE Vaccine SET HinhAnh = 'VC000005.jpg' WHERE MaVC = 'VC00000005';
UPDATE Vaccine SET HinhAnh = 'VC000006.jpg' WHERE MaVC = 'VC00000006';
UPDATE Vaccine SET HinhAnh = 'VC000007.jpg' WHERE MaVC = 'VC00000007';
UPDATE Vaccine SET HinhAnh = 'VC000008.jpg' WHERE MaVC = 'VC00000008';
UPDATE Vaccine SET HinhAnh = 'VC000009.jpg' WHERE MaVC = 'VC00000009';
UPDATE Vaccine SET HinhAnh = 'VC000010.jpg' WHERE MaVC = 'VC00000010';
UPDATE Vaccine SET HinhAnh = 'VC000011.jpg' WHERE MaVC = 'VC00000011';
UPDATE Vaccine SET HinhAnh = 'VC000012.jpg' WHERE MaVC = 'VC00000012';
UPDATE Vaccine SET HinhAnh = 'VC000013.jpg' WHERE MaVC = 'VC00000013';
UPDATE Vaccine SET HinhAnh = 'VC000014.png' WHERE MaVC = 'VC00000014';
UPDATE Vaccine SET HinhAnh = 'VC000015.jpg' WHERE MaVC = 'VC00000015';
UPDATE Vaccine SET HinhAnh = 'VC000016.jpg' WHERE MaVC = 'VC00000016';
UPDATE Vaccine SET HinhAnh = 'VC000017.jpg' WHERE MaVC = 'VC00000017';
UPDATE Vaccine SET HinhAnh = 'VC000018.jpg' WHERE MaVC = 'VC00000018';
UPDATE Vaccine SET HinhAnh = 'VC000019.jpg' WHERE MaVC = 'VC00000019';
UPDATE Vaccine SET HinhAnh = 'VC000020.jpg' WHERE MaVC = 'VC00000020';
UPDATE Vaccine SET HinhAnh = 'VC000021.jpg' WHERE MaVC = 'VC00000021';
UPDATE Vaccine SET HinhAnh = 'VC000022.jpg' WHERE MaVC = 'VC00000022';
UPDATE Vaccine SET HinhAnh = 'VC000023.jpg' WHERE MaVC = 'VC00000023';
UPDATE Vaccine SET HinhAnh = 'VC000024.jpg' WHERE MaVC = 'VC00000024';
UPDATE Vaccine SET HinhAnh = 'VC000025.jpg' WHERE MaVC = 'VC00000025';
UPDATE Vaccine SET HinhAnh = 'VC000026.jpg' WHERE MaVC = 'VC00000026';
UPDATE Vaccine SET HinhAnh = 'VC000027.jpg' WHERE MaVC = 'VC00000027';
UPDATE Vaccine SET HinhAnh = 'VC000028.jpg' WHERE MaVC = 'VC00000028';
UPDATE Vaccine SET HinhAnh = 'VC000029.jpg' WHERE MaVC = 'VC00000029';
UPDATE Vaccine SET HinhAnh = 'VC000030.jpg' WHERE MaVC = 'VC00000030';
UPDATE Vaccine SET HinhAnh = 'VC000031.jpg' WHERE MaVC = 'VC00000031';
UPDATE Vaccine SET HinhAnh = 'VC000032.jpg' WHERE MaVC = 'VC00000032';
UPDATE Vaccine SET HinhAnh = 'VC000033.jpg' WHERE MaVC = 'VC00000033';
UPDATE Vaccine SET HinhAnh = 'VC000034.jpg' WHERE MaVC = 'VC00000034';
UPDATE Vaccine SET HinhAnh = 'VC000035.jpg' WHERE MaVC = 'VC00000035';

PRINT N'Đã cập nhật đường dẫn hình ảnh cho 35 vaccines';
GO


/* =============================================================================  Trigger */ 

CREATE OR ALTER TRIGGER dbo.trg_ChiTietPhieuNhap_UpdateVaccineSoLuongTon
ON dbo.ChiTietPhieuNhap
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Tạo một bảng tạm @AffectedMaVCs
    -- để lưu trữ MaVC của các dòng vừa bị thay đổi.
    DECLARE @AffectedMaVCs TABLE (MaVC CHAR(10) PRIMARY KEY);

    -- Lấy MaVC từ các dòng MỚI được thêm (INSERT) hoặc SỬA (UPDATE)
    INSERT INTO @AffectedMaVCs (MaVC)
    SELECT MaVC FROM inserted
    GROUP BY MaVC; -- Dùng GROUP BY để lấy MaVC duy nhất
    
    -- Lấy MaVC từ các dòng CŨ bị XÓA (DELETE) hoặc SỬA (UPDATE)
    INSERT INTO @AffectedMaVCs (MaVC)
    SELECT MaVC FROM deleted
    WHERE MaVC NOT IN (SELECT MaVC FROM @AffectedMaVCs) -- Chỉ thêm nếu chưa có
    GROUP BY MaVC;

    -- 2. Cập nhật lại bảng TỔNG (Vaccine)
    UPDATE V
    SET
        -- Tính toán lại TỔNG TỒN KHO bằng cách SUM tất cả
        -- các lô (SoLuongTonKho) của vaccine này
        V.SoLuong = ISNULL(CTPN.TotalStock, 0)
    FROM
        dbo.Vaccine AS V
    INNER JOIN
        -- Chỉ cập nhật các MaVC bị ảnh hưởng
        @AffectedMaVCs AS A ON V.MaVC = A.MaVC
    LEFT JOIN
        (
            -- Tính tổng tồn kho mới của các lô
            SELECT
                MaVC,
                SUM(SoLuongTonKho) AS TotalStock
            FROM
                dbo.ChiTietPhieuNhap
            WHERE
                MaVC IN (SELECT MaVC FROM @AffectedMaVCs)
            GROUP BY
                MaVC
        ) AS CTPN ON V.MaVC = CTPN.MaVC;
END
GO



/* ============================================================================= */

IF OBJECT_ID('dbo.usp_HoSoTiemChung_GetAllWithKhachHang', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_HoSoTiemChung_GetAllWithKhachHang;
GO

CREATE PROCEDURE dbo.usp_HoSoTiemChung_GetAllWithKhachHang
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        hs.MaHSTC,
        hs.CCCD AS CCCDHS,
        hs.HoTen AS HoTen,      
        hs.GioiTinh,
        hs.NgaySinh,
        pick.VaiTro,
        kh.MaKH,
        kh.HoTen AS TenKhachHang,
        kh.CCCD AS CCCDKH,
        kh.SoDT,
        pick.NgayLienKet       
    FROM dbo.HoSoTiemChung AS hs
    OUTER APPLY (
        SELECT TOP 1 
               l.MaKH, l.VaiTro,
               l.NgayLienKet
        FROM dbo.LienKetHoSo l
        WHERE l.MaHSTC = hs.MaHSTC
        ORDER BY 
            CASE WHEN l.VaiTro = N'Bản thân' THEN 0 ELSE 1 END,  -- ưu tiên "Bản thân"                               
            l.NgayLienKet ASC,                                  
            l.MaKH                                             
    ) AS pick
    LEFT JOIN dbo.KhachHang AS kh ON kh.MaKH = pick.MaKH
    ORDER BY hs.MaHSTC;
END
GO
/* ============================================================================= */

IF OBJECT_ID('dbo.usp_HoSoTiemChung_GetQuanHeVoiKH', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_HoSoTiemChung_GetQuanHeVoiKH;
GO

CREATE PROCEDURE dbo.usp_HoSoTiemChung_GetQuanHeVoiKH
    @MaKH CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT HSTC.MaHSTC, HSTC.HoTen AS HoTenHS,LK.VaiTro,KH.HoTen as HoTenKH
    FROM HoSoTiemChung HSTC
    JOIN LienKetHoSo LK ON LK.MaHSTC = HSTC.MaHSTC
    JOIN KhachHang KH ON KH.MaKH = LK.MaKH
    WHERE RTRIM(lk.MaKH) = RTRIM(@MaKH) 
END
GO
/* ============================================================================= */

IF OBJECT_ID('dbo.usp_SeachMaHSTC', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_SeachMaHSTC;
GO

CREATE PROCEDURE dbo.usp_SeachMaHSTC
    @MaKHST CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaHSTC
    FROM HoSoTiemChung
    WHERE MaHSTC = @MaKHST
END
GO
/* ============================================================================= */

IF OBJECT_ID('dbo.usp_GetDanhSachVaccineChiTiet', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_GetDanhSachVaccineChiTiet;
GO

CREATE OR ALTER PROCEDURE dbo.usp_GetDanhSachVaccineChiTiet
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.MaVC,
        v.TenVC,
        v.GiaBan,
        (
            SELECT ISNULL(SUM(ctpn.SoLuongTonKho), 0)
            FROM dbo.ChiTietPhieuNhap AS ctpn
            WHERE ctpn.MaVC = v.MaVC
              AND ctpn.HanSuDung > GETDATE()
        ) AS SoLuongTonThucTe,
        
        V.SoLuong AS TongSoLuongTon, 
        v.SoMuiToiDa,
        v.SoThangCho,
        v.MoTa AS MoTaVaccine,
        v.HinhAnh,
        lv.TenLoai AS TenLoaiVaccine,
        v.MaLoai,
        ISNULL(STRING_AGG(lb.TenBenh, N', '), N'Chưa có') AS CacBenhPhongNgua,
        ISNULL(
            STUFF(
                (
                    SELECT DISTINCT N', ' + ctpn.NuocSanXuat
                    FROM dbo.ChiTietPhieuNhap AS ctpn
                    WHERE ctpn.MaVC = v.MaVC AND ctpn.NuocSanXuat IS NOT NULL
                    FOR XML PATH(''), TYPE
                ).value('.', 'NVARCHAR(MAX)'), 
                1, 2, N''
            ), 
            N'Chưa nhập'
        ) AS [Nước sản xuất]
    FROM
        dbo.Vaccine AS v
    LEFT JOIN
        dbo.LoaiVaccine AS lv ON v.MaLoai = lv.MaLoai
    LEFT JOIN
        dbo.VaccinePhongBenh AS vpb ON v.MaVC = vpb.MaVC
    LEFT JOIN
        dbo.LoaiBenh AS lb ON vpb.MaLoaiBenh = lb.MaLoaiBenh
    GROUP BY
        v.MaVC, v.TenVC, v.GiaBan, V.SoLuong, v.SoMuiToiDa,
        v.SoThangCho, v.MoTa, v.HinhAnh, lv.TenLoai, v.MaLoai
    ORDER BY
        v.TenVC;
END
GO

/* ============================================================================= */


IF OBJECT_ID('dbo.usp_GetDanhSachLichTiemChiTiet', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_GetDanhSachLichTiemChiTiet;
GO

CREATE PROCEDURE dbo.usp_GetDanhSachLichTiemChiTiet
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.MaVC,
        lt.MaHSTC AS [MaHSTC],
        lt.MaLT,
        hs.HoTen AS [Tên người tiêm],
        v.TenVC AS [Tên Vaccine],
        lt.NgayHenTiem AS [Ngày hẹn],
        lt.TrangThai AS [Trạng thái],
        lt.NgayTiemThucTe AS [Ngày tiêm thực tế],
        lt.SoMui
    FROM
        dbo.LichTiem AS lt
    -- Join để lấy Tên người tiêm (từ Hồ sơ)
    LEFT JOIN
        dbo.HoSoTiemChung AS hs ON lt.MaHSTC = hs.MaHSTC
    -- Join để lấy Tên Vaccine (từ Vaccine)
    LEFT JOIN
        dbo.Vaccine AS v ON lt.MaVC = v.MaVC
    ORDER BY
        lt.NgayHenTiem DESC; -- Sắp xếp lịch hẹn mới nhất lên đầu
END
GO

/* ============================================================================= */

IF OBJECT_ID('dbo.usp_GetVaccinesByGoiVaccine', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_GetVaccinesByGoiVaccine;
GO

CREATE OR ALTER PROCEDURE dbo.usp_GetVaccinesByGoiVaccine
    @MaGoi CHAR(10) -- Sửa thành CHAR(10) để khớp với bảng GoiVaccine
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.MaVC AS MaVC,                 -- Sửa: Bỏ dấu
        v.TenVC AS TenVC,               -- Sửa: Bỏ dấu
        
        -- Sửa: Đổi tên alias cho nhất quán
        ISNULL(
            STUFF(
                (
                    SELECT DISTINCT N', ' + lb.TenBenh
                    FROM dbo.VaccinePhongBenh AS vpb
                    INNER JOIN dbo.LoaiBenh AS lb ON vpb.MaLoaiBenh = lb.MaLoaiBenh
                    WHERE vpb.MaVC = v.MaVC
                    FOR XML PATH(''), TYPE
                ).value('.', 'NVARCHAR(MAX)'), 
                1, 2, N'' 
            ), 
            N'Chưa có'
        ) AS CacBenhPhongNgua,          -- Sửa: [Loại bệnh] -> CacBenhPhongNgua
        
        lv.TenLoai AS TenLoaiVaccine,   -- Sửa: [Loại Vaccine] -> TenLoaiVaccine
        
        -- Giữ nguyên alias này vì nó nhất quán với proc usp_GetDanhSachVaccineChiTiet
        COALESCE(ctpn.NuocSanXuat, N'Chưa xác định') AS [Nước sản xuất], 
        
        v.GiaBan AS GiaBan,             -- Sửa: Bỏ dấu
        ct.SoMui AS SoMui,              -- Sửa: Bỏ dấu
        ct.GhiChu AS GhiChu             -- Sửa: Bỏ dấu
    FROM 
        dbo.ChiTietGoiVaccine ct
    INNER JOIN 
        dbo.Vaccine v ON ct.MaVC = v.MaVC
    LEFT JOIN 
        dbo.LoaiVaccine lv ON v.MaLoai = lv.MaLoai
    LEFT JOIN (
        SELECT 
            MaVC, 
            NuocSanXuat,
            ROW_NUMBER() OVER (PARTITION BY MaVC ORDER BY HanSuDung DESC) AS rn
        FROM 
            dbo.ChiTietPhieuNhap
    ) ctpn ON v.MaVC = ctpn.MaVC AND ctpn.rn = 1
    WHERE 
        ct.MaGoi = @MaGoi
    GROUP BY 
        v.MaVC, v.TenVC, lv.TenLoai, ctpn.NuocSanXuat, v.GiaBan, ct.SoMui, ct.GhiChu;
END
GO
/* ============================================================================= */

IF OBJECT_ID('dbo.usp_ChiTietHoaDon_GetByMaHD', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ChiTietHoaDon_GetByMaHD;
GO

CREATE PROCEDURE dbo.usp_ChiTietHoaDon_GetByMaHD
    @MaHD CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        cthd.MaCTHD, 
        cthd.SoLuong, 
        cthd.DonGia,
        cthd.MaSanPham,
        cthd.LoaiSanPham, 
        cthd.MaHD,
        (cthd.SoLuong * cthd.DonGia) AS ThanhTien,
        
        COALESCE(v.TenVC, g.TenGoi) AS TenSanPham
    FROM 
        dbo.ChiTietHoaDon AS cthd
    LEFT JOIN 
        dbo.Vaccine AS v ON cthd.MaSanPham = v.MaVC 
                        AND cthd.LoaiSanPham = 'VACCINE'
    LEFT JOIN 
        dbo.GoiVaccine AS g ON cthd.MaSanPham = g.MaGoi 
                           AND cthd.LoaiSanPham = 'GOIVACCINE'
    WHERE 
        cthd.MaHD = @MaHD;
END
GO

/* ============================================================================= */

IF OBJECT_ID('dbo.usp_GetChiTietGoi_FirstDoses', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_GetChiTietGoi_FirstDoses;
GO

CREATE PROCEDURE dbo.usp_GetChiTietGoi_FirstDoses
    @MaGoi CHAR(10) -- Đã sửa thành CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Lấy tất cả MaVC (Mã Vaccine) trong gói mà có SoMui = 1
    SELECT MaVC 
    FROM dbo.ChiTietGoiVaccine
    WHERE MaGoi = @MaGoi AND SoMui = 1;
END
GO

/* ============================================================================= */

IF OBJECT_ID('dbo.usp_PhieuNhap_GetAllWithDetails', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_PhieuNhap_GetAllWithDetails;
GO

/**
 * Lấy danh sách tất cả các phiếu nhập.
 * Bao gồm thông tin chi tiết (Tên) của Nhân Viên lập phiếu và Nhà Cung Cấp.
 */
CREATE PROCEDURE dbo.usp_PhieuNhap_GetAllWithDetails
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        pn.MaPN AS [Mã Phiếu Nhập],
        pn.NgayLap AS [Ngày Lập],
        
        -- Thông tin Nhân Viên
        nv.MaNV AS [MaNV],
        nv.HoTen AS [Tên Nhân Viên Lập],
        
        -- Thông tin Nhà Cung Cấp
        ncc.MaNCC AS [MaNCC],
        ncc.TenNCC AS [Tên Nhà Cung Cấp],
        pn.TrangThai
        
    FROM
        dbo.PhieuNhapVaccine AS pn
    
    -- Dùng LEFT JOIN phòng trường hợp nhân viên hoặc NCC đã bị xóa
    -- nhưng chúng ta vẫn muốn xem phiếu nhập
    
    -- Join để lấy Tên Nhân Viên
    LEFT JOIN
        dbo.NhanVien AS nv ON pn.MaNV = nv.MaNV
        
    -- Join để lấy Tên Nhà Cung Cấp
    LEFT JOIN
        dbo.NhaCungCap AS ncc ON pn.MaNCC = ncc.MaNCC
        
    ORDER BY
        pn.NgayLap DESC; -- Sắp xếp phiếu mới nhất lên đầu
        
END
GO
/* ================================================================= */

IF OBJECT_ID('dbo.usp_PhieuNhap_GetDetailByMaPN', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_PhieuNhap_GetDetailByMaPN;
GO

/* Lấy thông tin chi tiết 1 phiếu nhập (dùng cho header) */
CREATE PROCEDURE dbo.usp_PhieuNhap_GetDetailByMaPN
    @MaPN CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        pn.MaPN AS [Mã Phiếu Nhập],
        pn.NgayLap AS [Ngày Lập],
        nv.HoTen AS [Tên Nhân Viên Lập],
        ncc.TenNCC AS [Tên Nhà Cung Cấp]
    FROM
        dbo.PhieuNhapVaccine AS pn
    LEFT JOIN
        dbo.NhanVien AS nv ON pn.MaNV = nv.MaNV
    LEFT JOIN
        dbo.NhaCungCap AS ncc ON pn.MaNCC = ncc.MaNCC
    WHERE
        pn.MaPN = @MaPN;
END
GO

/* ================================================================= */

IF OBJECT_ID('dbo.usp_ChiTietPhieuNhap_GetByMaPN', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ChiTietPhieuNhap_GetByMaPN;
GO

/* Lấy danh sách vaccine theo mã phiếu nhập (dùng cho DataGridView) */
CREATE OR ALTER PROCEDURE dbo.usp_ChiTietPhieuNhap_GetByMaPN
    @MaPN CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        ctpn.MaCTPN AS [Mã Chi Tiết],
        v.MaVC AS [Mã Vaccine],
        v.TenVC AS [Tên Vaccine],
        ctpn.NuocSanXuat AS [Nước Sản Xuất],
        
        -- SỬA: Thêm cả 2 cột số lượng
        ctpn.SoLuong AS [Số Lượng],
        ctpn.SoLuongTonKho AS [Số Lượng Tồn],
        
        ctpn.GiaNhap AS [Giá Nhập],
        ctpn.HanSuDung AS [Hạn Sử Dụng],
        (ctpn.SoLuong * ctpn.GiaNhap) AS [Thành Tiền] -- (Tính thành tiền của hàng tồn)
    FROM
        dbo.ChiTietPhieuNhap AS ctpn
    LEFT JOIN
        dbo.Vaccine AS v ON ctpn.MaVC = v.MaVC
    WHERE
        ctpn.MaPN = @MaPN
    ORDER BY
        v.TenVC;
END
GO

/* ================================================================= */

IF OBJECT_ID('dbo.usp_GetDanhSachVaccine_SingleDose', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_GetDanhSachVaccine_SingleDose;
GO
CREATE OR ALTER PROCEDURE dbo.usp_GetDanhSachVaccine_SingleDose
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.MaVC,
        v.TenVC,
        v.GiaBan,
        
        -- SỬA: Tính 'SoLuongTonThucTe' từ 'SoLuongTonKho'
        (
            SELECT ISNULL(SUM(ctpn.SoLuongTonKho), 0) -- SỬA TÊN CỘT
            FROM dbo.ChiTietPhieuNhap AS ctpn
            WHERE ctpn.MaVC = v.MaVC
              AND ctpn.HanSuDung > GETDATE()
        ) AS SoLuongTonThucTe,
        
        V.SoLuong AS TongSoLuongTon, 
        v.SoMuiToiDa,
        -- ... (Phần còn lại của proc giữ nguyên) ...
        v.SoThangCho,
        v.MoTa AS MoTaVaccine,
        v.HinhAnh,
        lv.TenLoai AS TenLoaiVaccine,
        v.MaLoai,
        ISNULL(STRING_AGG(lb.TenBenh, N', '), N'Chưa có') AS CacBenhPhongNgua,
        ISNULL(
            STUFF(
                (
                    SELECT DISTINCT N', ' + ctpn.NuocSanXuat
                    FROM dbo.ChiTietPhieuNhap AS ctpn
                    WHERE ctpn.MaVC = v.MaVC AND ctpn.NuocSanXuat IS NOT NULL
                    FOR XML PATH(''), TYPE
                ).value('.', 'NVARCHAR(MAX)'), 
                1, 2, N''
            ), 
            N'Chưa nhập'
        ) AS [Nước sản xuất]
    FROM
        dbo.Vaccine AS v
    LEFT JOIN
        dbo.LoaiVaccine AS lv ON v.MaLoai = lv.MaLoai
    LEFT JOIN
        dbo.VaccinePhongBenh AS vpb ON v.MaVC = vpb.MaVC
    LEFT JOIN
        dbo.LoaiBenh AS lb ON vpb.MaLoaiBenh = lb.MaLoaiBenh
    WHERE
        (ISNULL(v.SoMuiToiDa, 0) = 1)
        OR (ISNULL(v.SoMuiToiDa, 0) = 99)
    GROUP BY
        v.MaVC, v.TenVC, v.GiaBan, V.SoLuong, v.SoMuiToiDa,
        v.SoThangCho, v.MoTa, v.HinhAnh, lv.TenLoai, v.MaLoai
    ORDER BY
        v.TenVC;
END
GO


/* ================================================================= */
IF OBJECT_ID('dbo.usp_Vaccine_GiamTonKho', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Vaccine_GiamTonKho;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Vaccine_GiamTonKho
    @MaVC CHAR(10),
    @SoLuongGiam INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @MaCTPN_FEFO CHAR(10);
        DECLARE @SoLuongTonThucTe INT;

        -- 1. Tìm tổng tồn kho (dùng cột 'SoLuongTonKho')
        SELECT @SoLuongTonThucTe = ISNULL(SUM(SoLuongTonKho), 0)
        FROM dbo.ChiTietPhieuNhap
        WHERE MaVC = @MaVC
          AND HanSuDung > GETDATE()
          AND SoLuongTonKho > 0; 

        -- 2. Kiểm tra
        IF (@SoLuongTonThucTe < @SoLuongGiam)
        BEGIN
            RAISERROR('Không đủ số lượng tồn kho (hoặc vaccine đã hết hạn).', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- 3. Tìm lô FEFO
        SELECT TOP 1 @MaCTPN_FEFO = MaCTPN
        FROM dbo.ChiTietPhieuNhap
        WHERE MaVC = @MaVC
          AND HanSuDung > GETDATE()
          AND SoLuongTonKho > 0 
        ORDER BY
          HanSuDung ASC; 

        -- 4. Trừ kho chi tiết (Lô hàng)
        -- Trigger sẽ tự động kích hoạt ở đây và cập nhật bảng Vaccine
        UPDATE dbo.ChiTietPhieuNhap
        SET SoLuongTonKho = SoLuongTonKho - @SoLuongGiam 
        WHERE MaCTPN = @MaCTPN_FEFO;

        -- === ĐÃ XÓA PHẦN 'UPDATE dbo.Vaccine' ===

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO
/* ================================================================= */

IF OBJECT_ID('dbo.usp_XacNhanNhapKho', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_XacNhanNhapKho;
GO

CREATE OR ALTER PROCEDURE dbo.usp_XacNhanNhapKho
    @MaPN CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- 1. Kiểm tra phiếu nhập tồn tại
        IF NOT EXISTS (SELECT 1 FROM dbo.PhieuNhapVaccine WHERE MaPN = @MaPN)
        BEGIN
            RAISERROR(N'Phiếu nhập không tồn tại!', 16, 1);
        END
        
        -- 2. Kiểm tra phiếu nhập đã được xác nhận chưa
        IF EXISTS (SELECT 1 FROM dbo.PhieuNhapVaccine WHERE MaPN = @MaPN AND TrangThai = 1)
        BEGIN
            RAISERROR(N'Phiếu nhập đã được xác nhận trước đó!', 16, 1);
        END

        -- 3. Cập nhật tổng số lượng tồn kho vào bảng Vaccine (Kho tổng)
        UPDATE V
        SET V.SoLuong = V.SoLuong + CTPN.SoLuong
        FROM dbo.Vaccine V
        INNER JOIN dbo.ChiTietPhieuNhap CTPN ON V.MaVC = CTPN.MaVC
        WHERE CTPN.MaPN = @MaPN;
        
        -- 4. (MỚI) Cập nhật Số lượng tồn cho từng lô trong ChiTietPhieuNhap
        -- Logic: Khi mới nhập kho, Số lượng tồn của lô này chính bằng Số lượng nhập
        UPDATE dbo.ChiTietPhieuNhap
        SET SoLuongTonKho = SoLuong
        WHERE MaPN = @MaPN;

        -- 5. Cập nhật trạng thái phiếu nhập thành "Đã xác nhận"
        UPDATE dbo.PhieuNhapVaccine
        SET TrangThai = 1
        WHERE MaPN = @MaPN;
        
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO


/* ================================================================= */

IF OBJECT_ID('dbo.usp_ThongKe_GetDashboardKPI', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ThongKe_GetDashboardKPI;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ThongKe_GetDashboardKPI
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @StartOfMonth DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
    DECLARE @EndOfMonth DATE = EOMONTH(GETDATE());

    -- 1. Tổng Doanh Thu Tháng Này (Chỉ tính hóa đơn đã thanh toán)
    DECLARE @DoanhThu DECIMAL(18,0);
    SELECT @DoanhThu = ISNULL(SUM(TongTien), 0)
    FROM dbo.HoaDon
    WHERE TrangThai = 1 -- Đã thanh toán
      AND CAST(NgayLap AS DATE) BETWEEN @StartOfMonth AND @EndOfMonth;

    -- 2. Tổng Lượt Tiêm Tháng Này
    DECLARE @LuotTiem INT;
    SELECT @LuotTiem = COUNT(*)
    FROM dbo.LichTiem
    WHERE TrangThai = N'Đã tiêm'
      AND CAST(NgayTiemThucTe AS DATE) BETWEEN @StartOfMonth AND @EndOfMonth;

    -- 3. Khách Hàng Mới (Dựa trên ngày liên kết hồ sơ)
    DECLARE @KhachMoi INT;
    SELECT @KhachMoi = COUNT(*)
    FROM dbo.LienKetHoSo
    WHERE CAST(NgayLienKet AS DATE) BETWEEN @StartOfMonth AND @EndOfMonth;

    -- 4. Số Lô Vaccine Sắp Hết Hạn (Trong 60 ngày tới)
    DECLARE @SapHetHan INT;
    SELECT @SapHetHan = COUNT(*)
    FROM dbo.ChiTietPhieuNhap
    WHERE SoLuongTonKho > 0
      AND HanSuDung <= DATEADD(day, 60, GETDATE());

    -- Trả về kết quả 1 dòng
    SELECT 
        @DoanhThu AS DoanhThu,
        @LuotTiem AS LuotTiem,
        @KhachMoi AS KhachMoi,
        @SapHetHan AS SapHetHan;
END
GO

/* ================================================================= */

IF OBJECT_ID('dbo.usp_ThongKe_GetDoanhThu7Ngay', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ThongKe_GetDoanhThu7Ngay;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ThongKe_GetDoanhThu7Ngay
AS
BEGIN
    SET NOCOUNT ON;

    -- Lấy doanh thu của 7 ngày gần nhất (tính cả hôm nay)
    SELECT TOP 7
        CAST(NgayLap AS DATE) AS Ngay,
        SUM(TongTien) AS TongTien
    FROM dbo.HoaDon
    WHERE TrangThai = 1 -- Đã thanh toán
    GROUP BY CAST(NgayLap AS DATE)
    ORDER BY CAST(NgayLap AS DATE) ASC;
END
GO

/* ================================================================= */

IF OBJECT_ID('dbo.usp_ThongKe_GetTyLeDoanhThu', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ThongKe_GetTyLeDoanhThu;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ThongKe_GetTyLeDoanhThu
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CASE 
            WHEN LoaiSanPham = 'GOIVACCINE' THEN N'Gói Vaccine'
            WHEN LoaiSanPham = 'VACCINE' THEN N'Vaccine Lẻ'
            ELSE N'Khác'
        END AS LoaiHinh,
        SUM(SoLuong * DonGia) AS TongGiaTri
    FROM dbo.ChiTietHoaDon cthd
    JOIN dbo.HoaDon hd ON cthd.MaHD = hd.MaHD
    WHERE hd.TrangThai = 1 -- Chỉ tính hóa đơn đã thanh toán
    GROUP BY LoaiSanPham;
END
GO

/* ================================================================= */

IF OBJECT_ID('dbo.usp_ThongKe_GetVaccineSapHetHan', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ThongKe_GetVaccineSapHetHan;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ThongKe_GetVaccineSapHetHan
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        v.MaVC,
        v.TenVC AS [Tên Vaccine],
        ctpn.MaCTPN AS [Mã Lô],
        ctpn.SoLuongTonKho AS [Tồn Kho],
        ctpn.HanSuDung AS [Hạn Sử Dụng],
        DATEDIFF(day, GETDATE(), ctpn.HanSuDung) AS [Số Ngày Còn Lại],
        CASE 
            WHEN ctpn.HanSuDung < GETDATE() THEN N'Đã hết hạn'
            ELSE N'Sắp hết hạn'
        END AS [Trạng Thái]
    FROM dbo.ChiTietPhieuNhap ctpn
    JOIN dbo.Vaccine v ON ctpn.MaVC = v.MaVC
    WHERE 
        ctpn.SoLuongTonKho > 0 -- Vẫn còn hàng trong kho
        AND ctpn.HanSuDung <= DATEADD(day, 60, GETDATE()) -- Hết hạn trong 60 ngày tới (hoặc đã qua)
    ORDER BY 
        ctpn.HanSuDung ASC; -- Ưu tiên hiển thị cái nào hết hạn trước
END
GO

/* ================================================================= */


IF OBJECT_ID('dbo.usp_ThongKe_GetDoanhThuChiTiet', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ThongKe_GetDoanhThuChiTiet;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ThongKe_GetDoanhThuChiTiet
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Lấy dữ liệu từ đầu tháng đến cuối tháng hiện tại
    DECLARE @StartOfMonth DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
    DECLARE @EndOfMonth DATE = EOMONTH(GETDATE());

    SELECT 
        hd.MaHD AS [Mã Hóa Đơn],
        hd.NgayLap AS [Ngày Lập],
        kh.HoTen AS [Khách Hàng],
        nv.HoTen AS [Thu Ngân],
        hd.TongTien AS [Tổng Tiền]
    FROM dbo.HoaDon hd
    LEFT JOIN dbo.KhachHang kh ON hd.MaKH = kh.MaKH
    LEFT JOIN dbo.NhanVien nv ON hd.MaNV = nv.MaNV
    WHERE hd.TrangThai = 1 
      AND CAST(hd.NgayLap AS DATE) BETWEEN @StartOfMonth AND @EndOfMonth
    ORDER BY hd.NgayLap DESC;
END
GO

/* ================================================================= */


IF OBJECT_ID('dbo.usp_ThongKe_GetXuatNhapTon', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ThongKe_GetXuatNhapTon;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ThongKe_GetXuatNhapTon
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartOfMonth DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
    DECLARE @EndOfMonth DATE = EOMONTH(GETDATE());

    SELECT 
        v.MaVC AS [Mã Vaccine],
        v.TenVC AS [Tên Vaccine],
        
        -- 1. Tổng Nhập trong tháng
        ISNULL((
            SELECT SUM(ctpn.SoLuong) 
            FROM dbo.ChiTietPhieuNhap ctpn
            JOIN dbo.PhieuNhapVaccine pn ON ctpn.MaPN = pn.MaPN
            WHERE ctpn.MaVC = v.MaVC 
              AND CAST(pn.NgayLap AS DATE) BETWEEN @StartOfMonth AND @EndOfMonth
        ), 0) AS [SL Nhập],

        -- 2. Tổng Xuất (Đã tiêm) trong tháng
        ISNULL((
            SELECT COUNT(*)
            FROM dbo.LichTiem lt
            WHERE lt.MaVC = v.MaVC
              AND lt.TrangThai = N'Đã tiêm'
              AND CAST(lt.NgayTiemThucTe AS DATE) BETWEEN @StartOfMonth AND @EndOfMonth
        ), 0) AS [SL Xuất],

        -- 3. Tồn Kho Hiện Tại (Tổng)
        V.SoLuong AS [Tồn Cuối Kỳ]

    FROM dbo.Vaccine v
    ORDER BY v.TenVC;
END
GO

/* ================================================================= report */


IF OBJECT_ID('dbo.usp_Report_GetHoaDonIn', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Report_GetHoaDonIn;
GO

-- =============================================
-- Stored Procedure: Lấy dữ liệu in hóa đơn
-- Bao gồm: Giá gốc, Giá sau KM, Tiền giảm
-- =============================================
CREATE OR ALTER PROCEDURE dbo.usp_Report_GetHoaDonIn
    @MaHD CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        -- Thông tin hóa đơn
      hd.MaHD,
    hd.NgayLap,
  hd.TongTien AS TongTienHoaDon,
        
        -- Thông tin khách hàng
        kh.HoTen AS TenKhachHang,
        kh.SoDT AS SDTKhachHang,
        kh.DiaChi AS DiaChiKhachHang,
      
   -- Thông tin thu ngân
        nv.HoTen AS TenThuNgan,
        
   -- Thông tin chi tiết sản phẩm
      CASE 
          WHEN cthd.LoaiSanPham = 'VACCINE' THEN vc.TenVC
      WHEN cthd.LoaiSanPham = 'GOIVACCINE' THEN gvc.TenGoi
 ELSE N'Không xác định'
     END AS TenSanPham,
        
        cthd.SoLuong,
        
        -- === THÊM MỚI: Tính giá gốc và giá sau khuyến mãi ===
        
      -- 1. GiaGoc: Lấy giá gốc từ bảng Vaccine hoặc GoiVaccine
        CASE 
         WHEN cthd.LoaiSanPham = 'VACCINE' THEN vc.GiaBan
            WHEN cthd.LoaiSanPham = 'GOIVACCINE' THEN gvc.GiaGoi
    ELSE 0
        END AS GiaGoc,
        
        -- 2. DonGia: Giá ĐÃ ĐƯỢC LƯU trong ChiTietHoaDon (giá sau KM)
      cthd.DonGia AS DonGia,
        
        -- 3. TienGiam: Số tiền được giảm = (GiaGoc - DonGia)
        (
    CASE 
   WHEN cthd.LoaiSanPham = 'VACCINE' THEN vc.GiaBan
         WHEN cthd.LoaiSanPham = 'GOIVACCINE' THEN gvc.GiaGoi
   ELSE 0
            END - cthd.DonGia
   ) AS TienGiam,
        
  -- === KẾT THÚC THÊM MỚI ===
      
   -- 4. ThanhTien: Tính từ giá sau KM * số lượng
        (cthd.SoLuong * cthd.DonGia) AS ThanhTien
   
    FROM 
        dbo.HoaDon hd
    
    -- Join bảng khách hàng
  LEFT JOIN dbo.KhachHang kh ON hd.MaKH = kh.MaKH
    
    -- Join bảng nhân viên (thu ngân)
    LEFT JOIN dbo.NhanVien nv ON hd.MaNV = nv.MaNV
    
    -- Join chi tiết hóa đơn
    INNER JOIN dbo.ChiTietHoaDon cthd ON hd.MaHD = cthd.MaHD
    
    -- Join Vaccine (nếu là vaccine lẻ)
    LEFT JOIN dbo.Vaccine vc ON cthd.LoaiSanPham = 'VACCINE' 
        AND cthd.MaSanPham = vc.MaVC
    
    -- Join GoiVaccine (nếu là gói)
    LEFT JOIN dbo.GoiVaccine gvc ON cthd.LoaiSanPham = 'GOIVACCINE' 
        AND cthd.MaSanPham = gvc.MaGoi
    
    WHERE 
        hd.MaHD = @MaHD
    
    ORDER BY 
        cthd.MaCTHD; -- Sắp xếp theo thứ tự chi tiết hóa đơn
END
GO

/* =================================================================  */

IF OBJECT_ID('dbo.usp_Vaccine_GetSoLuongTonThucTe', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Vaccine_GetSoLuongTonThucTe;
GO

/**
 * Lấy số lượng tồn kho THỰC TẾ (còn hạn sử dụng) của 1 vaccine cụ thể.
 * Trả về 1 con số duy nhất (INT).
 */
CREATE PROCEDURE dbo.usp_Vaccine_GetSoLuongTonThucTe
    @MaVC CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Tính tổng tồn kho của các lô còn hạn
    SELECT 
        ISNULL(SUM(ctpn.SoLuongTonKho), 0) AS SoLuongTonThucTe
    FROM 
        dbo.ChiTietPhieuNhap AS ctpn
    WHERE 
        ctpn.MaVC = @MaVC
        AND ctpn.HanSuDung > GETDATE() -- Chỉ lấy lô còn hạn
        AND ctpn.SoLuongTonKho > 0;    -- Chỉ lấy lô còn hàng
END
GO

/* ================================================================= report */


IF OBJECT_ID('dbo.usp_Report_GetPhieuNhapIn', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Report_GetPhieuNhapIn;
GO
CREATE PROCEDURE dbo.usp_Report_GetPhieuNhapIn
    @MaPN CHAR(10)
AS
BEGIN
    SELECT 
        pn.MaPN, pn.NgayLap, ISNULL(nv.HoTen, N'Admin') AS TenNhanVien,
        ncc.TenNCC AS TenNhaCungCap, ncc.DiaChi AS DiaChiNCC, ncc.SoDT AS SDTNCC,
        v.TenVC AS TenVaccine, ctpn.NuocSanXuat, ctpn.HanSuDung,
        ctpn.SoLuong AS SoLuong, ctpn.GiaNhap,
        (ctpn.SoLuong * ctpn.GiaNhap) AS ThanhTien,SUM(ctpn.SoLuong * ctpn.GiaNhap) OVER () AS TongTienPhieuNhap
    FROM dbo.PhieuNhapVaccine pn
    LEFT JOIN dbo.NhanVien nv ON pn.MaNV = nv.MaNV
    LEFT JOIN dbo.NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC
    JOIN dbo.ChiTietPhieuNhap ctpn ON pn.MaPN = ctpn.MaPN
    LEFT JOIN dbo.Vaccine v ON ctpn.MaVC = v.MaVC
    WHERE pn.MaPN = @MaPN;
END


/* =================================================================  */


IF OBJECT_ID('dbo.usp_KhuyenMai_GetActive', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_KhuyenMai_GetActive;
GO
-- Proc 1: Lấy danh sách khuyến mãi đang chạy (Còn hạn & TrangThai=1)
CREATE OR ALTER PROCEDURE dbo.usp_KhuyenMai_GetActive
AS
BEGIN
    SELECT * FROM KhuyenMai
    WHERE TrangThai = 1 
      AND GETDATE() BETWEEN NgayBatDau AND NgayKetThuc
    ORDER BY NgayKetThuc ASC;
END
GO
/* =================================================================  */

IF OBJECT_ID('dbo.usp_KhuyenMai_GetForProduct', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_KhuyenMai_GetForProduct;
GO

-- Proc 2: Kiểm tra giá giảm cho 1 sản phẩm cụ thể (Dùng khi tính tiền)
-- Trả về: KieuGiam và GiaTriGiam tốt nhất (nếu có nhiều KM chồng chéo)
CREATE OR ALTER PROCEDURE dbo.usp_KhuyenMai_GetForProduct
    @MaSanPham CHAR(10),
    @LoaiSanPham NVARCHAR(50) -- 'VACCINE' hoặc 'GOIVACCINE'
AS
BEGIN
    SELECT TOP 1 
        km.TenKM,
        km.KieuGiam,
        km.GiaTriGiam
    FROM ChiTietKhuyenMai ct
    JOIN KhuyenMai km ON ct.MaKM = km.MaKM
    WHERE ct.MaSanPham = @MaSanPham 
      AND ct.LoaiSanPham = @LoaiSanPham
      AND km.TrangThai = 1
      AND CAST(GETDATE() AS DATE) >= CAST(km.NgayBatDau AS DATE)
      AND CAST(GETDATE() AS DATE) <= CAST(km.NgayKetThuc AS DATE)
    ORDER BY km.NgayBatDau DESC; 
END
GO
/* =================================================================  */
