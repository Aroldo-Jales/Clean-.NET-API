using Microsoft.EntityFrameworkCore;
using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Infrastructure.Database;
using Annotation = Prova1.Domain.Entities.Readings.Annotation;

namespace Prova1.Infrastructure.Repositories.Readings
{
    public class AnnotationsRepository : IAnnotationsRepository
    {
        private readonly AppDbContext _dbcontext;

        public AnnotationsRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Annotation> GetAnnotationById(Guid id)
        {
            return await _dbcontext.Annotations!.FindAsync(id);
        }

        public async Task<List<Annotation>> GetAllAnnotionsByReading(Guid readingId)
        {
            return await _dbcontext.Annotations!.Where(a => a.ReadingId == readingId).ToListAsync();
        }

        public async Task<Annotation> AddAnnotationAsync(Annotation annotation)
        {
            await _dbcontext.Annotations!.AddAsync(annotation);
            await _dbcontext.SaveChangesAsync();
            return annotation;
        }

        public async Task<Annotation> UpdateAnnotationAsync(Annotation annotation)
        {
            Annotation updateAnnotation = await GetAnnotationById(annotation.Id);

            updateAnnotation.Content = annotation.Content;
            updateAnnotation.Page = annotation.Page;

            await _dbcontext.SaveChangesAsync();
            return updateAnnotation;
        }

        public async Task RemoveAnnotationAsync(Guid id)
        {
            _dbcontext.Remove(await GetAnnotationById(id));
            await _dbcontext.SaveChangesAsync();
        }
    }
}
