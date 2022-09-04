using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Prova1.Application.Common.Interfaces.Authentication;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Application.Common.Interfaces.Persistence;

using Prova1.Infrastructure.Authentication;
using Prova1.Infrastructure.Services;
using Prova1.Infrastructure.Persistence;

using Prova1.Infrastructure.Database;

namespace Prova1.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));        
    
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();    
        
        services.AddScoped<IUserRepository, UserRepository>();        
        services.AddScoped<IUserValidationCodeRepository, UserValidationCodeRepository>();        

        return services;
    }
}