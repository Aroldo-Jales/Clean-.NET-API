using Prova1.Domain.Entities.Readings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova1.Application.Common.Interfaces.Persistence.Readings
{
    public interface IAnnotationsRepository
    {
        Task<Annotation> AddAnnotationAsync();
        Task<Annotation> UpdateAnnotationAsync();
        Task RemoveAnnotationAsync();
        Task<List<Annotation>> GetAllAnnotionsByReading(Guid readingId);
    }
}
