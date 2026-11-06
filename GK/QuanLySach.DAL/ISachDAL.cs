using System.Collections.Generic;
using QuanLySach.Entity;

namespace QuanLySach.DAL
{
    /// <summary>
    /// Giao diện DAL riêng biệt cho Sach, kế thừa IBaseDAL và bổ sung phương thức nghiệp vụ chuyên biệt.
    /// </summary>
    public interface ISachDAL : IBaseDAL<Sach, string>
    {
        /// <summary>
        /// Lấy tất cả sách kèm dữ liệu thể loại (Eager Loading)
        /// </summary>
        List<Sach> GetAllWithCategory();
    }
}
