namespace Prova1.Contracts.Authentication.Response;

public record AuthenticationResponse(
    Guid Id,
    string Name,
    string Email,    
    string AcessToken,
    string RefreshToken
);