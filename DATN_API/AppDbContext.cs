using DATN_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ThongBao> ThongBaos { get; set; }
        public DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }
        public DbSet<PhanLoai> PhanLoais { get; set; }
        public DbSet<PhanQuyen> PhanQuyens { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<NhanHieu> NhanHieus { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<MaGiamGia> MaGiamGias { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ThongBao relationships
            modelBuilder.Entity<ThongBao>()
                .HasOne(t => t.TaiKhoan)
                .WithMany()
                .HasForeignKey(t => t.TaiKhoanID)
                .OnDelete(DeleteBehavior.Restrict);

            // GioHangChiTiet relationships
            modelBuilder.Entity<GioHangChiTiet>()
                .HasOne(g => g.GioHang)
                .WithMany(g => g.GioHangChiTiets)
                .HasForeignKey(g => g.GioHangID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GioHangChiTiet>()
                .HasOne(g => g.SanPham)
                .WithMany(s => s.GioHangChiTiets)
                .HasForeignKey(g => g.SanPhamID)
                .OnDelete(DeleteBehavior.Restrict);

            // SanPham relationships
            modelBuilder.Entity<SanPham>()
                .HasOne(s => s.NhanHieu)
                .WithMany(n => n.SanPhams)
                .HasForeignKey(s => s.NhanHieuID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SanPham>()
                .HasOne(s => s.PhanLoai)
                .WithMany(p => p.SanPhams)
                .HasForeignKey(s => s.PhanLoaiID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SanPham>()
                .Navigation(s => s.NhanHieu).AutoInclude();

            modelBuilder.Entity<SanPham>()
                .Navigation(s => s.PhanLoai).AutoInclude();

            // DonHang relationships
            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.TaiKhoan)
                .WithMany()
                .HasForeignKey(d => d.TaiKhoanID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.NhanVien)
                .WithMany(n => n.DonHangs)
                .HasForeignKey(d => d.NhanVienID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.PhuongThucThanhToan)
                .WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.PhuongThucThanhToanID)
                .OnDelete(DeleteBehavior.Restrict);

            // TaiKhoan relationships
            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.KhachHang)
                .WithMany(k => k.TaiKhoans)
                .HasForeignKey(t => t.KhachHangID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.GioHang)
                .WithOne()
                .HasForeignKey<TaiKhoan>(t => t.GioHangID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.PhanQuyen)
                .WithMany(p => p.TaiKhoans)
                .HasForeignKey(t => t.PhanQuyenID)
                .OnDelete(DeleteBehavior.Restrict);

            // NhanVien relationships
            modelBuilder.Entity<NhanVien>()
                .HasOne(n => n.TaiKhoan)
                .WithOne()
                .HasForeignKey<NhanVien>(n => n.TaiKhoanID)
                .OnDelete(DeleteBehavior.Restrict);

            // DonHangChiTiet relationships
            modelBuilder.Entity<DonHangChiTiet>()
                .HasOne(d => d.SanPham)
                .WithMany(s => s.DonHangChiTiets)
                .HasForeignKey(d => d.SanPhamID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DonHangChiTiet>()
                .HasOne(d => d.DonHang)
                .WithMany(d => d.DonHangChiTiets)
                .HasForeignKey(d => d.DonHangID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DonHangChiTiet>()
                .HasOne(d => d.MaGiamGia)
                .WithMany(m => m.DonHangChiTiets)
                .HasForeignKey(d => d.MaGiamGiaID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}