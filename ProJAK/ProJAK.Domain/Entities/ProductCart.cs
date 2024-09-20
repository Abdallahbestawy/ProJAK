using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJAK.Domain.Entities
{
    public class ProductCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Cart")]
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }

    }
}
