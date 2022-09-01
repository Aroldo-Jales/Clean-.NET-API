using Prova1.Api.Application.Authentication;

namespace Prova1.Api.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationService>();
            return services;
        }
    }
}