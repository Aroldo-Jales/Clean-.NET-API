using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Services.Readings.Commands
{
    public interface IReadingsCommandService
    {
        Task<Reading> AddReadingAsync(Reading reading);                
        Task<Reading> UpdateReadingAsync(Reading reading);
        Task RemoveReadingAsync(Guid readingId);
    }
}