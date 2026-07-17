using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class VirtualMeetingDbContext : IdentityDbContext<InfrastructureUser>
    {
        public VirtualMeetingDbContext(DbContextOptions<VirtualMeetingDbContext> options) : base(options)
        {
        }

        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Meeting>(entity =>
            {
                // Configure HostId as a standard column. 
                // Notice there is NO .HasOne() or .HasForeignKey() relationship mapping!
                entity.Property(m => m.HostId)
                      .IsRequired()
                      .HasMaxLength(450); // Note: ASP.NET Identity string IDs are 450 characters by default.

                // 💡 PRO-TIP: Add an index on HostId. 
                // Because you don't have a Foreign Key, the database won't automatically index this column.
                // You will likely query "Get all meetings for this user", so an index is highly recommended for performance.
                entity.HasIndex(m => m.HostId);
            });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        }
    }
}