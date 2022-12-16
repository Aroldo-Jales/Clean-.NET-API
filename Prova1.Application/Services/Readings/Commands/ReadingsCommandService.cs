using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Application.Common.Interfaces.Services.Readings.Commands;
using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Services.Readings.Commands
{
    public class ReadingsCommandService : IReadingsCommandService
    {
        private readonly IReadingsRepository _readingsRepository;

        public ReadingsCommandService(IReadingsRepository readingsRepository)
        {
            _readingsRepository = readingsRepository;
        }

        public async Task<Reading> AddReadingAsync(Reading reading)
        {
            return await _readingsRepository.AddReadingAsync(reading);            
        }

        public async Task RemoveReadingAsync(Guid readingId)
        {
            await _readingsRepository.RemoveReadingAsync(readingId);
        }

        public async Task<Reading> UpdateReadingAsync(Reading reading)
        {
            return await _readingsRepository.UpdateReadingAsync(reading);
        }
    }
}