using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Entity
{
    public class LopHoc
    {
        public LopHoc()
        {
            SinhViens = new List<SinhVien>();
        }
        [Key]
        [Required(ErrorMessage = "Mã lớp không được để trống.")]
        [StringLength(20)]
        public string MaLop { get; set; }

        [Required(ErrorMessage = "Tên lớp không được để trống.")]
        [StringLength(100)]
        public string TenLop { get; set; }

        public virtual ICollection<SinhVien> SinhViens { get; set; }

        public bool IsValid(out IList<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
        }
    
        public bool Validate(out IList<ValidationResult> validationResults)
        {
            return IsValid(out validationResults);
        }
}
}
