using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Common.Interfaces.Persistence
{
    public interface IRefreshTokenRepository
    {
        Task Add(RefreshToken rf);

        Task<RefreshToken> Update(RefreshToken rf);

        Task Remove(RefreshToken rf);

        Task<IEnumerable<RefreshToken>> GetAllUsersRefreshTokens(Guid userId);

        Task<RefreshToken?> GetByToken(string token);

        Task RevokeAllTokensFromUser(Guid userId);
    }
}
