namespace DATN_API.Models
{
    public class ImageReview
    {
        public int ImageReviewID { get; set; }
        public int ProductReviewID { get; set; }
        public string ImageUrl { get; set; }
        public DateTime UploadedAt { get; set; }

        public ProductReview ProductReview { get; set; }
    }
}
