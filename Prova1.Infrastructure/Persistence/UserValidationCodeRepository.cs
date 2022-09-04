using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Domain.Entities.Authentication;
using Prova1.Infrastructure.Database;
using System.Data.Entity;

namespace Prova1.Infrastructure.Persistence
{
    public class UserValidationCodeRepository : IUserValidationCodeRepository
    {
        private readonly AppDbContext _dbcontext;
        
        public UserValidationCodeRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Add(UserValidationCode userValidationCode)
        {
            await _dbcontext.UserValidationCode!.AddAsync(userValidationCode);
            await _dbcontext.SaveChangesAsync(); 
        }

        public async Task Delete(UserValidationCode userValidationCode)
        {
            _dbcontext.UserValidationCode!.Remove(userValidationCode);
            await _dbcontext.SaveChangesAsync(); 
        }

        public async Task<List<UserValidationCode>?> GetByUser(User user)
        {
            return await (
                from u in _dbcontext.UserValidationCode
                where u.UserId == user.Id
                select u).ToListAsync();            
        }

        public async Task RenewCode(UserValidationCode userValidationCode)
        {
            UserValidationCode renewUserValidationCode = await _dbcontext.UserValidationCode!.SingleOrDefaultAsync(u => u == userValidationCode)!;
            renewUserValidationCode.RenewUserValidationCode();
            await _dbcontext.SaveChangesAsync();
        }
    }
}