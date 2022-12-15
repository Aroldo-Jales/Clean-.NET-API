using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Prova1.Application.Common.Interfaces.Utils.Authentication;
using Prova1.Application.Common.Interfaces.Persistence.Authentication;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Infrastructure.Authentication.Utils;
using Prova1.Infrastructure.Authentication;
using Prova1.Infrastructure.Database;
using Prova1.Infrastructure.Repositories;
using Prova1.Infrastructure.Services;

namespace Prova1.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>();

        services.AddAuth(configuration);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserValidationCodeRepository, UserValidationCodeRepository>();        

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        JwtSettings? jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings); // json to jwtsettings

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<ITokensUtils, TokensUtils>();

        return services;
    }
}