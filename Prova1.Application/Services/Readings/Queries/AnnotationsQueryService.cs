using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Application.Common.Interfaces.Services.Readings.Queries;
using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Services.Readings.Queries
{
    public class AnnotationsQueryService : IAnnotationsQueryService
    {
        private readonly IAnnotationsRepository _annotationsRepository;

        public AnnotationsQueryService(IAnnotationsRepository annotationsRepository)
        {
            _annotationsRepository = annotationsRepository;
        }

        public async Task<List<Annotation>> GetAllAnnotionsByReading(Guid readingId)
        {
            return await _annotationsRepository.GetAllAnnotionsByReading(readingId);
        }

        public async Task<Annotation> GetAnnotationById(Guid id)
        {
            return await _annotationsRepository.GetAnnotationById(id);
        }
    }
}
