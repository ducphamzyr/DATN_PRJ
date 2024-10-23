﻿// <auto-generated />
using System;
using DATN_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DATN_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241021170326_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DATN_API.Models.DonHang", b =>
                {
                    b.Property<long>("DonHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DonHangID"));

                    b.Property<DateTime>("CapNhatLanCuoi")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiaChiGiaoHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayDatHang")
                        .HasColumnType("datetime2");

                    b.Property<long>("NhanVienID")
                        .HasColumnType("bigint");

                    b.Property<long>("PhuongThucThanhToanID")
                        .HasColumnType("bigint");

                    b.Property<long>("TaiKhoanID")
                        .HasColumnType("bigint");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DonHangID");

                    b.HasIndex("NhanVienID");

                    b.HasIndex("PhuongThucThanhToanID");

                    b.HasIndex("TaiKhoanID");

                    b.ToTable("DonHangs");
                });

            modelBuilder.Entity("DATN_API.Models.DonHangChiTiet", b =>
                {
                    b.Property<long>("DonHangChiTietID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DonHangChiTietID"));

                    b.Property<long>("DonHangID")
                        .HasColumnType("bigint");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GiaTri")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IMEI")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long?>("MaGiamGiaID")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<long>("SanPhamID")
                        .HasColumnType("bigint");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("DonHangChiTietID");

                    b.HasIndex("DonHangID");

                    b.HasIndex("MaGiamGiaID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("DonHangChiTiets");
                });

            modelBuilder.Entity("DATN_API.Models.GioHang", b =>
                {
                    b.Property<long>("GioHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("GioHangID"));

                    b.Property<DateTime>("CapNhatLanCuoi")
                        .HasColumnType("datetime2");

                    b.HasKey("GioHangID");

                    b.ToTable("GioHangs");
                });

            modelBuilder.Entity("DATN_API.Models.GioHangChiTiet", b =>
                {
                    b.Property<long>("GioHangChiTietID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("GioHangChiTietID"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GioHangID")
                        .HasColumnType("bigint");

                    b.Property<long>("SanPhamID")
                        .HasColumnType("bigint");

                    b.Property<long>("SoLuong")
                        .HasColumnType("bigint");

                    b.HasKey("GioHangChiTietID");

                    b.HasIndex("GioHangID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("GioHangChiTiets");
                });

            modelBuilder.Entity("DATN_API.Models.KhachHang", b =>
                {
                    b.Property<long>("KhachHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("KhachHangID"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKhachHang")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("KhachHangID");

                    b.ToTable("KhachHangs");
                });

            modelBuilder.Entity("DATN_API.Models.MaGiamGia", b =>
                {
                    b.Property<long>("MaGiamGiaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MaGiamGiaID"));

                    b.Property<string>("DanhSachTaiKhoan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoaiMa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MaApDung")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("NgayHetHan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<int>("PhanTramGiam")
                        .HasColumnType("int");

                    b.Property<decimal>("SoTienToiDa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SoTienToiThieu")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TenMa")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaGiamGiaID");

                    b.ToTable("MaGiamGias");
                });

            modelBuilder.Entity("DATN_API.Models.NhanHieu", b =>
                {
                    b.Property<long>("NhanHieuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("NhanHieuID"));

                    b.Property<string>("TenNhanHieu")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("XuatXu")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("NhanHieuID");

                    b.ToTable("NhanHieus");
                });

            modelBuilder.Entity("DATN_API.Models.NhanVien", b =>
                {
                    b.Property<long>("NhanVienID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("NhanVienID"));

                    b.Property<string>("DiaChiThuongTru")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LuongHienTai")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("NgayNhanViec")
                        .HasColumnType("datetime2");

                    b.Property<string>("QueQuan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoCCCD")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TaiKhoanID")
                        .HasColumnType("bigint");

                    b.Property<string>("TenNhanVien")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("NhanVienID");

                    b.HasIndex("TaiKhoanID")
                        .IsUnique();

                    b.ToTable("NhanViens");
                });

            modelBuilder.Entity("DATN_API.Models.PhanLoai", b =>
                {
                    b.Property<long>("PhanLoaiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PhanLoaiID"));

                    b.Property<string>("TenPhanLoai")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PhanLoaiID");

                    b.ToTable("PhanLoais");
                });

            modelBuilder.Entity("DATN_API.Models.PhanQuyen", b =>
                {
                    b.Property<long>("PhanQuyenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PhanQuyenID"));

                    b.Property<string>("TenPhanQuyen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PhanQuyenID");

                    b.ToTable("PhanQuyens");
                });

            modelBuilder.Entity("DATN_API.Models.PhuongThucThanhToan", b =>
                {
                    b.Property<long>("PhuongThucThanhToanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PhuongThucThanhToanID"));

                    b.Property<string>("NhaCungCap")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenThanhToan")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PhuongThucThanhToanID");

                    b.ToTable("PhuongThucThanhToans");
                });

            modelBuilder.Entity("DATN_API.Models.SanPham", b =>
                {
                    b.Property<long>("SanPhamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SanPhamID"));

                    b.Property<string>("BoNho")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ConLai")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("KheSim")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Mau")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NgayKhoiTao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NhanHieuID")
                        .HasColumnType("bigint");

                    b.Property<long>("PhanLoaiID")
                        .HasColumnType("bigint");

                    b.Property<string>("Ram")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SanPhamID");

                    b.HasIndex("NhanHieuID");

                    b.HasIndex("PhanLoaiID");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("DATN_API.Models.TaiKhoan", b =>
                {
                    b.Property<long>("TaiKhoanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TaiKhoanID"));

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("GioHangID")
                        .HasColumnType("bigint");

                    b.Property<long>("KhachHangID")
                        .HasColumnType("bigint");

                    b.Property<string>("MatKhauHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhanQuyenID")
                        .HasColumnType("bigint");

                    b.Property<string>("TenDangNhap")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TaiKhoanID");

                    b.HasIndex("GioHangID")
                        .IsUnique();

                    b.HasIndex("KhachHangID");

                    b.HasIndex("PhanQuyenID");

                    b.ToTable("TaiKhoans");
                });

            modelBuilder.Entity("DATN_API.Models.ThongBao", b =>
                {
                    b.Property<long>("ThongBaoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ThongBaoID"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HetHan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TaiKhoanID")
                        .HasColumnType("bigint");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ThongBaoID");

                    b.HasIndex("TaiKhoanID");

                    b.ToTable("ThongBaos");
                });

            modelBuilder.Entity("DATN_API.Models.DonHang", b =>
                {
                    b.HasOne("DATN_API.Models.NhanVien", "NhanVien")
                        .WithMany("DonHangs")
                        .HasForeignKey("NhanVienID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.PhuongThucThanhToan", "PhuongThucThanhToan")
                        .WithMany("DonHangs")
                        .HasForeignKey("PhuongThucThanhToanID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.TaiKhoan", "TaiKhoan")
                        .WithMany()
                        .HasForeignKey("TaiKhoanID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NhanVien");

                    b.Navigation("PhuongThucThanhToan");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("DATN_API.Models.DonHangChiTiet", b =>
                {
                    b.HasOne("DATN_API.Models.DonHang", "DonHang")
                        .WithMany("DonHangChiTiets")
                        .HasForeignKey("DonHangID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.MaGiamGia", "MaGiamGia")
                        .WithMany("DonHangChiTiets")
                        .HasForeignKey("MaGiamGiaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.SanPham", "SanPham")
                        .WithMany("DonHangChiTiets")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("MaGiamGia");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DATN_API.Models.GioHangChiTiet", b =>
                {
                    b.HasOne("DATN_API.Models.GioHang", "GioHang")
                        .WithMany("GioHangChiTiets")
                        .HasForeignKey("GioHangID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.SanPham", "SanPham")
                        .WithMany("GioHangChiTiets")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GioHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DATN_API.Models.NhanVien", b =>
                {
                    b.HasOne("DATN_API.Models.TaiKhoan", "TaiKhoan")
                        .WithOne()
                        .HasForeignKey("DATN_API.Models.NhanVien", "TaiKhoanID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("DATN_API.Models.SanPham", b =>
                {
                    b.HasOne("DATN_API.Models.NhanHieu", "NhanHieu")
                        .WithMany("SanPhams")
                        .HasForeignKey("NhanHieuID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.PhanLoai", "PhanLoai")
                        .WithMany("SanPhams")
                        .HasForeignKey("PhanLoaiID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NhanHieu");

                    b.Navigation("PhanLoai");
                });

            modelBuilder.Entity("DATN_API.Models.TaiKhoan", b =>
                {
                    b.HasOne("DATN_API.Models.GioHang", "GioHang")
                        .WithOne()
                        .HasForeignKey("DATN_API.Models.TaiKhoan", "GioHangID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.KhachHang", "KhachHang")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("KhachHangID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.PhanQuyen", "PhanQuyen")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("PhanQuyenID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GioHang");

                    b.Navigation("KhachHang");

                    b.Navigation("PhanQuyen");
                });

            modelBuilder.Entity("DATN_API.Models.ThongBao", b =>
                {
                    b.HasOne("DATN_API.Models.TaiKhoan", "TaiKhoan")
                        .WithMany()
                        .HasForeignKey("TaiKhoanID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("DATN_API.Models.DonHang", b =>
                {
                    b.Navigation("DonHangChiTiets");
                });

            modelBuilder.Entity("DATN_API.Models.GioHang", b =>
                {
                    b.Navigation("GioHangChiTiets");
                });

            modelBuilder.Entity("DATN_API.Models.KhachHang", b =>
                {
                    b.Navigation("TaiKhoans");
                });

            modelBuilder.Entity("DATN_API.Models.MaGiamGia", b =>
                {
                    b.Navigation("DonHangChiTiets");
                });

            modelBuilder.Entity("DATN_API.Models.NhanHieu", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("DATN_API.Models.NhanVien", b =>
                {
                    b.Navigation("DonHangs");
                });

            modelBuilder.Entity("DATN_API.Models.PhanLoai", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("DATN_API.Models.PhanQuyen", b =>
                {
                    b.Navigation("TaiKhoans");
                });

            modelBuilder.Entity("DATN_API.Models.PhuongThucThanhToan", b =>
                {
                    b.Navigation("DonHangs");
                });

            modelBuilder.Entity("DATN_API.Models.SanPham", b =>
                {
                    b.Navigation("DonHangChiTiets");

                    b.Navigation("GioHangChiTiets");
                });
#pragma warning restore 612, 618
        }
    }
}
