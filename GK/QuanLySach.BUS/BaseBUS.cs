using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QuanLySach.DAL;

namespace QuanLySach.BUS
{
    /// <summary>
    /// Lớp cơ sở nghiệp vụ (Base Business Service).
    /// Áp dụng nguyên lý Kế thừa (Inheritance) trong OOP và Dependency Inversion qua IBaseDAL.
    /// Tích hợp cơ chế kiểm tra tính hợp lệ của Model bằng Validator theo lý thuyết Bài 2 (Slide 24, 48).
    /// </summary>
    /// <typeparam name="TEntity">Kiểu thực thể</typeparam>
    /// <typeparam name="TKey">Kiểu khóa chính</typeparam>
    public abstract class BaseBUS<TEntity, TKey> : IBaseBUS<TEntity, TKey> where TEntity : class
    {
        protected readonly IBaseDAL<TEntity, TKey> _baseDAL;

        protected BaseBUS(IBaseDAL<TEntity, TKey> dal)
        {
            _baseDAL = dal ?? throw new ArgumentNullException(nameof(dal));
        }

        public virtual List<TEntity> GetAll()
        {
            return _baseDAL.GetAll();
        }

        public virtual TEntity? GetById(TKey id)
        {
            return _baseDAL.GetById(id);
        }

        public virtual bool Exists(TKey id)
        {
            return _baseDAL.Exists(id);
        }

        public virtual void Add(TEntity entity)
        {
            ValidateModel(entity);
            _baseDAL.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            ValidateModel(entity);
            _baseDAL.Update(entity);
        }

        public virtual void Delete(TKey id)
        {
            _baseDAL.Delete(id);
        }

        /// <summary>
        /// Chủ động kiểm tra object bằng Validator theo lý thuyết Bài 2 (Slide 24):
        /// "Trong Console hoặc WinForms, chương trình có thể gọi Validator.TryValidateObject."
        /// </summary>
        protected virtual void ValidateModel(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Dữ liệu không được để trống!");
            }

            var context = new ValidationContext(entity);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, context, results, validateAllProperties: true);

            if (!isValid)
            {
                // Gom lỗi theo property (Bài 2, Slide 25) và ném lỗi đầu tiên
                var firstError = results.FirstOrDefault()?.ErrorMessage ?? "Dữ liệu thực thể không hợp lệ!";
                throw new ValidationException(firstError);
            }
        }
    }
}
