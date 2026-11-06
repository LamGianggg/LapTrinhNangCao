using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using QuanLySach.Entity;

namespace QuanLySach.DAL
{
    public class QLTVDBContext : DbContext
    {
        public DbSet<TheLoai> TheLoais { get; set; } = null!;
        public DbSet<Sach> Sachs { get; set; } = null!;

        public QLTVDBContext()
        {
        }

        public QLTVDBContext(DbContextOptions<QLTVDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Đọc chuỗi kết nối từ App.config theo yêu cầu đề thi
                string? connString = ConfigurationManager.ConnectionStrings["QLTV"]?.ConnectionString;
                if (string.IsNullOrWhiteSpace(connString))
                {
                    connString = "Server=localhost;Port=3306;Database=QLTVDB_GK;Uid=root;Pwd=123456;CharSet=utf8mb4;";
                }
                optionsBuilder.UseMySQL(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình TheLoai
            modelBuilder.Entity<TheLoai>(entity =>
            {
                entity.ToTable("TheLoai");
                entity.HasKey(e => e.MaLoai);
                entity.Property(e => e.MaLoai).HasMaxLength(10).IsRequired();
                entity.Property(e => e.TenLoai).HasMaxLength(100).IsRequired();
            });

            // Cấu hình Sach
            modelBuilder.Entity<Sach>(entity =>
            {
                entity.ToTable("Sach");
                entity.HasKey(e => e.MaSach);
                entity.Property(e => e.MaSach).HasMaxLength(20).IsRequired();
                entity.Property(e => e.TenSach).HasMaxLength(150).IsRequired();
                entity.Property(e => e.NgayNhap).IsRequired();
                entity.Property(e => e.SoLuong).IsRequired();
                entity.Property(e => e.DonGia).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.MaLoai).HasMaxLength(10).IsRequired();

                // Thiết lập quan hệ 1 - N: Một TheLoai có nhiều Sach
                entity.HasOne(e => e.TheLoai)
                      .WithMany(t => t.Sachs)
                      .HasForeignKey(e => e.MaLoai)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Khởi tạo tối thiểu 3 thể loại và 5 sách hợp lệ bằng mã C# qua EF (Seed Data)
            // Đảm bảo không tạo trùng dữ liệu khi chạy lại
            modelBuilder.Entity<TheLoai>().HasData(
                new TheLoai { MaLoai = "TL01", TenLoai = "Công nghệ thông tin" },
                new TheLoai { MaLoai = "TL02", TenLoai = "Kinh tế - Quản trị" },
                new TheLoai { MaLoai = "TL03", TenLoai = "Văn học - Nghệ thuật" }
            );

            modelBuilder.Entity<Sach>().HasData(
                new Sach
                {
                    MaSach = "S001",
                    TenSach = "Lập trình C# nâng cao",
                    NgayNhap = new DateTime(2026, 1, 10),
                    SoLuong = 20,
                    DonGia = 85000m,
                    MaLoai = "TL01"
                },
                new Sach
                {
                    MaSach = "S002",
                    TenSach = "Cơ sở dữ liệu và EF Core",
                    NgayNhap = new DateTime(2026, 2, 15),
                    SoLuong = 15,
                    DonGia = 120000m,
                    MaLoai = "TL01"
                },
                new Sach
                {
                    MaSach = "S003",
                    TenSach = "Kinh tế học vi mô",
                    NgayNhap = new DateTime(2026, 3, 5),
                    SoLuong = 30,
                    DonGia = 75000m,
                    MaLoai = "TL02"
                },
                new Sach
                {
                    MaSach = "S004",
                    TenSach = "Quản trị doanh nghiệp",
                    NgayNhap = new DateTime(2026, 4, 12),
                    SoLuong = 25,
                    DonGia = 95000m,
                    MaLoai = "TL02"
                },
                new Sach
                {
                    MaSach = "S005",
                    TenSach = "Truyện Kiều chú giải",
                    NgayNhap = new DateTime(2026, 5, 20),
                    SoLuong = 10,
                    DonGia = 60000m,
                    MaLoai = "TL03"
                }
            );
        }
    }
}
