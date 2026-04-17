using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<VirtualMeetingDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("ConnectionStrings"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("ConnectionStrings"))
                )
            );

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}