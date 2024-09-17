using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.ScreenDto
{
    public class ScreenDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Resolution field is required."),
        MaxLength(50, ErrorMessage = "Resolution must be at least 50 characters long")]
        public string Resolution { get; set; }
        [Required(ErrorMessage = "The PanelType field is required."),
       MaxLength(50, ErrorMessage = "PanelType must be at least 50 characters long")]
        public string PanelType { get; set; }
        public int RefreshRate { get; set; }
    }
}
