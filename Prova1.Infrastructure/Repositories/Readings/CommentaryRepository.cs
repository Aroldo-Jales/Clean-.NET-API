using Microsoft.EntityFrameworkCore;
using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Domain.Entities.Readings;
using Prova1.Infrastructure.Database;

namespace Prova1.Infrastructure.Repositories.Readings
{
    public class CommentaryRepository : ICommentaryRepository
    {
        private readonly AppDbContext _dbcontext;

        public CommentaryRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<Commentary>> GetCommentariesByReading(Guid readingId)
        {
            return await _dbcontext.Commentaries!.Where(c => c.ReadingId == readingId).ToListAsync();
        }

        public async Task<Commentary> AddCommentaryAsync(Commentary commentary)
        {
            await _dbcontext.Commentaries!.AddAsync(commentary);
            await _dbcontext.SaveChangesAsync();
            return commentary;
        }

        public async Task<Commentary> UpdateCommentaryAsync(Commentary commentary)
        {
            Commentary updateCommentary = await _dbcontext.Commentaries!.FindAsync(commentary.Id);

            updateCommentary.Content = commentary.Content;

            await _dbcontext.SaveChangesAsync();
            return updateCommentary;
        }

        public async Task RemoveCommentaryAsync(Guid id)
        {
            _dbcontext.Commentaries!.Remove(await _dbcontext.Commentaries!.FindAsync(id));
            await _dbcontext.SaveChangesAsync();
        }
    }
}
