using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLySach.Entity;

namespace QuanLySach.DAL
{
    /// <summary>
    /// Triển khai DAL cho Sach. Kế thừa BaseDAL (OOP Inheritance) và cài đặt ISachDAL.
    /// Override các phương thức để áp dụng Eager Loading (Include) và xử lý quan hệ.
    /// </summary>
    public class SachDAL : BaseDAL<Sach, string>, ISachDAL
    {
        public override List<Sach> GetAll()
        {
            return GetAllWithCategory();
        }

        public List<Sach> GetAllWithCategory()
        {
            using var db = CreateContext();
            return db.Sachs
                     .Include(s => s.TheLoai)
                     .AsNoTracking()
                     .OrderBy(s => s.MaSach)
                     .ToList();
        }

        public override Sach? GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            using var db = CreateContext();
            return db.Sachs
                     .Include(s => s.TheLoai)
                     .AsNoTracking()
                     .FirstOrDefault(s => s.MaSach == id.Trim());
        }

        public override bool Exists(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            using var db = CreateContext();
            return db.Sachs.Any(s => s.MaSach == id.Trim());
        }

        public override void Add(Sach entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var db = CreateContext();
            // Đảm bảo không gán lại navigation object khi insert
            entity.TheLoai = null;
            db.Sachs.Add(entity);
            db.SaveChanges();
        }

        public override void Update(Sach entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var db = CreateContext();
            var existing = db.Sachs.Find(entity.MaSach.Trim());
            if (existing == null)
            {
                throw new InvalidOperationException($"Không tìm thấy sách có mã {entity.MaSach} trong cơ sở dữ liệu!");
            }

            existing.TenSach = entity.TenSach;
            existing.NgayNhap = entity.NgayNhap;
            existing.SoLuong = entity.SoLuong;
            existing.DonGia = entity.DonGia;
            existing.MaLoai = entity.MaLoai;

            db.SaveChanges();
        }

        public override void Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return;
            using var db = CreateContext();
            var existing = db.Sachs.Find(id.Trim());
            if (existing != null)
            {
                db.Sachs.Remove(existing);
                db.SaveChanges();
            }
        }
    }
}
