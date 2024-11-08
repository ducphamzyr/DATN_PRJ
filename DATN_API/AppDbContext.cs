using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DATN_API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<MaGiamGia> MaGiamGias { get; set; }
        public DbSet<NhanHieu> NhanHieus { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<PhanLoai> PhanLoais { get; set; }
        public DbSet<PhanQuyen> PhanQuyens { get; set; }
        public DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure PK/FK relationships and constraints
            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.TaiKhoan)
                .WithMany(t => t.DonHangs)
                .HasForeignKey(d => d.TaiKhoanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.NhanVien)
                .WithMany(n => n.DonHangs)
                .HasForeignKey(d => d.NhanVienId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DonHangChiTiet>()
                .HasOne(d => d.DonHang)
                .WithMany(dh => dh.DonHangChiTiets)
                .HasForeignKey(d => d.DonHangId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GioHang>()
                .HasOne(g => g.TaiKhoan)
                .WithOne(t => t.GioHang)
                .HasForeignKey<GioHang>(g => g.TaiKhoanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GioHangChiTiet>()
                .HasOne(g => g.GioHang)
                .WithMany(gh => gh.GioHangChiTiets)
                .HasForeignKey(g => g.GioHangId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.KhachHang)
                .WithOne(k => k.TaiKhoan)
                .HasForeignKey<TaiKhoan>(t => t.KhachHangId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SanPham>()
                .HasOne(s => s.NhanHieu)
                .WithMany(n => n.SanPhams)
                .HasForeignKey(s => s.NhanHieuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SanPham>()
                .HasOne(s => s.PhanLoai)
                .WithMany(p => p.SanPhams)
                .HasForeignKey(s => s.PhanLoaiId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ThongBao>()
                .HasOne(t => t.TaiKhoan)
                .WithMany(tk => tk.ThongBaos)
                .HasForeignKey(t => t.TaiKhoanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add DateTime conversion for CreatedAt and UpdatedAt
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? v.Value.ToUniversalTime() : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.BaseType == typeof(BaseEntity))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.CreatedAt))
                        .HasConversion(dateTimeConverter);

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.UpdatedAt))
                        .HasConversion(nullableDateTimeConverter);
                }
            }

            // Additional configurations for constraints
            modelBuilder.Entity<DonHangChiTiet>()
                .Property(d => d.GiaTri)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MaGiamGia>()
                .Property(m => m.SoTienToiThieu)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MaGiamGia>()
                .Property(m => m.SoTienToiDa)
                .HasPrecision(18, 2);

            modelBuilder.Entity<NhanVien>()
                .Property(n => n.LuongHienTai)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SanPham>()
                .Property(s => s.Gia)
                .HasPrecision(18, 2);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && e.State is EntityState.Modified);

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}