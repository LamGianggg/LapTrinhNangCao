using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace QuanLySach.DAL
{
    /// <summary>
    /// Lớp cơ sở trừu tượng cho tất cả các lớp DAL (Generic Base Repository).
    /// Triển khai nguyên lý Kế thừa (Inheritance) và Đa hình (Polymorphism) trong OOP.
    /// Giúp tái sử dụng tối đa mã nguồn cho các thao tác CRUD cơ bản qua EF Core.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu thực thể</typeparam>
    /// <typeparam name="TKey">Kiểu của khóa chính</typeparam>
    public abstract class BaseDAL<TEntity, TKey> : IBaseDAL<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Tạo mới phiên làm việc với DbContext (Unit of Work)
        /// </summary>
        protected virtual QLTVDBContext CreateContext()
        {
            return new QLTVDBContext();
        }

        public virtual List<TEntity> GetAll()
        {
            using var db = CreateContext();
            return db.Set<TEntity>().AsNoTracking().ToList();
        }

        public virtual TEntity? GetById(TKey id)
        {
            if (id == null) return null;
            using var db = CreateContext();
            return db.Set<TEntity>().Find(id);
        }

        public virtual bool Exists(TKey id)
        {
            if (id == null) return false;
            using var db = CreateContext();
            return db.Set<TEntity>().Find(id) != null;
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var db = CreateContext();
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var db = CreateContext();
            db.Set<TEntity>().Update(entity);
            db.SaveChanges();
        }

        public virtual void Delete(TKey id)
        {
            if (id == null) return;
            using var db = CreateContext();
            var existing = db.Set<TEntity>().Find(id);
            if (existing != null)
            {
                db.Set<TEntity>().Remove(existing);
                db.SaveChanges();
            }
        }
    }
}
