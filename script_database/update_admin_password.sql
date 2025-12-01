-- =================================================================================
-- SCRIPT CẬP NHẬT MẬT KHẨU ADMIN SANG BCRYPT HASH
-- Mật khẩu: 123456
-- BCrypt Hash: $2a$12$7ymUclW3H9Q79qEra9SnieC3quxs3Lwm5a3w9eRLp2fD1ZXZjykbW
-- =================================================================================

-- Cập nhật mật khẩu cho tất cả tài khoản nhân viên (admin)
UPDATE TaiKhoan 
SET MatKhau = '$2a$12$7ymUclW3H9Q79qEra9SnieC3quxs3Lwm5a3w9eRLp2fD1ZXZjykbW'
WHERE MaTK IN (
    SELECT MaTK FROM NhanVien WHERE MaTK IS NOT NULL
);

-- Kiểm tra kết quả
SELECT 
    tk.MaTK,
    nv.HoTen,
    nv.ChucVu,
    nv.Email,
    nv.SoDT,
    tk.MatKhau
FROM TaiKhoan tk
INNER JOIN NhanVien nv ON tk.MaTK = nv.MaTK;

PRINT N'Đã cập nhật mật khẩu BCrypt cho tất cả tài khoản admin (123456)';
GO
