using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            if (await _userRepository.GetUserById(id) is User user)
            {
                return user;
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}