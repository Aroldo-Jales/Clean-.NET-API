using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using Prova1.Api.Domain;

namespace Prova1.Api.Infrastructure.Authentication
{
    public class JwtTokenGenerator
    {
        public JwtTokenGenerator()
        {
           
        }
        public string GenerateToken(User user)
        {

            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("super-secret-key")),
                    SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),                
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),                              
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: "ProjetoApi",
                audience: "ProjetoApi",
                expires: DateTime.Now.AddHours(2),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }
    }
}