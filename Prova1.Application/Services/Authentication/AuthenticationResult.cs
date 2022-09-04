using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Services.Authentication;

public record AuthenticationResult(
    User user,    
    string Token
);