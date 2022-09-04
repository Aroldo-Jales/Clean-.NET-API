namespace Prova1.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> SignIn(string email, string password);
    Task<InactiveUserResult> SignUp(string name, string email, string telefone, string password);
    Task<AuthenticationResult> ChangePassword(Guid id, string password);
}