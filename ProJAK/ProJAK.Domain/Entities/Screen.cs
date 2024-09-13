using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJAK.Domain.Entities
{
    public class Screen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required, MaxLength(50)]
        public string Resolution { get; set; }
        [Required, MaxLength(50)]
        public string PanelType { get; set; }
        public int RefreshRate { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
