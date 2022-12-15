using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Persistence.Readings
{
    public interface IReadingsRepository
    {
        Task<Reading> GetReadingByAsync(Guid readingId);
        Task AddReadingAsync(Reading reading);        
        Task RemoveReadingAsync(Reading reading);
        Task<Reading> UpdateReadingAsync(Reading reading);
        Task<List<Reading>> AllReadingsAsync(int count);
        Task<List<Reading>> AllReadingsByUserAsync(Guid userId);        
    }
}
