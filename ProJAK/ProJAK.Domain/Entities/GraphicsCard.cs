using ProJAK.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJAK.Domain.Entities
{
    public class GraphicsCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required, MaxLength(250)]
        public string Name { get; set; }
        public MemorySize MemorySize { get; set; }
        [ForeignKey("Manufacturer")]
        public Guid ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
