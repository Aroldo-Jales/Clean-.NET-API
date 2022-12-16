using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Services.Readings.Queries
{
    public interface ICommentaryQueryService
    {
        Task<List<Commentary>> GetCommentariesByReading(Guid readingId);
    }
}
