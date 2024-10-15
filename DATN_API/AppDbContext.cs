using DATN_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<SanPhamChiTiet> SanPhamChiTiets { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        public DbSet<NhanHieu> NhanHieus { get; set; }
        public DbSet<PhanLoai> PhanLoais { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<PhanQuyen> PhanQuyens { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<MaGiamGia> MaGiamGias { get; set; }
        public DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình các mối quan hệ
            modelBuilder.Entity<SanPham>()
                .HasOne(s => s.NhanHieu)
                .WithMany(n => n.SanPhams)
                .HasForeignKey(s => s.NhanHieuID);

            modelBuilder.Entity<SanPham>()
                .HasOne(s => s.PhanLoai)
                .WithMany(p => p.SanPhams)
                .HasForeignKey(s => s.PhanLoaiID);

            modelBuilder.Entity<SanPhamChiTiet>()
                .HasOne(s => s.SanPham)
                .WithMany(s => s.SanPhamChiTiets)
                .HasForeignKey(s => s.SanPhamID);

            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.TaiKhoan)
                .WithMany()
                .HasForeignKey(d => d.TaiKhoanID);

            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.NhanVien)
                .WithMany(n => n.DonHangs)
                .HasForeignKey(d => d.NhanVienID);

            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.PhuongThucThanhToan)
                .WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.PhuongThucThanhToanID);

            modelBuilder.Entity<DonHangChiTiet>()
                .HasOne(d => d.DonHang)
                .WithMany(d => d.DonHangChiTiets)
                .HasForeignKey(d => d.DonHangID);

            modelBuilder.Entity<DonHangChiTiet>()
                .HasOne(d => d.SanPham)
                .WithMany()
                .HasForeignKey(d => d.SanPhamID);

            modelBuilder.Entity<DonHangChiTiet>()
                .HasOne(d => d.MaGiamGia)
                .WithMany()
                .HasForeignKey(d => d.MaGiamGiaID);

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.KhachHang)
                .WithMany(k => k.TaiKhoans)
                .HasForeignKey(t => t.KhachHangID);

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.PhanQuyen)
                .WithMany(p => p.TaiKhoans)
                .HasForeignKey(t => t.RoleID);

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.GioHang)
                .WithOne(g => g.TaiKhoan)
                .HasForeignKey<GioHang>(g => g.TaiKhoanID);

            modelBuilder.Entity<GioHangChiTiet>()
                .HasOne(g => g.GioHang)
                .WithMany(g => g.GioHangChiTiets)
                .HasForeignKey(g => g.GioHangID);

            modelBuilder.Entity<GioHangChiTiet>()
                .HasOne(g => g.SanPham)
                .WithMany()
                .HasForeignKey(g => g.SanPhamID);

            modelBuilder.Entity<ThongBao>()
                .HasOne(t => t.TaiKhoan)
                .WithMany()
                .HasForeignKey(t => t.TaiKhoanID);

            modelBuilder.Entity<NhanVien>()
                .HasOne(n => n.TaiKhoan)
                .WithOne()
                .HasForeignKey<NhanVien>(n => n.TaiKhoanID);

            modelBuilder.Entity<MaGiamGia>()
                .HasOne(m => m.SanPham)
                .WithMany(s => s.MaGiamGias)
                .HasForeignKey(m => m.SanPhamID);
        }
    }
}