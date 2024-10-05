namespace DATN_API.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public int AccountID { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }

        public UserAccount UserAccount { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<ShippingAddress> ShippingAddresses { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
    }
}
