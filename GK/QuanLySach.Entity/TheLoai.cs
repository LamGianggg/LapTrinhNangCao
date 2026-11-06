using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.Entity
{
    /// <summary>
    /// Thực thể Thể loại sách. Kế thừa từ BaseEntity (Nguyên lý kế thừa OOP).
    /// </summary>
    [Table("TheLoai")]
    public class TheLoai : BaseEntity
    {
        [Key]
        [StringLength(10, ErrorMessage = "Mã thể loại tối đa 10 ký tự!")]
        [Required(ErrorMessage = "Mã thể loại không được để trống!")]
        public string MaLoai { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên thể loại không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên thể loại tối đa 100 ký tự!")]
        public string TenLoai { get; set; } = string.Empty;

        // Navigation property (Quan hệ 1 - N với Sach)
        public virtual ICollection<Sach> Sachs { get; set; } = new List<Sach>();
    }
}
