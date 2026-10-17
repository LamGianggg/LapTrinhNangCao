using System;
using System.Collections.Generic;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    public class LopBus
    {
        private readonly LopDAL lopDAL;

        public LopBus()
        {
            lopDAL = new LopDAL();
        }

        public LopBus(LopDAL dal)
        {
            lopDAL = dal ?? new LopDAL();
        }

        public List<LopHoc> GetAllLopHoc()
        {
            try
            {
                return lopDAL.GetAllLopHoc();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ lấy danh sách lớp học: " + ex.Message, ex);
            }
        }

        public LopHoc GetLopById(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return null;
            try
            {
                return lopDAL.GetLopById(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ tìm lớp học theo mã: " + ex.Message, ex);
            }
        }

        public void AddLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException("lh", "Thông tin lớp học không được để trống.");
            if (string.IsNullOrWhiteSpace(lh.MaLop)) throw new Exception("Mã lớp không được để trống.");
            if (string.IsNullOrWhiteSpace(lh.TenLop)) throw new Exception("Tên lớp không được để trống.");

            if (lopDAL.GetLopById(lh.MaLop) != null)
                throw new Exception("Mã lớp học đã tồn tại.");

            try
            {
                lopDAL.AddLopHoc(lh);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ thêm lớp học: " + ex.Message, ex);
            }
        }

        public void UpdateLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException("lh", "Thông tin lớp học không được để trống.");
            if (string.IsNullOrWhiteSpace(lh.MaLop)) throw new Exception("Mã lớp không được để trống.");

            if (lopDAL.GetLopById(lh.MaLop) == null)
                throw new Exception("Không tìm thấy lớp học để cập nhật.");

            try
            {
                lopDAL.UpdateLopHoc(lh);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ cập nhật lớp học: " + ex.Message, ex);
            }
        }

        public void DeleteLopHoc(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new Exception("Mã lớp không được để trống.");

            if (lopDAL.GetLopById(maLop) == null)
                throw new Exception("Không tìm thấy lớp học để xóa.");

            try
            {
                lopDAL.DeleteLopHoc(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ xóa lớp học: " + ex.Message, ex);
            }
        }
    }
}
