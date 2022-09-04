using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}