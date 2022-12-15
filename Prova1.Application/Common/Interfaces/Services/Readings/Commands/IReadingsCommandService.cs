using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Services.Readings.Commands
{
    public interface IReadingsCommandService
    {
        Task AddReadingAsync(Reading reading);        
        Task RemoveReadingAsync(Guid readingId);
        Task UpdateReadingAsync(Guid reading);
    }
}