using Prova1.Api.Domain;
using Prova1.Api.Infrastructure.Database;

namespace Prova1.Api.Infrastructure.Repositories
{    
    public class UserRepository
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

        public async Task Update(User user)
        {
            User updateUser = _dbcontext.Users!.SingleOrDefault(u => u == user)!;
            updateUser.Password = user.Password;
            await _dbcontext.SaveChangesAsync();
        }

        public User? GetUserByEmail(string email)
        {
            return _dbcontext.Users!.SingleOrDefault(user => user.Email == email);
        }

        public User? GetUserById(Guid id)
        {
            return _dbcontext.Users!.SingleOrDefault(user => user.Id == id);
        }
    }
}