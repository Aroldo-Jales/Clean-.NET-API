using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Services.Readings.Commands
{
    public interface ILikeCommandService
    {
        Task AddLike(Like like);
        Task RemoveLike(Guid id);
    }
}
