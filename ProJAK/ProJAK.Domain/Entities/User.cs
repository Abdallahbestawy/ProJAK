using Microsoft.AspNetCore.Identity;
using ProJAK.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProJAK.Domain.Entities
{
    public class User : IdentityUser
    {
        [Required, MaxLength(500)]
        public string Name { get; set; }
        [Required, MaxLength(11)]
        [RegularExpression(@"^\d+$")]
        public string PhoneNumber { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public UserType UserType { get; set; }
        [Required]
        public string Address { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
