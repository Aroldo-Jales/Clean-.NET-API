namespace Prova1.Contracts.Authentication.Request;

public record AddPhoneNumberRequest(    
    string userId,
    string phoneNumber
);