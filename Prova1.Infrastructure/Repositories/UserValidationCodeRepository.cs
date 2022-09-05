using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Domain.Entities.Authentication;
using Prova1.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Prova1.Infrastructure.Repositories
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

        public async Task RemoveUserConfirmation(UserValidationCode userValidationCode)
        {
            _dbcontext.UserValidationCode!.Remove(userValidationCode);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<UserValidationCode?> GetEmailValidationCodeByUserId(Guid userId)
        {            
            return await _dbcontext.UserValidationCode!.Where(u => u.UserId == userId && u.Type.Contains("@")).SingleOrDefaultAsync();
        }

        public async Task<UserValidationCode?> GetPhoneNumberValidationCodeByUserId(Guid userId)
        {
            return await _dbcontext.UserValidationCode!.Where(u => u.UserId == userId && !u.Type.Contains("@")).SingleOrDefaultAsync();
        }

        public async Task RenewCode(UserValidationCode userValidationCode)
        {
            UserValidationCode? renewUserValidationCode = await _dbcontext.UserValidationCode!.SingleOrDefaultAsync(u => u == userValidationCode);
            renewUserValidationCode!.RenewUserValidationCode();
            await _dbcontext.SaveChangesAsync();
        }
    }
}