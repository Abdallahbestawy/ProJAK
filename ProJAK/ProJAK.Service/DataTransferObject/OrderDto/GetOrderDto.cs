using ProJAK.Domain.Enum;

namespace ProJAK.Service.DataTransferObject.OrderDto
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public decimal OrderAmount { get; set; }
    }

}
