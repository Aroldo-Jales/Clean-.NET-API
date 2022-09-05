namespace Prova1.Contracts.Authentication.Request;

public record ConfirmationRequest(    
    string userId,
    int code
);