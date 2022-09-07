using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Prova1.Application.Common.Interfaces.Authentication;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Infrastructure.Authentication;
using Prova1.Infrastructure.Services;
using Prova1.Infrastructure.Repositories;

using Prova1.Infrastructure.Database;

namespace Prova1.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>();

        services.AddAuth(configuration);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();    
        
        services.AddScoped<IUserRepository, UserRepository>();        
        services.AddScoped<IUserValidationCodeRepository, UserValidationCodeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings); // json to jwtsettings

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<ITokensUtils, TokensUtils>();

        //services.AddAuthentication(option =>
        //{
        //    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //});
        /*
        services.AddAuthentication(
            defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))                
            });
        */

        return services;
    }
}