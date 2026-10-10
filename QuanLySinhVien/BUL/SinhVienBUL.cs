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

        private void AttachClass(SinhVien sv)
        {
            if (sv != null && !string.IsNullOrEmpty(sv.MaLop))
                sv.LopHoc = lopDAL.GetLopById(sv.MaLop);
        }

        public List<SinhVien> GetAllSinhVien()
        {
            var list = svDAL.GetAllSinhVien();
            foreach (var sv in list) AttachClass(sv);
            return list;
        }

        public SinhVien GetSinhVienById(string maSV)
        {
            var sv = svDAL.GetSinhVienById(maSV);
            AttachClass(sv);
            return sv;
        }

        public List<SinhVien> GetSinhVienByMaLop(string maLop)
        {
            var list = svDAL.GetSinhVienByMaLop(maLop);
            foreach (var sv in list) AttachClass(sv);
            return list;
        }

        public void AddSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException("sv");
            if (svDAL.GetSinhVienById(sv.MaSV) != null)
                throw new Exception("M\u00e3 sinh vi\u00ean \u0111\u00e3 t\u1ed3n t\u1ea1i.");
            if (lopDAL.GetLopById(sv.MaLop) == null)
                throw new Exception("L\u1edbp h\u1ecdc kh\u00f4ng t\u1ed3n t\u1ea1i.");
            svDAL.AddSinhVien(sv);
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException("sv");
            if (svDAL.GetSinhVienById(sv.MaSV) == null)
                throw new Exception("Kh\u00f4ng t\u00ecm th\u1ea5y sinh vi\u00ean.");
            if (lopDAL.GetLopById(sv.MaLop) == null)
                throw new Exception("L\u1edbp h\u1ecdc kh\u00f4ng t\u1ed3n t\u1ea1i.");
            svDAL.UpdateSinhVien(sv);
        }

        public void DeleteSinhVien(string maSV)
        {
            if (svDAL.GetSinhVienById(maSV) == null)
                throw new Exception("Kh\u00f4ng t\u00ecm th\u1ea5y sinh vi\u00ean.");
            svDAL.DeleteSinhVien(maSV);
        }

        public List<SinhVien> FilterStudents(string keyword, string maLop, decimal minDiem)
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
    }
}
