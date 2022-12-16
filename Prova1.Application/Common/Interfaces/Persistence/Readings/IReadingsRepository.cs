using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Persistence.Readings
{
    public interface IReadingsRepository
    {
        Task<Reading> GetReadingByIdAsync(Guid readingId);
        Task<List<Reading>> GetAllReadingsAsync(int count);
        Task<List<Reading>> GetReadingsByUserAsync(Guid userId);
        Task<Reading> AddReadingAsync(Reading reading);                
        Task<Reading> UpdateReadingAsync(Reading reading);
        Task RemoveReadingAsync(Guid readingId);
    }
}
