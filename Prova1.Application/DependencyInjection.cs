using Microsoft.Extensions.DependencyInjection;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Application.Services.Users;

namespace Prova1.Application.Services.Authentication;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}