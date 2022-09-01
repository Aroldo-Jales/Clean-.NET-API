namespace Prova1.Api.Contracts.Authentication;

public record SignUpRequest(
    string Name,
    string Email,
    string Telefone,
    string Password
);