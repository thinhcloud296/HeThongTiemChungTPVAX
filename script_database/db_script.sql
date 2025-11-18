--CREATE DATABASE QLTIEMCHUNG
--GO

USE QLTIEMCHUNG
GO
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
    LoaiKM NVARCHAR(100),
    KieuGiam NVARCHAR(50), -- 'PhanTram', 'SoTien'
    GiaTriGiam DECIMAL(18, 2) NOT NULL,
    NgayBatDau DATETIME NOT NULL,
    NgayKetThuc DATETIME NOT NULL,
    TrangThai BIT DEFAULT (0)
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
    SoLuongTon INT NOT NULL DEFAULT 0,
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
    LoaiSanPham NVARCHAR(50), -- 'GOIVACCINE', 'VACCINE'
    MaSanPham VARCHAR(20),
    NgayApDung DATE,
    NgayKetThuc DATE,
    GhiChu NVARCHAR(MAX),
    MaKM CHAR(10) NOT NULL,
    CONSTRAINT FK_CTKM_KhuyenMai FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM)
);
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
        V.SoLuongTon = ISNULL(CTPN.TotalStock, 0)
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
        
        v.SoLuongTon AS TongSoLuongTon, 
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
        v.MaVC, v.TenVC, v.GiaBan, v.SoLuongTon, v.SoMuiToiDa,
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
        MaCTHD, 
        SoLuong, 
        DonGia, 
        MaSanPham, 
        LoaiSanPham, 
        MaHD,
        (SoLuong * DonGia) AS ThanhTien
    FROM 
        dbo.ChiTietHoaDon 
    WHERE 
        MaHD = @MaHD;
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
        ncc.TenNCC AS [Tên Nhà Cung Cấp]
        
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
        
        v.SoLuongTon AS TongSoLuongTon, 
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
        v.MaVC, v.TenVC, v.GiaBan, v.SoLuongTon, v.SoMuiToiDa,
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
/**
 * Xác nhận nhập kho cho TOÀN BỘ phiếu nhập.
 * 1. Cập nhật SoLuongTonKho = SoLuongNhap trong ChiTietPhieuNhap.
 * 2. Cộng SoLuongNhap vào Vaccine.SoLuongTon (bảng tổng).
 */
CREATE OR ALTER PROCEDURE dbo.usp_XacNhanNhapKho
    @MaPN CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        
        -- Chỉ cần cập nhật TỒN KHO LÔ
        -- Trigger sẽ tự động cập nhật TỒN KHO TỔNG (Vaccine)
        UPDATE dbo.ChiTietPhieuNhap
        SET 
            SoLuongTonKho = SoLuong -- Set Tồn kho = Số lượng đã nhập
        WHERE 
            MaPN = @MaPN
            AND SoLuongTonKho = 0; -- Chỉ cập nhật các phiếu chưa được xác nhận

        -- === ĐÃ XÓA PHẦN 'MERGE INTO dbo.Vaccine' ===

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO