using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> GetUserById(Guid id);
    }
}