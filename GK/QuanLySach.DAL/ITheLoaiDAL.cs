using System.Collections.Generic;
using QuanLySach.Entity;

namespace QuanLySach.DAL
{
    /// <summary>
    /// Giao diện DAL riêng biệt cho TheLoai, kế thừa IBaseDAL.
    /// </summary>
    public interface ITheLoaiDAL : IBaseDAL<TheLoai, string>
    {
    }
}
