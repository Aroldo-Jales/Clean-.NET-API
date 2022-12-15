using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Common.Interfaces.Persistence.Authentication
{
    public interface IUserValidationCodeRepository
    {
        Task Add(UserValidationCode userValidationCode);
        Task<UserValidationCode?> GetEmailValidationCodeByUser(User user);
        Task<UserValidationCode?> GetPhoneNumberValidationCodeByUser(User user);
        Task RemoveUserConfirmation(UserValidationCode userValidationCode);
        Task RenewCode(UserValidationCode userValidationCode);
    }
}