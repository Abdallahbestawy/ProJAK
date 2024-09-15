using ProJAK.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJAK.Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required, MaxLength(500)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable => Quantity > 0;
        public decimal Price { get; set; }
        [ForeignKey("Categorie")]
        public Guid CategorieId { get; set; }
        public virtual Categorie Categorie { get; set; }
        [ForeignKey("Manufacturer")]
        public Guid ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [ForeignKey("Screen")]
        public Guid? ScreenId { get; set; }
        public virtual Screen? Screen { get; set; }
        [ForeignKey("Processor")]
        public Guid? ProcessorId { get; set; }
        public virtual Processor? Processor { get; set; }
        [ForeignKey("GraphicsCard")]
        public Guid? GraphicsCardId { get; set; }
        public virtual GraphicsCard? GraphicsCard { get; set; }
        [ForeignKey("Hard")]
        public Guid? HardId { get; set; }
        public virtual Hard? Hard { get; set; }
        public MemorySize? HardSize { get; set; }
        public MemorySize? RamSize { get; set; }
        public Color ProductColor { get; set; }
        public virtual ICollection<ProductCart> ProductCarts { get; set; } = new List<ProductCart>();
        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
