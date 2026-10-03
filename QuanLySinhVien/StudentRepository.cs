using System;
using System.Collections.Generic;
using System.Linq;
using QuanLySinhVien.Entity;

namespace QuanLySinhVien
{
    public interface IStudentRepository
    {
        IList<LopHoc> GetClasses();
        IList<SinhVien> GetStudents();
        SinhVien FindStudent(string maSV);
        void AddStudent(SinhVien student);
        void UpdateStudent(SinhVien student);
        void DeleteStudent(string maSV);
    }

    public sealed class InMemoryStudentRepository : IStudentRepository
    {
        private readonly List<LopHoc> classes;
        private readonly List<SinhVien> students;

        public InMemoryStudentRepository(IEnumerable<LopHoc> classes = null, IEnumerable<SinhVien> students = null)
        {
            this.classes = classes == null
                ? new List<LopHoc>
                {
                    new LopHoc { MaLop = "LT1", TenLop = "Lập trình 1" },
                    new LopHoc { MaLop = "LT2", TenLop = "Lập trình 2" },
                    new LopHoc { MaLop = "LT3", TenLop = "Lập trình 3" },
                    new LopHoc { MaLop = "LT4", TenLop = "Lập trình 4" }
                }
                : classes.ToList();
            this.students = students == null ? new List<SinhVien>() : students.ToList();

            foreach (var lopHoc in this.classes)
            {
                if (lopHoc.SinhViens == null)
                {
                    lopHoc.SinhViens = new List<SinhVien>();
                }
            }

            foreach (var student in this.students)
            {
                AttachClass(student);
            }
        }

        public IList<LopHoc> GetClasses()
        {
            return classes.ToList();
        }

        public IList<SinhVien> GetStudents()
        {
            foreach (var student in students)
            {
                AttachClass(student);
            }

            return students.ToList();
        }

        public SinhVien FindStudent(string maSV)
        {
            return string.IsNullOrWhiteSpace(maSV)
                ? null
                : students.FirstOrDefault(student => string.Equals(student.MaSV, maSV.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public void AddStudent(SinhVien student)
        {
            EnsureStudentAndClass(student);
            if (FindStudent(student.MaSV) != null)
            {
                throw new InvalidOperationException("Mã sinh viên đã tồn tại.");
            }

            AttachClass(student);
            students.Add(student);
        }

        public void UpdateStudent(SinhVien student)
        {
            EnsureStudentAndClass(student);
            var index = students.FindIndex(existing => string.Equals(existing.MaSV, student.MaSV, StringComparison.OrdinalIgnoreCase));
            if (index < 0)
            {
                throw new InvalidOperationException("Không tìm thấy sinh viên cần cập nhật.");
            }

            var previousClass = students[index].LopHoc;
            if (previousClass != null && previousClass.SinhViens != null)
            {
                previousClass.SinhViens.Remove(students[index]);
            }

            AttachClass(student);
            students[index] = student;
        }

        public void DeleteStudent(string maSV)
        {
            var student = FindStudent(maSV);
            if (student == null)
            {
                throw new InvalidOperationException("Không tìm thấy sinh viên cần xóa.");
            }

            students.Remove(student);
            if (student.LopHoc != null && student.LopHoc.SinhViens != null)
            {
                student.LopHoc.SinhViens.Remove(student);
            }
        }

        private void EnsureStudentAndClass(SinhVien student)
        {
            if (student == null)
            {
                throw new ArgumentNullException("student");
            }

            if (classes.All(lopHoc => !string.Equals(lopHoc.MaLop, student.MaLop, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Lớp học đã chọn không tồn tại.");
            }
        }

        private void AttachClass(SinhVien student)
        {
            var lopHoc = classes.FirstOrDefault(item => string.Equals(item.MaLop, student.MaLop, StringComparison.OrdinalIgnoreCase));
            student.LopHoc = lopHoc;
            if (lopHoc != null && !lopHoc.SinhViens.Contains(student))
            {
                lopHoc.SinhViens.Add(student);
            }
        }
    }
}
