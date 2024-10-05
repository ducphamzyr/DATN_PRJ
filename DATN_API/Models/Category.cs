namespace DATN_API.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
