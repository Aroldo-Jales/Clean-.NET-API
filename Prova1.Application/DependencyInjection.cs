using Microsoft.Extensions.DependencyInjection;
using Prova1.Application.Common.Interfaces.Services.Authentication.Command;
using Prova1.Application.Common.Interfaces.Services.Authentication.Queries;
using Prova1.Application.Services.Authentication.Commands;
using Prova1.Application.Services.Authentication.Queries;

namespace Prova1.Application.Services.Authentication;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

        return services;
    }
}