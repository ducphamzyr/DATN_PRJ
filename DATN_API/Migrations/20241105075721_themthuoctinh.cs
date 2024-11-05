using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN_API.Migrations
{
    /// <inheritdoc />
    public partial class themthuoctinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GioHangID1",
                table: "TaiKhoans",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenNguoiDung",
                table: "GioHangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenNguoiDung",
                table: "GioHangChiTiets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_GioHangID1",
                table: "TaiKhoans",
                column: "GioHangID1",
                unique: true,
                filter: "[GioHangID1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoans_GioHangs_GioHangID1",
                table: "TaiKhoans",
                column: "GioHangID1",
                principalTable: "GioHangs",
                principalColumn: "GioHangID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoans_GioHangs_GioHangID1",
                table: "TaiKhoans");

            migrationBuilder.DropIndex(
                name: "IX_TaiKhoans_GioHangID1",
                table: "TaiKhoans");

            migrationBuilder.DropColumn(
                name: "GioHangID1",
                table: "TaiKhoans");

            migrationBuilder.DropColumn(
                name: "TenNguoiDung",
                table: "GioHangs");

            migrationBuilder.DropColumn(
                name: "TenNguoiDung",
                table: "GioHangChiTiets");
        }
    }
}
