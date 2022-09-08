using Microsoft.EntityFrameworkCore;
using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Domain.Entities.Authentication;
using Prova1.Infrastructure.Database;

namespace Prova1.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _dbcontext;

        public RefreshTokenRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task Add(RefreshToken rf)
        {// se nao existir no banco adiciona o token se existir atualiza.            

            // posteriormente alterar para mais dispositivos podendo ter mais de um token por usuario

            if(await _dbcontext.RefreshTokens!.Where(r => r.UserId == rf.UserId).AnyAsync())
            {
                await Update(rf);
            }
            else
            {
                await _dbcontext.RefreshTokens!.AddAsync(rf);                
            }   
            await _dbcontext.SaveChangesAsync();             
        }        

        public async Task<RefreshToken> Update(RefreshToken rf)
        {
            RefreshToken updateRefreshToken = (await _dbcontext.RefreshTokens!.Where(r => r.UserId == rf.UserId).SingleOrDefaultAsync())!;

            // MUTABLES
            updateRefreshToken.Token = rf.Token;
            updateRefreshToken.Created = rf.Created;
            updateRefreshToken.Expires = rf.Expires;
            updateRefreshToken.Iat = rf.Iat;

            await _dbcontext.SaveChangesAsync();

            return updateRefreshToken;
        }

        public async Task Remove(RefreshToken rf)
        {
            _dbcontext.RefreshTokens!.Remove(rf);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RefreshToken>> GetAllUsersRefreshTokens(Guid userId)
        {
            IEnumerable<RefreshToken> list = await _dbcontext.RefreshTokens!.Where(r => r.UserId == userId).ToListAsync();
            return list;
        }

        public async Task<RefreshToken?> GetByToken(string token)
        {            
            RefreshToken? refreshToken = await _dbcontext.RefreshTokens!.Where(r => r.Token == token).SingleOrDefaultAsync();
            return refreshToken;
        }

        public async Task RevokeAllTokensFromUser(Guid userId)
        {
            IEnumerable<RefreshToken> list = await GetAllUsersRefreshTokens(userId);
            _dbcontext.RemoveRange(list);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> ValidateIatToken(Guid userId, string iat)
        {
            return await _dbcontext.RefreshTokens!.Where(r => r.UserId == userId && r.Iat == iat).AnyAsync();
        }
    }
}
