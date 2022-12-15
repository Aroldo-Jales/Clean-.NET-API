using Prova1.Application.Services.Authentication.Result;

namespace Prova1.Application.Common.Interfaces.Services.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {
        Task<AuthenticationResult> SignIn(string email, string password);
        AuthenticationResult RefreshToken(string refreshtoken, string acesstoken);
        Task<UserStatusResult> ConfirmEmail(Guid userId, int code);
        Task<UserStatusResult> ConfirmPhoneNumber(Guid userId, int code);   
    }
}