using Prova1.Api.Middlewares.Authentication;
using Prova1.Api.Middlewares;
using Prova1.Application.Services.Authentication;
using Prova1.Infrastructure;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
}

WebApplication? app = builder.Build();
{

    app.UseHttpsRedirection();

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseWhen(context => context.Request.Path.StartsWithSegments("/readings"), appBuilder =>
    {
        appBuilder.UseMiddleware<AuthMiddleware>();

    });

    app.MapControllers();
    app.Run();
}
