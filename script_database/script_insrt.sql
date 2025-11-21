
-- =================================================================================
-- PHẦN 1: INSERT DỮ LIỆU CƠ SỞ
-- =================================================================================

-- 1.1. Bảng LoaiBenh (SỬA TÊN CỘT VÀ ĐỘ DÀI MÃ)
INSERT INTO LoaiBenh (MaLoaiBenh, TenBenh, NhomDoiTuong) VALUES
('LB00000001', N'Bạch hầu', N'Trẻ em, người lớn'),
('LB00000002', N'Ho gà', N'Trẻ em, người lớn'),
('LB00000003', N'Uốn ván', N'Trẻ em, người lớn'),
('LB00000004', N'Bại liệt', N'Trẻ em'),
('LB00000005', N'Viêm gan B', N'Trẻ sơ sinh, trẻ em, người lớn'),
('LB00000006', N'Viêm phổi do Hib', N'Trẻ em'),
('LB00000007', N'Tiêu chảy do Rotavirus', N'Trẻ em'),
('LB00000008', N'Các bệnh do phế cầu', N'Trẻ em, người lớn'),
('LB00000009', N'Lao', N'Trẻ sơ sinh'),
('LB00000010', N'Viêm màng não do não mô cầu B', N'Trẻ em, người lớn'),
('LB00000011', N'Viêm màng não do não mô cầu BC', N'Trẻ em, người lớn'),
('LB00000012', N'Viêm màng não do não mô cầu ACYW', N'Trẻ em, người lớn'),
('LB00000013', N'Sởi', N'Trẻ em'),
('LB00000014', N'Quai bị', N'Trẻ em'),
('LB00000015', N'Rubella', N'Trẻ em, phụ nữ mang thai'),
('LB00000016', N'Thủy đậu', N'Trẻ em, người lớn'),
('LB00000017', N'Cúm mùa', N'Trẻ em, người lớn'),
('LB00000018', N'Viêm não Nhật Bản', N'Trẻ em'),
('LB00000019', N'Thương hàn', N'Người lớn'),
('LB00000020', N'Bệnh dại', N'Mọi đối tượng'),
('LB00000021', N'Ung thư cổ tử cung do HPV', N'Nữ giới, nam giới'),
('LB00000022', N'Viêm gan A', N'Trẻ em, người lớn'),
('LB00000023', N'Bệnh tả', N'Người lớn'),
('LB00000024', N'Zona thần kinh', N'Người lớn tuổi'),
('LB00000025', N'Sốt xuất huyết', N'Trẻ em, người lớn'),
('LB00000026', N'Virus hợp bào hô hấp (RSV)', N'Người lớn tuổi, phụ nữ mang thai');

-- 1.2. Bảng LoaiVaccine 
INSERT INTO LoaiVaccine (MaLoai, TenLoai, MoTa) VALUES
('LVC0000001', N'Vắc xin kết hợp', N'Vắc xin phòng nhiều bệnh trong một mũi tiêm (6in1, 5in1, 4in1, 3in1)'),
('LVC0000002', N'Vắc xin đơn lẻ', N'Vắc xin chỉ phòng một bệnh duy nhất'),
('LVC0000003', N'Vắc xin sống, giảm độc lực', N'Chứa vi sinh vật đã được làm yếu đi'),
('LVC0000004', N'Vắc xin bất hoạt', N'Chứa vi sinh vật đã bị giết chết'),
('LVC0000005', N'Vắc xin giải độc tố', N'Chứa độc tố của vi khuẩn đã được xử lý'),
('LVC0000006', N'Vắc xin tiểu đơn vị', N'Chỉ chứa các thành phần đặc trưng của mầm bệnh');

-- 1.3. Bảng NhaCungCap (SỬA ĐỘ DÀI MÃ VÀ CHUẨN HÓA MÃ)
INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi) VALUES
('NCC0000001', N'GlaxoSmithKline', N'Bỉ'),
('NCC0000002', N'Sanofi Pasteur', N'Pháp'),
('NCC0000003', N'Merck Sharp & Dohme', N'Mỹ'),
('NCC0000004', N'Pfizer', N'Mỹ'),
('NCC0000005', N'Công ty Vắc xin và Sinh phẩm số 1', N'Việt Nam'),
('NCC0000006', N'Trung tâm Nghiên cứu, Sản xuất Vắc xin và Sinh phẩm y tế', N'Việt Nam'),
('NCC0000007', N'Takeda Pharmaceutical', N'Nhật Bản');
GO

-- =================================================================================
-- PHẦN 2: INSERT DỮ LIỆU VACCINE VÀ QUAN HỆ VACCINE-BỆNH
-- =================================================================================

-- 2.1. Bảng Vaccine 
INSERT INTO Vaccine (MaVC, TenVC, GiaBan, SoLuong, MaLoai, MoTa) 
VALUES
-- SỬA: Bắt đầu bằng 0
('VC00000001', N'Infanrix Hexa (6 trong 1)', 1098000, 0, 'LVC0000001', N'Phác đồ 4 mũi: 3 mũi cơ bản (lúc 2, 3, 4 tháng tuổi) và 1 mũi nhắc lại (lúc 18-24 tháng).'),
('VC00000002', N'Hexaxim (6 trong 1)', 1098000, 0, 'LVC0000001', N'Phác đồ 4 mũi: 3 mũi cơ bản (lúc 2, 3, 4 tháng tuổi) và 1 mũi nhắc lại (lúc 18-24 tháng).'),
('VC00000003', N'Rotateq', 665000, 0, 'LVC0000003', N'Phác đồ 3 liều (uống). Liều 1: 7.5-12 tuần. Liều 2 & 3 cách nhau 4-10 tuần. Hoàn thành trước 32 tuần tuổi.'),
('VC00000004', N'Rotarix', 928000, 0, 'LVC0000003', N'Phác đồ 2 liều (uống). Liều 1: từ 6 tuần tuổi. Liều 2 cách liều 1 tối thiểu 4 tuần. Hoàn thành trước 24 tuần tuổi.'),
('VC00000005', N'Rotavin', 490000, 0, 'LVC0000003', N'Phác đồ 2 liều (uống). Hoàn thành trước 6 tháng tuổi.'),
('VC00000006', N'Synflorix', 1048000, 0, 'LVC0000006', N'Phác đồ 2, 3 hoặc 4 mũi tùy theo độ tuổi bắt đầu tiêm. Tối đa 4 mũi.'),
('VC00000007', N'Prevenar 13', 1290000, 0, 'LVC0000006', N'Phác đồ 1, 2, 3 hoặc 4 mũi tùy theo độ tuổi bắt đầu tiêm. Tối đa 4 mũi.'),
('VC00000008', N'BCG', 165000, 0, 'LVC0000003', N'Phác đồ 1 mũi duy nhất. Tiêm cho trẻ sơ sinh, tốt nhất trong 1 tháng đầu sau sinh.'),
('VC00000009', N'Gene Hbvax 1ml', 268000, 0, 'LVC0000006', N'Phác đồ 3 mũi cơ bản (0-1-6). Mũi 2 cách mũi 1 (1 tháng). Mũi 3 cách mũi 2 (5 tháng).'),
('VC00000010', N'Heberbiovac 1ml', 295000, 0, 'LVC0000006', N'Phác đồ 3 mũi cơ bản (0-1-6). Mũi 2 cách mũi 1 (1 tháng). Mũi 3 cách mũi 2 (5 tháng).'),
('VC00000011', N'Bexsero (Não mô cầu B)', 1788000, 0, 'LVC0000006', N'Phác đồ 2 mũi, mỗi mũi cách nhau tối thiểu 1 tháng. Dành cho trẻ từ 2 tháng tuổi.'),
('VC00000012', N'VA-Mengoc-BC (Não mô cầu BC)', 396000, 0, 'LVC0000006', N'Phác đồ 2 mũi, mỗi mũi cách nhau 6-8 tuần (khoảng 2 tháng). Dành cho trẻ từ 6 tháng tuổi.'),
('VC00000013', N'Nimenrix (Não mô cầu ACYW)', 1750000, 0, 'LVC0000006', N'Phác đồ 1 mũi duy nhất. Dành cho trẻ từ 6 tuần tuổi.'),
('VC00000014', N'MenQuadfi (Não mô cầu ACYW)', 1950000, 0, 'LVC0000006', N'Phác đồ 1 mũi duy nhất. Dành cho trẻ từ 6 tháng tuổi.'),
('VC00000015', N'Menactra (Não mô cầu ACYW)', 1370000, 0, 'LVC0000006', N'Phác đồ 2 mũi (cách nhau 3 tháng) hoặc 1 mũi (người từ 2 tuổi). Tối đa 2 mũi.'),
('VC00000016', N'MMR II (3 trong 1)', 495000, 0, 'LVC0000003', N'Phác đồ 2 mũi. Mũi 1: 12-15 tháng tuổi. Mũi 2: 4-6 tuổi (hoặc cách mũi 1 tối thiểu 1 tháng).'),
('VC00000017', N'Priorix', 498000, 0, 'LVC0000003', N'Phác đồ 2 mũi. Mũi 1: 12-15 tháng tuổi. Mũi 2: 4-6 tuổi (hoặc cách mũi 1 tối thiểu 1 tháng).'),
('VC00000018', N'Varivax', 1118000, 0, 'LVC0000003', N'Phác đồ 2 mũi. Trẻ 12 tháng - 12 tuổi: Mũi 2 cách mũi 1 (3 tháng). Từ 13 tuổi: Mũi 2 cách mũi 1 (1 tháng).'),
('VC00000019', N'Varilrix', 1138000, 0, 'LVC0000003', N'Phác đồ 2 mũi. Mũi 2 cách mũi 1 tối thiểu 1 tháng (tốt nhất là 3 tháng).'),
('VC00000020', N'Vaxigrip Tetra 0.5ml', 356000, 0, 'LVC0000004', N'Phác đồ 1 mũi. Tiêm nhắc lại hàng năm. (Trẻ 6 tháng - 9 tuổi lần đầu tiêm cần 2 mũi cách nhau 1 tháng).'),
('VC00000021', N'Influvac Tetra 0.5ml', 356000, 0, 'LVC0000004', N'Phác đồ 1 mũi. Tiêm nhắc lại hàng năm. (Trẻ 6 tháng - 9 tuổi lần đầu tiêm cần 2 mũi cách nhau 1 tháng).'),
('VC00000022', N'Gardasil', 1790000, 0, 'LVC0000006', N'Phác đồ 3 mũi (0-2-6). Mũi 2 cách mũi 1 (2 tháng). Mũi 3 cách mũi 2 (4 tháng).'),
('VC00000023', N'Gardasil 9', 2998000, 0, 'LVC0000006', N'Phác đồ 2 mũi (9-14 tuổi) hoặc 3 mũi (từ 15 tuổi, phác đồ 0-2-6). Tối đa 3 mũi.'),
('VC00000024', N'Imojev', 968000, 0, 'LVC0000003', N'Phác đồ 1 mũi cơ bản. Tiêm nhắc lại sau 1 năm (12 tháng).'),
('VC00000025', N'Jeev', 498000, 0, 'LVC0000004', N'Phác đồ 2 mũi cơ bản. Mũi 2 cách mũi 1 (28 ngày, ~1 tháng).'),
('VC00000026', N'Verorab', 538000, 0, 'LVC0000004', N'Phác đồ tiêm dự phòng (3 mũi) hoặc tiêm sau phơi nhiễm (5 mũi, tiêm theo ngày 0, 3, 7, 14, 28).'),
('VC00000027', N'Adacel', 798000, 0, 'LVC0000001', N'Phác đồ 1 mũi. Tiêm nhắc lại mỗi 10 năm. Dành cho trẻ từ 4 tuổi và người lớn.'),
('VC00000028', N'Boostrix', 856000, 0, 'LVC0000001', N'Phác đồ 1 mũi. Tiêm nhắc lại mỗi 10 năm. Dành cho trẻ từ 4 tuổi và người lớn.'),
('VC00000029', N'Tetraxim (4 trong 1)', 668000, 0, 'LVC0000001', N'Phác đồ 4 mũi (3 mũi cơ bản cách nhau 1 tháng + 1 mũi nhắc lại).'),
('VC00000030', N'Twinrix (VGA+VGB)', 768000, 0, 'LVC0000004', N'Phác đồ 3 mũi (0-1-6). Mũi 2 cách mũi 1 (1 tháng). Mũi 3 cách mũi 1 (6 tháng).'),
('VC00000031', N'Havax 0.5ml (VGA)', 295000, 0, 'LVC0000004', N'Phác đồ 2 mũi. Mũi 2 cách mũi 1 từ 6-12 tháng. Dành cho trẻ từ 12 tháng tuổi.'),
('VC00000032', N'Typhim VI (Thương hàn)', 438000, 0, 'LVC0000006', N'Phác đồ 1 mũi. Tiêm nhắc lại mỗi 3 năm (36 tháng).'),
('VC00000033', N'Qdenga (Sốt xuất huyết)', 1390000, 0, 'LVC0000003', N'Phác đồ 2 mũi. Mũi 2 cách mũi 1 (3 tháng). Dành cho người từ 4 tuổi.'),
('VC00000034', N'Abrysvo (Hô hấp hợp bào RSV)', 5458000, 0, 'LVC0000006', N'Phác đồ 1 mũi duy nhất. Tiêm cho phụ nữ mang thai ở tuần 32-36 để bảo vệ trẻ sơ sinh.'),
('VC00000035', N'Shingrix (Bệnh Zona)', 3890000, 0, 'LVC0000006', N'Phác đồ 2 mũi. Mũi 2 cách mũi 1 từ 2-6 tháng.');
GO
UPDATE Vaccine SET SoMuiToiDa = 4, SoThangCho = 1 WHERE MaVC IN ('VC00000001', 'VC00000002', 'VC00000029');
UPDATE Vaccine SET SoMuiToiDa = 3, SoThangCho = 1 WHERE MaVC IN ('VC00000003', 'VC00000009', 'VC00000010', 'VC00000030');
UPDATE Vaccine SET SoMuiToiDa = 3, SoThangCho = 2 WHERE MaVC IN ('VC00000022', 'VC00000023');
UPDATE Vaccine SET SoMuiToiDa = 2, SoThangCho = 1 WHERE MaVC IN ('VC00000004', 'VC00000005', 'VC00000011', 'VC00000025');
UPDATE Vaccine SET SoMuiToiDa = 2, SoThangCho = 2 WHERE MaVC IN ('VC00000012', 'VC00000035');
UPDATE Vaccine SET SoMuiToiDa = 2, SoThangCho = 3 WHERE MaVC IN ('VC00000015', 'VC00000018', 'VC00000019', 'VC00000033');
UPDATE Vaccine SET SoMuiToiDa = 2, SoThangCho = 36 WHERE MaVC IN ('VC00000016', 'VC00000017');
UPDATE Vaccine SET SoMuiToiDa = 2, SoThangCho = 6 WHERE MaVC = 'VC00000031';
UPDATE Vaccine SET SoMuiToiDa = 1, SoThangCho = 0 WHERE MaVC IN ('VC00000008', 'VC00000013', 'VC00000014', 'VC00000034');
UPDATE Vaccine SET SoMuiToiDa = 1, SoThangCho = 12 WHERE MaVC = 'VC00000024';
UPDATE Vaccine SET SoMuiToiDa = 99, SoThangCho = 12 WHERE MaVC IN ('VC00000020', 'VC00000021');
UPDATE Vaccine SET SoMuiToiDa = 99, SoThangCho = 120 WHERE MaVC IN ('VC00000027', 'VC00000028');
UPDATE Vaccine SET SoMuiToiDa = 99, SoThangCho = 36 WHERE MaVC = 'VC00000032';
UPDATE Vaccine SET SoMuiToiDa = 5, SoThangCho = 0 WHERE MaVC = 'VC00000026';
UPDATE Vaccine SET SoMuiToiDa = 4, SoThangCho = 1 WHERE MaVC IN ('VC00000006', 'VC00000007');
GO

-- 2.2. Bảng VaccinePhongBenh (SỬA TÊN CỘT VÀ ĐỘ DÀI MÃ)
INSERT INTO VaccinePhongBenh (MaVC, MaLoaiBenh) VALUES
('VC00000001', 'LB00000001'), ('VC00000001', 'LB00000002'), ('VC00000001', 'LB00000003'), ('VC00000001', 'LB00000004'), ('VC00000001', 'LB00000005'), ('VC00000001', 'LB00000006'),
('VC00000002', 'LB00000001'), ('VC00000002', 'LB00000002'), ('VC00000002', 'LB00000003'), ('VC00000002', 'LB00000004'), ('VC00000002', 'LB00000005'), ('VC00000002', 'LB00000006'),
('VC00000003', 'LB00000007'), 
('VC00000004', 'LB00000007'), 
('VC00000005', 'LB00000007'),
('VC00000006', 'LB00000008'), 
('VC00000007', 'LB00000008'),
('VC00000008', 'LB00000009'),
('VC00000009', 'LB00000005'), 
('VC00000010', 'LB00000005'),
('VC00000011', 'LB00000010'), 
('VC00000012', 'LB00000011'), 
('VC00000013', 'LB00000012'), 
('VC00000014', 'LB00000012'), 
('VC00000015', 'LB00000012'),
('VC00000016', 'LB00000013'), ('VC00000016', 'LB00000014'), ('VC00000016', 'LB00000015'),
('VC00000017', 'LB00000013'), ('VC00000017', 'LB00000014'), ('VC00000017', 'LB00000015'),
('VC00000018', 'LB00000016'), 
('VC00000019', 'LB00000016'),
('VC00000020', 'LB00000017'), 
('VC00000021', 'LB00000017'),
('VC00000022', 'LB00000021'), 
('VC00000023', 'LB00000021'),
('VC00000024', 'LB00000018'), 
('VC00000025', 'LB00000018'),
('VC00000026', 'LB00000020'),
('VC00000027', 'LB00000001'), ('VC00000027', 'LB00000002'), ('VC00000027', 'LB00000003'),
('VC00000028', 'LB00000001'), ('VC00000028', 'LB00000002'), ('VC00000028', 'LB00000003'),
('VC00000029', 'LB00000001'), ('VC00000029', 'LB00000002'), ('VC00000029', 'LB00000003'), ('VC00000029', 'LB00000004'),
('VC00000030', 'LB00000022'), ('VC00000030', 'LB00000005'),
('VC00000031', 'LB00000022'),
('VC00000032', 'LB00000019'),
('VC00000033', 'LB00000025'),
('VC00000034', 'LB00000026'),
('VC00000035', 'LB00000024');
GO

-- =================================================================================
-- PHẦN 3: INSERT DỮ LIỆU GÓI VACCINE VÀ CHI TIẾT
-- =================================================================================

-- 3.1. Bảng GoiVaccine 
INSERT INTO GoiVaccine (MaGoi, TenGoi, MoTa, DoiTuongApDung, GiaGoi, TrangThai) VALUES
('GOI0000001', N'Gói vắc xin cho trẻ từ 0-12 tháng', N'Bảo vệ con trong năm đầu đời với các vắc xin quan trọng nhất.', N'Trẻ từ 0 đến 12 tháng tuổi', 8554000, N'Đang áp dụng'),
('GOI0000002', N'Gói vắc xin cho trẻ từ 0-24 tháng', N'Gói vắc xin toàn diện nhất, bảo vệ con yêu đến 2 tuổi.', N'Trẻ từ 0 đến 24 tháng tuổi', 12872000, N'Đang áp dụng'),
('GOI0000003', N'Gói vắc xin cho phụ nữ chuẩn bị mang thai', N'Tiêm phòng đầy đủ trước khi mang thai để bảo vệ mẹ và bé.', N'Phụ nữ chuẩn bị mang thai', 2492000, N'Đang áp dụng'),
('GOI0000004', N'Gói vắc xin cho người lớn', N'Chủ động phòng ngừa các bệnh truyền nhiễm nguy hiểm cho người trưởng thành.', N'Người lớn', 3154000, N'Đang áp dụng');

-- 3.2. Bảng ChiTietGoiVaccine 
INSERT INTO ChiTietGoiVaccine (MaCTGoi, MaGoi, MaVC, SoMui) VALUES
('CTGV000001', 'GOI0000001', 'VC00000008', 1),
('CTGV000002', 'GOI0000001', 'VC00000009', 1),
('CTGV000003', 'GOI0000001', 'VC00000001', 1),
('CTGV000004', 'GOI0000001', 'VC00000003', 1),
('CTGV000005', 'GOI0000001', 'VC00000007', 1),
('CTGV000006', 'GOI0000001', 'VC00000001', 2),
('CTGV000007', 'GOI0000001', 'VC00000003', 2),
('CTGV000008', 'GOI0000001', 'VC00000001', 3),
('CTGV000009', 'GOI0000001', 'VC00000003', 3),
('CTGV000010', 'GOI0000001', 'VC00000007', 2),
('CTGV000011', 'GOI0000001', 'VC00000020', 1),
('CTGV000012', 'GOI0000001', 'VC00000016', 1),
('CTGV000013', 'GOI0000001', 'VC00000018', 1),
('CTGV000014', 'GOI0000001', 'VC00000024', 1),
('CTGV000015', 'GOI0000003', 'VC00000016', 1),
('CTGV000016', 'GOI0000003', 'VC00000018', 1),
('CTGV000017', 'GOI0000003', 'VC00000020', 1),
('CTGV000018', 'GOI0000003', 'VC00000028', 1);

-- BỔ SUNG CHO GOI00002
INSERT INTO ChiTietGoiVaccine (MaCTGoi, MaGoi, MaVC, SoMui) VALUES
('CTGV000019', 'GOI0000002', 'VC00000008', 1), -- BCG
('CTGV000020', 'GOI0000002', 'VC00000009', 1), -- VGB
('CTGV000021', 'GOI0000002', 'VC00000001', 1), -- 6in1 (Mũi 1)
('CTGV000022', 'GOI0000002', 'VC00000003', 1), -- Rota (Mũi 1)
('CTGV000023', 'GOI0000002', 'VC00000007', 1), -- Phế cầu (Mũi 1)
('CTGV000024', 'GOI0000002', 'VC00000001', 2), -- 6in1 (Mũi 2)
('CTGV000025', 'GOI0000002', 'VC00000003', 2), -- Rota (Mũi 2)
('CTGV000026', 'GOI0000002', 'VC00000001', 3), -- 6in1 (Mũi 3)
('CTGV000027', 'GOI0000002', 'VC00000003', 3), -- Rota (Mũi 3)
('CTGV000028', 'GOI0000002', 'VC00000007', 2), -- Phế cầu (Mũi 2)
('CTGV000029', 'GOI0000002', 'VC00000020', 1), -- Cúm (Mũi 1)
('CTGV000030', 'GOI0000002', 'VC00000020', 2), -- Cúm (Mũi 2, nếu tiêm lần đầu)
('CTGV000031', 'GOI0000002', 'VC00000016', 1), -- Sởi (MMR)
('CTGV000032', 'GOI0000002', 'VC00000012', 1), -- Não mô cầu BC (Mũi 1)
('CTGV000033', 'GOI0000002', 'VC00000012', 2), -- Não mô cầu BC (Mũi 2)
('CTGV000034', 'GOI0000002', 'VC00000018', 1), -- Thủy đậu
('CTGV000035', 'GOI0000002', 'VC00000024', 1), -- Viêm não NB
('CTGV000036', 'GOI0000002', 'VC00000015', 1), -- Não mô cầu ACYW
('CTGV000037', 'GOI0000002', 'VC00000001', 4), -- 6in1 (Nhắc lại)
('CTGV000038', 'GOI0000002', 'VC00000007', 3); -- Phế cầu (Nhắc lại)

-- BỔ SUNG CHO GOI00004
INSERT INTO ChiTietGoiVaccine (MaCTGoi, MaGoi, MaVC, SoMui) VALUES
('CTGV000039', 'GOI0000004', 'VC00000020', 1), -- Cúm
('CTGV000040', 'GOI0000004', 'VC00000007', 1), -- Phế cầu 13
('CTGV000041', 'GOI0000004', 'VC00000028', 1), -- Uốn ván (Boostrix)
('CTGV000042', 'GOI0000004', 'VC00000030', 1); -- Viêm gan A+B (Twinrix)
GO

PRINT N'-> Đã bổ sung chi tiết cho GOI00002 và GOI00004.';
GO

INSERT INTO TaiKhoan (MaTK, MatKhau) VALUES
('TK00000001', '$2a$12$lfc9fIBjwAEbBc0FhNhGXup9XavQgbFVyT646PQ43Un57WSvmAyRG'),
('TK00000002', '$2a$12$lfc9fIBjwAEbBc0FhNhGXup9XavQgbFVyT646PQ43Un57WSvmAyRG'),
('TK00000003', '$2a$12$lfc9fIBjwAEbBc0FhNhGXup9XavQgbFVyT646PQ43Un57WSvmAyRG'),
('TK00000004', '$2a$12$lfc9fIBjwAEbBc0FhNhGXup9XavQgbFVyT646PQ43Un57WSvmAyRG'),
('TK00000005', '$2a$12$lfc9fIBjwAEbBc0FhNhGXup9XavQgbFVyT646PQ43Un57WSvmAyRG');
GO
-- =================================================================================
-- BỔ SUNG NHÂN VIÊN MỚI THEO YÊU CẦU 
-- =================================================================================
INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, NgaySinh, CCCD, NgayVaoLam, SoDT, DiaChi, Email, ChucVu, TrangThai,MaTK) VALUES
('NV00000001', N'Tên Quản Lý', N'Nam', '2004-10-15', '079204001122', '2024-03-01', '0915111222', N'10 An Dương Vương, Quận 5, TP.HCM', 'thinh.nh@tpvax.com', 1, '1','TK00000001'),
('NV00000002', N'Tên Tiếp Nhận', N'Nam', '2004-07-22', '079204003344', '2024-03-01', '0915333444', N'20 Ngô Quyền, Quận 10, TP.HCM', 'tai.tt@tpvax.com', 2, '1','TK00000002'),
('NV00000003', N'Tên Kho', N'Nam', '2004-09-05', '079204005466', '2024-04-15', '0915555666', N'30 Trần Hưng Đạo, Quận 1, TP.HCM', 'phi.pv@tpvax.com', 3, '1','TK00000003'),
('NV00000004', N'Tên Y Tế', N'Nam', '2004-09-05', '079203005566', '2024-04-15', '0915555666', N'30 Trần Hưng Đạo, Quận 1, TP.HCM', 'phi.pv@tpvax.com', 4, '1','TK00000004'),
('NV00000005', N'Tên Thu Ngân', N'Nam', '2004-09-05', '079203005566', '2024-04-15', '0915555666', N'30 Trần Hưng Đạo, Quận 1, TP.HCM', 'phi.pv@tpvax.com', 5, '1','TK00000005');

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
 N'555 Nguyễn Văn Cừ, Quận 5, TP.HCM', '0977333444', 'hau.doan@email.com'),
('KH00000009', N'Nguyễn Minh Anh', '079600000001', '1988-03-12', N'Nam', N'12 Nguyễn Trãi, Q.1, TP.HCM', '0901234567', 'anh.nguyen@email.com'),
('KH00000010', N'Trần Thị Lan',     '079600000002', '1990-07-25', N'Nữ',  N'23 Cách Mạng Tháng 8, Q.3, TP.HCM', '0912345678', 'lan.tran@email.com'),
('KH00000011', N'Phạm Quốc Khánh', '079600000003', '1985-11-05', N'Nam', N'45 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0923456789', 'khanh.pham@email.com'),
('KH00000012', N'Lê Thu Hà',       '079600000004', '1992-02-17', N'Nữ',  N'78 Trường Chinh, Q.Tân Bình, TP.HCM', '0934567890', 'ha.le@email.com'),
('KH00000013', N'Vũ Đức Long',     '079600000005', '1989-09-09', N'Nam', N'56 Nguyễn Oanh, Q.Gò Vấp, TP.HCM', '0945678901', 'long.vu@email.com'),
('KH00000014', N'Bùi Ngọc Mai',    '079600000006', '1994-12-28', N'Nữ',  N'101 Lạc Long Quân, Q.11, TP.HCM', '0956789012', 'mai.bui@email.com'),
('KH00000015', N'Đặng Hoàng Nam',  '079600000007', '1987-06-03', N'Nam', N'34 Lê Duẩn, Q.1, TP.HCM', '0967890123', 'nam.dang@email.com'),
('KH00000016', N'Hồ Thu Trang',     '079600000008', '1991-04-14', N'Nữ',  N'89 Nguyễn Thị Minh Khai, Q.1, TP.HCM', '0978901234', 'trang.ho@email.com'),
('KH00000017', N'Nguyễn Hải Yến',  '079600000009', '1993-10-21', N'Nữ',  N'77 Phạm Văn Đồng, TP.Thủ Đức, TP.HCM', '0989012345', 'yen.nguyen@email.com'),
('KH00000018', N'Trần Văn Phúc',   '079600000010', '1986-01-30', N'Nam', N'200 Quang Trung, Q.Gò Vấp, TP.HCM', '0990123456', 'phuc.tran@email.com');
GO
PRINT '-> Đã thêm 15 khách hàng mới và 5 tài khoản tương ứng.';
GO

-- =================================================================================
-- DỮ LIỆU KHUYẾN MÃI 
-- =================================================================================
-- 1. Xóa dữ liệu cũ (để tránh trùng lặp khi chạy lại)


-- 2. Chèn dữ liệu bảng KHUYENMAI (Header)
INSERT INTO KhuyenMai (MaKM, TenKM, MoTa, LoaiKM, KieuGiam, GiaTriGiam, NgayBatDau, NgayKetThuc, TrangThai) 
VALUES
(
    'KM00000001', 
    N'Giảm 10% gói vắc xin 0-24 tháng', 
    N'Ưu đãi đặc biệt cho các bé trong dịp hè 2025. Áp dụng cho gói GOI00000002.', 
    N'Khuyến Mãi Hè', -- (Loại KM để quản lý)
    'PhanTram',      -- (Giảm theo %)
    10.00,           -- (Giá trị giảm: 10%)
    '2025-06-01 00:00:00', 
    '2025-06-30 23:59:59', 
    1                -- (Đang chạy)
),
(
    'KM00000002', 
    N'Giảm 50k phòng cúm mùa thu', 
    N'Chủ động phòng cúm với ưu đãi giảm 50,000đ cho vắc xin Vaxigrip Tetra.', 
    N'Khuyến Mãi Mùa', 
    'SoTien',        -- (Giảm tiền mặt)
    50000.00,        -- (Giá trị giảm: 50k)
    '2025-09-01 00:00:00', 
    '2025-09-30 23:59:59', 
    0                -- (Chưa kích hoạt/Tạm dừng)
);
GO


-- =================================================================================
-- DỮ LIỆU CHI TIẾT KHUYẾN MÃI 
-- =================================================================================
-- 3. Chèn dữ liệu bảng CHITIETKHUYENMAI (Details)
-- Lưu ý: Đã chuẩn hóa mã GOI00000002 cho đủ 10 ký tự
INSERT INTO ChiTietKhuyenMai (MaKM, LoaiSanPham, MaSanPham) 
VALUES
(
    'KM00000001', 
    'GOIVACCINE', 
    'GOI0000002' -- (Đã sửa lại mã cho đúng chuẩn CHAR(10))
),
(
    'KM00000002', 
    'VACCINE', 
    'VC00000034'  -- (Vaxigrip Tetra)
);
GO

PRINT '-> Đã thêm dữ liệu khuyến mãi thành công.';


-- =================================================================================
-- TẠO THÊM HỒ SƠ TIÊM CHỦNG (CHAR(10)) VÀ LỊCH TIÊM (CHAR(8))
-- =================================================================================
INSERT INTO HoSoTiemChung
    (MaHSTC, HoTen, GioiTinh, NgaySinh, CCCD, GhiChu, TrangThai)
VALUES
('HSTC000004', N'Phạm Thị Dung', N'Nữ',  '1998-01-20', '079812345678', N'Chuẩn bị mang thai', 1),
('HSTC000005', N'Đỗ Hùng Dũng',  N'Nam', '1993-09-08', '079923456789', N'Tiêm nhắc cúm hàng năm', 1),
('HSTC000006', N'Võ Bảo An',     N'Nữ',  '2024-01-01', '079923456712', N'Bé khỏe, theo dõi lịch tiêm chủng mở rộng', 1),
('HSTC000007', N'Bùi Tiến Dũng', N'Nam', '1997-02-28', '079445566778', N'Tiêm vắc xin Viêm gan B', 0),
('HSTC000008', N'Đoàn Văn Hậu',  N'Nam', '1999-04-19', '079556677889', N'Tiêm vắc xin dại do bị chó cắn', 1),
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

-- Bảng LienKetHoSo (FIX: BỔ SUNG CÁC HỒ SƠ CÒN LẠI)
INSERT INTO LienKetHoSo (MaLK,VaiTro, NgayLienKet, MaKH, MaHSTC) VALUES
('LKHS000001', N'Con', '2024-05-10', 'KH00000004', 'HSTC000006'),
('LKHS000002', N'Bản thân', '2024-05-10', 'KH00000004', 'HSTC000004'),
('LKHS000003', N'Bản thân', '2024-05-10', 'KH00000005', 'HSTC000005'),
('LKHS000004', N'Người giám hộ', '2024-03-01', 'KH00000006', 'HSTC000006'),
('LKHS000005', N'Bản thân', '2024-05-10', 'KH00000007', 'HSTC000007'),
('LKHS000006', N'Bản thân', '2024-07-20', 'KH00000008', 'HSTC000008'),
-- Bổ sung các liên kết còn thiếu
('LKHS000007', N'Bản thân', '2024-01-01', 'KH00000009', 'HSTC000009'),
('LKHS000008', N'Bản thân', '2024-01-01', 'KH00000010', 'HSTC000010'),
('LKHS000009', N'Bản thân', '2024-01-01', 'KH00000011', 'HSTC000011'),
('LKHS000010', N'Bản thân', '2024-01-01', 'KH00000012', 'HSTC000012'),
('LKHS000011', N'Bản thân', '2024-01-01', 'KH00000013', 'HSTC000013'),
('LKHS000012', N'Bản thân', '2024-01-01', 'KH00000014', 'HSTC000014'),
('LKHS000013', N'Bản thân', '2024-01-01', 'KH00000015', 'HSTC000015'),
('LKHS000014', N'Bản thân', '2024-01-01', 'KH00000016', 'HSTC000016'),
('LKHS000015', N'Bản thân', '2024-01-01', 'KH00000017', 'HSTC000017'),
('LKHS000016', N'Bản thân', '2024-01-01', 'KH00000018', 'HSTC000018');
GO

-- Bảng LichTiem 
INSERT INTO LichTiem (MaLT, MaHSTC, NgayHenTiem, NgayTiemThucTe, SoMui, TrangThai, GhiChu) VALUES
('LT00000005', 'HSTC000004', '2024-05-10 10:00:00', '2024-05-10 10:15:00', 1, N'Chưa tiêm', N'Tiêm mũi MMR'),
('LT00000006', 'HSTC000004', '2024-06-10 10:00:00', NULL, 2, N'Đã tiêm', N'Tiêm mũi Thủy đậu'),
('LT00000007', 'HSTC000006', '2024-03-01 09:30:00', '2024-03-01 09:35:00', 1, N'Chưa tiêm', N'Mũi 6in1 + Rota'),
('LT00000008', 'HSTC000006', '2024-04-01 09:30:00', '2024-04-01 10:00:00', 2, N'Đã tiêm', N'Mũi 6in1 + Rota lần 2'),
('LT00000009', 'HSTC000006', '2024-05-01 09:30:00', NULL, 3, N'Đã tiêm', N'Lịch hẹn mũi 6in1 + Rota lần 3'),
('LT00000011', 'HSTC000008', '2024-07-20 08:00:00', '2024-07-20 08:10:00', 1, N'Chưa tiêm', N'Mũi dại đầu tiên'),
('LT00000012', 'HSTC000008', '2024-07-23 08:00:00', NULL, 2, N'Đã tiêm', N'Hẹn tiêm mũi dại thứ 2');
GO


PRINT N'Bước 1: Cập nhật [MaVC] (Mã Vaccine) dựa trên GhiChu...';
GO

-- Cập nhật cho 'Tiêm mũi MMR' (LT00000005)
UPDATE LichTiem
SET MaVC = 'VC00000016' -- (Mã của MMR II)
WHERE MaLT = 'LT00000005' AND GhiChu LIKE N'%MMR%';

-- Cập nhật cho 'Tiêm mũi Thủy đậu' (LT00000006)
UPDATE LichTiem
SET MaVC = 'VC00000018' -- (Mã của Varivax)
WHERE MaLT = 'LT00000006' AND GhiChu LIKE N'%Thủy đậu%';

-- Cập nhật cho 'Mũi 6in1 + Rota' (LT00000007, 08, 09)
UPDATE LichTiem
SET MaVC = 'VC00000001' -- (Mã của 6in1 - Infanrix Hexa)
WHERE MaLT IN ('LT00000007', 'LT00000008', 'LT00000009') AND GhiChu LIKE N'%6in1%';

-- Cập nhật cho 'Mũi dại' (LT00000011, 000012)
UPDATE LichTiem
SET MaVC = 'VC00000026' -- (Mã của Verorab)
WHERE MaLT IN ('LT00000011', 'LT00000012') AND GhiChu LIKE N'%dại%';

GO



-- Tạm thời gán tất cả các mũi ĐÃ TIÊM (TrangThai = 1) cho nhân viên 'NV00000001'
UPDATE LichTiem
SET MaNV = 'NV00000001'
WHERE TrangThai = N'Đã tiêm' AND MaNV IS NULL;
GO


-- =================================================================================
-- TẠO THÊM DỮ LIỆU HÓA ĐƠN 
-- =================================================================================
INSERT INTO HoaDon (MaHD, NgayLap, TongTien, TrangThai, MaKH, MaNV, MaKM) VALUES
('HD00000004', '2024-05-10 10:20:00', 2492000, 1, 'KH00000004', 'NV00000001', NULL),
('HD00000005', '2024-03-01 09:40:00', 1763000, 1, 'KH00000006', 'NV00000002', NULL),
('HD00000006', '2024-04-01 10:05:00', 1763000, 1, 'KH00000006', 'NV00000001', NULL),
('HD00000007', '2024-07-20 08:15:00', 538000, 1, 'KH00000008', 'NV00000002', NULL),
('HD00000008', '2025-06-20 11:00:00', 11584800, 1, 'KH00000005', 'NV00000001',NULL);
GO

-- Chi tiết cho các hóa đơn mới 
INSERT INTO ChiTietHoaDon (MaCTHD, MaHD, MaSanPham, LoaiSanPham, SoLuong, DonGia) VALUES
('CTHD000001', 'HD00000004', 'GOI0000003', 'GOIVACCINE', 1, 2492000),
('CTHD000002', 'HD00000005', 'VC00000002', 'VACCINE', 1, 1098000),
('CTHD000003', 'HD00000005', 'VC00000003', 'VACCINE', 1, 665000),
('CTHD000004', 'HD00000006', 'VC00000002', 'VACCINE', 1, 1098000),
('CTHD000005', 'HD00000006', 'VC00000003', 'VACCINE', 1, 665000),
('CTHD000006', 'HD00000007', 'VC00000026', 'VACCINE', 1, 538000),
('CTHD000007', 'HD00000008', 'GOI0000002', 'GOIVACCINE', 1, 12872000);
GO

-- =================================================================================
-- TẠO THÊM DỮ LIỆU NHẬP KHO 
-- =================================================================================
INSERT INTO PhieuNhapVaccine (MaPN, NgayLap, MaNV, MaNCC,TrangThai) VALUES
('PN00000003', '2024-05-05 09:00:00', 'NV00000003', 'NCC0000002',1), -- SANOFI
('PN00000004', '2024-06-10 14:30:00', 'NV00000003', 'NCC0000004',1); -- PFIZER
GO

-- Chi tiết phiếu nhập mới 
INSERT INTO ChiTietPhieuNhap (MaCTPN, MaPN, MaVC, NuocSanXuat, SoLuong,SoLuongTonKho, GiaNhap, HanSuDung) VALUES
('CTPN000001', 'PN00000003', 'VC00000002', N'Pháp', 100,100, 850000, '2027-04-30'),
('CTPN000002', 'PN00000003', 'VC00000020', N'Pháp', 200,200, 280000, '2026-05-31'),
('CTPN000003', 'PN00000004', 'VC00000007', N'Mỹ', 150,150, 1050000, '2027-06-30');
GO
