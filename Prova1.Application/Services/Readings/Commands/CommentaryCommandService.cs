using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Application.Common.Interfaces.Services.Readings.Commands;
using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Services.Readings.Commands
{
    public class CommentaryCommandService : ICommentaryCommandService
    {
        private readonly ICommentaryRepository _commentaryRepository;

        public CommentaryCommandService(ICommentaryRepository commentaryRepository)
        {
            _commentaryRepository = commentaryRepository;
        }

        public async Task<Commentary> AddCommentaryAsync(Commentary commentary)
        {
            return await _commentaryRepository.AddCommentaryAsync(commentary);
        }

        public async Task<Commentary> UpdateCommentaryAsync(Commentary commentary)
        {
            return await _commentaryRepository.UpdateCommentaryAsync(commentary);
        }

        public async Task RemoveCommentaryAsync(Guid id)
        {
            await _commentaryRepository.RemoveCommentaryAsync(id);
        }
    }
}
