using Prova1.Domain.Entities.Readings;

namespace Prova1.Application.Common.Interfaces.Services.Readings.Commands
{
    public interface ICommentaryCommandService
    {        
        Task<Commentary> AddCommentaryAsync(Commentary commentary);
        Task<Commentary> UpdateCommentaryAsync(Commentary commentary);
        Task RemoveCommentaryAsync(Guid id);
    }
}
