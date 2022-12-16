using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Application.Common.Interfaces.Services.Readings.Queries;
using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Services.Readings.Queries
{
    public class CommentaryQueryService : ICommentaryQueryService
    {
        private readonly ICommentaryRepository _commentaryRepository;

        public CommentaryQueryService(ICommentaryRepository commentaryRepository)
        {
            _commentaryRepository = commentaryRepository;
        }

        public async Task<List<Commentary>> GetCommentariesByReading(Guid readingId)
        {
            return await _commentaryRepository.GetCommentariesByReading(readingId);
        }
    }
}
