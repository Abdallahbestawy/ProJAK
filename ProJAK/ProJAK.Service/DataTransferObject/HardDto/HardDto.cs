using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.HardDto
{
    public class HardDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Name field is required."),
        MaxLength(250, ErrorMessage = "Name must be at least 6 characters long")]
        public string Name { get; set; }
    }
}
