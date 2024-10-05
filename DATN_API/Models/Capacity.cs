namespace DATN_API.Models
{
    public class Capacity
    {
        public int CapacityID { get; set; }
        public string CapacityName { get; set; }
        public int TotalCapacity { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
