using System;

namespace QuanLySach.Entity
{
    /// <summary>
    /// ViewModel / DTO đại diện cho dữ liệu hiển thị của Sách trên Giao diện (Presentation Layer).
    /// Áp dụng theo lý thuyết Bài 3 (Slide 13, 241): "DTO phù hợp dữ liệu đi qua các tầng và hiển thị".
    /// </summary>
    public class SachViewModel
    {
        public string MaSach { get; set; } = string.Empty;
        public string TenSach { get; set; } = string.Empty;
        public string MaLoai { get; set; } = string.Empty;
        public string TenLoai { get; set; } = string.Empty;
        public DateTime NgayNhap { get; set; }
        public string NgayNhapFormatted => NgayNhap.ToString("dd/MM/yyyy");
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string DonGiaFormatted => DonGia.ToString("N0");
    }
}
