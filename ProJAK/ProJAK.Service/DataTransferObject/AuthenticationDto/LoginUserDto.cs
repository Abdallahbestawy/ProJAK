using System.ComponentModel.DataAnnotations;

namespace ProJAK.Service.DataTransferObject.AuthenticationDto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Email is required."),
        DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        //DataType(DataType.Password, ErrorMessage = "Invalid password format.")]
        public string Password { get; set; }
    }
}
