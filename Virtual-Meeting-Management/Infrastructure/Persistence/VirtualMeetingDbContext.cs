using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class VirtualMeetingDbContext : IdentityDbContext
    {
        public VirtualMeetingDbContext(DbContextOptions options) : base(options)
        {
        }

        protected virtual void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}