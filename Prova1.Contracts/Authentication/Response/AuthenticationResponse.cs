namespace Prova1.Contracts.Authentication.Response;

public record AuthenticationResponse(
    Guid Id,
    string Name,
    string Email,    
    string Telefone,    
    string AcessToken,
    string RefreshToken
);