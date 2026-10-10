using System;
using System.Collections.Generic;
using System.Linq;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class LopDAL
    {
        private static List<LopHoc> lopHocs = new List<LopHoc>
        {
            new LopHoc { MaLop = "CSE0001", TenLop = "Khoa h\u1ecdc m\u00e1y t\u00ednh 01" },
            new LopHoc { MaLop = "CSE0002", TenLop = "Khoa h\u1ecdc m\u00e1y t\u00ednh 02" },
            new LopHoc { MaLop = "CSE0003", TenLop = "Khoa h\u1ecdc m\u00e1y t\u00ednh 03" },
            new LopHoc { MaLop = "KTPM01",  TenLop = "K\u1ef9 thu\u1eadt ph\u1ea7n m\u1ec1m 01" }
        };

        public List<LopHoc> GetAllLopHoc()
        {
            return lopHocs.ToList();
        }

        public LopHoc GetLopById(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return null;
            return lopHocs.FirstOrDefault(l => string.Equals(l.MaLop, maLop.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public void AddLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException("lh");
            lopHocs.Add(lh);
        }

        public void UpdateLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException("lh");
            var index = lopHocs.FindIndex(l => string.Equals(l.MaLop, lh.MaLop, StringComparison.OrdinalIgnoreCase));
            if (index >= 0) lopHocs[index] = lh;
        }

        public void DeleteLopHoc(string maLop)
        {
            var lop = GetLopById(maLop);
            if (lop != null) lopHocs.Remove(lop);
        }
    }
}
