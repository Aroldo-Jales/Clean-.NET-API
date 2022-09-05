using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Common.Interfaces.Persistence
{
    public interface IUserValidationCodeRepository
    {
        Task Add(UserValidationCode userValidationCode);
        Task<UserValidationCode?> GetEmailValidationCodeByUserId(Guid userId);
        Task<UserValidationCode?> GetPhoneNumberValidationCodeByUserId(Guid userId);
        Task RemoveUserConfirmation(UserValidationCode userValidationCode);        
        Task RenewCode(UserValidationCode userValidationCode);
    }
}