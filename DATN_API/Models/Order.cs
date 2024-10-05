namespace DATN_API.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int StaffID { get; set; }
        public int VoucherID { get; set; }
        public int PaymentMethodID { get; set; }
        public decimal FinalAmount { get; set; }
        public DateTime OrderDate { get; set; } 
        public int ShippingAddressID { get; set; } 

        //  điều hướng
        public Customer Customer { get; set; } 
        public Staff Staff { get; set; } 
        public Voucher Voucher { get; set; } 
        public PaymentMethod PaymentMethod { get; set; } 
        public ShippingAddress ShippingAddress { get; set; } 

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>(); 
    }
}
