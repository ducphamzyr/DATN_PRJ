namespace DATN_API.Models
{
    public class Wishlist
    {
        public int WishlistID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public DateTime CreatedAt { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
