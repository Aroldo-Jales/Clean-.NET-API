using Prova1.Application.Common.Errors.Authentication;
using Prova1.Application.Common.Interfaces.Authentication;
using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Application.Helpers.Authentication;
using Prova1.Application.Services.Authentication.Result;
using Prova1.Domain.Entities.Authentication;
using System.Security.Claims;
namespace Prova1.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokensUtils _tokensUtils;

    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokensRepository;
    private readonly IUserValidationCodeRepository _userValidationCodeRepository;

    public AuthenticationService(ITokensUtils tokensUtils, IUserRepository userRepository, IRefreshTokenRepository refreshTokensRepository, IUserValidationCodeRepository userValidationCodeRepository)
    {
        _tokensUtils = tokensUtils;
        _userRepository = userRepository;
        _refreshTokensRepository = refreshTokensRepository;
        _userValidationCodeRepository = userValidationCodeRepository;
    }

    public async Task<AuthenticationResult> SignIn(string email, string password)
    {
        if (await _userRepository.GetUserByEmail(email) is User user && user.PasswordHash == Crypto.ReturnUserHash(user, password))
        {
            string? accessToken = _tokensUtils.GenerateJwtToken(user);
            ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(accessToken);
            
            RefreshToken? refreshToken = _tokensUtils.GenerateRefreshToken(user, claimsPrincipal);

            await _refreshTokensRepository.Add(refreshToken);

            return new AuthenticationResult(
                user,
                accessToken,
                refreshToken.Token
            );
        }
        else
        {
            throw new InvalidCredentialsException();
        }
    }

    public async Task<UserStatusResult> SignUp(string name, string email, string password)
    {

        if (await _userRepository.GetUserByEmail(email) is not User)
        {
            if (Validation.IsValidEmail(email))
            {

                //CREATE NEW USER
                User? user = new User(
                    name: name,
                    email: email
                );

                user.Salt = Crypto.GetSalt;
                user.PasswordHash = Crypto.ReturnUserHash(user, password);

                // ADD USER            
                await _userRepository.Add(user);

                // ADD EMAIL CODE CONFIRMATION                
                UserValidationCode? uvEmail = new UserValidationCode(user.Id, user.Email);
                await _userValidationCodeRepository.Add(uvEmail);

                return new UserStatusResult(
                    user
                );
            }
            else
            {
                throw new Exception("Invalid email.");
            }
        }
        else
        {
            throw new Exception("User already exists.");
        }
    }

    public AuthenticationResult RefreshToken(string refreshtoken, string acesstoken)
    {
        if(_tokensUtils.ValidateJwtToken(acesstoken) is null)
        {
            throw new Exception("Invalid access token.");
        }
        if(_refreshTokensRepository.GetByToken(refreshtoken).Result is not RefreshToken rf)
        {
            throw new Exception("Invalid refresh token.");
        }

        User user = _userRepository.GetUserById(rf.UserId).Result!;        
        ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(acesstoken);
        
        string newAccessToken = _tokensUtils.GenerateJwtToken(user, claimsPrincipal);
        RefreshToken newRefreshToken = _tokensUtils.GenerateRefreshToken(user, claimsPrincipal);

        _refreshTokensRepository.Update(newRefreshToken);

        return new AuthenticationResult(
            user,
            newAccessToken,
            newRefreshToken.Token
        );
    }

    public async Task<AuthenticationResult> ChangePassword(Guid id, string password)
    {
        if (await _userRepository.GetUserById(id) is User user)
        {
            if (Validation.IsValidPassword(password))
            {

                user.PasswordHash = Crypto.ReturnUserHash(user, password);

                string? acessToken = _tokensUtils.GenerateJwtToken(user);
                RefreshToken? refreshToken = _tokensUtils.GenerateRefreshToken(user);

                return new AuthenticationResult(
                    await _userRepository.Update(user),
                    acessToken,
                    refreshToken.Token
                );
            }
            else
            {
                throw new InvalidPasswordException();
            }
        }
        else
        {
            throw new UserNotFoundException();
        }
    }

    // CONFIRMATIONS
    public async Task<UserStatusResult> ConfirmEmail(Guid userId, int code)
    {
        // Refatorar validacao em service 
        if (await _userValidationCodeRepository.GetEmailValidationCodeByUser((await _userRepository.GetUserById(userId))!) is UserValidationCode uv)
        {
            if (uv.Expiration > DateTime.Now)
            {
                if (uv.Code == code)
                {
                    // implement transaction
                    await _userValidationCodeRepository.RemoveUserConfirmation(uv);

                    User? user = await _userRepository.GetUserById(userId);
                    user!.ActiveAccount = true;

                    return new UserStatusResult(
                        await _userRepository.Update(user)
                    );
                }
                else
                {
                    throw new Exception("Invalid code.");
                }
            }
            else
            {
                await _userValidationCodeRepository.RenewCode(uv);
                throw new Exception("Invalid code, a new code was resend.");
            }
        }
        else
        {
            throw new Exception("This confirmation not exists.");
        }
    }

    public async Task AddPhoneNumber(Guid userId, string phoneNumber)
    {
        if (!await _userRepository.UserPhoneNumberAlreadyExist(phoneNumber))
        {
            User user = (await _userRepository.GetUserById(userId))!;

            UserValidationCode? uvPhoneNumber = new UserValidationCode(user.Id, phoneNumber);

            await _userValidationCodeRepository.Add(uvPhoneNumber);
        }
        else
        {
            throw new Exception("This phone number already exists.");
        }
    }

    public async Task<UserStatusResult> ConfirmPhoneNumber(Guid userId, int code)
    {
        if (await _userValidationCodeRepository.GetPhoneNumberValidationCodeByUser((await _userRepository.GetUserById(userId))!) is UserValidationCode uv)
        {
            if (uv.Expiration > DateTime.Now)
            {
                if (uv.Code == code)
                {
                    // use transaction
                    await _userValidationCodeRepository.RemoveUserConfirmation(uv);

                    User? user = await _userRepository.GetUserById(userId);
                    user!.PhoneNumber = uv.Type;

                    return new UserStatusResult(
                        await _userRepository.Update(user)
                    );
                }
                else
                {
                    throw new Exception("Invalid code.");
                }
            }
            else
            {
                await _userValidationCodeRepository.RenewCode(uv);
                throw new Exception("Invalid code, a new code was resend.");
            }
        }
        else
        {
            throw new Exception("This confirmation not exists.");
        }
    }
}