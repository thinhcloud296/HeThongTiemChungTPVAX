# 🛒 LUỒNG NGHIỆP VỤ THANH TOÁN - HỆ THỐNG TPVAX

## 📊 PHÂN TÍCH DATABASE

### Các bảng liên quan đến thanh toán:

#### 1. **GioHang** (Giỏ hàng tạm)

```sql
CREATE TABLE GioHang (
    MaGH INT PRIMARY KEY IDENTITY(1,1),
    MaKH CHAR(10) NOT NULL,
    MaSanPham CHAR(10) NOT NULL,      -- MaVC hoặc MaGoi
    LoaiSanPham NVARCHAR(20) NOT NULL, -- 'VACCINE' hoặc 'GOIVACCINE'
    SoLuong INT NOT NULL DEFAULT 1
)
```

**Ý nghĩa:** Lưu tạm sản phẩm khách hàng chọn mua

#### 2. **HoaDon** (Hóa đơn chính)

```sql
CREATE TABLE HoaDon (
    MaHD CHAR(10) PRIMARY KEY,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18, 0) NOT NULL,
    TrangThai BIT,                     -- 0: Chưa thanh toán, 1: Đã thanh toán
    MaKH CHAR(10),
    MaNV CHAR(10),
    MaKM CHAR(10)                      -- Mã khuyến mãi (tùy chọn)
)
```

#### 3. **ChiTietHoaDon** (Chi tiết sản phẩm trong hóa đơn)

```sql
CREATE TABLE ChiTietHoaDon (
    MaCTHD CHAR(10) PRIMARY KEY,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18, 0) NOT NULL,    -- Giá ĐÃ TÍNH KHUYẾN MÃI
    MaSanPham CHAR(10) NOT NULL,       -- MaVC hoặc MaGoi
    LoaiSanPham NVARCHAR(20) NOT NULL, -- 'VACCINE' hoặc 'GOIVACCINE'
    MaHD CHAR(10) NOT NULL
)
```

#### 4. **KhuyenMai** (Mã giảm giá)

```sql
CREATE TABLE KhuyenMai (
    MaKM CHAR(10) PRIMARY KEY,
    TenKM NVARCHAR(255),
    KieuGiam NVARCHAR(50),             -- 'PhanTram' hoặc 'SoTien'
    GiaTriGiam DECIMAL(18, 2),         -- 10 (10%) hoặc 50000 (50,000đ)
    NgayBatDau DATETIME,
    NgayKetThuc DATETIME,
    TrangThai BIT
)
```

#### 5. **ChiTietKhuyenMai** (Sản phẩm áp dụng khuyến mãi)

```sql
CREATE TABLE ChiTietKhuyenMai (
    MaCTKM INT PRIMARY KEY IDENTITY(1,1),
    MaKM CHAR(10) NOT NULL,
    LoaiSanPham NVARCHAR(50) NOT NULL, -- 'VACCINE' hoặc 'GOIVACCINE'
    MaSanPham CHAR(10) NOT NULL        -- MaVC hoặc MaGoi
)
```

#### 6. **Vaccine** & **GoiVaccine** (Sản phẩm)

```sql
Vaccine: MaVC, TenVC, GiaBan, SoLuong...
GoiVaccine: MaGoi, TenGoi, GiaGoi...
```

---

## 🔄 LUỒNG THANH TOÁN HOÀN CHỈNH

### **GIAI ĐOẠN 1: CHỌN SẢN PHẨM & THÊM VÀO GIỎ HÀNG**

#### 1.1. Khách hàng duyệt sản phẩm

- Trang `/Vaccine/Index` hoặc `/GoiVaccine/Index`
- Hiển thị danh sách Vaccine/Gói vaccine với:
  - Tên sản phẩm
  - Giá bán
  - Hình ảnh
  - Nút "Thêm vào giỏ"

#### 1.2. Thêm vào giỏ hàng

**Endpoint:** `POST /GioHang/ThemVaoGio`

**Input:**

```csharp
{
    MaSanPham: "VC00000001",     // Mã vaccine hoặc gói
    LoaiSanPham: "VACCINE",      // hoặc "GOIVACCINE"
    SoLuong: 1
}
```

**Logic xử lý:**

```csharp
[HttpPost]
public JsonResult ThemVaoGio(string MaSanPham, string LoaiSanPham, int SoLuong = 1)
{
    var kh = Session["KH"] as KhachHang;
    if (kh == null)
        return Json(new { success = false, message = "Vui lòng đăng nhập" });

    // Kiểm tra sản phẩm đã có trong giỏ chưa
    var itemTrongGio = _context.GioHangs
        .FirstOrDefault(g => g.MaKH == kh.MaKH
                          && g.MaSanPham == MaSanPham
                          && g.LoaiSanPham == LoaiSanPham);

    if (itemTrongGio != null)
    {
        // Tăng số lượng
        itemTrongGio.SoLuong += SoLuong;
    }
    else
    {
        // Thêm mới
        var itemMoi = new GioHang
        {
            MaKH = kh.MaKH,
            MaSanPham = MaSanPham,
            LoaiSanPham = LoaiSanPham,
            SoLuong = SoLuong
        };
        _context.GioHangs.Add(itemMoi);
    }

    _context.SaveChanges();

    return Json(new { success = true, message = "Đã thêm vào giỏ hàng" });
}
```

---

### **GIAI ĐOẠN 2: XEM & QUẢN LÝ GIỎ HÀNG**

#### 2.1. Xem giỏ hàng

**Endpoint:** `GET /GioHang/Index`

**Logic xử lý:**

```csharp
public ActionResult Index()
{
    var kh = Session["KH"] as KhachHang;
    if (kh == null)
        return RedirectToAction("Login", "Account");

    // Load giỏ hàng
    var gioHang = _context.GioHangs
        .Where(g => g.MaKH == kh.MaKH)
        .ToList();

    // Load thông tin sản phẩm chi tiết
    var cartItems = new List<GioHangViewModel>();

    foreach (var item in gioHang)
    {
        if (item.LoaiSanPham == "VACCINE")
        {
            var vaccine = _context.Vaccines.Find(item.MaSanPham);
            if (vaccine != null)
            {
                cartItems.Add(new GioHangViewModel
                {
                    MaGH = item.MaGH,
                    TenSanPham = vaccine.TenVC,
                    DonGia = vaccine.GiaBan,
                    SoLuong = item.SoLuong,
                    ThanhTien = vaccine.GiaBan * item.SoLuong,
                    LoaiSanPham = item.LoaiSanPham,
                    HinhAnh = vaccine.HinhAnh
                });
            }
        }
        else if (item.LoaiSanPham == "GOIVACCINE")
        {
            var goi = _context.GoiVaccines.Find(item.MaSanPham);
            if (goi != null)
            {
                cartItems.Add(new GioHangViewModel
                {
                    MaGH = item.MaGH,
                    TenSanPham = goi.TenGoi,
                    DonGia = goi.GiaGoi,
                    SoLuong = item.SoLuong,
                    ThanhTien = goi.GiaGoi * item.SoLuong,
                    LoaiSanPham = item.LoaiSanPham,
                    HinhAnh = goi.HinhAnh
                });
            }
        }
    }

    return View(cartItems);
}
```

#### 2.2. Cập nhật số lượng

**Endpoint:** `POST /GioHang/CapNhatSoLuong`

```csharp
[HttpPost]
public JsonResult CapNhatSoLuong(int MaGH, int SoLuong)
{
    var kh = Session["KH"] as KhachHang;
    if (kh == null)
        return Json(new { success = false });

    var item = _context.GioHangs
        .FirstOrDefault(g => g.MaGH == MaGH && g.MaKH == kh.MaKH);

    if (item == null)
        return Json(new { success = false });

    if (SoLuong <= 0)
    {
        _context.GioHangs.Remove(item);
    }
    else
    {
        item.SoLuong = SoLuong;
    }

    _context.SaveChanges();

    return Json(new { success = true });
}
```

#### 2.3. Xóa khỏi giỏ

**Endpoint:** `POST /GioHang/XoaKhoiGio`

```csharp
[HttpPost]
public JsonResult XoaKhoiGio(int MaGH)
{
    var kh = Session["KH"] as KhachHang;
    if (kh == null)
        return Json(new { success = false });

    var item = _context.GioHangs
        .FirstOrDefault(g => g.MaGH == MaGH && g.MaKH == kh.MaKH);

    if (item != null)
    {
        _context.GioHangs.Remove(item);
        _context.SaveChanges();
    }

    return Json(new { success = true });
}
```

---

### **GIAI ĐOẠN 3: THANH TOÁN (CHECKOUT)**

#### 3.1. Trang thanh toán

**Endpoint:** `GET /HoaDon/Checkout`

**Hiển thị:**

- Danh sách sản phẩm trong giỏ
- Thông tin khách hàng (Họ tên, SĐT, Địa chỉ)
- Form nhập mã khuyến mãi
- Tổng tiền trước và sau khuyến mãi
- Phương thức thanh toán (COD, Online)
- Nút "Xác nhận thanh toán"

**Logic:**

```csharp
public ActionResult Checkout()
{
    var kh = Session["KH"] as KhachHang;
    if (kh == null)
        return RedirectToAction("Login", "Account");

    // Load giỏ hàng
    var gioHang = _context.GioHangs
        .Where(g => g.MaKH == kh.MaKH)
        .ToList();

    if (!gioHang.Any())
    {
        TempData["ErrorMessage"] = "Giỏ hàng trống";
        return RedirectToAction("Index", "GioHang");
    }

    // Load thông tin sản phẩm
    var cartItems = new List<GioHangViewModel>();
    decimal tongTien = 0;

    foreach (var item in gioHang)
    {
        // ... (logic tương tự phần 2.1)
        tongTien += cartItem.ThanhTien;
    }

    // Load danh sách khuyến mãi đang hoạt động
    var khuyenMais = _context.KhuyenMais
        .Where(km => km.NgayBatDau <= DateTime.Now
                  && km.NgayKetThuc >= DateTime.Now
                  && km.TrangThai == true)
        .ToList();

    var model = new CheckoutViewModel
    {
        KhachHang = kh,
        GioHang = cartItems,
        TongTienTruocGiam = tongTien,
        KhuyenMais = khuyenMais
    };

    return View(model);
}
```

#### 3.2. Áp dụng mã khuyến mãi

**Endpoint:** `POST /HoaDon/ApDungKhuyenMai`

```csharp
[HttpPost]
public JsonResult ApDungKhuyenMai(string MaKM)
{
    var kh = Session["KH"] as KhachHang;
    if (kh == null)
        return Json(new { success = false, message = "Chưa đăng nhập" });

    // Kiểm tra mã khuyến mãi
    var khuyenMai = _context.KhuyenMais
        .FirstOrDefault(km => km.MaKM == MaKM
                           && km.NgayBatDau <= DateTime.Now
                           && km.NgayKetThuc >= DateTime.Now
                           && km.TrangThai == true);

    if (khuyenMai == null)
        return Json(new { success = false, message = "Mã khuyến mãi không hợp lệ" });

    // Load giỏ hàng
    var gioHang = _context.GioHangs.Where(g => g.MaKH == kh.MaKH).ToList();

    decimal tongTien = 0;
    decimal tienGiam = 0;

    foreach (var item in gioHang)
    {
        decimal giaSanPham = 0;

        // Lấy giá sản phẩm
        if (item.LoaiSanPham == "VACCINE")
        {
            var vc = _context.Vaccines.Find(item.MaSanPham);
            giaSanPham = vc?.GiaBan ?? 0;
        }
        else
        {
            var goi = _context.GoiVaccines.Find(item.MaSanPham);
            giaSanPham = goi?.GiaGoi ?? 0;
        }

        tongTien += giaSanPham * item.SoLuong;

        // Kiểm tra sản phẩm có áp dụng KM không
        var apDung = _context.ChiTietKhuyenMais
            .Any(ct => ct.MaKM == MaKM
                    && ct.MaSanPham == item.MaSanPham
                    && ct.LoaiSanPham == item.LoaiSanPham);

        if (apDung)
        {
            decimal giamChoSanPham = 0;

            if (khuyenMai.KieuGiam == "PhanTram")
            {
                giamChoSanPham = giaSanPham * item.SoLuong * khuyenMai.GiaTriGiam / 100;
            }
            else // SoTien
            {
                giamChoSanPham = khuyenMai.GiaTriGiam * item.SoLuong;
            }

            tienGiam += giamChoSanPham;
        }
    }

    decimal tongTienSauGiam = tongTien - tienGiam;

    return Json(new {
        success = true,
        tongTien = tongTien,
        tienGiam = tienGiam,
        tongTienSauGiam = tongTienSauGiam,
        tenKM = khuyenMai.TenKM
    });
}
```

#### 3.3. Xác nhận thanh toán

**Endpoint:** `POST /HoaDon/XacNhanThanhToan`

**Logic hoàn chỉnh:**

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult XacNhanThanhToan(string MaKM, string PhuongThucThanhToan, string DiaChiGiaoHang, string GhiChu)
{
    var kh = Session["KH"] as KhachHang;
    if (kh == null)
        return RedirectToAction("Login", "Account");

    try
    {
        _context.Database.BeginTransaction();

        // 1. Load giỏ hàng
        var gioHang = _context.GioHangs
            .Where(g => g.MaKH == kh.MaKH)
            .ToList();

        if (!gioHang.Any())
        {
            TempData["ErrorMessage"] = "Giỏ hàng trống";
            return RedirectToAction("Index", "GioHang");
        }

        // 2. Tính tổng tiền và áp dụng khuyến mãi
        decimal tongTien = 0;
        decimal tienGiam = 0;
        KhuyenMai khuyenMai = null;

        // Load khuyến mãi nếu có
        if (!string.IsNullOrEmpty(MaKM))
        {
            khuyenMai = _context.KhuyenMais
                .FirstOrDefault(km => km.MaKM == MaKM
                                   && km.NgayBatDau <= DateTime.Now
                                   && km.NgayKetThuc >= DateTime.Now
                                   && km.TrangThai == true);
        }

        // Tính toán từng sản phẩm
        var danhSachThanhToan = new List<(string MaSP, string LoaiSP, int SoLuong, decimal DonGia, decimal DonGiaSauGiam)>();

        foreach (var item in gioHang)
        {
            decimal giaSanPham = 0;

            if (item.LoaiSanPham == "VACCINE")
            {
                var vc = _context.Vaccines.Find(item.MaSanPham);
                if (vc == null || vc.SoLuong < item.SoLuong)
                {
                    _context.Database.Rollback();
                    TempData["ErrorMessage"] = $"Vaccine {vc?.TenVC ?? item.MaSanPham} không đủ số lượng";
                    return RedirectToAction("Checkout");
                }
                giaSanPham = vc.GiaBan;
            }
            else
            {
                var goi = _context.GoiVaccines.Find(item.MaSanPham);
                if (goi == null)
                {
                    _context.Database.Rollback();
                    TempData["ErrorMessage"] = "Gói vaccine không tồn tại";
                    return RedirectToAction("Checkout");
                }
                giaSanPham = goi.GiaGoi;
            }

            decimal donGiaSauGiam = giaSanPham;

            // Áp dụng khuyến mãi cho sản phẩm
            if (khuyenMai != null)
            {
                var apDung = _context.ChiTietKhuyenMais
                    .Any(ct => ct.MaKM == MaKM
                            && ct.MaSanPham == item.MaSanPham
                            && ct.LoaiSanPham == item.LoaiSanPham);

                if (apDung)
                {
                    if (khuyenMai.KieuGiam == "PhanTram")
                    {
                        decimal giamGia = giaSanPham * khuyenMai.GiaTriGiam / 100;
                        donGiaSauGiam = giaSanPham - giamGia;
                        tienGiam += giamGia * item.SoLuong;
                    }
                    else // SoTien
                    {
                        donGiaSauGiam = giaSanPham - khuyenMai.GiaTriGiam;
                        tienGiam += khuyenMai.GiaTriGiam * item.SoLuong;
                    }
                }
            }

            tongTien += giaSanPham * item.SoLuong;

            danhSachThanhToan.Add((
                item.MaSanPham,
                item.LoaiSanPham,
                item.SoLuong,
                giaSanPham,
                donGiaSauGiam
            ));
        }

        decimal tongTienSauGiam = tongTien - tienGiam;

        // 3. Tạo mã hóa đơn
        string maHD;
        do
        {
            maHD = "HD" + DateTime.Now.ToString("yyMMddHHmmss").Substring(0, 8);
        } while (_context.HoaDons.Any(h => h.MaHD == maHD));

        // 4. Tạo hóa đơn
        var hoaDon = new HoaDon
        {
            MaHD = maHD,
            NgayLap = DateTime.Now,
            TongTien = tongTienSauGiam,
            TrangThai = (PhuongThucThanhToan == "Online"), // true nếu online, false nếu COD
            MaKH = kh.MaKH,
            MaNV = null, // Chưa có nhân viên xử lý
            MaKM = khuyenMai?.MaKM
        };
        _context.HoaDons.Add(hoaDon);
        _context.SaveChanges();

        // 5. Tạo chi tiết hóa đơn
        int stt = 1;
        foreach (var item in danhSachThanhToan)
        {
            var chiTiet = new ChiTietHoaDon
            {
                MaCTHD = maHD + stt.ToString("D2"),
                SoLuong = item.SoLuong,
                DonGia = item.DonGiaSauGiam, // Giá ĐÃ GIẢM
                MaSanPham = item.MaSP,
                LoaiSanPham = item.LoaiSP,
                MaHD = maHD
            };
            _context.ChiTietHoaDons.Add(chiTiet);
            stt++;

            // 6. Trừ số lượng vaccine (nếu là vaccine)
            if (item.LoaiSP == "VACCINE")
            {
                var vaccine = _context.Vaccines.Find(item.MaSP);
                if (vaccine != null)
                {
                    vaccine.SoLuong -= item.SoLuong;
                }
            }
        }
        _context.SaveChanges();

        // 7. Xóa giỏ hàng
        _context.GioHangs.RemoveRange(gioHang);
        _context.SaveChanges();

        // 8. Tạo lịch tiêm tự động (nếu cần)
        // TODO: Logic tạo lịch tiêm cho từng vaccine/gói

        _context.Database.Commit();

        TempData["SuccessMessage"] = $"Đặt hàng thành công! Mã hóa đơn: {maHD}";
        return RedirectToAction("ChiTietHoaDon", "Account", new { maHD = maHD });
    }
    catch (Exception ex)
    {
        _context.Database.Rollback();
        TempData["ErrorMessage"] = "Lỗi thanh toán: " + ex.Message;
        return RedirectToAction("Checkout");
    }
}
```

---

### **GIAI ĐOẠN 4: XEM HÓA ĐƠN**

#### 4.1. Danh sách hóa đơn

**Đã có trong AccountController.cs:**

```csharp
// GET: Account/HoaDon
public ActionResult HoaDon()
{
    var kh = Session["KH"] as KhachHang;
    var hoaDons = _context.HoaDons
        .Where(hd => hd.MaKH == kh.MaKH)
        .OrderByDescending(hd => hd.NgayLap)
        .ToList();

    return View(hoaDons);
}
```

#### 4.2. Chi tiết hóa đơn

**Đã có trong AccountController.cs:**

```csharp
// GET: Account/ChiTietHoaDon
public ActionResult ChiTietHoaDon(string maHD)
{
    var kh = Session["KH"] as KhachHang;
    var hoaDon = _context.HoaDons
        .FirstOrDefault(hd => hd.MaHD == maHD && hd.MaKH == kh.MaKH);

    if (hoaDon == null)
        return HttpNotFound();

    hoaDon.ChiTietHoaDon = _context.ChiTietHoaDons
        .Where(ct => ct.MaHD == maHD)
        .ToList();

    // Load thông tin sản phẩm
    foreach (var ct in hoaDon.ChiTietHoaDon)
    {
        if (ct.LoaiSanPham == "VACCINE")
        {
            ct.TenSanPham = _context.Vaccines.Find(ct.MaSanPham)?.TenVC;
        }
        else
        {
            ct.TenSanPham = _context.GoiVaccines.Find(ct.MaSanPham)?.TenGoi;
        }
    }

    return View(hoaDon);
}
```

---

## 📋 VIEWMODELS CẦN THIẾT

### 1. GioHangViewModel.cs

```csharp
public class GioHangViewModel
{
    public int MaGH { get; set; }
    public string MaSanPham { get; set; }
    public string TenSanPham { get; set; }
    public string LoaiSanPham { get; set; }
    public decimal DonGia { get; set; }
    public int SoLuong { get; set; }
    public decimal ThanhTien { get; set; }
    public string HinhAnh { get; set; }
}
```

### 2. CheckoutViewModel.cs

```csharp
public class CheckoutViewModel
{
    public KhachHang KhachHang { get; set; }
    public List<GioHangViewModel> GioHang { get; set; }
    public decimal TongTienTruocGiam { get; set; }
    public decimal TienGiam { get; set; }
    public decimal TongTienSauGiam { get; set; }
    public List<KhuyenMai> KhuyenMais { get; set; }
    public string MaKMApDung { get; set; }
}
```

### 3. HoaDonViewModel.cs

```csharp
public class HoaDonViewModel
{
    public string MaHD { get; set; }
    public DateTime NgayLap { get; set; }
    public decimal TongTien { get; set; }
    public bool TrangThai { get; set; }
    public string TenKhachHang { get; set; }
    public string SoDT { get; set; }
    public List<ChiTietHoaDonViewModel> ChiTiet { get; set; }
    public string TenKhuyenMai { get; set; }
}

public class ChiTietHoaDonViewModel
{
    public string TenSanPham { get; set; }
    public string LoaiSanPham { get; set; }
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien { get; set; }
}
```

---

## 🎨 VIEWS CẦN THIẾT

### 1. `/Views/GioHang/Index.cshtml` - Giỏ hàng

**Hiển thị:**

- Bảng danh sách sản phẩm (Hình ảnh, Tên, Giá, Số lượng)
- Nút tăng/giảm số lượng
- Nút xóa
- Tổng tiền
- Nút "Tiếp tục mua sắm" & "Thanh toán"

### 2. `/Views/HoaDon/Checkout.cshtml` - Thanh toán

**Hiển thị:**

- Thông tin khách hàng
- Danh sách sản phẩm trong giỏ
- Form nhập mã khuyến mãi
- Hiển thị giá giảm (nếu có)
- Chọn phương thức thanh toán (COD/Online)
- Nhập địa chỉ giao hàng
- Ghi chú
- Nút "Xác nhận đặt hàng"

### 3. `/Views/Account/HoaDon.cshtml` - Danh sách hóa đơn (ĐÃ CÓ)

### 4. `/Views/Account/ChiTietHoaDon.cshtml` - Chi tiết hóa đơn (ĐÃ CÓ)

---

## 🔐 VALIDATION & BUSINESS RULES

### 1. Kiểm tra đăng nhập

```csharp
var kh = Session["KH"] as KhachHang;
if (kh == null)
    return RedirectToAction("Login", "Account");
```

### 2. Kiểm tra số lượng tồn kho

```csharp
if (vaccine.SoLuong < soLuongMua)
{
    return Json(new { success = false, message = "Không đủ số lượng" });
}
```

### 3. Kiểm tra mã khuyến mãi hợp lệ

```csharp
var km = _context.KhuyenMais
    .FirstOrDefault(k => k.MaKM == maKM
                      && k.NgayBatDau <= DateTime.Now
                      && k.NgayKetThuc >= DateTime.Now
                      && k.TrangThai == true);
```

### 4. Kiểm tra sản phẩm áp dụng khuyến mãi

```csharp
var apDung = _context.ChiTietKhuyenMais
    .Any(ct => ct.MaKM == maKM
            && ct.MaSanPham == maSP
            && ct.LoaiSanPham == loaiSP);
```

---

## 📊 FLOWCHART

```
[Khách hàng duyệt sản phẩm]
         ↓
[Chọn Vaccine/Gói vaccine]
         ↓
[Bấm "Thêm vào giỏ"] → POST /GioHang/ThemVaoGio
         ↓
[Kiểm tra đăng nhập]
    ↓ (OK)
[Lưu vào bảng GioHang]
         ↓
[Xem giỏ hàng] → GET /GioHang/Index
         ↓
[Cập nhật số lượng / Xóa item] (tùy chọn)
         ↓
[Bấm "Thanh toán"] → GET /HoaDon/Checkout
         ↓
[Hiển thị trang thanh toán]
         ↓
[Nhập mã khuyến mãi] (tùy chọn) → POST /HoaDon/ApDungKhuyenMai
         ↓
[Hiển thị giá giảm]
         ↓
[Chọn phương thức thanh toán]
         ↓
[Bấm "Xác nhận"] → POST /HoaDon/XacNhanThanhToan
         ↓
[Transaction BEGIN]
         ↓
[Kiểm tra tồn kho]
    ↓ (OK)
[Tạo HoaDon]
    ↓
[Tạo ChiTietHoaDon (từng sản phẩm)]
    ↓
[Trừ số lượng Vaccine]
    ↓
[Xóa GioHang]
    ↓
[Tạo LichTiem (tùy chọn)]
    ↓
[Transaction COMMIT]
         ↓
[Redirect → Chi tiết hóa đơn]
```

---

## 🚀 TÍCH HỢP THANH TOÁN ONLINE (MỞ RỘNG)

### VNPay Integration (tùy chọn)

**Thêm vào Checkout:**

```csharp
if (PhuongThucThanhToan == "VNPay")
{
    // Lưu hóa đơn với TrangThai = false
    // Chuyển hướng đến VNPay
    string vnpayUrl = GenerateVNPayUrl(hoaDon.MaHD, hoaDon.TongTien);
    return Redirect(vnpayUrl);
}
```

**Callback từ VNPay:**

```csharp
public ActionResult VNPayCallback(string vnp_ResponseCode, string vnp_TxnRef)
{
    if (vnp_ResponseCode == "00") // Thanh toán thành công
    {
        var hoaDon = _context.HoaDons.Find(vnp_TxnRef);
        hoaDon.TrangThai = true;
        _context.SaveChanges();

        TempData["SuccessMessage"] = "Thanh toán thành công!";
    }
    else
    {
        TempData["ErrorMessage"] = "Thanh toán thất bại!";
    }

    return RedirectToAction("ChiTietHoaDon", "Account", new { maHD = vnp_TxnRef });
}
```

---

## ✅ CHECKLIST IMPLEMENTATION

### Controllers

- [ ] `GioHangController.ThemVaoGio()` - Thêm vào giỏ
- [ ] `GioHangController.Index()` - Xem giỏ hàng
- [ ] `GioHangController.CapNhatSoLuong()` - Cập nhật số lượng
- [ ] `GioHangController.XoaKhoiGio()` - Xóa khỏi giỏ
- [ ] `HoaDonController.Checkout()` - Trang thanh toán
- [ ] `HoaDonController.ApDungKhuyenMai()` - Áp dụng mã KM
- [ ] `HoaDonController.XacNhanThanhToan()` - Xác nhận thanh toán
- [x] `AccountController.HoaDon()` - Danh sách hóa đơn (ĐÃ CÓ)
- [x] `AccountController.ChiTietHoaDon()` - Chi tiết hóa đơn (ĐÃ CÓ)

### ViewModels

- [ ] `GioHangViewModel.cs`
- [ ] `CheckoutViewModel.cs`
- [ ] `HoaDonViewModel.cs`

### Views

- [ ] `/Views/GioHang/Index.cshtml`
- [ ] `/Views/HoaDon/Checkout.cshtml`
- [ ] Update `/Views/Vaccine/Index.cshtml` - Thêm nút "Thêm vào giỏ"
- [ ] Update `/Views/GoiVaccine/Index.cshtml` - Thêm nút "Thêm vào giỏ"

### JavaScript

- [ ] Cart functions (thêm, xóa, cập nhật)
- [ ] Apply voucher AJAX
- [ ] Checkout validation

---

## 💡 LƯU Ý QUAN TRỌNG

1. **Transaction:** Bắt buộc dùng transaction cho quá trình thanh toán để đảm bảo tính toàn vẹn dữ liệu

2. **Kiểm tra tồn kho:** Phải kiểm tra số lượng vaccine trước khi thanh toán

3. **Giá lưu trong ChiTietHoaDon:** Lưu giá ĐÃ GIẢM để tránh thay đổi giá sau này ảnh hưởng

4. **Xóa giỏ hàng:** Chỉ xóa sau khi thanh toán thành công

5. **Mã hóa đơn:** Đảm bảo unique (dùng timestamp + check exists)

6. **Security:** Validate tất cả input, đặc biệt là MaKM và số lượng

7. **UX:** Hiển thị loading, notification cho mọi action

---

## 🎯 KẾT LUẬN

Luồng thanh toán hoàn chỉnh bao gồm:

- ✅ Quản lý giỏ hàng (CRUD)
- ✅ Áp dụng khuyến mãi thông minh
- ✅ Xử lý thanh toán với transaction
- ✅ Trừ tồn kho tự động
- ✅ Tạo hóa đơn & chi tiết
- ✅ Tích hợp sẵn sàng cho payment gateway

**Code sẵn sàng để implement!** 🚀
