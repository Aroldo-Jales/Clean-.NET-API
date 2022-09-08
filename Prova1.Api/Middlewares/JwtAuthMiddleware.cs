using Prova1.Application.Common.Interfaces.Authentication;
using Prova1.Application.Common.Interfaces.Services;

namespace Prova1.Api.Middlewares
{
    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, ITokensUtils tokensUtils)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            Guid? userId = tokensUtils.ValidateJwtToken(token!);

            if (userId != null)
            {
                context.User = tokensUtils.ExtractClaimsFromToken(token!);

                await _next(context);
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