namespace DATN_API.Models
{
    public class ShippingAddress
    {
        public int ShippingAddressID { get; set; }
        public int CustomerID { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressDetails { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
