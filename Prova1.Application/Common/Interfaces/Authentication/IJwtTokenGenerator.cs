using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshJwtToken(User user);
    }
}