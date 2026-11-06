using QuanLySach.DAL;
using QuanLySach.Entity;

namespace QuanLySach.BUS
{
    /// <summary>
    /// Triển khai nghiệp vụ TheLoai. Kế thừa BaseBUS (OOP Inheritance) và cài đặt ITheLoaiBUS.
    /// Hỗ trợ Dependency Injection qua Constructor.
    /// </summary>
    public class TheLoaiBUS : BaseBUS<TheLoai, string>, ITheLoaiBUS
    {
        public TheLoaiBUS() : this(new TheLoaiDAL())
        {
        }

        public TheLoaiBUS(ITheLoaiDAL dal) : base(dal)
        {
        }
    }
}
