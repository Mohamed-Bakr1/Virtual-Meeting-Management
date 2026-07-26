using Application.DTOs.AuthenticationDto;
using Application.DTOs.StreamingDto;
using Application.Interfaces;
using Application.UseCases.AuthenticationUseCase;
using Application.UseCases.StreamingUseCase;
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
            services.AddScoped<IUseCase<LoginDto, Result<ValidLoginDto>>, LoginUseCase>();
            services.AddScoped<IUseCase<CreateMeetingDto, Result>, CreateMeetingUseCase>();

            return services;
        }
    }
}