namespace Prova1.Contracts.Authentication.Request;

public record SignUpRequest(
    string Name,    
    string Email,    
    string Password
);