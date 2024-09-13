using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJAK.Domain.Entities
{
    public class OrderedProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
