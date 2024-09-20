namespace ProJAK.Service.DataTransferObject.CartDto
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }
}
