using System.Collections.Generic;
using QuanLySach.Entity;

namespace QuanLySach.BUS
{
    /// <summary>
    /// Giao diện nghiệp vụ cho Sach, kế thừa IBaseBUS và bổ sung các phương thức chuyên biệt.
    /// </summary>
    public interface ISachBUS : IBaseBUS<Sach, string>
    {
        /// <summary>
        /// Lấy toàn bộ sách dưới dạng ViewModel phục vụ tầng hiển thị WinForms (LINQ Projection).
        /// </summary>
        List<SachViewModel> GetAllViewModels();

        /// <summary>
        /// Kiểm tra quy tắc nghiệp vụ chuyên sâu theo Câu 4c của đề thi.
        /// </summary>
        void ValidateBusinessRules(Sach sach, bool isAdding);
    }
}
