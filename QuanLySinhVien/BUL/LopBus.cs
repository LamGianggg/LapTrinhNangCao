using System.Collections.Generic;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    public class LopBus
    {
        private readonly LopDAL lopDAL;
        public LopBus() { lopDAL = new LopDAL(); }

        public List<LopHoc> GetAllLopHoc() { return lopDAL.GetAllLopHoc(); }
        public LopHoc GetLopById(string maLop) { return lopDAL.GetLopById(maLop); }
    }
}
