using DATN_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<ReviewReply> ReviewReplies { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ImageReview> ImageReviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AccountNotification> AccountNotifications { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Capacity> Capacities { get; set; }

    }
}
