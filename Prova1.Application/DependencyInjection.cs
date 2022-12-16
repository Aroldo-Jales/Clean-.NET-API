using Microsoft.Extensions.DependencyInjection;
using Prova1.Application.Common.Interfaces.Services.Authentication.Command;
using Prova1.Application.Common.Interfaces.Services.Authentication.Queries;
using Prova1.Application.Common.Interfaces.Services.Readings.Commands;
using Prova1.Application.Common.Interfaces.Services.Readings.Queries;
using Prova1.Application.Services.Authentication.Commands;
using Prova1.Application.Services.Authentication.Queries;
using Prova1.Application.Services.Readings.Commands;
using Prova1.Application.Services.Readings.Queries;

namespace Prova1.Application.Services.Authentication;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

        services.AddScoped<IReadingsCommandService, ReadingsCommandService>();
        services.AddScoped<IReadingsQueryService, ReadingsQueryService>();

        services.AddScoped<IAnnotationsCommandService, AnnotationsCommandService>();
        services.AddScoped<IAnnotationsQueryService, AnnotationsQueryService>();

        services.AddScoped<ICommentaryCommandService, CommentaryCommandService>();
        services.AddScoped<ICommentaryQueryService, CommentaryQueryService>();

        services.AddScoped<ILikeCommandService, LikeCommandService>();

        return services;
    }
}