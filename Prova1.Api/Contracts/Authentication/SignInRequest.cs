namespace Prova1.Api.Contracts.Authentication;

public record SignInRequest(    
    string Email,
    string Password
);