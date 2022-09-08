using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Prova1.Application.Common.Interfaces.Authentication;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Domain.Entities.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Prova1.Infrastructure.Authentication
{
    public class TokensUtils : ITokensUtils
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeProvider _datetimeprovider;

        public TokensUtils(IDateTimeProvider datetimeprovider, IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _datetimeprovider = datetimeprovider;
        }

        public string GenerateJwtToken(User user, ClaimsPrincipal? claimsPrincipal = null)
        {           

            SigningCredentials signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                    SecurityAlgorithms.HmacSha256
            );

            string iat = claimsPrincipal is not null ? 
                claimsPrincipal.Claims.First(claim => claim.Type == "iat").Value : _datetimeprovider.UtcNow.ToString();
            
            Claim[]? claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, iat)
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

        public RefreshToken GenerateRefreshToken(User user, ClaimsPrincipal? claimsPrincipal = null)
        {
            string iat = claimsPrincipal is not null ?
                claimsPrincipal.Claims.First(claim => claim.Type == "iat").Value : _datetimeprovider.UtcNow.ToString();

            RefreshToken? refreshToken = new RefreshToken(
                userId: user.Id,
                token: getUniqueToken(),
                created: DateTime.Now,
                expires: DateTime.Now.AddDays(7),
                device: "Android sddjkas",
                iat: iat
            );

            return refreshToken;

            string getUniqueToken()
            {
                string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                return token;
            }
        }

        public Guid? ValidateJwtToken(string token)
        {

            byte[]? key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            try
            {
                new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))

                }, out SecurityToken validatedToken);

                JwtSecurityToken? jwtToken = (JwtSecurityToken)validatedToken;
                Guid userId = Guid.Parse(jwtToken.Subject);

                return userId;
            }
            catch
            {
                return null;
            }
        }

        public ClaimsPrincipal ExtractClaimsFromToken(string token)
        {
            TokenValidationParameters? tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            };


            ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParams, out SecurityToken st);
            if (st is not JwtSecurityToken jwtst)
            {
                throw new Exception("Invalid token to extract claims.");
            }

            return claimsPrincipal;
        }


    }
}