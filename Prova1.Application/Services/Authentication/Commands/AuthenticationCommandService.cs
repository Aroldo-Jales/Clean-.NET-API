using Prova1.Application.Common.Errors.Authentication;
using Prova1.Application.Common.Interfaces.Persistence.Authentication;
using Prova1.Application.Common.Interfaces.Services.Authentication.Command;
using Prova1.Application.Common.Interfaces.Utils.Authentication;
using Prova1.Application.Services.Authentication.Result;
using Prova1.Application.Utils.Authentication;
using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly ITokensUtils _tokensUtils;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokensRepository;
    private readonly IUserValidationCodeRepository _userValidationCodeRepository;

    public AuthenticationCommandService(ITokensUtils tokensUtils, IUserRepository userRepository, IRefreshTokenRepository refreshTokensRepository, IUserValidationCodeRepository userValidationCodeRepository)
    {
        _tokensUtils = tokensUtils;
        _userRepository = userRepository;
        _refreshTokensRepository = refreshTokensRepository;
        _userValidationCodeRepository = userValidationCodeRepository;
    }

    public async Task<UserStatusResult> SignUp(string name, string email, string password)
    {
        // USER EMAIL VERIFICATION
        if (await _userRepository.GetUserByEmail(email) is User)
        {
            throw new UserAlreadyExistsException();
        }

        // EMAIL VALIDATION
        if (!Validation.IsValidEmail(email))
        {
            throw new InvalidEmailException();
        }

        // PASSWORD VALIDATION
        if (!Validation.IsValidPassword(password))
        {
            throw new InvalidPasswordException();
        }

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
        UserValidationCode uvEmail = new UserValidationCode(user.Id, user.Email);
        await _userValidationCodeRepository.Add(uvEmail);

        return new UserStatusResult(
            user
        );
    }

    public async Task<AuthenticationResult> ChangePassword(Guid id, string password)
    {
        if (!(await _userRepository.GetUserById(id) is User user))
        {
            throw new UserNotFoundException();
        }

        if (!Validation.IsValidPassword(password))
        {
            throw new InvalidPasswordException();
        }

        user.PasswordHash = Crypto.ReturnUserHash(user, password);

        string? acessToken = _tokensUtils.GenerateJwtToken(user);
        RefreshToken? refreshToken = _tokensUtils.GenerateRefreshToken(user);

        return new AuthenticationResult(
            await _userRepository.Update(user),
            acessToken,
            refreshToken.Token
        );
    }

    public async Task AddPhoneNumber(Guid userId, string phoneNumber)
    {
        if (await _userRepository.UserPhoneNumberAlreadyExist(phoneNumber))
        {
            throw new PhoneNumberAlreadyExistsException();
        }

        User user = (await _userRepository.GetUserById(userId))!;

        UserValidationCode? uvPhoneNumber = new UserValidationCode(user.Id, phoneNumber);

        await _userValidationCodeRepository.Add(uvPhoneNumber);
    }
}