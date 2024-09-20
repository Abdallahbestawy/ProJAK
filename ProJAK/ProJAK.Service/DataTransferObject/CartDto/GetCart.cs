namespace ProJAK.Service.DataTransferObject.CartDto
{
    public class GetCart
    {
        public decimal TotalPrice { get; set; }
        public List<GetCartDetails> GetCartDetails { get; set; } = new List<GetCartDetails>();

    }
}
