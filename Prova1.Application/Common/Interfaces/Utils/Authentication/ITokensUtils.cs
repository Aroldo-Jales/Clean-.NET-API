using System.Security.Claims;
using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Common.Interfaces.Utils.Authentication
{
    public interface ITokensUtils
    {
        string GenerateJwtToken(User user, ClaimsPrincipal? claimsPrincipal = null);
        RefreshToken GenerateRefreshToken(User user, ClaimsPrincipal? claimsPrincipal = null);
        Guid? ValidateJwtToken(string token);
        ClaimsPrincipal ExtractClaimsFromToken(string token);
    }
}