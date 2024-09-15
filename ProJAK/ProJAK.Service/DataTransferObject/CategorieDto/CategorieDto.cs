using ProJAK.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.CategorieDto
{
    public class CategorieDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [MinLength(5, ErrorMessage = "The Name must be at least 5 characters long.")]
        public string Name { get; set; }
        public CategorieType CategorieType { get; set; }
    }
}
