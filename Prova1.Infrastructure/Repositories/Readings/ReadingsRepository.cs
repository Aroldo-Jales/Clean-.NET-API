using Microsoft.EntityFrameworkCore;
using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Domain.Entities.Readings;
using Prova1.Infrastructure.Database;
using System.Data.Entity;

namespace Prova1.Infrastructure.Repositories.Readings
{
    public class ReadingsRepository : IReadingsRepository
    {
        private readonly AppDbContext _dbcontext;

        public ReadingsRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Reading> GetReadingByIdAsync(Guid readingId)
        {
            return await _dbcontext.Readings!.FindAsync(readingId);
        }        
    
        public async Task<List<Reading>> GetAllReadingsAsync(int count)
        {
            return _dbcontext.Readings!.Take(count).ToList();
        }

        public async Task<List<Reading>> GetReadingsByUserAsync(Guid userId)
        {
            return await _dbcontext.Readings!.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<Reading> AddReadingAsync(Reading reading)
        {
            await _dbcontext.Readings!.AddAsync(reading);
            await _dbcontext.SaveChangesAsync();
            return reading;
        }

        public async Task<Reading> UpdateReadingAsync(Reading reading)
        {
            Reading updateReading = await GetReadingByIdAsync(reading.Id);

            updateReading.Title = reading.Title;
            updateReading.SubTitle = reading.SubTitle;            
            updateReading.Stopped = reading.Stopped;
            updateReading.Completed = reading.Completed;
            updateReading.CurrentPage = reading.CurrentPage;

            await _dbcontext.SaveChangesAsync();
            return updateReading;
        }

        public async Task RemoveReadingAsync(Guid readingId)
        {
            _dbcontext.Readings!.Remove(await GetReadingByIdAsync(readingId));
            _dbcontext.Annotations!.RemoveRange(_dbcontext.Annotations!.Where(a => a.ReadingId == readingId).ToList());
            _dbcontext.Commentaries!.RemoveRange(_dbcontext.Commentaries!.Where(c => c.ReadingId == readingId).ToList());
            _dbcontext.Likes!.RemoveRange(_dbcontext.Likes!.Where(l => l.ReadingId == readingId).ToList());

            await _dbcontext.SaveChangesAsync();
        }
    }
}
