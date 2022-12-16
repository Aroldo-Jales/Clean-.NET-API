using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Domain.Entities.Readings;
using Prova1.Infrastructure.Database;

namespace Prova1.Infrastructure.Repositories.Readings
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _dbContext;

        public LikeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddLikeAsync(Like like)
        {
            await _dbContext.Likes!.AddAsync(like);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveLike(Guid id)
        {
            _dbContext.Remove(await _dbContext.Likes!.FindAsync(id));
            await _dbContext.SaveChangesAsync();
        }


    }
}
