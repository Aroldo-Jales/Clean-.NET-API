using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Update(User user);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid id);
    }
}