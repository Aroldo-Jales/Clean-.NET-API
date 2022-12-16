using Prova1.Application.Common.Errors.Authentication;
using Prova1.Application.Common.Interfaces.Persistence.Authentication;
using Prova1.Application.Common.Interfaces.Services.Authentication.Queries;
using Prova1.Application.Common.Interfaces.Utils.Authentication;
using Prova1.Application.Services.Authentication.Result;
using Prova1.Application.Utils.Authentication;
using Prova1.Domain.Entities.Authentication;
using System.Security.Claims;

namespace Prova1.Application.Services.Authentication.Queries
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokensUtils _tokensUtils;
        private readonly IRefreshTokenRepository _refreshTokensRepository;
        private readonly IUserValidationCodeRepository _userValidationCodeRepository;

        public AuthenticationQueryService(IUserRepository userRepository, ITokensUtils tokensUtils, IRefreshTokenRepository refreshTokenRepository, IUserValidationCodeRepository userValidationCodeRepository)
        {
            _tokensUtils = tokensUtils;
            _userRepository = userRepository;
            _refreshTokensRepository = refreshTokenRepository;
            _userValidationCodeRepository = userValidationCodeRepository;
        }

        public async Task<AuthenticationResult> SignIn(string email, string password)
        {
            if (!(await _userRepository.GetUserByEmail(email) is User user && user.PasswordHash == Crypto.ReturnUserHash(user, password)))
            {
                throw new InvalidCredentialsException();
            }
            
            string? accessToken = _tokensUtils.GenerateJwtToken(user);
            ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(accessToken);
            
            RefreshToken? refreshToken = _tokensUtils.GenerateRefreshToken(user, claimsPrincipal);

            await _refreshTokensRepository.Add(refreshToken);

            return new AuthenticationResult(user, accessToken, refreshToken.Token);
        }

        public AuthenticationResult RefreshToken(string refreshtoken, string acesstoken)
        {
            if (_tokensUtils.ValidateJwtToken(acesstoken) is null)
            {
                throw new Exception("Invalid access token.");
            }
            if (_refreshTokensRepository.GetByToken(refreshtoken).Result is not RefreshToken rf)
            {
                throw new Exception("Invalid refresh token.");
            }

            User user = _userRepository.GetUserById(rf.UserId).Result!;        
            ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(acesstoken);
        
            string newAccessToken = _tokensUtils.GenerateJwtToken(user, claimsPrincipal);
            RefreshToken newRefreshToken = _tokensUtils.GenerateRefreshToken(user, claimsPrincipal);

            _refreshTokensRepository.Update(newRefreshToken);

            return new AuthenticationResult(user, newAccessToken, newRefreshToken.Token);
        }

        public async Task<UserStatusResult> ConfirmEmail(Guid userId, int code)
        {
            if (await _userValidationCodeRepository.GetEmailValidationCodeByUser((await _userRepository.GetUserById(userId))!) is not UserValidationCode uv) {
                throw new Exception("This confirmation not exists.");
            }

            if (uv.Expiration < DateTime.Now) {
                throw new Exception("Invalid code, a new code was resend.");
            }

            if (uv.Code != code) {
                throw new Exception("Invalid code.");
            }

            // implement transaction
            await _userValidationCodeRepository.RemoveUserConfirmation(uv);

            User? user = await _userRepository.GetUserById(userId);
            user!.ActiveAccount = true;

            return new UserStatusResult(await _userRepository.Update(user));
        }

        public async Task<UserStatusResult> ConfirmPhoneNumber(Guid userId, int code)
        {
            if (await _userValidationCodeRepository.GetPhoneNumberValidationCodeByUser((await _userRepository.GetUserById(userId))!) is not UserValidationCode uv)
            {
                throw new Exception("This confirmation not exists.");
            }

            if (uv.Expiration < DateTime.Now)
            {
                throw new Exception("Invalid code, a new code was resend.");
            }

            if (uv.Code != code)
            {
                throw new Exception("Invalid code.");
            }

            // use transaction
            await _userValidationCodeRepository.RemoveUserConfirmation(uv);

            User? user = await _userRepository.GetUserById(userId);
            user!.PhoneNumber = uv.Type;

            return new UserStatusResult(await _userRepository.Update(user));
        }
    }
}