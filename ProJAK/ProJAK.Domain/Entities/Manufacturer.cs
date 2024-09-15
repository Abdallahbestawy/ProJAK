using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJAK.Domain.Entities
{
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required, MaxLength(250)]
        public string Name { get; set; }
        public virtual ICollection<GraphicsCard> GraphicsCards { get; set; } = new List<GraphicsCard>();
        public virtual ICollection<Processor> Processors { get; set; } = new List<Processor>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
