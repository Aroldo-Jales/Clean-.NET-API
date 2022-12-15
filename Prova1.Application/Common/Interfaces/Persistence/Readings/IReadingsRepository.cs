using Prova1.Domain.Entities.Reading;

namespace Prova1.Application.Common.Interfaces.Persistence.Readings
{
    public interface IReadingsRepository
    {
        Task AddReading(Reading reading);
        List<Reading> AllReadings();
        Task RemoveReading(Guid readingId);
        Task UpdateReading(Guid reading);
    }
}
