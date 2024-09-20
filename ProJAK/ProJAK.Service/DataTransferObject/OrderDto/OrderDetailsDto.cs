namespace ProJAK.Service.DataTransferObject.OrderDto
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
    }
}
