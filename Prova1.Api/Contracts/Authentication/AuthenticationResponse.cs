namespace Prova1.Api.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string Name,   
    string Email,   
    string Telefone, 
    string Token
);