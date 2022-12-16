using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Application.Common.Interfaces.Services.Readings.Commands;
using Annotation = Prova1.Domain.Entities.Readings.Annotation;

namespace Prova1.Application.Services.Readings.Commands
{
    public class AnnotationsCommandService : IAnnotationsCommandService
    {
        private readonly IAnnotationsRepository _annotationsRepository;

        public AnnotationsCommandService(IAnnotationsRepository annotationsRepository)
        {
            _annotationsRepository = annotationsRepository;
        }

        public async Task<Annotation> AddAnnotationAsync(Annotation annotation)
        {
            return await _annotationsRepository.AddAnnotationAsync(annotation);
        }

        public async Task<Annotation> UpdateAnnotationAsync(Annotation annotation)
        {
            return await _annotationsRepository.UpdateAnnotationAsync(annotation);
        }

        public async Task RemoveAnnotationAsync(Guid id)
        {
            await _annotationsRepository.RemoveAnnotationAsync(id);
        }
    }
}
