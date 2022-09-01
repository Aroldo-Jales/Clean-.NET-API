using Prova1.Api.Infrastructure.Database;
using Prova1.Api.Infrastructure.Authentication;
using Prova1.Api.Infrastructure.Repositories;

namespace Prova1.Api.Infrastructure
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddSingleton<JwtTokenGenerator>();
            services.AddScoped<UserRepository>();        

            return services;
        }
    }
}