using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AuthenticationDto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        [DefaultValue("cabtinmohamed32@gmail.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DefaultValue("123456")]
        public string Password { get; set; }
    }
}