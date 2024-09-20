namespace ProJAK.Service.DataTransferObject.OrderDto
{
    public class OrderDto
    {
        public decimal TotalAmount { get; set; }
        public List<OrderDetailsDto> orderDetails { get; set; } = new List<OrderDetailsDto>();

    }
}
