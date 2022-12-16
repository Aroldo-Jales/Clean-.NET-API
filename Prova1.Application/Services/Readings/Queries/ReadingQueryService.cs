
using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Application.Common.Interfaces.Services.Readings.Queries;
using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Services.Readings.Queries
{
    public class ReadingsQueryService : IReadingsQueryService
    {
        private readonly IReadingsRepository _readingsRepository;

        public ReadingsQueryService(IReadingsRepository readingsRepository)
        {
            _readingsRepository = readingsRepository;
        }

        public async Task<Reading> GetReadingById(Guid id)
        {
            return await _readingsRepository.GetReadingByIdAsync(id);
        }

        public async Task<List<Reading>> AllReadingsAsync(int count)
        {
            return await _readingsRepository.GetAllReadingsAsync(count);
        }

        public async Task<List<Reading>> AllReadingsByUserAsync(Guid userId)
        {
            return await _readingsRepository.GetReadingsByUserAsync(userId);
        }        
    }
}