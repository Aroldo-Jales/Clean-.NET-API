using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Persistence.Readings
{
    public interface IAnnotationsRepository
    {
        Task<Annotation> GetAnnotationById(Guid id);
        Task<List<Annotation>> GetAllAnnotionsByReading(Guid readingId);
        Task<Annotation> AddAnnotationAsync(Annotation annotation);
        Task<Annotation> UpdateAnnotationAsync(Annotation annotation);
        Task RemoveAnnotationAsync(Guid id);        
    }
}
