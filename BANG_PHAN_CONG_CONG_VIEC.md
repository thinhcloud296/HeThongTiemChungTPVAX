# BẢNG PHÂN CÔNG CÔNG VIỆC
## Dự án: Hệ Thống Tiêm Chủng TPVAX

---

## 1. THÔNG TIN NHÓM

| STT | Họ và Tên | Vai trò | Khối lượng |
|:---:|-----------|---------|:----------:|
| 1 | Nguyễn Hoàng Thịnh | Phát triển Winform, Báo cáo | **45%** |
| 2 | Trần Tấn Tài | Phát triển Website, Báo cáo | **45%** |
| 3 | Phạm Văn Phi | Hỗ trợ giao diện, Báo cáo | **10%** |

---

## 2. BẢNG PHÂN CÔNG CHI TIẾT

| STT | Công việc | Mô tả chi tiết | Người thực hiện |
|:---:|-----------|----------------|:---------------:|
| **A** | **WINFORM APPLICATION** | | |
| A.1 | TPVAXWinform_DAL (24 files) | DBConnect, TaoMaTuDong, VaccineDAL, LoaiVaccineDAL, GoiVaccineDAL, ChiTietGoiVaccineDAL, KhachHangDAL, HoSoTiemChungDAL, LienKetHoSoDAL, LichTiemDAL, HoaDonDAL, ChiTietHoaDonDAL, HoaDonInDAL, PhieuNhapDAL, ChiTietPhieuNhapDAL, PhieuNhapInDAL, NhanVienDAL, TaiKhoanDAL, NhaCungCapDAL, LoaiBenhDAL, VaccinePhongBenhDAL, KhuyenMaiDAL, ChiTietKhuyenMaiDAL, ThongKeDAL | Nguyễn Hoàng Thịnh |
| A.2 | TPVAXWinform_BLL (22 files) | VaccineBLL, LoaiVaccineBLL, GoiVaccineBLL, ChiTietGoiVaccineBLL, KhachHangBLL, HoSoTiemChungBLL, LienKetHoSoBLL, LichTiemBLL, HoaDonBLL, ChiTietHoaDonBLL, HoaDonInBLL, PhieuNhapBLL, ChiTietPhieuNhapBLL, PhieuNhapInBLL, NhanVienBLL, TaiKhoanBLL, NhaCungCapBLL, LoaiBenhBLL, VaccinePhongBenhBLL, KhuyenMaiBLL, ChiTietKhuyenMaiBLL, ThongKeBLL | Nguyễn Hoàng Thịnh |
| A.3 | TPVAXWinform_DTO (21 files) | VaccineDTO, LoaiVaccineDTO, GoiVaccineDTO, ChiTietGoiVaccineDTO, KhachHangDTO, HoSoTiemChungDTO, LienKetHoSoDTO, LichTiemDTO, HoaDonDTO, ChiTietHoaDonDTO, HoaDonInDTO, PhieuNhapDTO, ChiTietPhieuNhapDTO, PhieuNhapInDTO, NhanVienDTO, TaiKhoanDTO, NhaCungCapDTO, LoaiBenhDTO, VaccinePhongBenhDTO, KhuyenMaiDTO, ChiTietKhuyenMaiDTO | Nguyễn Hoàng Thịnh |
| A.4 | TPVAXWinform_GUI - Forms (23 forms) | frmMain, frmDangNhap, frmDoiMatKhau, frmDoiMatKhauBatBuoc, frmQuanLyDanhMuc, frmQuanLyGoiVaccine, frmThemGoiVaccine, frmSuaGoiVaccine, frmChiTietGoiVaccine, frmThemHSTC_KH, frmEditHSTC, frmEditKH, frmThemNV, frmEditNV, frmThemPhieuNhap, frmChiTietPhieuNhap, frmChiTietHoaDon, frmThemMuiTiem, frmThemSuaKhuyenMai, frmXemThongTin, XacNhanTiemForm, frmInHoaDon, frmInPhieuNhap | Nguyễn Hoàng Thịnh |
| A.5 | TPVAXWinform_GUI - UserControls (10 controls) | BangDieuKhienControl, VaccineControl, HoSoTiemChungControl, LichTiemControl, HoaDonControl, PhieuNhapControl, NhanVienControl, TaiKhoanControl, KhuyenMaiControl, ThongKeControl | Nguyễn Hoàng Thịnh |
| A.6 | TPVAXWinform_GUI - Reports | rptHoaDon.rdlc, rptPhieuNhap.rdlc, dsHoaDon.xsd, dsPhieuNhap.xsd, Program.cs, RoleManager.cs | Nguyễn Hoàng Thịnh |
| **B** | **WEBSITE APPLICATION** | | |
| B.1 | Controllers (18 files) | HomeController, AccountController, AdminController, VaccineController, GoiVaccineController, GioHangController, HoaDonController, PaymentController, LichTiemController, HoSoController, HoSoTiemChungController, KhuyenMaiController, BaiVietController, BenhTruyenNhiemController, MeVaBeController, TheoDoiTuongController, ToiNenTiemGiController, VaccinePhongBenhController | Trần Tấn Tài |
| B.2 | Views/Account (10 views) | Login, Register, RegisterExisting, ConfirmCreateAccount, DangKyTuHoSo, Dashboard, Profile, ChiTietHoaDon, XacNhanHoSoTiemChung, XacNhanLienKetHoSo | Trần Tấn Tài |
| B.3 | Views/Admin (25 views) | _AdminLayout, _AdminHeader, _AdminFooter, _AdminSidebar, Index, Dashboard, Login, Register, RegisterExisting, ConfirmCreateAccount, DangKyTuHoSo, Profile, Vaccines, GoiVaccine, Customers, NhanVien, Appointments, KhuyenMai, BaiViets, Reports, ChiTietHoaDon, InvoiceDetails, XacNhanHoSoTiemChung, XacNhanLienKetHoSo | Trần Tấn Tài |
| B.4 | Views/Vaccine & GoiVaccine (5 views) | Vaccine/Index, Vaccine/ChiTiet, GoiVaccine/Index, GoiVaccine/Index_NEW, GoiVaccine/Detail | Trần Tấn Tài |
| B.5 | Views/GioHang, HoaDon, Payment (6 views) | GioHang/Index, GioHang/Cart, HoaDon/Index, HoaDon/Checkout, HoaDon/ChiTiet, Payment/PaymentCallback | Trần Tấn Tài |
| B.6 | Views/LichTiem & HoSo | LichTiem/Index, LichTiem/DatLich, HoSo/*, HoSoTiemChung/* | Trần Tấn Tài |
| B.7 | Views/Thông tin & Bài viết | BaiViet/*, BenhTruyenNhiem/*, KienThucTiemChung/*, MeVaBe/*, TheoDoiTuong/*, ToiNenTiemGi/*, VaccinePhongBenh/*, KhuyenMai/* | Trần Tấn Tài |
| B.8 | Views/Shared & Home | _Layout, _Header, _Footer, AccessDenied, Home/Index | Trần Tấn Tài |
| B.9 | Common, Services, DAL, Models | VnPayLibrary.cs, Services/*, DAL/*, Models/* | Trần Tấn Tài |
| **C** | **DATABASE** | | |
| C.1 | Script Database | db_script.sql, script_insrt.sql | Nguyễn Hoàng Thịnh |
| **D** | **HỖ TRỢ GIAO DIỆN** | | |
| D.1 | Hỗ trợ UI Website | CSS/Styling, Responsive, UI Testing | Phạm Văn Phi |
| D.2 | Hỗ trợ UI Winform | Form Design, Icon/Image, UI Testing | Phạm Văn Phi |
| **E** | **BÁO CÁO** | | |
| E.1 | Báo cáo Word - Phần Winform | Phân tích, thiết kế, hướng dẫn sử dụng Winform | Nguyễn Hoàng Thịnh |
| E.2 | Báo cáo Word - Phần Website | Phân tích, thiết kế, hướng dẫn sử dụng Website | Trần Tấn Tài |
| E.3 | Báo cáo Word - Phần chung | Giới thiệu, kết luận, kiểm tra lỗi chính tả | Phạm Văn Phi |
| E.4 | PowerPoint - Demo Winform | Slide demo ứng dụng Winform | Nguyễn Hoàng Thịnh |
| E.5 | PowerPoint - Demo Website | Slide demo ứng dụng Website | Trần Tấn Tài |
| E.6 | PowerPoint - Thiết kế | Hỗ trợ thiết kế slide, format | Phạm Văn Phi |

---

## 3. BẢNG TỔNG HỢP KHỐI LƯỢNG

| Thành viên | Winform | Website | Database | Báo cáo | Tổng |
|------------|:-------:|:-------:|:--------:|:-------:|:----:|
| Nguyễn Hoàng Thịnh | 35% | 0% | 5% | 5% | **45%** |
| Trần Tấn Tài | 0% | 40% | 0% | 5% | **45%** |
| Phạm Văn Phi | 3% | 3% | 0% | 4% | **10%** |

---

## 4. THỐNG KÊ SỐ LƯỢNG FILE

| Thành viên | Loại file | Số lượng |
|------------|-----------|:--------:|
| Nguyễn Hoàng Thịnh | DAL files | 24 |
| | BLL files | 22 |
| | DTO files | 21 |
| | GUI Forms | 23 |
| | GUI UserControls | 10 |
| | Reports & Others | 6 |
| | Database Scripts | 2 |
| | **Tổng** | **108** |
| Trần Tấn Tài | Controllers | 18 |
| | Views | 60+ |
| | Models, Services, DAL | 20+ |
| | **Tổng** | **98+** |
| Phạm Văn Phi | Hỗ trợ UI | - |
| | Báo cáo | - |

---

*Ngày lập: 08/12/2024*  
*Nhóm thực hiện: Nhóm TPVAX*
