using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class SinhVienDAL
    {
        protected readonly DataHelper dataHelper;

        public SinhVienDAL()
        {
            dataHelper = new DataHelper();
        }

        public SinhVienDAL(DataHelper helper)
        {
            dataHelper = helper ?? new DataHelper();
        }

        // Đọc dữ liệu bằng DataReader (Connected Architecture)
        public virtual List<SinhVien> getAllSinhVien1()
        {
            List<SinhVien> list = new List<SinhVien>();
            try
            {
                using (DbDataReader dr = dataHelper.ExcuteReader("SELECT * FROM SinhVien ORDER BY MaSV"))
                {
                    while (dr.Read())
                    {
                        list.Add(MapSinhVien(dr));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi đọc danh sách sinh viên bằng DataReader: " + ex.Message, ex);
            }
            finally
            {
                dataHelper.CloseConnection();
            }
        }

        public virtual List<SinhVien> GetAllSinhVien1()
        {
            return getAllSinhVien1();
        }

        // Đọc dữ liệu bằng DataAdapter & DataTable (Disconnected Architecture)
        public virtual List<SinhVien> GetAllSinhVien()
        {
            try
            {
                DataTable dt = dataHelper.GetDataTable("SELECT * FROM SinhVien ORDER BY MaSV");
                List<SinhVien> list = new List<SinhVien>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(MapSinhVien(dr));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi lấy danh sách tất cả sinh viên: " + ex.Message, ex);
            }
        }

        public virtual SinhVien GetSinhVienById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return null;
            try
            {
                var p = dataHelper.CreateParameter("@MaSV", maSV.Trim());
                DataTable dt = dataHelper.GetDataTable("SELECT * FROM SinhVien WHERE MaSV = @MaSV", p);
                if (dt.Rows.Count == 0) return null;
                return MapSinhVien(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi tìm sinh viên theo mã: " + ex.Message, ex);
            }
        }

        public virtual SinhVien GetSinhVienByMaSV(string maSV)
        {
            return GetSinhVienById(maSV);
        }

        public virtual List<SinhVien> GetSinhViensByMaLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return GetAllSinhVien();
            try
            {
                var p = dataHelper.CreateParameter("@MaLop", maLop.Trim());
                DataTable dt = dataHelper.GetDataTable("SELECT * FROM SinhVien WHERE MaLop = @MaLop ORDER BY MaSV", p);
                List<SinhVien> list = new List<SinhVien>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(MapSinhVien(dr));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi lấy danh sách sinh viên theo lớp: " + ex.Message, ex);
            }
        }

        public virtual List<SinhVien> GetSinhVienByMaLop(string maLop)
        {
            return GetSinhViensByMaLop(maLop);
        }

        public virtual void AddSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException("sv", "Thông tin sinh viên không được để trống.");
            try
            {
                string sql = "INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, DienThoai, TrangThai, Diem, MaLop) " +
                             "VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @DienThoai, @TrangThai, @Diem, @MaLop)";

                var pMaSV = dataHelper.CreateParameter("@MaSV", sv.MaSV != null ? sv.MaSV.Trim() : (object)DBNull.Value);
                var pHoTen = dataHelper.CreateParameter("@HoTen", sv.HoTen != null ? sv.HoTen.Trim() : (object)DBNull.Value);
                var pNgaySinh = dataHelper.CreateParameter("@NgaySinh", sv.NgaySinh.ToString("yyyy-MM-dd"));
                var pGioiTinh = dataHelper.CreateParameter("@GioiTinh", sv.GioiTinh != null ? sv.GioiTinh.Trim() : (object)DBNull.Value);
                var pEmail = dataHelper.CreateParameter("@Email", sv.Email != null ? sv.Email.Trim() : (object)DBNull.Value);
                var pDienThoai = dataHelper.CreateParameter("@DienThoai", sv.DienThoai != null ? sv.DienThoai.Trim() : (object)DBNull.Value);
                var pTrangThai = dataHelper.CreateParameter("@TrangThai", string.IsNullOrWhiteSpace(sv.TrangThai) ? "Đang học" : sv.TrangThai.Trim());
                var pDiem = dataHelper.CreateParameter("@Diem", sv.Diem);
                var pMaLop = dataHelper.CreateParameter("@MaLop", sv.MaLop != null ? sv.MaLop.Trim() : (object)DBNull.Value);

                dataHelper.ExecuteNonQuery(sql, pMaSV, pHoTen, pNgaySinh, pGioiTinh, pEmail, pDienThoai, pTrangThai, pDiem, pMaLop);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi thêm sinh viên: " + ex.Message, ex);
            }
        }

        public virtual void UpdateSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException("sv", "Thông tin sinh viên không được để trống.");
            try
            {
                string sql = "UPDATE SinhVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, " +
                             "Email=@Email, DienThoai=@DienThoai, TrangThai=@TrangThai, Diem=@Diem, MaLop=@MaLop " +
                             "WHERE MaSV=@MaSV";

                var pMaSV = dataHelper.CreateParameter("@MaSV", sv.MaSV != null ? sv.MaSV.Trim() : (object)DBNull.Value);
                var pHoTen = dataHelper.CreateParameter("@HoTen", sv.HoTen != null ? sv.HoTen.Trim() : (object)DBNull.Value);
                var pNgaySinh = dataHelper.CreateParameter("@NgaySinh", sv.NgaySinh.ToString("yyyy-MM-dd"));
                var pGioiTinh = dataHelper.CreateParameter("@GioiTinh", sv.GioiTinh != null ? sv.GioiTinh.Trim() : (object)DBNull.Value);
                var pEmail = dataHelper.CreateParameter("@Email", sv.Email != null ? sv.Email.Trim() : (object)DBNull.Value);
                var pDienThoai = dataHelper.CreateParameter("@DienThoai", sv.DienThoai != null ? sv.DienThoai.Trim() : (object)DBNull.Value);
                var pTrangThai = dataHelper.CreateParameter("@TrangThai", string.IsNullOrWhiteSpace(sv.TrangThai) ? "Đang học" : sv.TrangThai.Trim());
                var pDiem = dataHelper.CreateParameter("@Diem", sv.Diem);
                var pMaLop = dataHelper.CreateParameter("@MaLop", sv.MaLop != null ? sv.MaLop.Trim() : (object)DBNull.Value);

                dataHelper.ExecuteNonQuery(sql, pHoTen, pNgaySinh, pGioiTinh, pEmail, pDienThoai, pTrangThai, pDiem, pMaLop, pMaSV);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi cập nhật sinh viên: " + ex.Message, ex);
            }
        }

        public virtual void DeleteSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new ArgumentException("Mã sinh viên không được để trống.", "maSV");
            try
            {
                string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                var p = dataHelper.CreateParameter("@MaSV", maSV.Trim());
                dataHelper.ExecuteNonQuery(sql, p);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DataAccess Layer khi xóa sinh viên: " + ex.Message, ex);
            }
        }

        // Aliases for DAO naming
        public virtual List<SinhVien> GetAll() { return GetAllSinhVien(); }
        public virtual SinhVien GetById(string maSV) { return GetSinhVienById(maSV); }
        public virtual void Insert(SinhVien sv) { AddSinhVien(sv); }
        public virtual void Update(SinhVien sv) { UpdateSinhVien(sv); }
        public virtual void Delete(string maSV) { DeleteSinhVien(maSV); }
        public virtual List<SinhVien> GetByClass(string maLop) { return GetSinhViensByMaLop(maLop); }

        private static SinhVien MapSinhVien(DataRow dr)
        {
            var sv = new SinhVien();
            sv.MaSV = dr["MaSV"] != null ? dr["MaSV"].ToString() : "";
            sv.HoTen = dr["HoTen"] != null ? dr["HoTen"].ToString() : "";

            if (dr.Table.Columns.Contains("GioiTinh") && dr["GioiTinh"] != DBNull.Value)
                sv.GioiTinh = dr["GioiTinh"].ToString();
            else if (dr.Table.Columns.Contains("gioitinh") && dr["gioitinh"] != DBNull.Value)
                sv.GioiTinh = dr["gioitinh"].ToString();

            if (dr.Table.Columns.Contains("NgaySinh") && dr["NgaySinh"] != DBNull.Value)
            {
                DateTime dt;
                if (DateTime.TryParse(dr["NgaySinh"].ToString(), out dt))
                    sv.NgaySinh = dt;
                else
                    sv.NgaySinh = DateTime.Today;
            }
            else
            {
                sv.NgaySinh = DateTime.Today;
            }

            if (dr.Table.Columns.Contains("DienThoai") && dr["DienThoai"] != DBNull.Value)
                sv.DienThoai = dr["DienThoai"].ToString();
            else if (dr.Table.Columns.Contains("SoDienThoai") && dr["SoDienThoai"] != DBNull.Value)
                sv.DienThoai = dr["SoDienThoai"].ToString();

            if (dr.Table.Columns.Contains("TrangThai") && dr["TrangThai"] != DBNull.Value)
                sv.TrangThai = dr["TrangThai"].ToString();
            else
                sv.TrangThai = "Đang học";

            sv.MaLop = dr["MaLop"] != null ? dr["MaLop"].ToString() : "";

            if (dr.Table.Columns.Contains("Diem") && dr["Diem"] != DBNull.Value)
            {
                decimal diem;
                if (decimal.TryParse(dr["Diem"].ToString(), out diem))
                    sv.Diem = diem;
            }

            if (dr.Table.Columns.Contains("Email") && dr["Email"] != DBNull.Value)
                sv.Email = dr["Email"].ToString();
            else if (dr.Table.Columns.Contains("email") && dr["email"] != DBNull.Value)
                sv.Email = dr["email"].ToString();

            return sv;
        }

        private static SinhVien MapSinhVien(IDataRecord dr)
        {
            var sv = new SinhVien();
            sv.MaSV = dr["MaSV"] != null ? dr["MaSV"].ToString() : "";
            sv.HoTen = dr["HoTen"] != null ? dr["HoTen"].ToString() : "";

            try { sv.GioiTinh = dr["GioiTinh"] != null ? dr["GioiTinh"].ToString() : ""; }
            catch { try { sv.GioiTinh = dr["gioitinh"] != null ? dr["gioitinh"].ToString() : ""; } catch { } }

            try
            {
                if (dr["NgaySinh"] != DBNull.Value)
                {
                    DateTime dt;
                    if (DateTime.TryParse(dr["NgaySinh"].ToString(), out dt))
                        sv.NgaySinh = dt;
                    else
                        sv.NgaySinh = DateTime.Today;
                }
            }
            catch { sv.NgaySinh = DateTime.Today; }

            try { sv.DienThoai = dr["DienThoai"] != null ? dr["DienThoai"].ToString() : ""; }
            catch { try { sv.DienThoai = dr["SoDienThoai"] != null ? dr["SoDienThoai"].ToString() : ""; } catch { } }

            try { sv.TrangThai = dr["TrangThai"] != null ? dr["TrangThai"].ToString() : "Đang học"; }
            catch { sv.TrangThai = "Đang học"; }

            sv.MaLop = dr["MaLop"] != null ? dr["MaLop"].ToString() : "";

            try
            {
                if (dr["Diem"] != DBNull.Value)
                {
                    decimal diem;
                    if (decimal.TryParse(dr["Diem"].ToString(), out diem))
                        sv.Diem = diem;
                }
            }
            catch { }

            try { sv.Email = dr["Email"] != null ? dr["Email"].ToString() : ""; }
            catch { try { sv.Email = dr["email"] != null ? dr["email"].ToString() : ""; } catch { } }

            return sv;
        }
    }

    public class SinhVienDAO : SinhVienDAL
    {
        public SinhVienDAO() : base() { }
        public SinhVienDAO(DataHelper helper) : base(helper) { }
    }
}
