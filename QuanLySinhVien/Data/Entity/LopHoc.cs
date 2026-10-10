using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Data.Entity
{
    public class LopHoc
    {
        public LopHoc()
        {
            SinhViens = new List<SinhVien>();
        }

        [Key]
        [Required(ErrorMessage = "M\u00e3 l\u1edbp kh\u00f4ng \u0111\u01b0\u1ee3c \u0111\u1ec3 tr\u1ed1ng.")]
        [StringLength(20)]
        public string MaLop { get; set; }

        [Required(ErrorMessage = "T\u00ean l\u1edbp kh\u00f4ng \u0111\u01b0\u1ee3c \u0111\u1ec3 tr\u1ed1ng.")]
        [StringLength(100)]
        public string TenLop { get; set; }

        public virtual ICollection<SinhVien> SinhViens { get; set; }

        public bool Validate(out IList<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
        }
    }
}
