using Application.Interfaces;
using Application.Interfaces.IUser;
using Domain.Interfaces;
using Infrastructure.Identity;
using Infrastructure.LiveKit;
using Infrastructure.Persistence;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<VirtualMeetingDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
                )
            );

            // Register ASP.NET Core Identity with the custom InfrastructureUser class
            services.AddIdentity<InfrastructureUser, IdentityRole>()
                .AddEntityFrameworkStores<VirtualMeetingDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            // Register services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<ILiveKitService, LiveKitService>();
            services.Configure<LiveKitSettings>(configuration.GetSection("LiveKit"));



            // Identity Configuration
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "jwt";
                options.DefaultChallengeScheme = "jwt";
            }).AddJwtBearer("jwt", option =>
            {
                var SecretKey = configuration.GetSection("SecretKey").Value;
                var SecretKeyByte = Encoding.UTF8.GetBytes(SecretKey);
                SecurityKey securityKey = new SymmetricSecurityKey(SecretKeyByte);

                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    //i added that
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.NameIdentifier
                };

                option.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/chat"))
                        {
                            context.Token = accessToken;
                        }
                        return System.Threading.Tasks.Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}