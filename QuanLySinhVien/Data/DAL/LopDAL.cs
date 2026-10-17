using System;
using System.Collections.Generic;
using System.Data;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class LopDAL
    {
        protected readonly DataHelper dataHelper;

        public LopDAL()
        {
            dataHelper = new DataHelper();
        }

        public LopDAL(DataHelper helper)
        {
            dataHelper = helper ?? new DataHelper();
        }

        public virtual List<LopHoc> GetAllLopHoc()
        {
            try
            {
                DataTable dt = dataHelper.GetDataTable("SELECT MaLop, TenLop FROM LopHoc ORDER BY MaLop");
                List<LopHoc> list = new List<LopHoc>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapLopHoc(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi lấy danh sách lớp học: " + ex.Message, ex);
            }
        }

        public virtual LopHoc GetLopById(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return null;
            try
            {
                var pMaLop = dataHelper.CreateParameter("@MaLop", maLop.Trim());
                DataTable dt = dataHelper.GetDataTable("SELECT MaLop, TenLop FROM LopHoc WHERE MaLop = @MaLop", pMaLop);
                if (dt.Rows.Count == 0) return null;
                return MapLopHoc(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi tìm lớp học theo mã: " + ex.Message, ex);
            }
        }

        public virtual void AddLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException("lh", "Thông tin lớp học không được để trống.");
            try
            {
                var pMaLop = dataHelper.CreateParameter("@MaLop", lh.MaLop != null ? lh.MaLop.Trim() : (object)DBNull.Value);
                var pTenLop = dataHelper.CreateParameter("@TenLop", lh.TenLop != null ? lh.TenLop.Trim() : (object)DBNull.Value);
                dataHelper.ExecuteNonQuery("INSERT INTO LopHoc (MaLop, TenLop) VALUES (@MaLop, @TenLop)", pMaLop, pTenLop);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi thêm lớp học: " + ex.Message, ex);
            }
        }

        public virtual void UpdateLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException("lh", "Thông tin lớp học không được để trống.");
            try
            {
                var pMaLop = dataHelper.CreateParameter("@MaLop", lh.MaLop != null ? lh.MaLop.Trim() : (object)DBNull.Value);
                var pTenLop = dataHelper.CreateParameter("@TenLop", lh.TenLop != null ? lh.TenLop.Trim() : (object)DBNull.Value);
                dataHelper.ExecuteNonQuery("UPDATE LopHoc SET TenLop = @TenLop WHERE MaLop = @MaLop", pTenLop, pMaLop);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi cập nhật lớp học: " + ex.Message, ex);
            }
        }

        public virtual void DeleteLopHoc(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new ArgumentException("Mã lớp không được để trống.", "maLop");
            try
            {
                var pMaLop = dataHelper.CreateParameter("@MaLop", maLop.Trim());
                dataHelper.ExecuteNonQuery("DELETE FROM LopHoc WHERE MaLop = @MaLop", pMaLop);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi xóa lớp học: " + ex.Message, ex);
            }
        }

        // Aliases for DAO naming
        public virtual List<LopHoc> GetAll() { return GetAllLopHoc(); }
        public virtual LopHoc GetById(string maLop) { return GetLopById(maLop); }
        public virtual void Insert(LopHoc lh) { AddLopHoc(lh); }
        public virtual void Update(LopHoc lh) { UpdateLopHoc(lh); }
        public virtual void Delete(string maLop) { DeleteLopHoc(maLop); }

        private static LopHoc MapLopHoc(DataRow row)
        {
            return new LopHoc
            {
                MaLop = row["MaLop"] != null ? row["MaLop"].ToString() : "",
                TenLop = row["TenLop"] != null ? row["TenLop"].ToString() : ""
            };
        }
    }

    public class LopHocDAO : LopDAL
    {
        public LopHocDAO() : base() { }
        public LopHocDAO(DataHelper helper) : base(helper) { }
    }
}
