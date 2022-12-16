using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Services.Readings.Queries
{
    public interface IAnnotationsQueryService
    {
        Task<Annotation> GetAnnotationById(Guid id);
        Task<List<Annotation>> GetAllAnnotionsByReading(Guid readingId);
    }
}
