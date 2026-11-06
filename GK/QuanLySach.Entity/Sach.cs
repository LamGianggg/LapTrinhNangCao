using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.Entity
{
    /// <summary>
    /// Thực thể Sách. Kế thừa BaseEntity và cài đặt IValidatableObject (Lý thuyết Bài 2, Slide 26).
    /// </summary>
    [Table("Sach")]
    public class Sach : BaseEntity, IValidatableObject
    {
        [Key]
        [StringLength(20, ErrorMessage = "Mã sách tối đa 20 ký tự!")]
        [Required(ErrorMessage = "Mã sách không được để trống!")]
        public string MaSach { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sách không được để trống!")]
        [StringLength(150, ErrorMessage = "Tên sách tối đa 150 ký tự!")]
        public string TenSach { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày nhập không được để trống!")]
        [NotFutureDate(ErrorMessage = "Ngày nhập không được lớn hơn ngày hiện tại!")]
        public DateTime NgayNhap { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống!")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải là số nguyên lớn hơn 0!")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Đơn giá không được để trống!")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Đơn giá phải là số lớn hơn 0!")]
        public decimal DonGia { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thể loại cho sách!")]
        [StringLength(10, ErrorMessage = "Mã thể loại tối đa 10 ký tự!")]
        public string MaLoai { get; set; } = string.Empty;

        // Navigation property (Quan hệ N - 1 với TheLoai)
        [ForeignKey(nameof(MaLoai))]
        public virtual TheLoai? TheLoai { get; set; }

        /// <summary>
        /// Cài đặt IValidatableObject theo lý thuyết Bài 2 (Slide 26) để kiểm tra các ràng buộc liên thuộc tính.
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayNhap.Date > DateTime.Today)
            {
                yield return new ValidationResult(
                    "Ngày nhập không được lớn hơn ngày hiện tại!",
                    new[] { nameof(NgayNhap) }
                );
            }

            if (SoLuong <= 0)
            {
                yield return new ValidationResult(
                    "Số lượng phải là số nguyên lớn hơn 0!",
                    new[] { nameof(SoLuong) }
                );
            }

            if (DonGia <= 0)
            {
                yield return new ValidationResult(
                    "Đơn giá phải là số lớn hơn 0!",
                    new[] { nameof(DonGia) }
                );
            }
        }
    }
}
