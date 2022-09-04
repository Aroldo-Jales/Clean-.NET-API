namespace Prova1.Contracts.Authentication;

public record ChangePasswordRequest(    
    string Id,
    string NewPassword
);