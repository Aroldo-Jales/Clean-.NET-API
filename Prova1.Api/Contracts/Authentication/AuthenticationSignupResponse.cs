namespace Prova1.Api.Contracts.Authentication;

public record AuthenticationSignupResponse(
    Guid Id,
    string Name,   
    string Email,   
    string Telefone, 
    string Token
);