using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Persistence.Readings
{
    public interface ILikeRepository
    {
        Task AddLikeAsync(Like like);
        Task RemoveLike(Guid id);        
    }
}
