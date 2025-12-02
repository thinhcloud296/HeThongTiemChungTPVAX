# 💉 Hệ Thống Quản Lý Tiêm Chủng TPVAX

<p align="center">
  <img src="https://img.shields.io/badge/.NET%20Framework-4.7.2+-blue?style=for-the-badge&logo=dotnet" alt=".NET Framework"/>
  <img src="https://img.shields.io/badge/ASP.NET%20MVC-5.2.9-green?style=for-the-badge&logo=dotnet" alt="ASP.NET MVC"/>
  <img src="https://img.shields.io/badge/SQL%20Server-2019+-red?style=for-the-badge&logo=microsoftsqlserver" alt="SQL Server"/>
  <img src="https://img.shields.io/badge/Entity%20Framework-6.4.4-purple?style=for-the-badge" alt="Entity Framework"/>
</p>

---

## 👥 Tác Giả

| STT | Họ và Tên | Vai Trò |
|:---:|-----------|---------|
| 1 | **Nguyễn Hoàng Thịnh** | Developer |
| 2 | **Trần Tấn Tài** | Developer |
| 3 | **Phạm Văn Phi** | Developer |

---

## 📋 Giới Thiệu

**TPVAX** là hệ thống quản lý tiêm chủng toàn diện, được phát triển nhằm hỗ trợ các cơ sở y tế trong việc quản lý quy trình tiêm chủng một cách hiệu quả và chuyên nghiệp. Hệ thống bao gồm 2 thành phần chính:

- 🖥️ **Ứng dụng Desktop (WinForms)**: Dành cho nhân viên quản lý nội bộ
- 🌐 **Website (ASP.NET MVC)**: Dành cho khách hàng đăng ký và theo dõi lịch tiêm

---

## 🏗️ Kiến Trúc Hệ Thống

```
HeThongTiemChungTPVAX/
│
├── 📁 TPVAXWebsite/              # Website ASP.NET MVC
│   ├── Controllers/              # Điều khiển xử lý request
│   ├── Models/                   # Domain & ViewModels
│   ├── Views/                    # Giao diện Razor
│   ├── DAL/                      # Data Access Layer
│   ├── Services/                 # Business Logic
│   └── Content/                  # CSS, Images, Assets
│
├── 📁 TPVAXWinform_GUI/          # Giao diện WinForms
│   ├── Forms/                    # Các form chức năng
│   ├── UserControls/             # Custom controls
│   ├── Dashboard/                # Bảng điều khiển
│   └── Reports/                  # Báo cáo RDLC
│
├── 📁 TPVAXWinform_BLL/          # Business Logic Layer
├── 📁 TPVAXWinform_DAL/          # Data Access Layer
├── 📁 TPVAXWinform_DTO/          # Data Transfer Objects
│
├── 📁 script_database/           # Script SQL Server
│   ├── db_script.sql             # Tạo cấu trúc database
│   └── script_insrt.sql          # Dữ liệu mẫu
│
└── 📁 TPVAX_Setup/               # Installer package
```

---

## 🗄️ Cơ Sở Dữ Liệu

### Sơ Đồ Các Bảng Chính

| Bảng | Mô Tả |
|------|-------|
| `TaiKhoan` | Quản lý tài khoản đăng nhập |
| `KhachHang` | Thông tin khách hàng |
| `NhanVien` | Thông tin nhân viên |
| `HoSoTiemChung` | Hồ sơ tiêm chủng cá nhân |
| `LienKetHoSo` | Liên kết khách hàng với hồ sơ |
| `Vaccine` | Danh mục vaccine |
| `LoaiVaccine` | Phân loại vaccine |
| `LoaiBenh` | Danh mục bệnh |
| `GoiVaccine` | Gói vaccine combo |
| `ChiTietGoiVaccine` | Chi tiết vaccine trong gói |
| `LichTiem` | Lịch hẹn tiêm chủng |
| `HoaDon` | Hóa đơn thanh toán |
| `ChiTietHoaDon` | Chi tiết hóa đơn |
| `PhieuNhapVaccine` | Phiếu nhập kho |
| `ChiTietPhieuNhap` | Chi tiết nhập kho |
| `NhaCungCap` | Nhà cung cấp vaccine |
| `KhuyenMai` | Chương trình khuyến mãi |
| `GioHang` | Giỏ hàng online |
| `BaiViet` | Bài viết kiến thức tiêm chủng |

---

## 🖥️ Ứng Dụng Desktop (WinForms)

### Chức Năng Chính

| Module | Mô Tả |
|--------|-------|
| 🔐 **Đăng nhập** | Xác thực tài khoản, đổi mật khẩu bắt buộc |
| 📊 **Bảng điều khiển** | Dashboard thống kê KPI, biểu đồ doanh thu |
| 👤 **Quản lý nhân viên** | CRUD nhân viên, phân quyền chức vụ |
| 💉 **Quản lý Vaccine** | Danh mục vaccine, loại vaccine, loại bệnh |
| 📦 **Quản lý gói Vaccine** | Tạo/sửa gói combo vaccine |
| 📋 **Hồ sơ tiêm chủng** | Quản lý hồ sơ, liên kết khách hàng |
| 📅 **Lịch tiêm** | Đặt lịch, xác nhận tiêm, theo dõi mũi tiêm |
| 🧾 **Hóa đơn** | Tạo hóa đơn, in hóa đơn (RDLC Report) |
| 📥 **Phiếu nhập kho** | Nhập vaccine, quản lý tồn kho FEFO |
| 🎁 **Khuyến mãi** | Tạo chương trình giảm giá |
| 📈 **Thống kê** | Báo cáo doanh thu, lượt tiêm |
| 👥 **Tài khoản** | Quản lý tài khoản hệ thống |

### Phân Quyền Chức Vụ

- **Quản lý**: Toàn quyền hệ thống
- **Nhân viên tiêm chủng**: Quản lý lịch tiêm, hồ sơ
- **Nhân viên kho**: Quản lý nhập kho, tồn kho
- **Nhân viên bán hàng**: Tạo hóa đơn, thanh toán

---

## 🌐 Website (ASP.NET MVC)

### Chức Năng Dành Cho Khách Hàng

| Trang | Mô Tả |
|-------|-------|
| 🏠 **Trang chủ** | Giới thiệu, tin tức tiêm chủng |
| 💉 **Vaccine** | Xem danh sách vaccine, chi tiết |
| 📦 **Gói Vaccine** | Xem các gói combo vaccine |
| 🛒 **Giỏ hàng** | Thêm vaccine/gói vào giỏ |
| 📝 **Đăng ký tiêm** | Đăng ký lịch tiêm online |
| 👤 **Tài khoản** | Đăng ký, đăng nhập, quản lý hồ sơ |
| 📋 **Hồ sơ tiêm chủng** | Xem lịch sử tiêm, hồ sơ liên kết |
| 📅 **Lịch tiêm** | Xem lịch hẹn, trạng thái |
| 🧾 **Hóa đơn** | Xem chi tiết hóa đơn |
| 🎁 **Khuyến mãi** | Xem chương trình ưu đãi |
| 📚 **Kiến thức tiêm chủng** | Bài viết về vaccine, bệnh truyền nhiễm |
| 👶 **Mẹ và Bé** | Thông tin tiêm chủng cho trẻ em |
| 🎯 **Tôi nên tiêm gì?** | Gợi ý vaccine theo đối tượng |

### Chức Năng Admin Website

| Trang | Mô Tả |
|-------|-------|
| 📊 **Reports** | Báo cáo thống kê |
| 📝 **Quản lý bài viết** | CRUD bài viết kiến thức |

---

## 🛠️ Công Nghệ Sử Dụng

### Backend
- **.NET Framework 4.7.2+**
- **ASP.NET MVC 5.2.9**
- **Entity Framework 6.4.4**
- **SQL Server 2019+**

### Frontend
- **Windows Forms** (Desktop)
- **Razor Views** (Web)
- **Bootstrap** (CSS Framework)
- **jQuery**

### Thư Viện Bổ Sung
- **BCrypt.Net-Next 4.0.3** - Mã hóa mật khẩu
- **LiveCharts 0.9.7** - Biểu đồ thống kê
- **Newtonsoft.Json 13.0.3** - Xử lý JSON
- **Microsoft.ReportingServices** - In báo cáo RDLC
- **Microsoft.Office.Interop.Excel** - Xuất Excel

---

## ⚙️ Hướng Dẫn Cài Đặt

### Yêu Cầu Hệ Thống
- Windows 10/11
- Visual Studio 2019+ với .NET Desktop Development
- SQL Server 2019+ hoặc SQL Server Express
- .NET Framework 4.7.2+

### Các Bước Cài Đặt

**1. Clone Repository**
```bash
git clone https://github.com/your-repo/HeThongTiemChungTPVAX.git
```

**2. Tạo Database**
```sql
-- Mở SQL Server Management Studio
-- Tạo database mới tên: TPVAX
-- Chạy script: script_database/db_script.sql
-- Chạy script dữ liệu mẫu: script_database/script_insrt.sql
```

**3. Cấu Hình Connection String**

*WinForms (App.config):*
```xml
<connectionStrings>
  <add name="TPVAXConnection" 
       connectionString="Server=.;Database=TPVAX;Trusted_Connection=True;" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

*Website (Web.config):*
```xml
<connectionStrings>
  <add name="TPVAXConnection" 
       connectionString="Server=.;Database=TPVAX;Trusted_Connection=True;" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

**4. Build và Chạy**
- Mở file `HeThongTiemChungTPVAX.sln` bằng Visual Studio
- Restore NuGet Packages
- Build Solution (Ctrl + Shift + B)
- Chạy project WinForms hoặc Website

---

## 📸 Screenshots

> *Thêm screenshots của ứng dụng tại đây*

---

## 📄 License

Dự án này được phát triển cho mục đích học tập và nghiên cứu.

---

## 📞 Liên Hệ

Nếu có thắc mắc hoặc góp ý, vui lòng liên hệ qua:
- 📧 Email: [contact@tpvax.com]
- 🌐 Website: [https://tpvax.com]

---

<p align="center">
  <b>© 2024 TPVAX - Hệ Thống Quản Lý Tiêm Chủng</b>
</p>
