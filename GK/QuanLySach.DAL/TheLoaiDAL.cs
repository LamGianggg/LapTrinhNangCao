using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLySach.Entity;

namespace QuanLySach.DAL
{
    /// <summary>
    /// Triển khai DAL cho TheLoai. Kế thừa từ BaseDAL (Tái sử dụng mã CRUD cơ sở)
    /// và cài đặt ITheLoaiDAL.
    /// </summary>
    public class TheLoaiDAL : BaseDAL<TheLoai, string>, ITheLoaiDAL
    {
        public override List<TheLoai> GetAll()
        {
            using var db = CreateContext();
            return db.TheLoais
                     .AsNoTracking()
                     .OrderBy(t => t.MaLoai)
                     .ToList();
        }
    }
}
