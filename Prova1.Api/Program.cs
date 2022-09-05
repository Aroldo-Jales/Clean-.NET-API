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

    app.UseHttpsRedirection();

    app.UseAuthentication(); // jwt bearer validation
    app.UseAuthorization(); // user can acess endpoints

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.MapControllers();
    app.Run();
}
