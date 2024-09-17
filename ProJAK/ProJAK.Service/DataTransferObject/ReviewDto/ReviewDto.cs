namespace ProJAK.Service.DataTransferObject.ReviewDto
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ReviewText { get; set; }
    }
}
