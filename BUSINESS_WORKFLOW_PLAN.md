# KẾ HOẠCH XÂY DỰNG LUỒNG NGHIỆP VỤ CRUD - HỆ THỐNG TPVAX

## 📋 TỔNG QUAN HỆ THỐNG

### Cấu trúc Database (Đã phân tích)

```
TaiKhoan (Tài khoản đăng nhập)
├── KhachHang (Khách hàng)
├── NhanVien (Nhân viên)

LoaiVaccine (Loại vaccine - Vaccine kết hợp, Vaccine đơn vị...)
├── Vaccine (Vaccine) - CÓ HÌNH ẢNH
    ├── VaccinePhongBenh (Vaccine phòng bệnh nào - nhiều-nhiều)
    │   └── LoaiBenh (Loại bệnh)
    ├── ChiTietGoiVaccine (Chi tiết gói)
    └── LichTiem (Lịch tiêm)

NhaCungCap (Nhà cung cấp) - KHÔNG CÓ HÌNH ẢNH
└── PhieuNhapVaccine (Phiếu nhập)
    └── ChiTietPhieuNhap (Chi tiết phiếu nhập)

GoiVaccine (Gói vaccine) - CÓ HÌNH ẢNH
└── ChiTietGoiVaccine (Chi tiết gói - nhiều vaccine trong 1 gói)

KhuyenMai (Khuyến mãi) - CÓ HÌNH ẢNH
└── ChiTietKhuyenMai (Áp dụng cho vaccine hoặc gói vaccine)
```

---

## 🎯 CHỨC NĂNG 1: QUẢN LÝ VẮC XIN (Vaccine)

### 📊 Cấu trúc Database

```sql
Vaccine:
- MaVC (CHAR(10), PK) - Auto: VC00000001
- TenVC (NVARCHAR(255)) *required
- GiaBan (DECIMAL(18,0)) *required
- SoLuong (INT, DEFAULT 0) *required
- SoMuiToiDa (INT) - Số mũi tối đa
- SoThangCho (INT) - Số tháng chờ giữa các mũi
- MaLoai (CHAR(10), FK) *required -> LoaiVaccine
- MoTa (NVARCHAR(MAX))
- HinhAnh (VARCHAR(255)) - Đường dẫn ảnh

VaccinePhongBenh: (Quan hệ nhiều-nhiều)
- MaVC + MaLoaiBenh (PK)
- GhiChu (NVARCHAR(MAX))
```

### 🔄 Luồng Nghiệp Vụ CRUD

#### 1️⃣ CREATE - Thêm Vaccine Mới

**Bước 1: Hiển thị Form**

- Endpoint: `GET /Admin/Vaccines` (Trang danh sách có nút "+ Thêm vắc xin")
- Mở Modal với form trống
- Load dữ liệu dropdown:
  - `GET /Admin/GetLoaiVaccineList` → Danh sách loại vaccine
  - `GET /Admin/GetLoaiBenhList` → Danh sách bệnh (checkbox)

**Bước 2: Validate Input (Client-side)**

- Tên vaccine: Required, max 255 ký tự
- Giá bán: Required, > 0, số nguyên
- Số lượng: Required, >= 0
- Số mũi tối đa: Optional, 1-10
- Số tháng chờ: Optional, 0-120
- Loại vaccine: Required (dropdown)
- Hình ảnh: Optional, chỉ cho phép .jpg, .jpeg, .png, .gif, max 5MB
- Bệnh phòng ngừa: Chọn ít nhất 1 (checkbox)

**Bước 3: Submit Form**

```javascript
POST /Admin/CreateVaccine
Content-Type: multipart/form-data
[ValidateAntiForgeryToken]

Request Body:
{
  TenVC: "Infanrix Hexa (6 trong 1)",
  GiaBan: 1098000,
  SoLuong: 100,
  SoMuiToiDa: 1,
  SoThangCho: null,
  MaLoai: "LVCN000001",
  MoTa: "Vắc xin kết hợp...",
  ImageFile: [File],
  SelectedLoaiBenhIds: ["LB00000001", "LB00000002"]
}
```

**Bước 4: Xử lý Server-side**

```csharp
1. Validate ModelState
2. Generate MaVC mới: VC00000001 (tăng tự động)
3. Upload hình ảnh:
   - Validate extension (.jpg, .jpeg, .png, .gif)
   - Generate unique filename: {GUID}.jpg
   - Lưu vào: ~/Content/Images/vaccines/
   - Lưu path: /Content/Images/vaccines/{GUID}.jpg
4. Tạo entity Vaccine mới
5. Insert vào database
6. Tạo VaccinePhongBenh cho mỗi bệnh được chọn
7. SaveChanges()
8. Return JSON: { success: true, message: "Thêm thành công!" }
```

**Bước 5: Xử lý Response**

- Success: Đóng modal, reload DataTable, hiển thị SweetAlert success
- Error: Hiển thị lỗi trong modal, giữ nguyên dữ liệu

---

#### 2️⃣ READ - Xem Danh Sách & Chi Tiết

**A. Danh Sách Vaccine**

```
GET /Admin/Vaccines
View: Vaccines.cshtml
Model: List<AdminVaccineViewModel>

DataTable hiển thị:
- Mã Vắc xin (MaVC)
- Tên Vắc xin (TenVC)
- Phòng bệnh (Join VaccinePhongBenh)
- Tồn kho (SoLuong)
- Giá (GiaBan - format: 1.098.000 VNĐ)
- Trạng thái (Hết hàng/Còn hàng dựa trên SoLuong)
- Hành động (Sửa/Xóa)

Features:
- Pagination (10/25/50/100 items)
- Search (tìm theo tên, mã)
- Sort (tất cả cột)
- Filter theo loại vaccine
```

**B. Chi Tiết Vaccine**

```
GET /Admin/GetVaccine?id=VC00000001
Returns: AdminVaccineCreateEditViewModel

Include:
- Thông tin cơ bản
- Hình ảnh hiện tại
- Loại vaccine
- Danh sách bệnh đang phòng ngừa (SelectedLoaiBenhIds)
```

---

#### 3️⃣ UPDATE - Cập Nhật Vaccine

**Bước 1: Load Dữ Liệu**

```javascript
- Click nút "Sửa" trên table
- AJAX GET /Admin/GetVaccine?id=VC00000001
- Fill dữ liệu vào form modal
- Hiển thị preview hình ảnh hiện tại
- Check các checkbox bệnh phòng ngừa
```

**Bước 2: Validate & Submit**

```javascript
POST /Admin/EditVaccine
[ValidateAntiForgeryToken]

Request Body:
{
  MaVC: "VC00000001", // Required, read-only
  TenVC: "Infanrix Hexa (6 trong 1) - Cập nhật",
  GiaBan: 1200000,
  SoLuong: 150,
  SoMuiToiDa: 1,
  SoThangCho: null,
  MaLoai: "LVCN000001",
  MoTa: "Mô tả cập nhật...",
  ImageFile: [File] hoặc null, // Nếu có thì cập nhật
  SelectedLoaiBenhIds: ["LB00000001", "LB00000003"]
}
```

**Bước 3: Xử lý Server-side**

```csharp
1. Validate ModelState
2. Tìm vaccine theo MaVC
3. Nếu có ImageFile mới:
   - Xóa ảnh cũ (DeleteFile)
   - Upload ảnh mới
4. Update các field
5. Xóa tất cả VaccinePhongBenh cũ
6. Thêm VaccinePhongBenh mới theo SelectedLoaiBenhIds
7. SaveChanges()
8. Return success
```

**Quy tắc nghiệp vụ:**

- Không cho phép thay đổi MaVC
- Nếu vaccine đã có trong lịch tiêm, cảnh báo khi giảm số lượng
- Nếu không upload ảnh mới, giữ nguyên ảnh cũ

---

#### 4️⃣ DELETE - Xóa Vaccine

**Bước 1: Confirm**

```javascript
- Click nút "Xóa"
- Hiển thị SweetAlert confirm:
  "Bạn có chắc muốn xóa vaccine [Tên Vaccine]?"
```

**Bước 2: Submit**

```javascript
POST / Admin / DeleteVaccine[ValidateAntiForgeryToken];
Request: {
  id: "VC00000001";
}
```

**Bước 3: Xử lý Server-side**

```csharp
1. Tìm vaccine theo id
2. Kiểm tra ràng buộc:
   - Có trong LichTiem? → Không cho xóa
   - Có trong ChiTietGoiVaccine? → Không cho xóa
   - Có trong ChiTietPhieuNhap? → Không cho xóa
3. Nếu OK:
   - Xóa VaccinePhongBenh liên quan
   - Xóa file hình ảnh
   - Xóa Vaccine
   - SaveChanges()
4. Return success hoặc error
```

**Quy tắc nghiệp vụ:**

- KHÔNG CHO XÓA nếu đã có lịch tiêm
- KHÔNG CHO XÓA nếu đã có trong gói vaccine
- Có thể xóa nếu chỉ có phiếu nhập (soft delete - đánh dấu ngừng kinh doanh)

---

## 🎯 CHỨC NĂNG 2: QUẢN LÝ NHÀ CUNG CẤP (NhaCungCap)

### 📊 Cấu trúc Database

```sql
NhaCungCap:
- MaNCC (CHAR(10), PK) - Auto: NCC0000001
- TenNCC (NVARCHAR(255)) *required
- DiaChi (NVARCHAR(500))
- Email (VARCHAR(100))
- SoDT (VARCHAR(10)) - Regex: 10 chữ số
- TenNganHang (NVARCHAR(100))
- SoTK (VARCHAR(30))
```

### 🔄 Luồng Nghiệp Vụ CRUD

#### 1️⃣ CREATE - Thêm Nhà Cung Cấp

**Validate Input:**

- Tên NCC: Required, max 255 ký tự
- Địa chỉ: Optional, max 500 ký tự
- Email: Optional, format email hợp lệ
- Số điện thoại: Optional, regex `^\d{10}$`
- Tên ngân hàng: Optional, max 100 ký tự
- Số tài khoản: Optional, max 30 ký tự

**Server Processing:**

```csharp
1. Validate ModelState
2. Generate MaNCC: NCC0000001
3. Tạo entity NhaCungCap
4. Insert database
5. SaveChanges()
6. Return success
```

**Đặc điểm:**

- KHÔNG CÓ HÌNH ẢNH
- Form đơn giản hơn Vaccine
- Không có quan hệ phức tạp

---

#### 2️⃣ READ - Danh Sách & Chi Tiết

**View:**

```
NhaCungCap.cshtml (CẦN TẠO MỚI)
Model: List<AdminNhaCungCapViewModel>

DataTable:
- Mã NCC
- Tên công ty
- Người liên hệ
- Số điện thoại
- Email
- Địa chỉ
- Trạng thái (Hoạt động/Ngừng)
- Hành động (Sửa/Xóa)
```

**Statistics Cards:**

```
- Tổng nhà cung cấp: 12
- Đang hoạt động: 9
- Tạm ngừng: 2
- Tổng đơn nhập: 456
```

---

#### 3️⃣ UPDATE - Cập Nhật NCC

**Luồng:**

1. Load data: `GET /Admin/GetNhaCungCap?id=NCC0000001`
2. Fill form
3. Submit: `POST /Admin/EditNhaCungCap`
4. Update entity
5. SaveChanges()

**Quy tắc:**

- Không cho thay đổi MaNCC
- Có thể vô hiệu hóa thay vì xóa

---

#### 4️⃣ DELETE - Xóa NCC

**Kiểm tra:**

```csharp
- Có PhieuNhapVaccine? → Không cho xóa, đề xuất vô hiệu hóa
- Chưa có phiếu nhập? → Cho phép xóa
```

---

## 🎯 CHỨC NĂNG 3: QUẢN LÝ GÓI VẮC XIN (GoiVaccine)

### 📊 Cấu trúc Database

```sql
GoiVaccine:
- MaGoi (CHAR(10), PK) - Auto: GOI0000001
- TenGoi (NVARCHAR(255)) *required
- MoTa (NVARCHAR(MAX))
- DoiTuongApDung (NVARCHAR(255)) *required
  * "Trẻ từ 0-12 tháng"
  * "Trẻ từ 0-24 tháng"
  * "Phụ nữ chuẩn bị mang thai"
  * "Người lớn"
- GiaGoi (DECIMAL(18,0)) *required
- TrangThai (NVARCHAR(50))
  * "Đang hoạt động"
  * "Tạm ngừng"
  * "Hết hàng"
- HinhAnh (VARCHAR(255))

ChiTietGoiVaccine:
- MaCTGoi (CHAR(10), PK) - Auto: CTGV0001
- MaGoi (CHAR(10), FK)
- MaVC (CHAR(10), FK) *required
- SoMui (INT) - Số mũi tiêm trong gói (phác đồ)
- GhiChu (NVARCHAR(MAX))
```

### 🔄 Luồng Nghiệp Vụ CRUD

#### 1️⃣ CREATE - Thêm Gói Vaccine

**Form Fields:**

```javascript
{
  TenGoi: "Gói vắc xin cho trẻ (0-12 tháng)",
  MoTa: "Bảo vệ con trẻ...",
  DoiTuongApDung: "Trẻ từ 0-12 tháng", // Dropdown
  GiaGoi: 8554000,
  TrangThai: "Đang hoạt động", // Dropdown
  ImageFile: [File],
  SelectedVaccineIds: ["VC00000001", "VC00000002"], // Multi-select
  VaccineSoMui: {
    "VC00000001": 1,
    "VC00000002": 2
  }
}
```

**Validate:**

- Tên gói: Required, max 255
- Giá gói: Required, > 0
- Đối tượng: Required
- Hình ảnh: Optional, image files only
- Vaccines: Chọn ít nhất 1 vaccine

**Processing:**

```csharp
1. Validate input
2. Generate MaGoi: GOI0000001
3. Upload image → /Content/Images/packages/
4. Tạo GoiVaccine entity
5. Insert database
6. Tạo ChiTietGoiVaccine cho mỗi vaccine:
   - Generate MaCTGoi
   - MaGoi = GOI0000001
   - MaVC = vaccine được chọn
   - SoMui = số mũi nhập vào
7. SaveChanges()
8. Return success
```

**UI Features:**

```javascript
// Dynamic vaccine selection
- Hiển thị bảng vaccines có sẵn (SoLuong > 0)
- Checkbox chọn vaccine
- Input số mũi cho mỗi vaccine
- Hiển thị tổng giá gói (suggestion = tổng giá vaccines)
- Preview hình ảnh trước khi upload
```

---

#### 2️⃣ READ - Danh Sách & Chi Tiết

**View:**

```
GoiVaccine.cshtml (CẦN TẠO MỚI)
Model: List<AdminGoiVaccineViewModel>

Layout dạng CARDS:
┌─────────────────────────────────────┐
│  [Hình ảnh gói]                     │
│  GOI0000001 | Tạm dừng              │
├─────────────────────────────────────┤
│  Gói vắc xin cho trẻ (0-12 tháng)  │
│  🎯 Đối tượng áp dụng:              │
│     Trẻ từ 0 đến 12 tháng tuổi     │
│  📋 Bảo vệ con toàn diện...         │
├─────────────────────────────────────┤
│  Giá: 8.554.000 VNĐ                │
├─────────────────────────────────────┤
│  [Sửa] [Chi tiết]                  │
└─────────────────────────────────────┘
```

**Statistics:**

```
- Tổng gói vắc xin: 15
- Gói trẻ em: 8
- Gói người lớn: 5
- Gói đã bán: 256
```

**Detail Modal:**

```
Gói: Gói vắc xin cho trẻ (0-12 tháng)
Đối tượng: Trẻ từ 0-12 tháng

Danh sách vaccine trong gói:
┌──────┬───────────────────┬────────┬──────────┐
│ STT  │ Tên Vaccine       │ Số mũi │ Đơn giá  │
├──────┼───────────────────┼────────┼──────────┤
│ 1    │ Infanrix Hexa    │ 1      │ 1.098.000│
│ 2    │ Hexaxim          │ 2      │ 1.098.000│
└──────┴───────────────────┴────────┴──────────┘
Tổng giá: 8.554.000 VNĐ
```

---

#### 3️⃣ UPDATE - Cập Nhật Gói

**Load Data:**

```javascript
GET /Admin/GetGoiVaccine?id=GOI0000001
Returns: AdminGoiVaccineCreateEditViewModel

Include:
- Thông tin gói
- Danh sách vaccine hiện tại (ExistingVaccines)
- Hình ảnh hiện tại
```

**Processing:**

```csharp
1. Validate
2. Update GoiVaccine entity
3. Upload image mới (nếu có)
4. Xóa tất cả ChiTietGoiVaccine cũ
5. Thêm ChiTietGoiVaccine mới
6. SaveChanges()
```

**Business Rules:**

- Không cho xóa vaccine nếu gói đã có hóa đơn
- Cảnh báo nếu thay đổi giá gói (ảnh hưởng đến khách hàng đã mua)

---

#### 4️⃣ DELETE - Xóa Gói

**Checks:**

```csharp
1. Có trong GioHang? → Không cho xóa
2. Có trong HoaDon? → Không cho xóa
3. Có trong ChiTietKhuyenMai? → Xóa trước khuyến mãi
4. OK:
   - Xóa ChiTietGoiVaccine
   - Xóa hình ảnh
   - Xóa GoiVaccine
```

---

## 🎯 CHỨC NĂNG 4: QUẢN LÝ KHUYẾN MÃI (KhuyenMai) - MỚI

### 📊 Cấu trúc Database

```sql
KhuyenMai:
- MaKM (CHAR(10), PK) - Auto: KM00000001
- TenKM (NVARCHAR(255)) *required
- MoTa (NVARCHAR(MAX))
- LoaiKM (NVARCHAR(100))
  * "Lễ Tết" (Tết, Quốc khánh...)
  * "Sinh Nhật" (Sinh nhật công ty, khách hàng)
  * "Khuyến mãi đặc biệt"
  * "Flash Sale"
- KieuGiam (NVARCHAR(50)) *required
  * "PhanTram" (Giảm theo %)
  * "SoTien" (Giảm tiền cố định)
- GiaTriGiam (DECIMAL(18,2)) *required
  * Nếu PhanTram: 10 (nghĩa là 10%)
  * Nếu SoTien: 50000 (nghĩa là giảm 50k)
- NgayBatDau (DATETIME) *required
- NgayKetThuc (DATETIME) *required
- TrangThai (BIT) DEFAULT 1
  * true = Đang chạy
  * false = Tạm dừng/Hết hạn
- HinhAnh (VARCHAR(255))

ChiTietKhuyenMai:
- MaCTKM (INT, PK, IDENTITY)
- MaKM (CHAR(10), FK)
- LoaiSanPham (NVARCHAR(50)) *required
  * "Vaccine"
  * "GoiVaccine"
- MaSanPham (CHAR(10)) *required
  * MaVC hoặc MaGoi
- NgayApDung (DATETIME)
- NgayKetThuc (DATETIME)
- GhiChu (NVARCHAR(MAX))
```

### 🔄 Luồng Nghiệp Vụ CRUD

#### 1️⃣ CREATE - Thêm Khuyến Mãi

**Form Layout:**

```html
<!-- Tab 1: Thông tin khuyến mãi -->
<div class="tab-pane active" id="info">
  <input name="TenKM" placeholder="VD: Giảm giá Tết 2025" />
  <textarea name="MoTa" />
  <select name="LoaiKM">
    <option>Lễ Tết</option>
    <option>Sinh Nhật</option>
    <option>Flash Sale</option>
  </select>

  <div class="row">
    <select name="KieuGiam" onchange="updateGiaTriLabel()">
      <option value="PhanTram">Giảm theo %</option>
      <option value="SoTien">Giảm tiền cố định</option>
    </select>
    <input name="GiaTriGiam" type="number" />
    <span id="giaTriLabel">%</span>
  </div>

  <input type="datetime-local" name="NgayBatDau" />
  <input type="datetime-local" name="NgayKetThuc" />

  <input type="file" name="ImageFile" accept="image/*" />

  <label>
    <input type="checkbox" name="TrangThai" />
    Kích hoạt ngay
  </label>
</div>

<!-- Tab 2: Chọn sản phẩm áp dụng -->
<div class="tab-pane" id="products">
  <h5>Vaccines</h5>
  <div id="vaccineList">
    <!-- Load từ GetVaccineList -->
    <label>
      <input type="checkbox" name="SelectedVaccineIds" value="VC00000001" />
      Infanrix Hexa (1.098.000đ)
    </label>
  </div>

  <h5>Gói Vaccines</h5>
  <div id="packageList">
    <!-- Load từ GetGoiVaccineList -->
    <label>
      <input type="checkbox" name="SelectedGoiVaccineIds" value="GOI0000001" />
      Gói trẻ 0-12 tháng (8.554.000đ)
    </label>
  </div>
</div>
```

**Validate:**

```javascript
- Tên KM: Required, max 255
- Kiểu giảm: Required
- Giá trị giảm: Required, > 0
  * Nếu PhanTram: 0 < GiaTriGiam <= 100
  * Nếu SoTien: GiaTriGiam > 0
- Ngày bắt đầu: Required
- Ngày kết thúc: Required, phải sau ngày bắt đầu
- Sản phẩm: Chọn ít nhất 1 vaccine hoặc 1 gói
```

**Processing:**

```csharp
POST /Admin/CreateKhuyenMai

1. Validate dates (NgayKetThuc > NgayBatDau)
2. Validate GiaTriGiam theo KieuGiam
3. Generate MaKM: KM00000001
4. Upload image → /Content/Images/promotions/
5. Tạo KhuyenMai entity
6. Insert database
7. Tạo ChiTietKhuyenMai:
   - Với mỗi vaccine được chọn:
     * LoaiSanPham = "Vaccine"
     * MaSanPham = MaVC
   - Với mỗi gói được chọn:
     * LoaiSanPham = "GoiVaccine"
     * MaSanPham = MaGoi
8. SaveChanges()
9. Return success
```

---

#### 2️⃣ READ - Danh Sách & Chi Tiết

**View:**

```
KhuyenMai.cshtml (CẦN TẠO MỚI)
Model: List<AdminKhuyenMaiViewModel>

Layout:
┌─────────────────────────────────────────────┐
│  THỐNG KÊ                                   │
├─────────────────────────────────────────────┤
│  [Tổng KM: 25] [Đang chạy: 5]              │
│  [Sắp diễn ra: 3] [Đã kết thúc: 17]       │
└─────────────────────────────────────────────┘

┌─────────────────────────────────────────────┐
│  DANH SÁCH KHUYẾN MÃI                       │
├─────┬───────────┬────────┬──────────┬───────┤
│ [IMG]│ Tên KM   │ Loại   │ Giảm giá │Status │
├─────┼───────────┼────────┼──────────┼───────┤
│ [🎉] │ Tết 2025 │ Lễ Tết │ 15%      │🟢Đang │
│ [🎂] │ Sinh nhật│ SN     │ 50.000đ  │🟡Sắp  │
└─────┴───────────┴────────┴──────────┴───────┘
```

**Status Badge:**

```javascript
function getStatusBadge(khuyenMai) {
  const now = new Date();
  const start = new Date(khuyenMai.NgayBatDau);
  const end = new Date(khuyenMai.NgayKetThuc);

  if (!khuyenMai.TrangThai) {
    return '<span class="badge bg-secondary">Tạm dừng</span>';
  }

  if (now < start) {
    const daysToStart = Math.ceil((start - now) / (1000 * 60 * 60 * 24));
    return `<span class="badge bg-warning">Sắp diễn ra (${daysToStart} ngày)</span>`;
  }

  if (now >= start && now <= end) {
    const daysLeft = Math.ceil((end - now) / (1000 * 60 * 60 * 24));
    return `<span class="badge bg-success">Đang chạy (còn ${daysLeft} ngày)</span>`;
  }

  return '<span class="badge bg-danger">Hết hạn</span>';
}
```

**Detail View:**

```
Khuyến mãi: Giảm giá Tết Nguyên Đán 2025
Loại: Lễ Tết
Giảm: 15% (tối đa 200.000đ)
Thời gian: 01/01/2025 - 15/02/2025
Trạng thái: 🟢 Đang chạy (còn 23 ngày)

Sản phẩm áp dụng:
📌 Vaccines (5):
   - Infanrix Hexa
   - Hexaxim
   ...

📦 Gói Vaccines (3):
   - Gói trẻ 0-12 tháng
   - Gói trẻ 0-24 tháng
   ...

Thống kê:
- Lượt áp dụng: 125
- Tổng giảm: 15.250.000đ
```

---

#### 3️⃣ UPDATE - Cập Nhật Khuyến Mãi

**Load Data:**

```javascript
GET /Admin/GetKhuyenMai?id=KM00000001
Returns: AdminKhuyenMaiCreateEditViewModel

Include:
- Thông tin KM
- ExistingSanPham (vaccines + gói đang áp dụng)
- Hình ảnh
```

**Business Rules:**

```javascript
- Không cho sửa nếu KM đã hết hạn
- Cảnh báo nếu giảm GiaTriGiam (ảnh hưởng khách đã mua)
- Nếu KM đang chạy:
  * Cho phép thay đổi NgayKetThuc (gia hạn)
  * Cho phép bật/tắt TrangThai
  * KHÔNG cho thay đổi KieuGiam, GiaTriGiam
```

**Processing:**

```csharp
POST /Admin/EditKhuyenMai

1. Validate dates
2. Check business rules (đã hết hạn?)
3. Update KhuyenMai entity
4. Upload image mới (nếu có)
5. Xóa ChiTietKhuyenMai cũ
6. Thêm ChiTietKhuyenMai mới
7. SaveChanges()
```

---

#### 4️⃣ DELETE - Xóa Khuyến Mãi

**Checks:**

```csharp
POST /Admin/DeleteKhuyenMai

1. Có trong HoaDon? → KHÔNG CHO XÓA
   Message: "Không thể xóa khuyến mãi đã được sử dụng!"

2. Đang chạy (TrangThai = true && now < NgayKetThuc)?
   → Confirm: "KM đang chạy, chắc chắn xóa?"

3. OK:
   - Xóa ChiTietKhuyenMai
   - Xóa hình ảnh
   - Xóa KhuyenMai
   - SaveChanges()
```

---

## 📁 CẤU TRÚC FILE CẦN TẠO/CẬP NHẬT

### Models/ViewModels/AdminViewModels.cs

```csharp
✅ AdminVaccineViewModel (Đã có)
✅ AdminVaccineCreateEditViewModel (Đã có)
✅ AdminNhaCungCapViewModel (Đã có)
✅ AdminNhaCungCapCreateEditViewModel (Đã có)
✅ AdminGoiVaccineViewModel (Đã có)
✅ AdminGoiVaccineCreateEditViewModel (Đã có)
✅ AdminKhuyenMaiViewModel (Đã có)
✅ AdminKhuyenMaiCreateEditViewModel (Đã có)
✅ ChiTietGoiVaccineViewModel (Đã có)
✅ ChiTietKhuyenMaiViewModel (Đã có)
```

### Controllers/AdminController.cs

```csharp
✅ Vaccines() - Đã có
✅ GetVaccine(id) - Đã có
✅ CreateVaccine(model) - Đã có
✅ EditVaccine(model) - Đã có
✅ DeleteVaccine(id) - Đã có
✅ GetLoaiVaccineList() - Đã có
✅ GetLoaiBenhList() - Đã có

✅ NhaCungCap() - Đã có
✅ GetNhaCungCap(id) - Đã có
✅ CreateNhaCungCap(model) - Đã có
✅ EditNhaCungCap(model) - Đã có
✅ DeleteNhaCungCap(id) - Đã có

✅ GoiVaccine() - Đã có
✅ GetGoiVaccine(id) - Đã có
✅ CreateGoiVaccine(model) - Đã có
✅ EditGoiVaccine(model) - Đã có
✅ DeleteGoiVaccine(id) - Đã có
✅ GetVaccineList() - Đã có

✅ KhuyenMai() - Đã có
✅ GetKhuyenMai(id) - Đã có
✅ CreateKhuyenMai(model) - Đã có
✅ EditKhuyenMai(model) - Đã có
✅ DeleteKhuyenMai(id) - Đã có
✅ GetGoiVaccineList() - Đã có

✅ Helper: SaveUploadedFile(file, subfolder) - Đã có
✅ Helper: DeleteFile(filePath) - Đã có
```

### Views/Admin/

```
✅ Vaccines.cshtml - Đã có (cần kiểm tra)
❌ NhaCungCap.cshtml - CẦN TẠO MỚI
❌ GoiVaccine.cshtml - CẦN TẠO MỚI
❌ KhuyenMai.cshtml - CẦN TẠO MỚI
✅ _AdminLayout.cshtml - Đã có
✅ _AdminSidebar.cshtml - Đã có (cần thêm menu item)
```

### Content/Admin/js/

```
❌ vaccines.js - Script riêng cho vaccine CRUD
❌ nhacungcap.js - Script riêng cho NCC CRUD
❌ goivaccine.js - Script riêng cho gói vaccine CRUD
❌ khuyenmai.js - Script riêng cho khuyến mãi CRUD
✅ app.js - Common functions (đã có)
```

### Common/KeyGenerator.cs

```csharp
✅ GenerateVaccineCode(_unitOfWork) - Đã có
✅ GenerateNhaCungCapCode(_unitOfWork) - Đã có
✅ GenerateGoiVaccineCode(_unitOfWork) - Đã có
✅ GenerateChiTietGoiVaccineCode(_unitOfWork) - Đã có
❌ GenerateKhuyenMaiCode(_unitOfWork) - CẦN THÊM
```

---

## 🎨 UI/UX GUIDELINES

### Design Pattern

```
Admin Panel: AdminLTE 3.2
- Sidebar navigation
- DataTables for listings
- Bootstrap 5 modals
- SweetAlert2 for alerts
- Font Awesome icons
```

### Color Scheme

```css
Primary: #0077b6 (TPVAX Blue)
Success: #28a745 (Đang hoạt động)
Warning: #ffc107 (Sắp diễn ra, Tạm dừng)
Danger: #dc3545 (Hết hàng, Hết hạn)
Info: #17a2b8
```

### Responsive

- Desktop: Full features
- Tablet: Adapted layout
- Mobile: Essential functions only

---

## 🔐 BẢO MẬT & PHÂN QUYỀN

### Authentication

```csharp
[Authorize(Roles = "Admin")]
public class AdminController : Controller
```

### Authorization

```
Admin: Full CRUD
Nhân viên: Read + Update (không Delete)
```

### Validation

```
- Client-side: jQuery Validation
- Server-side: ModelState, Data Annotations
- CSRF: [ValidateAntiForgeryToken]
```

---

## 🚀 TRIỂN KHAI THEO THỨ TỰ

### Phase 1: Vaccine CRUD (HOÀN THÀNH)

✅ Backend: Controller actions
✅ Frontend: Vaccines.cshtml với modal
✅ Image upload handling
✅ Multi-select bệnh phòng ngừa

### Phase 2: Nhà Cung Cấp CRUD (CẦN LÀM)

1. Tạo NhaCungCap.cshtml
2. Implement modal form (no image)
3. DataTable with filters
4. AJAX CRUD operations

### Phase 3: Gói Vaccine CRUD (CẦN LÀM)

1. Tạo GoiVaccine.cshtml
2. Card layout với hình ảnh
3. Modal với multi-select vaccines
4. Dynamic số mũi input
5. Price calculation

### Phase 4: Khuyến Mãi CRUD (CẦN LÀM - MỚI)

1. Tạo KhuyenMai.cshtml
2. Tab layout (Info + Products)
3. Dynamic discount type switching
4. Status badges với countdown
5. Product multi-select (vaccines + packages)
6. Statistics dashboard

### Phase 5: Testing & Polish

1. Unit tests cho business logic
2. Integration tests
3. UI/UX refinement
4. Performance optimization

---

## 📊 DATABASE MIGRATION NOTES

### Kiểm tra constraint

```sql
-- Vaccine
SELECT * FROM Vaccine WHERE HinhAnh IS NULL;

-- GoiVaccine
SELECT * FROM GoiVaccine WHERE HinhAnh IS NULL;

-- KhuyenMai
SELECT * FROM KhuyenMai;
-- Kiểm tra column HinhAnh có tồn tại chưa
```

### Thêm cột HinhAnh cho KhuyenMai (nếu chưa có)

```sql
ALTER TABLE KhuyenMai
ADD HinhAnh VARCHAR(255);
```

---

## 🧪 TEST CASES

### Vaccine CRUD

```
✅ Create vaccine với hình ảnh
✅ Create vaccine không hình ảnh
✅ Update vaccine giữ nguyên ảnh
✅ Update vaccine thay ảnh mới
✅ Delete vaccine chưa dùng
❌ Delete vaccine đã có lịch tiêm (expect: error)
✅ Multi-select bệnh phòng ngừa
```

### NCC CRUD

```
✅ Create NCC với đầy đủ thông tin
✅ Create NCC chỉ thông tin bắt buộc
✅ Validate email format
✅ Validate số điện thoại (10 chữ số)
❌ Delete NCC có phiếu nhập (expect: error)
```

### Gói Vaccine CRUD

```
✅ Create gói với multiple vaccines
✅ Tính giá gói suggestion
✅ Update gói - thêm/bớt vaccine
❌ Delete gói đã có trong hóa đơn (expect: error)
```

### Khuyến Mãi CRUD

```
✅ Create KM giảm theo %
✅ Create KM giảm theo tiền
✅ Validate NgayKetThuc > NgayBatDau
✅ Apply cho vaccines
✅ Apply cho gói vaccines
✅ Apply cho cả hai
✅ Status badge đúng (Sắp diễn ra/Đang chạy/Hết hạn)
❌ Delete KM đã dùng trong hóa đơn (expect: error)
```

---

## 📝 LƯU Ý QUAN TRỌNG

### 1. Image Storage

```
Folder structure:
/Content/Images/
  ├── vaccines/     (Vaccine images)
  ├── packages/     (GoiVaccine images)
  ├── promotions/   (KhuyenMai images)
  └── temp/         (Temporary uploads)

Naming: {GUID}.{extension}
Max size: 5MB
Allowed: .jpg, .jpeg, .png, .gif
```

### 2. Code Generation

```csharp
Vaccine: VC00000001
NhaCungCap: NCC0000001
GoiVaccine: GOI0000001
ChiTietGoiVaccine: CTGV0001
KhuyenMai: KM00000001
```

### 3. Business Constraints

```
- Vaccine: Không xóa nếu có lịch tiêm
- NCC: Không xóa nếu có phiếu nhập
- GoiVaccine: Không xóa nếu có trong hóa đơn
- KhuyenMai: Không xóa nếu đã áp dụng
```

### 4. Performance

```
- Use Include() for eager loading
- Index on foreign keys
- Pagination cho danh sách lớn
- Image optimization (max width: 800px)
```

---

## 🔗 API ENDPOINTS SUMMARY

### Vaccine

```
GET  /Admin/Vaccines               → View danh sách
GET  /Admin/GetVaccine?id=xxx      → Get detail JSON
POST /Admin/CreateVaccine          → Create
POST /Admin/EditVaccine            → Update
POST /Admin/DeleteVaccine          → Delete
GET  /Admin/GetLoaiVaccineList     → Dropdown data
GET  /Admin/GetLoaiBenhList        → Checkbox data
```

### Nhà Cung Cấp

```
GET  /Admin/NhaCungCap             → View danh sách
GET  /Admin/GetNhaCungCap?id=xxx   → Get detail JSON
POST /Admin/CreateNhaCungCap       → Create
POST /Admin/EditNhaCungCap         → Update
POST /Admin/DeleteNhaCungCap       → Delete
```

### Gói Vaccine

```
GET  /Admin/GoiVaccine             → View danh sách
GET  /Admin/GetGoiVaccine?id=xxx   → Get detail JSON
POST /Admin/CreateGoiVaccine       → Create
POST /Admin/EditGoiVaccine         → Update
POST /Admin/DeleteGoiVaccine       → Delete
GET  /Admin/GetVaccineList         → Dropdown data
```

### Khuyến Mãi

```
GET  /Admin/KhuyenMai              → View danh sách
GET  /Admin/GetKhuyenMai?id=xxx    → Get detail JSON
POST /Admin/CreateKhuyenMai        → Create
POST /Admin/EditKhuyenMai          → Update
POST /Admin/DeleteKhuyenMai        → Delete
GET  /Admin/GetVaccineList         → Select vaccines
GET  /Admin/GetGoiVaccineList      → Select packages
```

---

## ✅ CHECKLIST TRIỂN KHAI

### Backend (Controllers)

- [x] Vaccine CRUD - HOÀN THÀNH
- [x] NhaCungCap CRUD - HOÀN THÀNH
- [x] GoiVaccine CRUD - HOÀN THÀNH
- [x] KhuyenMai CRUD - HOÀN THÀNH
- [x] Helper methods - HOÀN THÀNH

### Frontend (Views)

- [x] Vaccines.cshtml - CÓ SẴN
- [ ] NhaCungCap.cshtml - CẦN TẠO
- [ ] GoiVaccine.cshtml - CẦN TẠO
- [ ] KhuyenMai.cshtml - CẦN TẠO
- [x] \_AdminSidebar.cshtml - CẦN THÊM MENU ITEM

### JavaScript

- [ ] vaccines.js - CẦN TẠO
- [ ] nhacungcap.js - CẦN TẠO
- [ ] goivaccine.js - CẦN TẠO
- [ ] khuyenmai.js - CẦN TẠO

### Database

- [x] Kiểm tra schema
- [ ] Thêm cột HinhAnh cho KhuyenMai (nếu cần)
- [ ] Seed data test

### Testing

- [ ] Unit tests
- [ ] Integration tests
- [ ] UI tests

---

**Kết luận:** Hệ thống đã có backend HOÀN CHỈNH cho cả 4 chức năng. Cần tập trung vào việc tạo Views (Razor) và JavaScript để hoàn thiện giao diện người dùng.
