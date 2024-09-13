using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJAK.Domain.Entities
{
    public class Categorie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [ForeignKey("SubCategorie")]
        public Guid? SubCategorieId { get; set; }
        public Categorie? SubCategorie { get; set; }
    }
}
