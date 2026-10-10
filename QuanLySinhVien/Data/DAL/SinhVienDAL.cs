using System;
using System.Collections.Generic;
using System.Linq;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class SinhVienDAL
    {
        private static List<SinhVien> dataList = new List<SinhVien>
        {
            new SinhVien { MaSV="SV001", HoTen="Nguy\u1ec5n V\u0103n An", NgaySinh=new DateTime(2004,8,15), GioiTinh="Nam", Email="an.nv@vju.ac.vn", DienThoai="0912345678", Diem=8.5m, MaLop="CSE0001", TrangThai="\u0110ang h\u1ecdc" },
            new SinhVien { MaSV="SV002", HoTen="Tr\u1ea7n Minh Anh", NgaySinh=new DateTime(2004,1,22), GioiTinh="N\u1eef", Email="anh.tm@vju.ac.vn", DienThoai="0987654321", Diem=9.0m, MaLop="CSE0002", TrangThai="\u0110ang h\u1ecdc" },
            new SinhVien { MaSV="SV003", HoTen="L\u00ea Ho\u00e0ng B\u00ecnh", NgaySinh=new DateTime(2004,5,9), GioiTinh="Nam", Email="binh.lh@vju.ac.vn", DienThoai="0355556677", Diem=7.4m, MaLop="CSE0001", TrangThai="\u0110ang h\u1ecdc" },
            new SinhVien { MaSV="SV004", HoTen="\u0110\u1ed7 Th\u1ecb H\u1ed3ng", NgaySinh=new DateTime(2004,11,30), GioiTinh="N\u1eef", Email="hong.dt@vju.ac.vn", DienThoai="0777888999", Diem=8.1m, MaLop="CSE0003", TrangThai="\u0110ang h\u1ecdc" }
        };

        public List<SinhVien> GetAllSinhVien() { return dataList.ToList(); }

        public SinhVien GetSinhVienById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return null;
            return dataList.FirstOrDefault(sv => string.Equals(sv.MaSV, maSV.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public List<SinhVien> GetSinhVienByMaLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return GetAllSinhVien();
            return dataList.Where(sv => string.Equals(sv.MaLop, maLop.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void AddSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException("sv");
            dataList.Add(sv);
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException("sv");
            var index = dataList.FindIndex(s => string.Equals(s.MaSV, sv.MaSV, StringComparison.OrdinalIgnoreCase));
            if (index >= 0) dataList[index] = sv;
        }

        public void DeleteSinhVien(string maSV)
        {
            var sv = GetSinhVienById(maSV);
            if (sv != null) dataList.Remove(sv);
        }
    }
}
