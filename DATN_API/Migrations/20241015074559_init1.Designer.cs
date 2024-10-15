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
    [Migration("20241015074559_init1")]
    partial class init1
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
                    b.Property<int>("DonHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonHangID"));

                    b.Property<DateTime?>("CapNhatLanCuoi")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiaChiGiaoHang")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("NgayDatHang")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NhanVienID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("PhuongThucThanhToanID")
                        .HasColumnType("int");

                    b.Property<int>("TaiKhoanID")
                        .HasColumnType("int");

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
                    b.Property<int>("DonHangChiTietID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonHangChiTietID"));

                    b.Property<int>("DonHangID")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("GiaTri")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IMEI")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("MaGiamGiaID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

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
                    b.Property<int>("GioHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GioHangID"));

                    b.Property<DateTime>("CapNhatLanCuoi")
                        .HasColumnType("datetime2");

                    b.Property<int>("TaiKhoanID")
                        .HasColumnType("int");

                    b.HasKey("GioHangID");

                    b.HasIndex("TaiKhoanID")
                        .IsUnique();

                    b.ToTable("GioHangs");
                });

            modelBuilder.Entity("DATN_API.Models.GioHangChiTiet", b =>
                {
                    b.Property<int>("GioHangChiTietID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GioHangChiTietID"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GioHangID")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("GioHangChiTietID");

                    b.HasIndex("GioHangID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("GioHangChiTiets");
                });

            modelBuilder.Entity("DATN_API.Models.KhachHang", b =>
                {
                    b.Property<int>("KhachHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KhachHangID"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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
                    b.Property<int>("MaGiamGiaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaGiamGiaID"));

                    b.Property<string>("DanhSachTaiKhoan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoaiMa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MaApDung")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("NgayHetHan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<int>("PhanTramGiam")
                        .HasColumnType("int");

                    b.Property<int?>("SanPhamID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<decimal>("SoTienToiDa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SoTienToiThieu")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TenMa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaGiamGiaID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("MaGiamGias");
                });

            modelBuilder.Entity("DATN_API.Models.NhanHieu", b =>
                {
                    b.Property<int>("NhanHieuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NhanHieuID"));

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
                    b.Property<int>("NhanVienID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NhanVienID"));

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
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaiKhoanID")
                        .HasColumnType("int");

                    b.Property<string>("TenNhanVien")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NhanVienID");

                    b.HasIndex("TaiKhoanID")
                        .IsUnique();

                    b.ToTable("NhanViens");
                });

            modelBuilder.Entity("DATN_API.Models.PhanLoai", b =>
                {
                    b.Property<int>("PhanLoaiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhanLoaiID"));

                    b.Property<string>("TenPhanLoai")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PhanLoaiID");

                    b.ToTable("PhanLoais");
                });

            modelBuilder.Entity("DATN_API.Models.PhanQuyen", b =>
                {
                    b.Property<int>("PhanQuyenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhanQuyenID"));

                    b.Property<string>("TenPhanQuyen")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PhanQuyenID");

                    b.ToTable("PhanQuyens");
                });

            modelBuilder.Entity("DATN_API.Models.PhuongThucThanhToan", b =>
                {
                    b.Property<int>("PhuongThucThanhToanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhuongThucThanhToanID"));

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
                    b.Property<int>("SanPhamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SanPhamID"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayPhatHanh")
                        .HasColumnType("datetime2");

                    b.Property<int>("NhanHieuID")
                        .HasColumnType("int");

                    b.Property<int>("PhanLoaiID")
                        .HasColumnType("int");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SanPhamID");

                    b.HasIndex("NhanHieuID");

                    b.HasIndex("PhanLoaiID");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("DATN_API.Models.SanPhamChiTiet", b =>
                {
                    b.Property<int>("SanPhamChiTietID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SanPhamChiTietID"));

                    b.Property<string>("BoNho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ConLai")
                        .HasColumnType("int");

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("KichSim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mau")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RAM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SanPhamChiTietID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("SanPhamChiTiets");
                });

            modelBuilder.Entity("DATN_API.Models.TaiKhoan", b =>
                {
                    b.Property<int>("TaiKhoanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaiKhoanID"));

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GioHangID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("GioHangID_1")
                        .HasColumnType("datetime2");

                    b.Property<int?>("KhachHangID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("MatKhauHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("TenDangNhap")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TaiKhoanID");

                    b.HasIndex("KhachHangID");

                    b.HasIndex("RoleID");

                    b.ToTable("TaiKhoans");
                });

            modelBuilder.Entity("DATN_API.Models.ThongBao", b =>
                {
                    b.Property<int>("ThongBaoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ThongBaoID"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("HetHan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaiKhoanID")
                        .HasColumnType("int");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ThongBaoID");

                    b.HasIndex("TaiKhoanID");

                    b.ToTable("ThongBaos");
                });

            modelBuilder.Entity("DATN_API.Models.DonHang", b =>
                {
                    b.HasOne("DATN_API.Models.NhanVien", "NhanVien")
                        .WithMany("DonHangs")
                        .HasForeignKey("NhanVienID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.PhuongThucThanhToan", "PhuongThucThanhToan")
                        .WithMany("DonHangs")
                        .HasForeignKey("PhuongThucThanhToanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.TaiKhoan", "TaiKhoan")
                        .WithMany()
                        .HasForeignKey("TaiKhoanID")
                        .OnDelete(DeleteBehavior.Cascade)
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.MaGiamGia", "MaGiamGia")
                        .WithMany()
                        .HasForeignKey("MaGiamGiaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.SanPham", "SanPham")
                        .WithMany()
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("MaGiamGia");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DATN_API.Models.GioHang", b =>
                {
                    b.HasOne("DATN_API.Models.TaiKhoan", "TaiKhoan")
                        .WithOne("GioHang")
                        .HasForeignKey("DATN_API.Models.GioHang", "TaiKhoanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("DATN_API.Models.GioHangChiTiet", b =>
                {
                    b.HasOne("DATN_API.Models.GioHang", "GioHang")
                        .WithMany("GioHangChiTiets")
                        .HasForeignKey("GioHangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.SanPham", "SanPham")
                        .WithMany()
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GioHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DATN_API.Models.MaGiamGia", b =>
                {
                    b.HasOne("DATN_API.Models.SanPham", "SanPham")
                        .WithMany("MaGiamGias")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DATN_API.Models.NhanVien", b =>
                {
                    b.HasOne("DATN_API.Models.TaiKhoan", "TaiKhoan")
                        .WithOne()
                        .HasForeignKey("DATN_API.Models.NhanVien", "TaiKhoanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("DATN_API.Models.SanPham", b =>
                {
                    b.HasOne("DATN_API.Models.NhanHieu", "NhanHieu")
                        .WithMany("SanPhams")
                        .HasForeignKey("NhanHieuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.PhanLoai", "PhanLoai")
                        .WithMany("SanPhams")
                        .HasForeignKey("PhanLoaiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanHieu");

                    b.Navigation("PhanLoai");
                });

            modelBuilder.Entity("DATN_API.Models.SanPhamChiTiet", b =>
                {
                    b.HasOne("DATN_API.Models.SanPham", "SanPham")
                        .WithMany("SanPhamChiTiets")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DATN_API.Models.TaiKhoan", b =>
                {
                    b.HasOne("DATN_API.Models.KhachHang", "KhachHang")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("KhachHangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN_API.Models.PhanQuyen", "PhanQuyen")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");

                    b.Navigation("PhanQuyen");
                });

            modelBuilder.Entity("DATN_API.Models.ThongBao", b =>
                {
                    b.HasOne("DATN_API.Models.TaiKhoan", "TaiKhoan")
                        .WithMany()
                        .HasForeignKey("TaiKhoanID")
                        .OnDelete(DeleteBehavior.Cascade)
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
                    b.Navigation("MaGiamGias");

                    b.Navigation("SanPhamChiTiets");
                });

            modelBuilder.Entity("DATN_API.Models.TaiKhoan", b =>
                {
                    b.Navigation("GioHang")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
