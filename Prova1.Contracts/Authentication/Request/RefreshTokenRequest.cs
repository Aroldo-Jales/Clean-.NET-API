namespace Prova1.Contracts.Authentication.Request;

public record RefreshTokenRequest(
    string RefreshToken,
    string AccessToken
);