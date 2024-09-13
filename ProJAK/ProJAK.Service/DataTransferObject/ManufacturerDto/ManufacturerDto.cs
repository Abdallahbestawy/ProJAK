using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.ManufacturerDto
{
    public class ManufacturerDto
    {
        public Guid Id { get; set; }
        [Required, MaxLength(250)]
        public string Name { get; set; }
    }
}
