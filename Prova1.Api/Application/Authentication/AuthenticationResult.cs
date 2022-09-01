using Prova1.Api.Domain;

namespace Prova1.Api.Application.Authentication;

public record AuthenticationResult(
    User user,    
    string Token
);