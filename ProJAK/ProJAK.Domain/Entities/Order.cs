using ProJAK.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJAK.Domain.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime OrderDate { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public decimal OrderAmount { get; set; }
        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();

    }
}
