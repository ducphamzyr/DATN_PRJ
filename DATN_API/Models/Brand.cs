﻿namespace DATN_API.Models
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
