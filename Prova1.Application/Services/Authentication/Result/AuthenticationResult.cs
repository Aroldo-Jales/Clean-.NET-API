using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Services.Authentication.Result;

public record AuthenticationResult(
    User user,
    string AcessToken,
    string RefreshToken
);