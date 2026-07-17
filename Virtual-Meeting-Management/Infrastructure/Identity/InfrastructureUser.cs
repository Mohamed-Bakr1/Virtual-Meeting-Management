using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class InfrastructureUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public string? ProfilePicturePath { get; set; }
    }
}