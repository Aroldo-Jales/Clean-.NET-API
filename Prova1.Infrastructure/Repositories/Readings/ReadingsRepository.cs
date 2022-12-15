using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Domain.Entities.Authentication;
using Prova1.Domain.Entities.Readings;
using Prova1.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova1.Infrastructure.Repositories.Readings
{
    public class ReadingsRepository : IReadingsRepository
    {
        private readonly AppDbContext _dbcontext;

        public ReadingsRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Reading> GetReadingByAsync(Guid readingId)
        {
            return await _dbcontext.Readings!.FindAsync(readingId);
        }

        public async Task AddReadingAsync(Reading reading)
        {
            await _dbcontext.Readings!.AddAsync(reading);
            await _dbcontext.SaveChangesAsync();
        }        

        public async Task<List<Reading>> AllReadingsAsync(int count)
        {
            return await _dbcontext.Readings!.Take(count).ToListAsync();
        }

        public async Task<List<Reading>> AllReadingsByUserAsync(Guid userId)
        {
            return await _dbcontext.Readings!.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task RemoveReadingAsync(Reading reading)
        {
            _dbcontext.Remove(reading);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<Reading> UpdateReadingAsync(Reading reading)
        {
            Reading updateReading = await GetReadingByAsync(reading.Id);

            // MUTABLES
            updateReading.Title = reading.Title;
            updateReading.SubTitle = reading.SubTitle;
            updateReading.TagId = reading.TagId;
            updateReading.Stopped = reading.Stopped;
            updateReading.Completed = reading.Completed;
            updateReading.CurrentPage = reading.CurrentPage;

            await _dbcontext.SaveChangesAsync();
            return updateReading;            
        }
    }
}
