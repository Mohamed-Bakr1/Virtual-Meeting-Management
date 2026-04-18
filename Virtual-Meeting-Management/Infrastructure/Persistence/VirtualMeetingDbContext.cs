using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class VirtualMeetingDbContext : IdentityDbContext<ApplicationUser>
    {
        public VirtualMeetingDbContext(DbContextOptions<VirtualMeetingDbContext> options) : base(options)
        {
        }

        protected virtual void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}