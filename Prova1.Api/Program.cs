using Prova1.Application.Services.Authentication;
using Prova1.Infrastructure;
using Prova1.Api.Middlewares;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Application.Services.Users;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);        

    builder.Services.AddControllers();
}

var app = builder.Build();
{   

    app.UseHttpsRedirection();

    app.UseMiddleware<ErrorHandlingMiddleware>();    

    app.UseWhen(context => context.Request.Path.StartsWithSegments("/readings"), appBuilder => {
        appBuilder.UseMiddleware<JwtAuthMiddleware>();
        
    });
    
    app.MapControllers();
    app.Run();
}
