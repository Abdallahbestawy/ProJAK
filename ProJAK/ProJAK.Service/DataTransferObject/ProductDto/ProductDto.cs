using ProJAK.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.ProductDto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Name field is required."),
         MaxLength(500, ErrorMessage = "The Name must be at least 500 characters long.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable => Quantity > 0;
        public decimal Price { get; set; }
        public Guid CategorieId { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid? ScreenId { get; set; }
        public Guid? ProcessorId { get; set; }
        public Guid? GraphicsCardId { get; set; }
        public Guid? HardId { get; set; }
        public MemorySize? HardSize { get; set; }
        public MemorySize? RamSize { get; set; }
        public Color ProductColor { get; set; }
        public List<ImageDto> Image { get; set; }
    }
}
