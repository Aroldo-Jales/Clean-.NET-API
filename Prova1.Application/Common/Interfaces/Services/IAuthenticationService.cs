using Prova1.Application.Services.Authentication.Result;

namespace Prova1.Application.Common.Interfaces.Services;
public interface IAuthenticationService
{
    Task<AuthenticationResult> SignIn(string email, string password);
    Task<UserStatusResult> SignUp(string name, string email, string password);    
    Task<AuthenticationResult> ChangePassword(Guid id, string password);    
    Task<UserStatusResult> ConfirmEmail(Guid userId, int code);
    Task AddPhoneNumber(Guid userId, string phoneNumber);
    Task<UserStatusResult> ConfirmPhoneNumber(Guid userId, int code);
}