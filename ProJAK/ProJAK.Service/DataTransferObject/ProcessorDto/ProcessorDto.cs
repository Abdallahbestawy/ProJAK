using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.ProcessorDto
{
    public class ProcessorDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Name field is required."),
       MaxLength(500, ErrorMessage = "Name must be at least 500 characters long")]
        public string Name { get; set; }
        public Guid ManufacturerId { get; set; }
    }
}
