namespace DATN_API.Models
{
    public class Voucher
    {
        public int VoucherID { get; set; }
        public string VoucherCode { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int MinimumTakeAmount { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
