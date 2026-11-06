using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using QuanLySach.DAL;
using QuanLySach.Entity;

namespace QuanLySach.BUS
{
    /// <summary>
    /// Triển khai nghiệp vụ cho Sách.
    /// Kế thừa từ BaseBUS (OOP Inheritance & Code Reuse) và cài đặt ISachBUS.
    /// Ứng dụng:
    /// - Data Annotations & Validator (Bài 2, Slide 24)
    /// - Regular Expression để chuẩn hóa và kiểm tra chuỗi (Bài 3, Slide 24, 26)
    /// - LINQ Method Syntax để xử lý danh sách và ánh xạ ViewModel (Bài 3, Slide 11, 13)
    /// - Dependency Injection qua constructor (Slide DI)
    /// </summary>
    public class SachBUS : BaseBUS<Sach, string>, ISachBUS
    {
        private readonly ISachDAL _sachDAL;
        private readonly ITheLoaiDAL _theLoaiDAL;

        public SachBUS() : this(new SachDAL(), new TheLoaiDAL())
        {
        }

        public SachBUS(ISachDAL sachDAL, ITheLoaiDAL theLoaiDAL) : base(sachDAL)
        {
            _sachDAL = sachDAL ?? throw new ArgumentNullException(nameof(sachDAL));
            _theLoaiDAL = theLoaiDAL ?? throw new ArgumentNullException(nameof(theLoaiDAL));
        }

        /// <summary>
        /// Sử dụng LINQ Method Syntax để chuyển đổi (mapping) sang Danh sách ViewModel (Bài 3, Slide 13).
        /// </summary>
        public List<SachViewModel> GetAllViewModels()
        {
            var dsSach = _sachDAL.GetAll();

            return dsSach
                .OrderBy(s => s.MaSach)
                .Select(s => new SachViewModel
                {
                    MaSach = s.MaSach,
                    TenSach = s.TenSach,
                    MaLoai = s.MaLoai,
                    TenLoai = s.TheLoai != null ? s.TheLoai.TenLoai : string.Empty,
                    NgayNhap = s.NgayNhap,
                    SoLuong = s.SoLuong,
                    DonGia = s.DonGia
                })
                .ToList();
        }

        /// <summary>
        /// Kiểm tra quy tắc nghiệp vụ theo Câu 4c đề thi và lý thuyết Bài 2 & Bài 3:
        /// 1. Chuẩn hóa khoảng trắng thừa bằng Regex.Replace (Bài 3, Slide 26)
        /// 2. Kiểm tra tính hợp lệ bằng Data Annotations qua Validator (Bài 2, Slide 24)
        /// 3. Kiểm tra các quy tắc nghiệp vụ phức tạp (Mã trùng, thể loại tồn tại...)
        /// </summary>
        public void ValidateBusinessRules(Sach sach, bool isAdding)
        {
            if (sach == null)
            {
                throw new ArgumentNullException(nameof(sach), "Thông tin sách không được để trống!");
            }

            // 1. Chuẩn hóa chuỗi bằng Regex theo Bài 3 (Slide 26: Regex.Replace(raw.Trim(), @"\s+", " "))
            sach.MaSach = NormalizeString(sach.MaSach);
            sach.TenSach = NormalizeString(sach.TenSach);
            sach.MaLoai = NormalizeString(sach.MaLoai);

            // 2. Kích hoạt kiểm tra Data Annotations & IValidatableObject của thực thể qua lớp cha BaseBUS
            ValidateModel(sach);

            // 3. Kiểm tra thể loại có tồn tại trong hệ thống hay không (Quy tắc nghiệp vụ liên cơ sở dữ liệu)
            if (!_theLoaiDAL.Exists(sach.MaLoai))
            {
                throw new InvalidOperationException($"Thể loại có mã '{sach.MaLoai}' không tồn tại trong hệ thống!");
            }

            // 4. Kiểm tra mã trùng khi thêm mới (Bài 2, Slide 28: Business Rule không nên đặt ở Annotation)
            if (isAdding)
            {
                if (_sachDAL.Exists(sach.MaSach))
                {
                    throw new InvalidOperationException($"Mã sách '{sach.MaSach}' đã tồn tại trong cơ sở dữ liệu! Vui lòng nhập mã khác.");
                }
            }
            else
            {
                // Khi sửa, giữ nguyên khóa chính và sách phải tồn tại
                if (!_sachDAL.Exists(sach.MaSach))
                {
                    throw new InvalidOperationException($"Không tìm thấy cuốn sách có mã '{sach.MaSach}' để cập nhật!");
                }
            }
        }

        public override void Add(Sach entity)
        {
            ValidateBusinessRules(entity, isAdding: true);
            _sachDAL.Add(entity);
        }

        public override void Update(Sach entity)
        {
            ValidateBusinessRules(entity, isAdding: false);
            _sachDAL.Update(entity);
        }

        public override void Delete(string id)
        {
            string maSach = NormalizeString(id);
            if (string.IsNullOrWhiteSpace(maSach))
            {
                throw new ArgumentException("Mã sách cần xóa không được để trống!");
            }

            if (!_sachDAL.Exists(maSach))
            {
                throw new InvalidOperationException($"Không tìm thấy sách có mã '{maSach}' để xóa!");
            }

            _sachDAL.Delete(maSach);
        }

        /// <summary>
        /// Hàm tiện ích chuẩn hóa chuỗi bằng Regex theo Bài 3 (Slide 26):
        /// Thay thế nhiều khoảng trắng liên tiếp bằng 1 khoảng trắng duy nhất và bỏ khoảng trắng đầu/cuối.
        /// </summary>
        private static string NormalizeString(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return Regex.Replace(input.Trim(), @"\s+", " ");
        }
    }
}
