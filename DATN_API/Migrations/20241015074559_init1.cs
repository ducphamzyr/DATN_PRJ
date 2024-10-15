using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    KhachHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhachHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.KhachHangID);
                });

            migrationBuilder.CreateTable(
                name: "NhanHieus",
                columns: table => new
                {
                    NhanHieuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhanHieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    XuatXu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanHieus", x => x.NhanHieuID);
                });

            migrationBuilder.CreateTable(
                name: "PhanLoais",
                columns: table => new
                {
                    PhanLoaiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhanLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanLoais", x => x.PhanLoaiID);
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyens",
                columns: table => new
                {
                    PhanQuyenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhanQuyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyens", x => x.PhanQuyenID);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToans",
                columns: table => new
                {
                    PhuongThucThanhToanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThanhToan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NhaCungCap = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongThucThanhToans", x => x.PhuongThucThanhToanID);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    SanPhamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NhanHieuID = table.Column<int>(type: "int", nullable: false),
                    NgayPhatHanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhanLoaiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.SanPhamID);
                    table.ForeignKey(
                        name: "FK_SanPhams_NhanHieus_NhanHieuID",
                        column: x => x.NhanHieuID,
                        principalTable: "NhanHieus",
                        principalColumn: "NhanHieuID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhams_PhanLoais_PhanLoaiID",
                        column: x => x.PhanLoaiID,
                        principalTable: "PhanLoais",
                        principalColumn: "PhanLoaiID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    TaiKhoanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangID = table.Column<int>(type: "int", nullable: false),
                    GioHangID = table.Column<int>(type: "int", nullable: true),
                    TenDangNhap = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhauHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioHangID_1 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.TaiKhoanID);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_KhachHangs_KhachHangID",
                        column: x => x.KhachHangID,
                        principalTable: "KhachHangs",
                        principalColumn: "KhachHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_PhanQuyens_RoleID",
                        column: x => x.RoleID,
                        principalTable: "PhanQuyens",
                        principalColumn: "PhanQuyenID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaGiamGias",
                columns: table => new
                {
                    MaGiamGiaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaApDung = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LoaiMa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DanhSachTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTienToiThieu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienToiDa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanPhamID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaGiamGias", x => x.MaGiamGiaID);
                    table.ForeignKey(
                        name: "FK_MaGiamGias_SanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamChiTiets",
                columns: table => new
                {
                    SanPhamChiTietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanPhamID = table.Column<int>(type: "int", nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BoNho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RAM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichSim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConLai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamChiTiets", x => x.SanPhamChiTietID);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_SanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    GioHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoanID = table.Column<int>(type: "int", nullable: false),
                    CapNhatLanCuoi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.GioHangID);
                    table.ForeignKey(
                        name: "FK_GioHangs_TaiKhoans_TaiKhoanID",
                        column: x => x.TaiKhoanID,
                        principalTable: "TaiKhoans",
                        principalColumn: "TaiKhoanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    NhanVienID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoanID = table.Column<int>(type: "int", nullable: false),
                    TenNhanVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiThuongTru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoCCCD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LuongHienTai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayNhanViec = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.NhanVienID);
                    table.ForeignKey(
                        name: "FK_NhanViens_TaiKhoans_TaiKhoanID",
                        column: x => x.TaiKhoanID,
                        principalTable: "TaiKhoans",
                        principalColumn: "TaiKhoanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongBaos",
                columns: table => new
                {
                    ThongBaoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoanID = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HetHan = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaos", x => x.ThongBaoID);
                    table.ForeignKey(
                        name: "FK_ThongBaos_TaiKhoans_TaiKhoanID",
                        column: x => x.TaiKhoanID,
                        principalTable: "TaiKhoans",
                        principalColumn: "TaiKhoanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiets",
                columns: table => new
                {
                    GioHangChiTietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GioHangID = table.Column<int>(type: "int", nullable: false),
                    SanPhamID = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiets", x => x.GioHangChiTietID);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiets_GioHangs_GioHangID",
                        column: x => x.GioHangID,
                        principalTable: "GioHangs",
                        principalColumn: "GioHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiets_SanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    DonHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoanID = table.Column<int>(type: "int", nullable: false),
                    NhanVienID = table.Column<int>(type: "int", nullable: false),
                    PhuongThucThanhToanID = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NgayDatHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CapNhatLanCuoi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.DonHangID);
                    table.ForeignKey(
                        name: "FK_DonHangs_NhanViens_NhanVienID",
                        column: x => x.NhanVienID,
                        principalTable: "NhanViens",
                        principalColumn: "NhanVienID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangs_PhuongThucThanhToans_PhuongThucThanhToanID",
                        column: x => x.PhuongThucThanhToanID,
                        principalTable: "PhuongThucThanhToans",
                        principalColumn: "PhuongThucThanhToanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangs_TaiKhoans_TaiKhoanID",
                        column: x => x.TaiKhoanID,
                        principalTable: "TaiKhoans",
                        principalColumn: "TaiKhoanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHangChiTiets",
                columns: table => new
                {
                    DonHangChiTietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanPhamID = table.Column<int>(type: "int", nullable: false),
                    DonHangID = table.Column<int>(type: "int", nullable: false),
                    MaGiamGiaID = table.Column<int>(type: "int", nullable: false),
                    IMEI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaTri = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangChiTiets", x => x.DonHangChiTietID);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiets_DonHangs_DonHangID",
                        column: x => x.DonHangID,
                        principalTable: "DonHangs",
                        principalColumn: "DonHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiets_MaGiamGias_MaGiamGiaID",
                        column: x => x.MaGiamGiaID,
                        principalTable: "MaGiamGias",
                        principalColumn: "MaGiamGiaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiets_SanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_DonHangID",
                table: "DonHangChiTiets",
                column: "DonHangID");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_MaGiamGiaID",
                table: "DonHangChiTiets",
                column: "MaGiamGiaID");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_SanPhamID",
                table: "DonHangChiTiets",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_NhanVienID",
                table: "DonHangs",
                column: "NhanVienID");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_PhuongThucThanhToanID",
                table: "DonHangs",
                column: "PhuongThucThanhToanID");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_TaiKhoanID",
                table: "DonHangs",
                column: "TaiKhoanID");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiets_GioHangID",
                table: "GioHangChiTiets",
                column: "GioHangID");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiets_SanPhamID",
                table: "GioHangChiTiets",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_TaiKhoanID",
                table: "GioHangs",
                column: "TaiKhoanID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaGiamGias_SanPhamID",
                table: "MaGiamGias",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_TaiKhoanID",
                table: "NhanViens",
                column: "TaiKhoanID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_SanPhamID",
                table: "SanPhamChiTiets",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_NhanHieuID",
                table: "SanPhams",
                column: "NhanHieuID");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_PhanLoaiID",
                table: "SanPhams",
                column: "PhanLoaiID");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_KhachHangID",
                table: "TaiKhoans",
                column: "KhachHangID");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_RoleID",
                table: "TaiKhoans",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaos_TaiKhoanID",
                table: "ThongBaos",
                column: "TaiKhoanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonHangChiTiets");

            migrationBuilder.DropTable(
                name: "GioHangChiTiets");

            migrationBuilder.DropTable(
                name: "SanPhamChiTiets");

            migrationBuilder.DropTable(
                name: "ThongBaos");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "MaGiamGias");

            migrationBuilder.DropTable(
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToans");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "NhanHieus");

            migrationBuilder.DropTable(
                name: "PhanLoais");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "PhanQuyens");
        }
    }
}
