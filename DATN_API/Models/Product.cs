namespace DATN_API.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Chipset { get; set; }
        public string RAM { get; set; }
        public string ScreenSize { get; set; }
        public string Material { get; set; }
        public decimal ProductWeight { get; set; }
        public string Resolution { get; set; }
        public string BatteryCapacity { get; set; }
        public string CommunicationPort { get; set; }
        public int BrandID { get; set; }

        public Brand Brand { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductDetail> ProductDetails { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
