
namespace Prova1.Api.Application.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult SignIn(string email, string password);
        Task<AuthenticationResult> SignUp(string Name, string Email, string Telefone, string Password);
        Task<AuthenticationResult> ChangePassword(Guid id, string password);
    }
}