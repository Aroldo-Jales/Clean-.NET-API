using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Services.Readings.Commands
{
    public interface IAnnotationsCommandService
    {
        Task<Annotation> AddAnnotationAsync(Annotation annotation);
        Task<Annotation> UpdateAnnotationAsync(Annotation annotation);
        Task RemoveAnnotationAsync(Guid id);
    }
}
