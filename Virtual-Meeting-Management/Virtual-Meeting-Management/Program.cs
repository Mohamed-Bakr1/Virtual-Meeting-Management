using Application;
using Application.Interfaces;
using Infrastructure.DI;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Virtual_Meeting_Management.Web;

namespace Virtual_Meeting_Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register Application Layer
            builder.Services.AddApplicationLayer(builder.Configuration);

            // Register Infrastructure
            builder.Services.AddInfrastructureLayer(builder.Configuration);

            


            builder.Services.AddScoped<IUserContextService, UserContextService>();


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                // Add the JWT Bearer definition
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and the JWT token."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                //var basePath = AppContext.BaseDirectory;

                //// API project XML
                //var apiXml = Path.Combine(basePath, "E-Learning.xml");
                //if (File.Exists(apiXml))
                //    c.IncludeXmlComments(apiXml, true);

                //// Application project XML
                //var appXmlPath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Application\bin\Debug\net8.0\Application.xml");
                //appXmlPath = Path.GetFullPath(appXmlPath);
                //if (File.Exists(appXmlPath))
                //    c.IncludeXmlComments(appXmlPath, true);

                //// Domain project XML (optional)
                //var domainXmlPath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Domain\bin\Debug\net8.0\Domain.xml");
                //domainXmlPath = Path.GetFullPath(domainXmlPath);
                //if (File.Exists(domainXmlPath))
                //    c.IncludeXmlComments(domainXmlPath, true);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}