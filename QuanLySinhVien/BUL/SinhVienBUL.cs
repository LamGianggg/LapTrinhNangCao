using System;
using System.Collections.Generic;
using System.Linq;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    public class SinhVienBUL
    {
        private readonly SinhVienDAL svDAL;
        private readonly LopDAL lopDAL;

        public SinhVienBUL()
        {
            svDAL = new SinhVienDAL();
            lopDAL = new LopDAL();
        }

        public SinhVienBUL(SinhVienDAL svDal, LopDAL lDal)
        {
            svDAL = svDal ?? new SinhVienDAL();
            lopDAL = lDal ?? new LopDAL();
        }

        private void AttachClass(SinhVien sv)
        {
            if (sv != null && !string.IsNullOrEmpty(sv.MaLop))
            {
                try
                {
                    sv.LopHoc = lopDAL.GetLopById(sv.MaLop);
                }
                catch
                {
                    // Giữ lại MaLop nếu không tìm thấy chi tiết lớp
                }
            }
        }

        public List<SinhVien> GetAllSinhVien()
        {
            try
            {
                List<SinhVien> list = svDAL.GetAllSinhVien();
                foreach (var sv in list)
                {
                    AttachClass(sv);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách sinh viên: " + ex.Message, ex);
            }
        }

        public SinhVien GetSinhVienById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return null;
            try
            {
                SinhVien sv = svDAL.GetSinhVienById(maSV);
                AttachClass(sv);
                return sv;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi tìm sinh viên: " + ex.Message, ex);
            }
        }

        public List<SinhVien> GetSinhVienByMaLop(string maLop)
        {
            try
            {
                List<SinhVien> list = svDAL.GetSinhVienByMaLop(maLop);
                foreach (var sv in list)
                {
                    AttachClass(sv);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy sinh viên theo lớp: " + ex.Message, ex);
            }
        }

        public void AddSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException("sv", "Thông tin sinh viên không được để trống.");
            if (string.IsNullOrWhiteSpace(sv.MaSV)) throw new Exception("Mã sinh viên không được để trống.");

            if (svDAL.GetSinhVienById(sv.MaSV) != null)
                throw new Exception("Mã sinh viên đã tồn tại.");

            if (!string.IsNullOrWhiteSpace(sv.MaLop) && lopDAL.GetLopById(sv.MaLop) == null)
                throw new Exception("Lớp học không tồn tại.");

            try
            {
                svDAL.AddSinhVien(sv);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi thêm sinh viên: " + ex.Message, ex);
            }
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException("sv", "Thông tin sinh viên không được để trống.");
            if (string.IsNullOrWhiteSpace(sv.MaSV)) throw new Exception("Mã sinh viên không được để trống.");

            if (svDAL.GetSinhVienById(sv.MaSV) == null)
                throw new Exception("Không tìm thấy sinh viên để cập nhật.");

            if (!string.IsNullOrWhiteSpace(sv.MaLop) && lopDAL.GetLopById(sv.MaLop) == null)
                throw new Exception("Lớp học không tồn tại.");

            try
            {
                svDAL.UpdateSinhVien(sv);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi cập nhật sinh viên: " + ex.Message, ex);
            }
        }

        public void DeleteSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new Exception("Mã sinh viên không được để trống.");

            if (svDAL.GetSinhVienById(maSV) == null)
                throw new Exception("Không tìm thấy sinh viên để xóa.");

            try
            {
                svDAL.DeleteSinhVien(maSV);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi xóa sinh viên: " + ex.Message, ex);
            }
        }

        public List<SinhVien> FilterStudents(string keyword, string maLop, decimal minDiem)
        {
            try
            {
                var list = GetAllSinhVien().AsEnumerable();
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var kw = keyword.Trim();
                    list = list.Where(sv =>
                        (sv.MaSV != null && sv.MaSV.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (sv.HoTen != null && sv.HoTen.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (sv.Email != null && sv.Email.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (sv.DienThoai != null && sv.DienThoai.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0));
                }
                if (!string.IsNullOrWhiteSpace(maLop))
                    list = list.Where(sv => string.Equals(sv.MaLop, maLop.Trim(), StringComparison.OrdinalIgnoreCase));
                if (minDiem > 0)
                    list = list.Where(sv => sv.Diem >= minDiem);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lọc danh sách sinh viên: " + ex.Message, ex);
            }
        }
    }
}
