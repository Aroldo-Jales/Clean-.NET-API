namespace Prova1.Contracts.Authentication;

public record SignInRequest(    
    string Email,
    string Password
);