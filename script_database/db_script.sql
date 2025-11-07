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
    MaTK CHAR(8) PRIMARY KEY,
    TenDangNhap VARCHAR(10) NOT NULL UNIQUE, -- Đăng nhập bằng SĐT KhachHang
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
    MaTK CHAR(8),
    CONSTRAINT FK_KhachHang_TaiKhoan FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

-- 2. Bảng NhanVien
CREATE TABLE NhanVien (
    MaNV CHAR(8) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    CCCD CHAR(12),
    NgayVaoLam DATE NOT NULL,
    SoDT VARCHAR(10),
    DiaChi NVARCHAR(500),
    Email VARCHAR(100),
    ChucVu INT,
    TrangThai BIT DEFAULT (0),
    MaTK CHAR(8),
    CONSTRAINT FK_NhanVien_TaiKhoan FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

-- 3. Bảng NhaCungCap
CREATE TABLE NhaCungCap (
    MaNCC CHAR(8) PRIMARY KEY,
    TenNCC NVARCHAR(255) NOT NULL,
    DiaChi NVARCHAR(500),
    Email VARCHAR(100),
    SoDT VARCHAR(10),
    TenNganHang NVARCHAR(100),
    SoTK VARCHAR(30)
);

-- 4. Bảng KhuyenMai
CREATE TABLE KhuyenMai (
    MaKM CHAR(8) PRIMARY KEY,
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
    MaLoai CHAR(8) PRIMARY KEY,
    TenLoai NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX)
);

-- 6. Bảng LoaiBenh
CREATE TABLE LoaiBenh (
    MaLoaiBenh CHAR(8) PRIMARY KEY,
    TenBenh NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX),
    NhomDoiTuong NVARCHAR(255)
);

-- 7. Bảng GoiVaccine
CREATE TABLE GoiVaccine (
    MaGoi CHAR(8) PRIMARY KEY,
    TenGoi NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX),
    DoiTuongApDung NVARCHAR(255),
    GiaGoi DECIMAL(18, 0) NOT NULL,
    NgayBatDau DATE,
    NgayKetThuc DATE,
    TrangThai NVARCHAR(50)
);

-- =================================================================================
-- BẢNG CÓ KHÓA NGOẠI
-- =================================================================================

-- 8. Bảng Vaccine
CREATE TABLE Vaccine (
    MaVC CHAR(8) PRIMARY KEY, -- VCCN0001
    TenVC NVARCHAR(255) NOT NULL,
    GiaBan DECIMAL(18, 0) NOT NULL,
    SoLuongTon INT NOT NULL DEFAULT 0,
    MaLoai CHAR(8),
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
    VaiTro   NVARCHAR(100) NULL,     -- 'Người giám hộ' / 'Bản thân' / ...
    NgayLienKet DATE       NOT NULL,
    MaKH     CHAR(10)      NOT NULL,
    MaHSTC   CHAR(10)      NOT NULL,
    CreatedAt DATETIME2(3) NOT NULL CONSTRAINT DF_LienKetHoSo_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_LienKetHoSo PRIMARY KEY (MaLK),
    CONSTRAINT FK_LienKet_KhachHang
        FOREIGN KEY (MaKH)   REFERENCES dbo.KhachHang(MaKH),
    CONSTRAINT FK_LienKet_HoSoTiemChung
        FOREIGN KEY (MaHSTC) REFERENCES dbo.HoSoTiemChung(MaHSTC)
);


-- 12. Bảng PhieuNhapVaccine
CREATE TABLE PhieuNhapVaccine (
    MaPN CHAR(8) PRIMARY KEY,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    MaNV CHAR(8),
    MaNCC CHAR(8),
    CONSTRAINT FK_PhieuNhap_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_PhieuNhap_NhaCungCap FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

-- 13. Bảng HoaDon
CREATE TABLE HoaDon (
    MaHD CHAR(8) PRIMARY KEY,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18, 0) NOT NULL,
    TrangThai BIT,
    MaKH CHAR(10),
    MaNV CHAR(8),
    MaKM CHAR(8),
    CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_HoaDon_KhuyenMai FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM)
);

-- 14. Bảng LichTiem
CREATE TABLE LichTiem (
    MaLT CHAR(8) PRIMARY KEY,
    NgayHenTiem DATETIME NOT NULL,
    NgayTiemThucTe DATETIME,
    SoMui INT, -- Tiêm mũi thứ mấy
    TrangThai BIT,
    GhiChu NVARCHAR(MAX),
    MaHSTC CHAR(10) NOT NULL,
    CONSTRAINT FK_LichTiem_HoSoTiemChung FOREIGN KEY (MaHSTC) REFERENCES HoSoTiemChung(MaHSTC)
);

-- =================================================================================
-- BẢNG LIÊN KẾT 
-- =================================================================================

-- 15. Bảng VaccinePhongBenh
CREATE TABLE VaccinePhongBenh (
    MaVC CHAR(8) NOT NULL,
    MaLoaiBenh CHAR(8) NOT NULL,
    GhiChu NVARCHAR(MAX),
    PRIMARY KEY (MaVC, MaLoaiBenh),
    CONSTRAINT FK_VPB_Vaccine FOREIGN KEY (MaVC) REFERENCES Vaccine(MaVC),
    CONSTRAINT FK_VPB_LoaiBenh FOREIGN KEY (MaLoaiBenh) REFERENCES LoaiBenh(MaLoaiBenh)
);

-- 16. Bảng ChiTietGoiVaccine
CREATE TABLE ChiTietGoiVaccine (
    MaCTGoi CHAR(8) PRIMARY KEY, -- CTGV0001
    SoMui INT, -- Phác đồ
    ThangTiem INT, -- Ví dụ: 'Tháng thứ 2 sau sinh'
    GhiChu NVARCHAR(MAX),
    MaGoi CHAR(8) NOT NULL,
    MaVC CHAR(8) NOT NULL,
    CONSTRAINT FK_CTGV_GoiVaccine FOREIGN KEY (MaGoi) REFERENCES GoiVaccine(MaGoi),
    CONSTRAINT FK_CTGV_Vaccine FOREIGN KEY (MaVC) REFERENCES Vaccine(MaVC)
);

-- 17. Bảng ChiTietPhieuNhap
CREATE TABLE ChiTietPhieuNhap (
    MaCTPN CHAR(8) PRIMARY KEY,
    NuocSanXuat NVARCHAR(100),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    GiaNhap DECIMAL(18, 0) NOT NULL,
    HanSuDung DATE,
    MaPN CHAR(8) NOT NULL,
    MaVC CHAR(8) NOT NULL,
    CONSTRAINT FK_CTPN_PhieuNhap FOREIGN KEY (MaPN) REFERENCES PhieuNhapVaccine(MaPN),
    CONSTRAINT FK_CTPN_Vaccine FOREIGN KEY (MaVC) REFERENCES Vaccine(MaVC)
);

-- 18. GioHang
CREATE TABLE GioHang (
    MaGH INT PRIMARY KEY IDENTITY(1,1),
    MaKH CHAR(10) NOT NULL,
    MaSanPham CHAR(8) NOT NULL, --  MaGoi hoặc MaVC
    LoaiSanPham NVARCHAR(20) NOT NULL, -- 'GOIVACCINE' hoặc 'VACCINE'
    SoLuong INT NOT NULL DEFAULT 1,
    CONSTRAINT FK_GioHang_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- 19. ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaCTHD CHAR(8) PRIMARY KEY,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18, 0) NOT NULL,
    MaSanPham CHAR(8) NOT NULL, --  MaGoi hoặc MaVC
    LoaiSanPham NVARCHAR(20) NOT NULL, -- 'GOIVACCINE' hoặc 'VACCINE'
    MaHD CHAR(8) NOT NULL,
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
    MaKM CHAR(8) NOT NULL,
    CONSTRAINT FK_CTKM_KhuyenMai FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM)
);
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
        hs.HoTen       AS HoTen,       -- tên hồ sơ
        hs.GioiTinh,
        hs.NgaySinh,
        pick.VaiTro,
        kh.MaKH,
        kh.HoTen       AS TenKhachHang,
        kh.CCCD,
        pick.CreatedAt,                -- để kiểm tra bản ghi đã chọn
        pick.NgayLienKet               -- fallback khi chưa có CreatedAt
    FROM dbo.HoSoTiemChung AS hs
    OUTER APPLY (
        SELECT TOP 1 
               l.MaKH, l.VaiTro,
               l.CreatedAt,
               l.NgayLienKet
        FROM dbo.LienKetHoSo l
        WHERE l.MaHSTC = hs.MaHSTC
        ORDER BY 
            CASE WHEN l.VaiTro = N'Bản thân' THEN 0 ELSE 1 END,  -- ưu tiên "Bản thân"
            l.CreatedAt ASC,                                     -- sớm nhất theo CreatedAt
            l.NgayLienKet ASC,                                   -- fallback nếu CreatedAt chưa có
            l.MaKH                                              -- tie-break ổn định
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
