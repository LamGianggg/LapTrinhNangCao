using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien.Entity
{
    public class SinhVien
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
        [StringLength(20, ErrorMessage = "Mã sinh viên không được vượt quá 20 ký tự.")]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [StringLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự.")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống.")]
        public System.DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
        [StringLength(10)]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [StringLength(254, ErrorMessage = "Email không được vượt quá 254 ký tự.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Điện thoại không được để trống.")]
        [StringLength(20, ErrorMessage = "Điện thoại không được vượt quá 20 ký tự.")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng.")]
        public string DienThoai { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [Range(typeof(decimal), "0", "10", ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10.")]
        public decimal Diem { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn lớp học.")]
        [StringLength(20)]
        public string MaLop { get; set; }

        [ForeignKey("MaLop")]
        public virtual LopHoc LopHoc { get; set; }

        public bool Validate(out IList<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
        }

        public bool IsValid(out IList<ValidationResult> validationResults)
        {
            return Validate(out validationResults);
        }
    }
}
