using Prova1.Application.Common.Interfaces.Authentication;
using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Infrastructure.Repositories;
using Prova1.Domain.Entities.Authentication;
namespace Prova1.Api.Middlewares
{
    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository, IRefreshTokenRepository tokenRepository,ITokensUtils tokensUtils)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            Guid? userId = tokensUtils.ValidateJwtToken(token!);

            if (userId is not null)
            {                
                if (await userRepository.GetUserById(userId.Value) is User user && 
                    await tokenRepository.ValidateIatToken(user.Id, token!))
                {
                    context.User = tokensUtils.ExtractClaimsFromToken(token!);
                    await _next(context);
                }                                        
                else
                {
                    throw new UnauthorizedAccessException("Expired, invalid or revoked token.");
                }
            }
            else
            {
                if (token != string.Empty)
                {
                    throw new UnauthorizedAccessException("Expired, invalid or revoked token.");
                }
                else
                {
                    throw new UnauthorizedAccessException("Request without token.");
                }
            }
        }
    }
}