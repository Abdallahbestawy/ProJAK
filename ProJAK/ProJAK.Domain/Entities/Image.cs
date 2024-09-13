using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJAK.Domain.Entities
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Path { get; set; }
        public bool MainImage { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
