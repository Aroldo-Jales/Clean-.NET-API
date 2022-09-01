namespace Prova1.Api.Contracts.Authentication;

public record ChangePasswordRequest(    
    string Id,
    string NewPassword
);