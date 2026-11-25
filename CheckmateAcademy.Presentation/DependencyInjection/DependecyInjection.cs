using CheckmateAcademy.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheckmateAcademy.Presentation.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<AuthenticationOptions>(
                configuration.GetSection("Authentication"));

            return services;
        }
    }
}
