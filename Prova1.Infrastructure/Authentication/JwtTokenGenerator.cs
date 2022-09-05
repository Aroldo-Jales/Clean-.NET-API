using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Prova1.Application.Common.Interfaces.Authentication;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Domain.Entities.Authentication;

namespace Prova1.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeProvider _datetimeprovider;

        public JwtTokenGenerator(IDateTimeProvider datetimeprovider, IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _datetimeprovider = datetimeprovider;            
        }
        
        public string GenerateJwtToken(User user)
        {

            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                    SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),                
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),                                
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _datetimeprovider.UtcNow.AddHours(2),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }
        
        public string GenerateRefreshJwtToken(User user)
        {
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                                SecurityAlgorithms.HmacSha256
                        );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _datetimeprovider.UtcNow.AddDays(30),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public ClaimsPrincipal ExtractClaimFromExpiredToken(string token)
        {
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            };


            var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParams, out SecurityToken st);
            if(st is not JwtSecurityToken jwtst)
            {
                throw new Exception("Invalid token to extract claims.");
            }

            return claimsPrincipal;
        }
        
        
    }
}