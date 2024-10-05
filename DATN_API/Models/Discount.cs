namespace DATN_API.Models
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
