using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien.Data.Entity
{
    public class SinhVien
    {
        [Key]
        [Required(ErrorMessage = "M\u00e3 sinh vi\u00ean kh\u00f4ng \u0111\u01b0\u1ee3c \u0111\u1ec3 tr\u1ed1ng.")]
        [StringLength(20)]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "H\u1ecd v\u00e0 t\u00ean kh\u00f4ng \u0111\u01b0\u1ee3c \u0111\u1ec3 tr\u1ed1ng.")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Ng\u00e0y sinh kh\u00f4ng \u0111\u01b0\u1ee3c \u0111\u1ec3 tr\u1ed1ng.")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Vui l\u00f2ng ch\u1ecdn gi\u1edbi t\u00ednh.")]
        [StringLength(10)]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Email kh\u00f4ng \u0111\u01b0\u1ee3c \u0111\u1ec3 tr\u1ed1ng.")]
        [StringLength(254)]
        [EmailAddress(ErrorMessage = "Email kh\u00f4ng \u0111\u00fang \u0111\u1ecbnh d\u1ea1ng.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "\u0110i\u1ec7n tho\u1ea1i kh\u00f4ng \u0111\u01b0\u1ee3c \u0111\u1ec3 tr\u1ed1ng.")]
        [StringLength(20)]
        [Phone(ErrorMessage = "S\u1ed1 \u0111i\u1ec7n tho\u1ea1i kh\u00f4ng \u0111\u00fang \u0111\u1ecbnh d\u1ea1ng.")]
        public string DienThoai { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [Range(typeof(decimal), "0", "10", ErrorMessage = "\u0110i\u1ec3m ph\u1ea3i t\u1eeb 0 \u0111\u1ebfn 10.")]
        public decimal Diem { get; set; }

        [Required(ErrorMessage = "Vui l\u00f2ng ch\u1ecdn l\u1edbp h\u1ecdc.")]
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

        public IList<ValidationResult> IsInValid()
        {
            IList<ValidationResult> results;
            Validate(out results);
            return results;
        }
    }
}
