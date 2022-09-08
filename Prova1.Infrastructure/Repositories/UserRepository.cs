using Microsoft.EntityFrameworkCore;
using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Domain.Entities.Authentication;
using Prova1.Infrastructure.Database;

namespace Prova1.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbcontext;

        public UserRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task Add(User user)
        {
            await _dbcontext.Users!.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<User> Update(User user)
        {
            User updateUser = _dbcontext.Users!.SingleOrDefault(u => u == user)!;

            // MUTABLES
            updateUser.Name = user.Name;
            updateUser.PhoneNumber = user.PhoneNumber;
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.ActiveAccount = user.ActiveAccount;

            await _dbcontext.SaveChangesAsync();

            return updateUser;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbcontext.Users!.Where(user => user.Email == email).SingleOrDefaultAsync();
        }

        public async Task<bool> UserPhoneNumberAlreadyExist(string phoneNumber)
        {
            return await _dbcontext.Users!.AnyAsync(user => user.PhoneNumber == phoneNumber);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _dbcontext.Users!.Where(user => user.Id == id).SingleOrDefaultAsync();
        }
    }
}