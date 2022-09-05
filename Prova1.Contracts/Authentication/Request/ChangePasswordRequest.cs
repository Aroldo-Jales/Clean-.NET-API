namespace Prova1.Contracts.Authentication.Request;

public record ChangePasswordRequest(    
    string userId,
    string NewPassword
);