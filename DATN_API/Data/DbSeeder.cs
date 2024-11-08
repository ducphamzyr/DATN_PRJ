using DATN_API.Models;
using System.Security.Cryptography;
using System.Text;

namespace DATN_API.Data
{
    public static class DbSeeder
    {
        public static async Task SeedData(AppDbContext context)
        {
            if (!context.PhanQuyens.Any())
            {
                // Seed data -> Tao 2 phan quyen co ban neu chua ton tai
                var adminRole = new PhanQuyen { TenPhanQuyen = "Admin" };
                var userRole = new PhanQuyen { TenPhanQuyen = "Users" };

                context.PhanQuyens.AddRange(adminRole, userRole);
                await context.SaveChangesAsync();

                // Import thong tin khach hang cho admin + user kiem thu
                var adminInfo = new KhachHang
                {
                    TenKhachHang = "Admin",
                    DiaChi = "Dia Chi Quan Tri Vien",
                    Email = "admin@mobilehub.com",
                    SoDienThoai = "0816487751"
                };
                var userInfo = new KhachHang
                {
                    TenKhachHang = "Khach Hang Kiem Thu",
                    DiaChi = "Dia Chi Khach Hang",
                    Email = "user@mobilehub.com",
                    SoDienThoai = "0926100949"
                };
                context.KhachHangs.Add(userInfo);
                context.KhachHangs.Add(adminInfo);
                await context.SaveChangesAsync();

                // Tao tai khoan moi cho admin + user kiem thu
                var adminAccount = new TaiKhoan
                {
                    TenDangNhap = "admin",
                    MatKhauHash = HashPassword("admin"), // mat khau chua hash
                    KhachHangId = adminInfo.Id,
                    PhanQuyenId = adminRole.Id,
                    Status = ENums.TrangThaiTaiKhoan.HoatDong
                };
                var userAccount = new TaiKhoan
                {
                    TenDangNhap = "user",
                    MatKhauHash = HashPassword("user"), // mat khau chua hash
                    KhachHangId = userInfo.Id,
                    PhanQuyenId = userRole.Id,
                    Status = ENums.TrangThaiTaiKhoan.HoatDong
                };
                context.TaiKhoans.Add(userAccount);
                context.TaiKhoans.Add(adminAccount);
                await context.SaveChangesAsync();
            }
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}