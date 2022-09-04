namespace Prova1.Contracts.Authentication;

public record SignUpRequest(
    string Name,    
    string Email,
    string PhoneNumber,
    string Password
);