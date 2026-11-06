# BÀI THI GIỮA KỲ - LẬP TRÌNH NÂNG CAO C#
## Đề tài: Quản lý sách theo thể loại
### Kiến trúc nâng cao: Three-Layer Architecture, Generic Repository, Dependency Injection, Data Annotations & Regex

---

### 1. Các lý thuyết bài giảng đã áp dụng (Theo tài liệu thư mục `LTNCC#`)

Dự án đã được tái cấu trúc, cải tiến và kế thừa toàn diện dựa trên các bài giảng trong thư mục `D:\Năm 3 học kỳ 1\LapTrinhNangCao\GK\LTNCC#`:

#### 1.1. Nguyên lý Hướng đối tượng OOP & Tính Kế Thừa (Bài 2: `Bai_2_On_tap_OOP_Property_Data_Annotations.pptx`)
- **Kế thừa & Trừu tượng hóa Entity (Slide 13, 14, 15)**:
  - Tạo lớp cơ sở trừu tượng [`BaseEntity`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.Entity/BaseEntity.cs).
  - [`TheLoai`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.Entity/TheLoai.cs) và [`Sach`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.Entity/Sach.cs) kế thừa từ `BaseEntity`.
- **Generic Repository Pattern ở DAL (Slide 15, 16)**:
  - Định nghĩa interface [`IBaseDAL<TEntity, TKey>`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.DAL/IBaseDAL.cs).
  - Xây dựng lớp cơ sở [`BaseDAL<TEntity, TKey>`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.DAL/BaseDAL.cs) triển khai sẵn các thao tác CRUD cơ bản qua EF Core.
  - [`SachDAL`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.DAL/SachDAL.cs) và [`TheLoaiDAL`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.DAL/TheLoaiDAL.cs) kế thừa từ `BaseDAL`, override phương thức để thực hiện Eager Loading (`.Include()`).
- **Base Business Service ở BUS**:
  - Định nghĩa interface [`IBaseBUS<TEntity, TKey>`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.BUS/IBaseBUS.cs).
  - Xây dựng lớp cơ sở [`BaseBUS<TEntity, TKey>`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.BUS/BaseBUS.cs) quản lý ủy quyền nghiệp vụ và xác thực dữ liệu tập trung.
  - [`SachBUS`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.BUS/SachBUS.cs) và [`TheLoaiBUS`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.BUS/TheLoaiBUS.cs) kế thừa từ `BaseBUS`.

#### 1.2. Ràng buộc dữ liệu & Data Annotations (Bài 2: Slide 20, 24, 26, 27)
- **Tạo Validation Attribute tùy chỉnh (Slide 27)**:
  - Xây dựng thuộc tính [`[NotFutureDate]`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.Entity/NotFutureDateAttribute.cs) kế thừa từ `ValidationAttribute` để bảo đảm `NgayNhap <= DateTime.Today`.
- **Ràng buộc liên thuộc tính qua `IValidatableObject` (Slide 26)**:
  - Lớp `Sach` cài đặt `IValidatableObject` để kiểm tra chéo các điều kiện phức tạp (ngày nhập, số lượng, đơn giá).
- **Chủ động kiểm tra Model bằng `Validator` (Slide 24, 25)**:
  - Tầng `BaseBUS` gọi `Validator.TryValidateObject(...)` để tự động kiểm tra toàn bộ Data Annotations trước khi đẩy xuống cơ sở dữ liệu.

#### 1.3. Regular Expression & LINQ (Bài 3: `Bai_3_Lambda_LINQ_Regular_Expression_CSharp.pptx`)
- **Chuẩn hóa chuỗi bằng Regex (Slide 26)**:
  - Áp dụng `Regex.Replace(raw.Trim(), @"\s+", " ")` để làm sạch khoảng trắng thừa trong `MaSach`, `TenSach` trước khi kiểm tra mã trùng và lưu vào CSDL.
- **LINQ Method Syntax & DTO / ViewModel (Slide 13, 241)**:
  - Tạo lớp [`SachViewModel`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.Entity/SachViewModel.cs).
  - `SachBUS.GetAllViewModels()` sử dụng LINQ (`OrderBy`, `Select`) để ánh xạ sang ViewModel phục vụ hiển thị trên DataGridView, tách biệt hoàn toàn Entity với GUI.

#### 1.4. Mẫu thiết kế Dependency Injection (Slide: `3. Dependence Injection.pptx`)
- Ứng dụng gói `Microsoft.Extensions.DependencyInjection`.
- Đăng ký dịch vụ trong [`Program.cs`](file:///D:/Năm%203%20học%20kỳ%201/LapTrinhNangCao/GK/QuanLySach.GUI/Program.cs) qua `ServiceCollection` (`AddScoped`, `AddTransient`).
- Tiêm phụ thuộc qua Constructor (`Constructor Injection`) vào `Form1` và `SachBUS`.

#### 1.5. Trải nghiệm giao diện & ErrorProvider (Bài 2, Slide 25 & Bài 3, Slide 281)
- Form tích hợp `ErrorProvider` để hiển thị biểu tượng chấm than đỏ và tooltip thông báo lỗi trực quan ngay cạnh các ô nhập liệu bị sai.

---

### 2. Cấu trúc Solution hoàn chỉnh
```
D:\Năm 3 học kỳ 1\LapTrinhNangCao\GK\
├── QuanLySach.sln (và QuanLySach.slnx)
│
├── QuanLySach.Entity/
│   ├── BaseEntity.cs            (OOP: Lớp trừu tượng cơ sở)
│   ├── NotFutureDateAttribute.cs (Custom Validation Attribute kế thừa ValidationAttribute)
│   ├── TheLoai.cs               (Kế thừa BaseEntity, Data Annotations)
│   ├── Sach.cs                  (Kế thừa BaseEntity, cài đặt IValidatableObject)
│   └── SachViewModel.cs         (DTO/ViewModel cho Presentation)
│
├── QuanLySach.DAL/
│   ├── IBaseDAL.cs              (OOP: Generic Repository Interface)
│   ├── BaseDAL.cs               (OOP: Lớp trừu tượng triển khai CRUD qua EF Core)
│   ├── ITheLoaiDAL.cs           (Kế thừa IBaseDAL)
│   ├── TheLoaiDAL.cs            (Kế thừa BaseDAL, cài đặt ITheLoaiDAL)
│   ├── ISachDAL.cs              (Kế thừa IBaseDAL, mở rộng Eager Loading)
│   ├── SachDAL.cs               (Kế thừa BaseDAL, cài đặt ISachDAL)
│   ├── QLTVDBContext.cs         (Đọc chuỗi kết nối từ App.config, Code First Seed Data)
│   └── Migrations/              (InitialCreate migration)
│
├── QuanLySach.BUS/
│   ├── IBaseBUS.cs              (OOP: Generic Business Interface)
│   ├── BaseBUS.cs               (OOP: Lớp nghiệp vụ cơ sở, tích hợp Validator)
│   ├── ITheLoaiBUS.cs           (Kế thừa IBaseBUS)
│   ├── TheLoaiBUS.cs            (Kế thừa BaseBUS, cài đặt ITheLoaiBUS)
│   ├── ISachBUS.cs              (Kế thừa IBaseBUS, mở rộng GetAllViewModels)
│   └── SachBUS.cs               (Kế thừa BaseBUS, chuẩn hóa Regex, kiểm tra nghiệp vụ)
│
└── QuanLySach.GUI/
    ├── App.config               (Lưu chuỗi kết nối MySQL)
    ├── Program.cs               (Cấu hình Dependency Injection Container)
    ├── Form1.cs                 (Constructor Injection, ErrorProvider, xử lý phím tắt & sự kiện)
    └── Form1.Designer.cs        (Bố cục Form chuẩn, DataGridView, Controls)
```
