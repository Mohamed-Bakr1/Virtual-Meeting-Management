using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AuthenticationDto
{
    public class RegisterDto
    {
        #region Documentation
        /// <summary>
        /// The user's Name used for Regsiter and show it in his profile.
        /// </summary>
        /// <example>Mohamed Bakr</example> 
        #endregion
        [Required(ErrorMessage = "The Name field is required.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "The Name must contain only letters (Arabic or English).")]
        [MaxLength(30, ErrorMessage = "The Name should be not more than 30 character")]
        [DefaultValue("Mohamed Bakr")]
        public string Name { get; set; }

        #region Documentation
        /// <summary>
        /// The user's email address used for Register and verify the user.
        /// </summary>
        /// <example>cabtinmohamed32@gmail.com</example> 
        #endregion
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        [DefaultValue("cabtinmohamed32@gmail.com")]
        public string Email { get; set; }

        #region Documentation
        /// <summary>
        /// The user's password, created by user to use it in login in the future.
        /// </summary>
        /// <example>ab123456</example> 
        #endregion
        [Required(ErrorMessage = "The Password field is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 8 characters.")]
        [DefaultValue("123456")]
        public string Password { get; set; }

        #region Documentation
        /// <summary>
        /// User confirm password to ensure he is write the password in the right way as he want.
        /// </summary>
        /// <example>ab123456</example> 
        #endregion
        [Required(ErrorMessage = "The Password field is required.")]
        [Compare("Password", ErrorMessage = "ConfirmPassword must be the same with Password.")]
        [DefaultValue("123456")]
        public string ConfirmPassword { get; set; }

        [DefaultValue(true)]
        public bool Gender { get; set; }
    }
}