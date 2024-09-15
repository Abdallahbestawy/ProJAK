using ProJAK.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.GraphicsCardDto
{
    public class GraphicsCardDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(250)]
        public string Name { get; set; }
        public MemorySize MemorySize { get; set; }
        public Guid ManufacturerId { get; set; }
    }
}
