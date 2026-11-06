using System;

namespace QuanLySach.Entity
{
    /// <summary>
    /// Lớp cơ sở trừu tượng cho tất cả Entity trong hệ thống (OOP Inheritance & Abstraction).
    /// </summary>
    public abstract class BaseEntity
    {
        // Có thể mở rộng các thuộc tính chung như CreatedAt, UpdatedAt trong tương lai
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
