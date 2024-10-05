using System.Drawing;

namespace DATN_API.Models
{
    public class ProductDetail
    {
        public int ProductDetailID { get; set; }
        public int ProductID { get; set; }
        public int ColorID { get; set; }
        public int CapacityID { get; set; }
        public decimal Price { get; set; }

        public Product Product { get; set; }
        public Color Color { get; set; }
        public Capacity Capacity { get; set; }
    }
}
