using Prova1.Application.Common.Interfaces.Persistence.Readings;
using Prova1.Application.Common.Interfaces.Services.Readings.Commands;
using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Services.Readings.Commands
{
    public class LikeCommandService : ILikeCommandService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeCommandService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task AddLike(Like like)
        {
            await _likeRepository.AddLikeAsync(like);
        }

        public async Task RemoveLike(Guid id)
        {
            await _likeRepository.RemoveLike(id);
        }
    }
}
