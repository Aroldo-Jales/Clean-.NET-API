namespace Prova1.Contracts.Authentication;

public record UserInactiveResponse(
    Guid Id,
    string Name,
    string Email,
    bool ActiveAccount
);