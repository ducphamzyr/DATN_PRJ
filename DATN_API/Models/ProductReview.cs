namespace DATN_API.Models
{
    public class ProductReview
    {
        public int ProductReviewID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public ICollection<ReviewReply> ReviewReplies { get; set; }
        public ICollection<ImageReview> ImageReviews { get; set; }
    }
}
