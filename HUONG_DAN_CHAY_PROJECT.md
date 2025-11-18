# HƯỚNG DẪN KẾT NỐI DATABASE VÀ CHẠY PROJECT

## 📋 YÊU CẦU:

- Visual Studio 2019/2022
- SQL Server hoặc SQL Server Express
- .NET Framework 4.8.1

---

## BƯỚC 1: KIỂM TRA CONNECTION STRING

File: `Web.config` (dòng 11)

```xml
<connectionStrings>
    <add name="TPVAXConnection"
         connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLTIEMCHUNG;Integrated Security=True;Connect Timeout=30;"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

### Các kiểu kết nối:

**A. LocalDB (Hiện tại):**

```xml
Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLTIEMCHUNG;Integrated Security=True
```

**B. SQL Server Express:**

```xml
Data Source=.\SQLEXPRESS;Initial Catalog=QLTIEMCHUNG;Integrated Security=True
```

**C. SQL Server đầy đủ:**

```xml
Data Source=YOUR_SERVER_NAME;Initial Catalog=QLTIEMCHUNG;User ID=sa;Password=your_password
```

**D. SQL Server với tên máy:**

```xml
Data Source=LAPTOP-ABC123\SQLEXPRESS;Initial Catalog=QLTIEMCHUNG;Integrated Security=True
```

---

## BƯỚC 2: TẠO DATABASE

### Cách 1: Chạy script SQL

1. Mở **SQL Server Management Studio (SSMS)**
2. Kết nối tới server của bạn
3. Mở file: `script_database/db_script.sql`
4. Chạy script để tạo database và tables
5. Mở file: `script_database/script_insrt.sql`
6. Chạy script để insert dữ liệu mẫu

### Cách 2: Dùng Entity Framework Migration

Mở **Package Manager Console** trong Visual Studio:

```powershell
# 1. Enable Migrations
Enable-Migrations

# 2. Tạo Migration đầu tiên
Add-Migration InitialCreate

# 3. Update Database
Update-Database -Verbose
```

---

## BƯỚC 3: KIỂM TRA KẾT NỐI

### Trong Visual Studio:

1. Mở **Server Explorer** (View → Server Explorer)
2. Right-click **Data Connections** → Add Connection
3. Chọn **Microsoft SQL Server**
4. Nhập Server name:
   - LocalDB: `(localdb)\MSSQLLocalDB`
   - SQL Express: `.\SQLEXPRESS`
   - Hoặc tên server của bạn
5. Chọn database: `QLTIEMCHUNG`
6. Click **Test Connection** → Phải thấy "Test connection succeeded"

---

## BƯỚC 4: BUILD PROJECT

### Trong Visual Studio:

1. **Clean Solution:**

   - Menu: Build → Clean Solution
   - Hoặc nhấn: `Ctrl + Shift + B` rồi chọn Clean

2. **Rebuild Solution:**

   - Menu: Build → Rebuild Solution
   - Hoặc: Right-click Solution → Rebuild

3. **Xem Output:**
   - Mở Output window (View → Output)
   - Chọn "Show output from: Build"
   - Kiểm tra không có errors (chỉ warnings là OK)

---

## BƯỚC 5: CHẠY PROJECT

### A. Chạy với IIS Express (Khuyến nghị):

1. Nhấn **F5** hoặc click nút ▶️ **IIS Express**
2. Browser sẽ tự động mở: `http://localhost:XXXX`
3. Trang chủ sẽ hiển thị

### B. Chạy với Local IIS:

1. Right-click project TPVAXWebsite → Properties
2. Tab **Web**
3. Chọn **Local IIS**
4. Project URL: `http://localhost/TPVAXWebsite`
5. Click **Create Virtual Directory**
6. Save và nhấn F5

---

## BƯỚC 6: KIỂM TRA CÁC URL

Sau khi chạy thành công, test các URLs:

### Client URLs:

- Trang chủ: `/` hoặc `/Home/Index`
- Vắc xin: `/Vaccine/Index`
- Giỏ hàng: `/GioHang/Index`
- Đặt lịch: `/LichTiem/DatLich`
- Đăng nhập: `/Account/Login`
- Đăng ký: `/Account/Register`
- Gói vắc xin: `/GoiVaccine/Index`
- Khuyến mãi: `/KhuyenMai/Index`
- Bệnh truyền nhiễm: `/BenhTruyenNhiem/Index`

### Admin URLs:

- Dashboard: `/Admin/Index`
- Quản lý vắc xin: `/Admin/Vaccines`
- Quản lý khách hàng: `/Admin/Customers`
- Quản lý lịch hẹn: `/Admin/Appointments`

---

## ⚠️ XỬ LÝ LỖI THƯỜNG GẶP

### Lỗi 1: "Cannot open database QLTIEMCHUNG"

**Giải pháp:**

- Database chưa được tạo
- Chạy script `db_script.sql` trong SSMS
- Hoặc chạy `Update-Database` trong Package Manager Console

### Lỗi 2: "Login failed for user"

**Giải pháp:**

- Sửa Connection String trong Web.config
- Nếu dùng SQL Server Authentication, thêm User ID và Password
- Nếu dùng Windows Authentication, dùng `Integrated Security=True`

### Lỗi 3: "The entity type XXX requires a primary key"

**Giải pháp:**

- Kiểm tra Models trong folder Models/Domain
- Đảm bảo tất cả entities có [Key] attribute
- Rebuild project

### Lỗi 4: "HTTP Error 500.19"

**Giải pháp:**

- Kiểm tra Web.config syntax
- Đảm bảo .NET Framework 4.8.1 đã cài đặt
- Repair Visual Studio

### Lỗi 5: "Could not load file or assembly"

**Giải pháp:**

- Clean Solution
- Xóa folder bin và obj
- Rebuild Solution
- Restore NuGet packages

---

## 🔧 TOOLS HỮU ÍCH

### 1. Package Manager Console

Menu: Tools → NuGet Package Manager → Package Manager Console

```powershell
# Restore packages
Update-Package -Reinstall

# Check migrations
Get-Migrations

# Update database
Update-Database -Verbose
```

### 2. SQL Server Object Explorer

Menu: View → SQL Server Object Explorer

- Xem database structure
- Xem data trong tables
- Chạy queries

### 3. Browser Developer Tools

Nhấn F12 trong browser để:

- Xem Console errors
- Kiểm tra Network requests
- Debug JavaScript

---

## 📝 CHECKLIST TRƯỚC KHI CHẠY

- [ ] SQL Server đã cài đặt và chạy
- [ ] Connection string đã đúng trong Web.config
- [ ] Database QLTIEMCHUNG đã được tạo
- [ ] Tables đã có dữ liệu (chạy script_insrt.sql)
- [ ] Project build thành công (0 errors)
- [ ] NuGet packages đã restore

---

## 🎯 TEST DATABASE CONNECTION

Tạo file test trong project:

```csharp
// TestDbConnection.aspx.cs
using System;
using TPVAXWebsite.DAL;

protected void Page_Load(object sender, EventArgs e)
{
    try
    {
        using (var context = new TPVAXDbContext())
        {
            var count = context.Database.SqlQuery<int>("SELECT COUNT(*) FROM Vaccine").FirstOrDefault();
            Response.Write("Kết nối thành công! Có " + count + " vắc xin trong database.");
        }
    }
    catch (Exception ex)
    {
        Response.Write("Lỗi kết nối: " + ex.Message);
    }
}
```

---

## 📞 HỖ TRỢ

Nếu gặp lỗi khác, cung cấp:

1. Message lỗi đầy đủ
2. Stack trace
3. Connection string đang dùng (ẩn password)
4. Phiên bản SQL Server

---

**Chúc bạn chạy project thành công! 🚀**
