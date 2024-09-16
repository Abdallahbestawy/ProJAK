using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.AuthenticationDto
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Name is required"),
        MaxLength(500, ErrorMessage = "Name cannot exceed 500 characters"),
        MinLength(6, ErrorMessage = "Name must be at least 6 characters long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required"), MaxLength(11, ErrorMessage = "Phone number cannot exceed 11 digits")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only digits")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required"), DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password format")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password format")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
