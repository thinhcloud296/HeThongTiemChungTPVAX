-- =================================================================================
-- =================================================================================
-- =================================================================================
-- ============================INSERT DỮ LIỆU (ĐÃ SỬA LỖI ĐỘ DÀI KHÓA)===============
-- =================================================================================
-- =================================================================================

-- =================================================================================
-- PHẦN 1: INSERT DỮ LIỆU CƠ SỞ
-- --- SỬA LỖI: Đã đệm các Ma... (CHAR(8))
-- =================================================================================

-- 1.1. Bảng LoaiBenh (CHAR(8))
INSERT INTO LoaiBenh (MaLoaiBenh, TenBenh, NhomDoiTuong) VALUES
('LB000001', N'Bạch hầu', N'Trẻ em, người lớn'),
('LB000002', N'Ho gà', N'Trẻ em, người lớn'),
('LB000003', N'Uốn ván', N'Trẻ em, người lớn'),
('LB000004', N'Bại liệt', N'Trẻ em'),
('LB000005', N'Viêm gan B', N'Trẻ sơ sinh, trẻ em, người lớn'),
('LB000006', N'Viêm phổi do Hib', N'Trẻ em'),
('LB000007', N'Tiêu chảy do Rotavirus', N'Trẻ em'),
('LB000008', N'Các bệnh do phế cầu', N'Trẻ em, người lớn'),
('LB000009', N'Lao', N'Trẻ sơ sinh'),
('LB000010', N'Viêm màng não do não mô cầu B', N'Trẻ em, người lớn'),
('LB000011', N'Viêm màng não do não mô cầu BC', N'Trẻ em, người lớn'),
('LB000012', N'Viêm màng não do não mô cầu ACYW', N'Trẻ em, người lớn'),
('LB000013', N'Sởi', N'Trẻ em'),
('LB000014', N'Quai bị', N'Trẻ em'),
('LB000015', N'Rubella', N'Trẻ em, phụ nữ mang thai'),
('LB000016', N'Thủy đậu', N'Trẻ em, người lớn'),
('LB000017', N'Cúm mùa', N'Trẻ em, người lớn'),
('LB000018', N'Viêm não Nhật Bản', N'Trẻ em'),
('LB000019', N'Thương hàn', N'Người lớn'),
('LB000020', N'Bệnh dại', N'Mọi đối tượng'),
('LB000021', N'Ung thư cổ tử cung do HPV', N'Nữ giới, nam giới'),
('LB000022', N'Viêm gan A', N'Trẻ em, người lớn'),
('LB000023', N'Bệnh tả', N'Người lớn'),
('LB000024', N'Zona thần kinh', N'Người lớn tuổi'),
('LB000025', N'Sốt xuất huyết', N'Trẻ em, người lớn'),
('LB000026', N'Virus hợp bào hô hấp (RSV)', N'Người lớn tuổi, phụ nữ mang thai');

-- 1.2. Bảng LoaiVaccine (CHAR(8))
INSERT INTO LoaiVaccine (MaLoai, TenLoai, MoTa) VALUES
('LVC00001', N'Vắc xin kết hợp', N'Vắc xin phòng nhiều bệnh trong một mũi tiêm (6in1, 5in1, 4in1, 3in1)'),
('LVC00002', N'Vắc xin đơn lẻ', N'Vắc xin chỉ phòng một bệnh duy nhất'),
('LVC00003', N'Vắc xin sống, giảm độc lực', N'Chứa vi sinh vật đã được làm yếu đi'),
('LVC00004', N'Vắc xin bất hoạt', N'Chứa vi sinh vật đã bị giết chết'),
('LVC00005', N'Vắc xin giải độc tố', N'Chứa độc tố của vi khuẩn đã được xử lý'),
('LVC00006', N'Vắc xin tiểu đơn vị', N'Chỉ chứa các thành phần đặc trưng của mầm bệnh');

-- 1.3. Bảng NhaCungCap (CHAR(8))
-- --- GHI CHÚ: Các khóa tự nhiên (natural key) như 'GSK' sẽ được CSDL tự đệm khoảng trắng phía sau.
-- --- Vẫn giữ nguyên để dễ tham chiếu.
INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi) VALUES
('GSK', N'GlaxoSmithKline', N'Bỉ'),
('SANOFI', N'Sanofi Pasteur', N'Pháp'),
('MSD', N'Merck Sharp & Dohme', N'Mỹ'),
('PFIZER', N'Pfizer', N'Mỹ'),
('VABIOTEC', N'Công ty Vắc xin và Sinh phẩm số 1', N'Việt Nam'),
('POLYVAC', N'Trung tâm Nghiên cứu, Sản xuất Vắc xin và Sinh phẩm y tế', N'Việt Nam'),
('TAKEDA', N'Takeda Pharmaceutical', N'Nhật Bản');
GO

-- =================================================================================
-- PHẦN 2: INSERT DỮ LIỆU VACCINE VÀ QUAN HỆ VACCINE-BỆNH
-- =================================================================================

-- 2.1. Bảng Vaccine (CHAR(8))
INSERT INTO Vaccine (MaVC, TenVC, GiaBan, SoLuongTon, MaLoai) VALUES
('VC000001', N'Infanrix Hexa (6 trong 1)', 1098000, 100, 'LVC00001'),
('VC000002', N'Hexaxim (6 trong 1)', 1098000, 100, 'LVC00001'),
('VC000003', N'Rotateq', 665000, 100, 'LVC00003'),
('VC000004', N'Rotarix', 928000, 100, 'LVC00003'),
('VC000005', N'Rotavin', 490000, 100, 'LVC00003'),
('VC000006', N'Synflorix', 1048000, 100, 'LVC00006'),
('VC000007', N'Prevenar 13', 1290000, 100, 'LVC00006'),
('VC000008', N'BCG', 165000, 100, 'LVC00003'),
('VC000009', N'Gene Hbvax 1ml', 268000, 100, 'LVC00006'),
('VC000010', N'Heberbiovac 1ml', 295000, 100, 'LVC00006'),
('VC000011', N'Bexsero', 1788000, 100, 'LVC00006'),
('VC000012', N'VA-Mengoc-BC', 396000, 100, 'LVC00006'),
('VC000013', N'Nimenrix', 1750000, 100, 'LVC00006'),
('VC000014', N'MenQuadfi', 1950000, 100, 'LVC00006'),
('VC000015', N'Menactra', 1370000, 100, 'LVC00006'),
('VC000016', N'MMR II (3 trong 1)', 495000, 100, 'LVC00003'),
('VC000017', N'Priorix', 498000, 100, 'LVC00003'),
('VC000018', N'Varivax', 1118000, 100, 'LVC00003'),
('VC000019', N'Varilrix', 1138000, 100, 'LVC00003'),
('VC000020', N'Vaxigrip Tetra 0.5ml', 356000, 100, 'LVC00004'),
('VC000021', N'Influvac Tetra 0.5ml', 356000, 100, 'LVC00004'),
('VC000022', N'Gardasil', 1790000, 100, 'LVC00006'),
('VC000023', N'Gardasil 9', 2998000, 100, 'LVC00006'),
('VC000024', N'Imojev', 968000, 100, 'LVC00003'),
('VC000025', N'Jeev', 498000, 100, 'LVC00004'),
('VC000026', N'Verorab', 538000, 100, 'LVC00004'),
('VC000027', N'Adacel', 798000, 100, 'LVC00001'),
('VC000028', N'Boostrix', 856000, 100, 'LVC00001'),
('VC000029', N'Tetraxim', 668000, 100, 'LVC00001'),
('VC000030', N'Twinrix', 768000, 100, 'LVC00004'),
('VC000031', N'Havax 0.5ml', 295000, 100, 'LVC00004'),
('VC000032', N'Typhim VI', 438000, 100, 'LVC00006'),
('VC000033', N'Qdenga', 1390000, 100, 'LVC00003'),
('VC000034', N'Abrysvo', 5458000, 100, 'LVC00006'),
('VC000035', N'Shingrix', 3890000, 100, 'LVC00006');

-- 2.2. Bảng VaccinePhongBenh (Dùng khóa đã đệm)
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES
('VC000001', 'LB000001'), ('VC000001', 'LB000002'), ('VC000001', 'LB000003'), ('VC000001', 'LB000004'), ('VC000001', 'LB000005'), ('VC000001', 'LB000006'),
('VC000002', 'LB000001'), ('VC000002', 'LB000002'), ('VC000002', 'LB000003'), ('VC000002', 'LB000004'), ('VC000002', 'LB000005'), ('VC000002', 'LB000006');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000003', 'LB000007'), ('VC000004', 'LB000007'), ('VC000005', 'LB000007');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000006', 'LB000008'), ('VC000007', 'LB000008');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000008', 'LB000009');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000009', 'LB000005'), ('VC000010', 'LB000005');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES
('VC000011', 'LB000010'), ('VC000012', 'LB000011'), ('VC000013', 'LB000012'), ('VC000014', 'LB000012'), ('VC000015', 'LB000012');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES
('VC000016', 'LB000013'), ('VC000016', 'LB000014'), ('VC000016', 'LB000015'),
('VC000017', 'LB000013'), ('VC000017', 'LB000014'), ('VC000017', 'LB000015');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000018', 'LB000016'), ('VC000019', 'LB000016');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000020', 'LB000017'), ('VC000021', 'LB000017');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000022', 'LB000021'), ('VC000023', 'LB000021');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000024', 'LB000018'), ('VC000025', 'LB000018');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000026', 'LB000020');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES
('VC000027', 'LB000001'), ('VC000027', 'LB000002'), ('VC000027', 'LB000003'),
('VC000028', 'LB000001'), ('VC000028', 'LB000002'), ('VC000028', 'LB000003');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000029', 'LB000001'), ('VC000029', 'LB000002'), ('VC000029', 'LB000003'), ('VC000029', 'LB000004');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000030', 'LB000022'), ('VC000030', 'LB000005');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000031', 'LB000022');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000032', 'LB000019');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000033', 'LB000025');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000034', 'LB000026');
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES ('VC000035', 'LB000024');
GO

-- =================================================================================
-- PHẦN 3: INSERT DỮ LIỆU GÓI VACCINE VÀ CHI TIẾT
-- =================================================================================

-- 3.1. Bảng GoiVaccine (CHAR(8))
INSERT INTO GoiVaccine (MaGoi, TenGoi, MoTa, DoiTuongApDung, GiaGoi, TrangThai) VALUES
('GOI00001', N'Gói vắc xin cho trẻ từ 0-12 tháng', N'Bảo vệ con trong năm đầu đời với các vắc xin quan trọng nhất.', N'Trẻ từ 0 đến 12 tháng tuổi', 8554000, N'Đang áp dụng'),
('GOI00002', N'Gói vắc xin cho trẻ từ 0-24 tháng', N'Gói vắc xin toàn diện nhất, bảo vệ con yêu đến 2 tuổi.', N'Trẻ từ 0 đến 24 tháng tuổi', 12872000, N'Đang áp dụng'),
('GOI00003', N'Gói vắc xin cho phụ nữ chuẩn bị mang thai', N'Tiêm phòng đầy đủ trước khi mang thai để bảo vệ mẹ và bé.', N'Phụ nữ chuẩn bị mang thai', 2492000, N'Đang áp dụng'),
('GOI00004', N'Gói vắc xin cho người lớn', N'Chủ động phòng ngừa các bệnh truyền nhiễm nguy hiểm cho người trưởng thành.', N'Người lớn', 3154000, N'Đang áp dụng');

-- 3.2. Bảng ChiTietGoiVaccine (CHAR(8))
-- --- SỬA LỖI: Đã sửa MaCTGoi thành 'CTGVxxxx' (CHAR(8)) theo gợi ý của File 2 và đệm các khóa ngoại.
INSERT INTO ChiTietGoiVaccine (MaCTGoi, MaGoi, MaVC, ThangTiem, SoMui) VALUES
('CTGV0001', 'GOI00001', 'VC000008', 0, 1),
('CTGV0002', 'GOI00001', 'VC000009', 0, 1),
('CTGV0003', 'GOI00001', 'VC000001', 2, 1),
('CTGV0004', 'GOI00001', 'VC000003', 2, 1),
('CTGV0005', 'GOI00001', 'VC000007', 2, 1),
('CTGV0006', 'GOI00001', 'VC000001', 3, 2),
('CTGV0007', 'GOI00001', 'VC000003', 3, 2),
('CTGV0008', 'GOI00001', 'VC000001', 4, 3),
('CTGV0009', 'GOI00001', 'VC000003', 4, 3),
('CTGV0010', 'GOI00001', 'VC000007', 4, 2),
('CTGV0011', 'GOI00001', 'VC000020', 6, 1),
('CTGV0012', 'GOI00001', 'VC000016', 9, 1),
('CTGV0013', 'GOI00001', 'VC000018', 12, 1),
('CTGV0014', 'GOI00001', 'VC000024', 12, 1);

INSERT INTO ChiTietGoiVaccine (MaCTGoi, MaGoi, MaVC, ThangTiem, SoMui) VALUES
('CTGV0015', 'GOI00003', 'VC000016', -3, 1),
('CTGV0016', 'GOI00003', 'VC000018', -3, 1),
('CTGV0017', 'GOI00003', 'VC000020', -1, 1),
('CTGV0018', 'GOI00003', 'VC000028', -1, 1);
GO


-- =================================================================================
-- BỔ SUNG NHÂN VIÊN MỚI THEO YÊU CẦU (CHAR(8))
-- =================================================================================
INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, NgaySinh, CCCD, NgayVaoLam, SoDT, DiaChi, Email, ChucVu, TrangThai) VALUES
('NV000004', N'Nguyễn Hoàng Thịnh', N'Nam', '2004-10-15', '079204001122', '2024-03-01', '0915111222', N'10 An Dương Vương, Quận 5, TP.HCM', 'thinh.nh@tpvax.com', 1, 1),
('NV000005', N'Trần Tấn Tài', N'Nam', '2004-07-22', '079204003344', '2024-03-01', '0915333444', N'20 Ngô Quyền, Quận 10, TP.HCM', 'tai.tt@tpvax.com', 1, 1),
('NV000006', N'Phạm Văn Phi', N'Nam', '2004-09-05', '079204005566', '2024-04-15', '0915555666', N'30 Trần Hưng Đạo, Quận 1, TP.HCM', 'phi.pv@tpvax.com', 2, 1);
GO

PRINT '-> Đã thêm 3 nhân viên mới: Thịnh, Tài, Phi.';
GO

-- =================================================================================
-- TẠO THÊM DỮ LIỆU KHÁCH HÀNG (CHAR(10)) VÀ TÀI KHOẢN (CHAR(8))
-- =================================================================================
INSERT INTO KhachHang
    (MaKH, HoTen, CCCD, NgaySinh, GioiTinh, DiaChi, SoDT, Email)
VALUES
('KH00000004', N'Phạm Thị Dung', '079812345678', '1998-01-20', N'Nữ',
 N'111 Hai Bà Trưng, Quận 1, TP.HCM', '0988123123', 'dung.pham@email.com'),

('KH00000005', N'Đỗ Hùng Dũng',  '079923456789', '1993-09-08', N'Nam',
 N'222 Lý Thường Kiệt, Quận 11, TP.HCM', '0988456456', 'dung.do@email.com'),

('KH00000006', N'Võ Văn Thanh',  '079334455667', '1996-04-14', N'Nam',
 N'333 Lê Văn Sỹ, Quận 3, TP.HCM', '0988789789', 'thanh.vo@email.com'),

('KH00000007', N'Bùi Tiến Dũng', '079445566778', '1997-02-28', N'Nam',
 N'444 Cộng Hòa, Quận Tân Bình, TP.HCM', '0977111222', 'dung.bui@email.com'),

('KH00000008', N'Đoàn Văn Hậu',  '079556677889', '1999-04-19', N'Nam',
 N'555 Nguyễn Văn Cừ, Quận 5, TP.HCM', '0977333444', 'hau.doan@email.com');
GO
INSERT INTO KhachHang
    (MaKH, HoTen, CCCD, NgaySinh, GioiTinh, DiaChi, SoDT, Email)
VALUES
('KH00000009', N'Nguyễn Minh Anh', '079600000001', '1988-03-12', N'Nam', N'12 Nguyễn Trãi, Q.1, TP.HCM', '0901234567', 'anh.nguyen@email.com'),
('KH00000010', N'Trần Thị Lan',    '079600000002', '1990-07-25', N'Nữ',  N'23 Cách Mạng Tháng 8, Q.3, TP.HCM', '0912345678', 'lan.tran@email.com'),
('KH00000011', N'Phạm Quốc Khánh', '079600000003', '1985-11-05', N'Nam', N'45 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0923456789', 'khanh.pham@email.com'),
('KH00000012', N'Lê Thu Hà',       '079600000004', '1992-02-17', N'Nữ',  N'78 Trường Chinh, Q.Tân Bình, TP.HCM', '0934567890', 'ha.le@email.com'),
('KH00000013', N'Vũ Đức Long',     '079600000005', '1989-09-09', N'Nam', N'56 Nguyễn Oanh, Q.Gò Vấp, TP.HCM', '0945678901', 'long.vu@email.com'),
('KH00000014', N'Bùi Ngọc Mai',    '079600000006', '1994-12-28', N'Nữ',  N'101 Lạc Long Quân, Q.11, TP.HCM', '0956789012', 'mai.bui@email.com'),
('KH00000015', N'Đặng Hoàng Nam',  '079600000007', '1987-06-03', N'Nam', N'34 Lê Duẩn, Q.1, TP.HCM', '0967890123', 'nam.dang@email.com'),
('KH00000016', N'Hồ Thu Trang',    '079600000008', '1991-04-14', N'Nữ',  N'89 Nguyễn Thị Minh Khai, Q.1, TP.HCM', '0978901234', 'trang.ho@email.com'),
('KH00000017', N'Nguyễn Hải Yến',  '079600000009', '1993-10-21', N'Nữ',  N'77 Phạm Văn Đồng, TP.Thủ Đức, TP.HCM', '0989012345', 'yen.nguyen@email.com'),
('KH00000018', N'Trần Văn Phúc',   '079600000010', '1986-01-30', N'Nam', N'200 Quang Trung, Q.Gò Vấp, TP.HCM', '0990123456', 'phuc.tran@email.com');
GO


INSERT INTO TaiKhoan (MaTK, TenDangNhap, MatKhau) VALUES
('TK000004', 'dung.pham', 'password123'),
('TK000005', 'dung.do', 'password123'),
('TK000006', 'thanh.vo', 'password123'),
('TK000007', 'dung.bui', 'password123'),
('TK000008', 'hau.doan', 'password123');
GO

-- Cập nhật khóa ngoại đã đệm
UPDATE KhachHang SET MaTK = 'TK000004' WHERE MaKH = 'KH00000004';
UPDATE KhachHang SET MaTK = 'TK000005' WHERE MaKH = 'KH00000005';
UPDATE KhachHang SET MaTK = 'TK000006' WHERE MaKH = 'KH00000006';
UPDATE KhachHang SET MaTK = 'TK000007' WHERE MaKH = 'KH00000007';
UPDATE KhachHang SET MaTK = 'TK000008' WHERE MaKH = 'KH00000008';
GO

PRINT '-> Đã thêm 5 khách hàng mới và tài khoản tương ứng.';
GO


-- =================================================================================
-- DỮ LIỆU KHUYẾN MÃI (CHAR(8))
-- =================================================================================
INSERT INTO KhuyenMai (MaKM, TenKM, MoTa, LoaiKM, KieuGiam, GiaTriGiam, NgayBatDau, NgayKetThuc, TrangThai) VALUES
('KM000001', N'Giảm 10% gói vắc xin 0-24 tháng', N'Ưu đãi đặc biệt cho các bé trong dịp hè 2025. Áp dụng cho gói GOI02.', N'Sản phẩm', 'PhanTram', 10.00, '2025-06-01 00:00:00', '2025-06-30 23:59:59', 1),
('KM000002', N'Giảm 50k phòng cúm mùa thu', N'Chủ động phòng cúm với ưu đãi giảm 50,000đ cho vắc xin Vaxigrip Tetra.', N'Sản phẩm', 'SoTien', 50000, '2025-09-01 00:00:00', '2025-09-30 23:59:59', 0);
GO

PRINT '-> Đã chèn thành công 2 chương trình khuyến mãi.';
GO

-- =================================================================================
-- DỮ LIỆU CHI TIẾT KHUYẾN MÃI (Dùng khóa đã đệm)
-- =================================================================================
INSERT INTO ChiTietKhuyenMai (MaKM, LoaiSanPham, MaSanPham, GhiChu) VALUES
('KM000001', 'GOIVACCINE', 'GOI00002', N'Áp dụng cho khách hàng mua trọn gói 0-24 tháng'),
('KM000002', 'VACCINE', 'VC000020', N'Áp dụng cho vắc xin cúm của Sanofi');
GO

PRINT '-> Đã liên kết thành công khuyến mãi với các sản phẩm cụ thể.';
GO

-- =================================================================================
-- TẠO THÊM HỒ SƠ TIÊM CHỦNG (CHAR(10)) VÀ LỊCH TIÊM (CHAR(8))
-- =================================================================================
INSERT INTO HoSoTiemChung
    (MaHSTC, HoTen, GioiTinh, NgaySinh, CCCD, GhiChu, TrangThai)
VALUES
('HSTC000004', N'Phạm Thị Dung', N'Nữ',  '1998-01-20', '079812345678', N'Chuẩn bị mang thai', 1),
('HSTC000005', N'Đỗ Hùng Dũng',  N'Nam', '1993-09-08', '079923456789', N'Tiêm nhắc cúm hàng năm', 1),
('HSTC000006', N'Võ Bảo An',     N'Nữ',  '2024-01-01',       '079923456712',           N'Bé khỏe, theo dõi lịch tiêm chủng mở rộng', 1),
('HSTC000007', N'Bùi Tiến Dũng', N'Nam', '1997-02-28', '079445566778', N'Tiêm vắc xin Viêm gan B', 0),
('HSTC000008', N'Đoàn Văn Hậu',  N'Nam', '1999-04-19', '079556677889', N'Tiêm vắc xin dại do bị chó cắn', 1);
GO
INSERT INTO HoSoTiemChung
    (MaHSTC, HoTen, GioiTinh, NgaySinh, CCCD, GhiChu, TrangThai)
VALUES
('HSTC000009', N'Nguyễn Minh Anh', N'Nam', '1988-03-12' , '079600000001', N'Tiêm phòng uốn ván', 1),
('HSTC000010', N'Trần Thị Lan',    N'Nữ',  '1990-07-25' , '079600000002', N'Tiêm phòng cúm mùa', 1),
('HSTC000011', N'Phạm Quốc Khánh', N'Nam', '1985-11-05' , '079600000003', N'Tiêm nhắc viêm gan B', 1),
('HSTC000012', N'Lê Thu Hà',       N'Nữ',  '1992-02-17' , '079600000004', N'Tiêm HPV mũi 1', 1),
('HSTC000013', N'Vũ Đức Long',     N'Nam', '1989-09-09' , '079600000005', N'Tiêm phòng dại do phơi nhiễm', 1),
('HSTC000014', N'Bùi Ngọc Mai',    N'Nữ',  '1994-12-28' , '079600000006', N'Lịch tiêm trước thai kỳ', 1),
('HSTC000015', N'Đặng Hoàng Nam',  N'Nam', '1987-06-03' , '079600000007', N'Tiêm uốn ván mũi 2', 1),
('HSTC000016', N'Hồ Thu Trang',    N'Nữ',  '1991-04-14' , '079600000008', N'Tiêm viêm màng não mô cầu', 1),
('HSTC000017', N'Nguyễn Hải Yến',  N'Nữ',  '1993-10-21' , '079600000009', N'Tiêm phòng sởi - rubella', 1),
('HSTC000018', N'Trần Văn Phúc',   N'Nam', '1986-01-30' , '079600000010', N'Tiêm phòng viêm phổi', 1);
GO

-- Bảng LienKetHoSo (CHAR(10))
INSERT INTO LienKetHoSo (MaLK,VaiTro, NgayLienKet, MaKH, MaHSTC) VALUES
( 'LKHS000001',N'Con', '2024-05-10', 'KH00000004', 'HSTC000006'),
( 'LKHS000002',N'Bản thân', '2024-05-10', 'KH00000004', 'HSTC000004'),
( 'LKHS000003',N'Bản thân', '2024-05-10', 'KH00000005', 'HSTC000005'),
( 'LKHS000004',N'Người giám hộ', '2024-03-01', 'KH00000006', 'HSTC000006'),
( 'LKHS000005',N'Bản thân', '2024-05-10', 'KH00000007', 'HSTC000007'),
( 'LKHS000006',N'Bản thân', '2024-07-20', 'KH00000008', 'HSTC000008');
GO
INSERT INTO LienKetHoSo (MaLK, VaiTro, NgayLienKet, MaKH, MaHSTC, CreatedAt) VALUES
-- KH00000009 liên kết 3 hồ sơ (bản thân + giám hộ 2 hồ sơ khác)
('LKHS000007', N'Bản thân',         '2024-06-01', 'KH00000009', 'HSTC000009', '2024-06-01T08:00:00'),
('LKHS000008', N'Người giám hộ',    '2024-06-05', 'KH00000009', 'HSTC000011', '2024-06-05T09:00:00'),
('LKHS000009', N'Người giám hộ',    '2024-06-10', 'KH00000009', 'HSTC000012', '2024-06-10T10:00:00'),

-- KH00000010 liên kết 2 hồ sơ (bản thân + giám hộ)
('LKHS000010', N'Bản thân',         '2024-06-02', 'KH00000010', 'HSTC000010', '2024-06-02T08:30:00'),
('LKHS000011', N'Người chăm sóc',   '2024-06-06', 'KH00000010', 'HSTC000013', '2024-06-06T09:30:00'),

-- KH00000011 liên kết 2 hồ sơ
('LKHS000012', N'Bản thân',         '2024-06-03', 'KH00000011', 'HSTC000011', '2024-06-03T08:45:00'),
('LKHS000013', N'Người giám hộ',    '2024-06-07', 'KH00000011', 'HSTC000014', '2024-06-07T09:45:00'),

-- KH00000012 liên kết 2 hồ sơ
('LKHS000014', N'Bản thân',         '2024-06-04', 'KH00000012', 'HSTC000012', '2024-06-04T08:15:00'),
('LKHS000015', N'Người giám hộ',    '2024-06-08', 'KH00000012', 'HSTC000015', '2024-06-08T09:15:00'),

-- KH00000013 liên kết 1 hồ sơ (bản thân)
('LKHS000016', N'Bản thân',         '2024-06-05', 'KH00000013', 'HSTC000013', '2024-06-05T08:05:00');
GO

-- Bảng LichTiem (CHAR(8))
INSERT INTO LichTiem (MaLT, MaHSTC, NgayHenTiem, NgayTiemThucTe, SoMui, TrangThai, GhiChu) VALUES
('LT000005', 'HSTC000004', '2024-05-10 10:00:00', '2024-05-10 10:15:00', 1, 1, N'Tiêm mũi MMR'),
('LT000006', 'HSTC000004', '2024-06-10 10:00:00', NULL, 2, 1, N'Tiêm mũi Thủy đậu'),
('LT000007', 'HSTC000006', '2024-03-01 09:30:00', '2024-03-01 09:35:00', 1, 1, N'Mũi 6in1 + Rota'),
('LT000008', 'HSTC000006', '2024-04-01 09:30:00', '2024-04-01 10:00:00', 2, 1, N'Mũi 6in1 + Rota lần 2'),
('LT000009', 'HSTC000006', '2024-05-01 09:30:00', NULL, 3, 1, N'Lịch hẹn mũi 6in1 + Rota lần 3'),
('LT000010', 'HSTC000005', '2024-09-15 14:00:00', NULL, 1, 0, N'Khách hàng đặt online'),
('LT000011', 'HSTC000008', '2024-07-20 08:00:00', '2024-07-20 08:10:00', 1, 1, N'Mũi dại đầu tiên'),
('LT000012', 'HSTC000008', '2024-07-23 08:00:00', NULL, 2, 1, N'Hẹn tiêm mũi dại thứ 2');
GO

PRINT '-> Đã thêm hồ sơ tiêm chủng và lịch tiêm mới.';
GO

-- =================================================================================
-- TẠO THÊM DỮ LIỆU HÓA ĐƠN (CHAR(8))
-- =================================================================================
INSERT INTO HoaDon (MaHD, NgayLap, TongTien, TrangThai, MaKH, MaNV, MaKM) VALUES
('HD000004', '2024-05-10 10:20:00', 2492000, 1, 'KH00000004', 'NV000004', NULL),
('HD000005', '2024-03-01 09:40:00', 1763000, 1, 'KH00000006', 'NV000005', NULL),
('HD000006', '2024-04-01 10:05:00', 1763000, 1, 'KH00000006', 'NV000004', NULL),
('HD000007', '2024-07-20 08:15:00', 538000, 1, 'KH00000008', 'NV000005', NULL),
('HD000008', '2025-06-20 11:00:00', 11584800, 1, 'KH00000005', 'NV000004', 'KM000001');
GO

-- Chi tiết cho các hóa đơn mới (MaCTHD CHAR(8))
-- --- SỬA LỖI: Đã sửa MaCTHD thành 'CTHDxxxx' (CHAR(8)) và đệm các khóa ngoại.
INSERT INTO ChiTietHoaDon (MaCTHD, MaHD, MaSanPham, LoaiSanPham, SoLuong, DonGia) VALUES
('CTHD0001', 'HD000004', 'GOI00003', 'GOIVACCINE', 1, 2492000),
('CTHD0002', 'HD000005', 'VC000002', 'VACCINE', 1, 1098000),
('CTHD0003', 'HD000005', 'VC000003', 'VACCINE', 1, 665000),
('CTHD0004', 'HD000006', 'VC000002', 'VACCINE', 1, 1098000),
('CTHD0005', 'HD000006', 'VC000003', 'VACCINE', 1, 665000),
('CTHD0006', 'HD000007', 'VC000026', 'VACCINE', 1, 538000),
('CTHD0007', 'HD000008', 'GOI00002', 'GOIVACCINE', 1, 12872000);
GO

PRINT '-> Đã thêm nhiều hóa đơn và chi tiết hóa đơn mới.';
GO

-- =================================================================================
-- TẠO THÊM DỮ LIỆU NHẬP KHO (CHAR(8))
-- =================================================================================
INSERT INTO PhieuNhapVaccine (MaPN, NgayLap, MaNV, MaNCC) VALUES
('PN000003', '2024-05-05 09:00:00', 'NV000006', 'SANOFI'),
('PN000004', '2024-06-10 14:30:00', 'NV000006', 'PFIZER');
GO

-- Chi tiết phiếu nhập mới (MaCTPN CHAR(8))
-- --- SỬA LỖI: Đã sửa MaCTPN thành 'CTPNxxxx' (CHAR(8)) và đệm các khóa ngoại.
INSERT INTO ChiTietPhieuNhap (MaCTPN, MaPN, MaVC, NuocSanXuat, SoLuong, GiaNhap, HanSuDung) VALUES
('CTPN0001', 'PN000003', 'VC000002', N'Pháp', 100, 850000, '2027-04-30'),
('CTPN0002', 'PN000003', 'VC000020', N'Pháp', 200, 280000, '2026-05-31'),
('CTPN0003', 'PN000004', 'VC000007', N'Mỹ', 150, 1050000, '2027-06-30');
GO

PRINT '-> Đã thêm phiếu nhập kho mới do nhân viên Phi thực hiện.';
GO

PRINT 'Hoàn tất việc chèn dữ liệu nâng cao (ĐÃ SỬA LỖI ĐỘ DÀI KHÓA)!';
GO

-- =================================================================================
-- GÁN ĐƯỜNG DẪN HÌNH ẢNH SAU KHI INSERT DỮ LIỆU
-- =================================================================================

-- 1. Cập nhật ảnh cho 35 Vaccine
UPDATE Vaccine SET HinhAnh = RTRIM(MaVC) + '.jpg';

-- 2. Cập nhật ảnh cho 4 Gói Vaccine
UPDATE GoiVaccine SET HinhAnh = RTRIM(MaGoi) + '.jpg';

-- 3. Cập nhật ảnh cho 2 Khuyến Mãi
UPDATE KhuyenMai SET HinhAnhBanner = RTRIM(MaKM) + '.jpg';

PRINT '-> Đã gán đường dẫn ảnh cho 41 bản ghi (Vaccine, GoiVaccine, KhuyenMai).';
GO