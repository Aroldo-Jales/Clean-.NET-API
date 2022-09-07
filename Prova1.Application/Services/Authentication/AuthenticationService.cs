using Prova1.Application.Helpers.Authentication;
using Prova1.Application.Common.Interfaces.Authentication;
using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Application.Services.Authentication.Result;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Domain.Entities.Authentication;
using System.Transactions;

namespace Prova1.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{   
    private readonly ITokensUtils _jwtTokenGenerator; 
    private readonly IUserRepository _userRepository;
    private readonly IUserValidationCodeRepository _userValidationCodeRepository;
    public AuthenticationService(ITokensUtils jwtTokenGenerator, IUserRepository userRepository, IUserValidationCodeRepository userValidationCodeRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _userValidationCodeRepository = userValidationCodeRepository;
    }

    public async Task<AuthenticationResult> SignIn(string email, string password)
    {            
        if(await _userRepository.GetUserByEmail(email) is User user && user.PasswordHash == Crypto.ReturnUserHash(user, password))
        {
            // Gerar tokens novos
            
            var acessToken = _jwtTokenGenerator.GenerateJwtToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(user);

            return new AuthenticationResult(
                user,          
                acessToken,
                refreshToken.Token
            );
        }
        else
        {
            throw new Exception("Invalid credentials");
        }
    }

    public async Task<UserStatusResult> SignUp(string name, string email, string password)
    { 
        using (TransactionScope ts = new TransactionScope())
      {

      }  
        if(await _userRepository.GetUserByEmail(email) is not User)
        {
            if(Validation.IsValidEmail(email))
            {

                //CREATE NEW USER
                var user = new User(
                    name: name,
                    email: email                    
                );                

                user.Salt = Crypto.GetSalt;
                user.PasswordHash = Crypto.ReturnUserHash(user, password);                
                
                // ADD USER            
                await _userRepository.Add(user);
                
                // ADD EMAIL CODE CONFIRMATION                
                var uvEmail = new UserValidationCode(user.Id, user.Email);                
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

    public async Task<AuthenticationResult> ChangePassword(Guid id, string password)
    {
        if(await _userRepository.GetUserById(id) is User user)
        {
            if(Validation.IsValidPassword(password))
            {                    
                
                user.PasswordHash = Crypto.ReturnUserHash(user, password);                
                
                var acessToken = _jwtTokenGenerator.GenerateJwtToken(user);
                var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(user);

                return new AuthenticationResult(
                    await _userRepository.Update(user),             
                    acessToken,
                    refreshToken.Token
                );
            }
            else
            {
                throw new Exception("Invalid password.");    
            }
        }
        else
        {
            throw new Exception("User not found.");
        }
    }

    public async Task<UserStatusResult> ConfirmEmail(Guid userId, int code)
    {            
        // Refatorar validacao em service 
        if(await _userValidationCodeRepository.GetEmailValidationCodeByUser((await _userRepository.GetUserById(userId))!) is UserValidationCode uv)
        {              
            if(uv.Expiration > DateTime.Now)
            {
                if(uv.Code == code)
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
        if(!await _userRepository.UserPhoneNumberAlreadyExist(phoneNumber))         
        {
            User user = (await _userRepository.GetUserById(userId))!;

            var uvPhoneNumber = new UserValidationCode(user.Id, phoneNumber);     
                       
            await _userValidationCodeRepository.Add(uvPhoneNumber);            
        }
        else
        {            
            throw new Exception("This phone number already exists.");
        }        
    }
    
    public async Task<UserStatusResult> ConfirmPhoneNumber(Guid userId, int code)
    {
        if(await _userValidationCodeRepository.GetPhoneNumberValidationCodeByUser((await _userRepository.GetUserById(userId))!) is UserValidationCode uv)
        {
            if(uv.Expiration > DateTime.Now)
            {                
                if(uv.Code == code)
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