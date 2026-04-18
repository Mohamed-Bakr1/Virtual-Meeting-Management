using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePicturePath { get; set; }
    }
}