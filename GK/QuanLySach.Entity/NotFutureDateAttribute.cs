using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLySach.Entity
{
    /// <summary>
    /// Custom Validation Attribute theo đúng lý thuyết Bài 2 (Slide 27):
    /// "Kế thừa ValidationAttribute khi một quy tắc được tái sử dụng ở nhiều model."
    /// Ràng buộc: Ngày không được lớn hơn ngày hiện tại.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NotFutureDateAttribute : ValidationAttribute
    {
        public NotFutureDateAttribute() : base("Ngày không được ở tương lai!")
        {
        }

        public override bool IsValid(object? value)
        {
            if (value is null) return true; // Giá trị null sẽ do [Required] đảm nhiệm nếu có
            return value is DateTime date && date.Date <= DateTime.Today;
        }
    }
}
