using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Common.Interfaces.Persistence
{
    public interface IUserValidationCodeRepository
    {
        Task Add(UserValidationCode userValidationCode);
        Task<List<UserValidationCode>?> GetByUser(User user);                        
        Task Delete(UserValidationCode userValidationCode);        
        Task RenewCode(UserValidationCode userValidationCode);
    }
}