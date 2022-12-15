using Prova1.Application.Services.Authentication.Result;

namespace Prova1.Application.Common.Interfaces.Services.Authentication.Command
{
    public interface IAuthenticationCommandService
    {
        Task<UserStatusResult> SignUp(string name, string email, string password);
        Task<AuthenticationResult> ChangePassword(Guid id, string password);
        Task AddPhoneNumber(Guid userId, string phoneNumber);
    }
}