using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.ProductDto
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Name Path is required.")]
        public string Path { get; set; }
        public bool MainImage { get; set; }
        public Guid ProductId { get; set; }
    }
}
