using Application.DTOs;
using Application.Interfaces;
using Application.UseCases;
using Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationLayerRegistration
    {
        public static IServiceCollection AddApplicationLayer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUseCase<RegisterDto, Result>, RegisterUseCase>();

            return services;
        }
    }
}