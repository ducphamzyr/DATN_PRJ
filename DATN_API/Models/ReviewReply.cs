namespace DATN_API.Models
{
    public class ReviewReply
    {
        public int ReviewReplyID { get; set; }
        public int ProductReviewID { get; set; }
        public string Status { get; set; }
        public string ReplyComment { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProductReview ProductReview { get; set; }
    }
}
