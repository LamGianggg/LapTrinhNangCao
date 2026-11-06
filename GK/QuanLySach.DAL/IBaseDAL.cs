using System.Collections.Generic;

namespace QuanLySach.DAL
{
    /// <summary>
    /// Generic Interface cho tầng Data Access Layer (DAL) theo Repository Pattern.
    /// Áp dụng nguyên lý Abstraction (Trừu tượng) trong OOP.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu thực thể</typeparam>
    /// <typeparam name="TKey">Kiểu khóa chính</typeparam>
    public interface IBaseDAL<TEntity, TKey> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity? GetById(TKey id);
        bool Exists(TKey id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
    }
}
