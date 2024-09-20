namespace ProJAK.Service.DataTransferObject.CartDto
{
    public class GetCartDetails
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
