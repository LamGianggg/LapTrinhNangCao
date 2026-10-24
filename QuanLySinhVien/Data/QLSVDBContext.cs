using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data
{
    public class QLSVDBContext : DbContext
    {
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }

        public QLSVDBContext()
        {
        }

        public QLSVDBContext(DbContextOptions<QLSVDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Mặc định hoặc cấu hình thông qua DbContextOptions
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình bảng LopHoc
            modelBuilder.Entity<LopHoc>(entity =>
            {
                entity.ToTable("LopHoc");
                entity.HasKey(e => e.MaLop);
                entity.Property(e => e.MaLop).HasMaxLength(20).IsRequired();
                entity.Property(e => e.TenLop).HasMaxLength(100).IsRequired();
            });

            // Cấu hình bảng SinhVien
            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.ToTable("SinhVien");
                entity.HasKey(e => e.MaSV);
                entity.Property(e => e.MaSV).HasMaxLength(20).IsRequired();
                entity.Property(e => e.HoTen).HasMaxLength(100).IsRequired();
                entity.Property(e => e.GioiTinh).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(254).IsRequired();
                entity.Property(e => e.DienThoai).HasMaxLength(20).IsRequired();
                entity.Property(e => e.TrangThai).HasMaxLength(50);
                entity.Property(e => e.Diem).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MaLop).HasMaxLength(20).IsRequired();

                // Thiết lập quan hệ 1 - N (Một LopHoc có nhiều SinhVien)
                entity.HasOne(e => e.LopHoc)
                      .WithMany(l => l.SinhViens)
                      .HasForeignKey(e => e.MaLop)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
