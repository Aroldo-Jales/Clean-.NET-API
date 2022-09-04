using Prova1.Application.Services.Authentication;
using Prova1.Infrastructure;
using Prova1.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services    
        .AddApplication()
        .AddInfrastructure(builder.Configuration);            
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
