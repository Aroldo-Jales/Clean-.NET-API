using Prova1.Application.Helpers.Authentication;
using Prova1.Application.Common.Interfaces.Authentication;
using Prova1.Application.Common.Interfaces.Persistence;
using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{   
    private readonly IJwtTokenGenerator _jwtTokenGenerator; 
    private readonly IUserRepository _userRepository;
    private readonly IUserValidationCodeRepository _userValidationCodeRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IUserValidationCodeRepository userValidationCodeRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _userValidationCodeRepository = userValidationCodeRepository;
    }

    public async Task<AuthenticationResult> SignIn(string email, string password)
    {            
        // Change implementation to uses hash email+password in database

        if(await _userRepository.GetUserByEmail(email) is User user && user.PasswordHash == Crypto.ReturnUserHash(user, password))
        {
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,          
                token
            );
        }
        else
        {
            throw new Exception("Invalid credentials");
        }
    }

    public async Task<InactiveUserResult> SignUp(string name, string email, string phonNumber, string password)
    {   
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
                
                // ADD PHONENUMBER CONFIRMATION
                var uvPhoneNumber = new UserValidationCode(user.Id, phonNumber);
                var uvEmail = new UserValidationCode(user.Id, user.Email);
                await _userValidationCodeRepository.Add(uvPhoneNumber);
                await _userValidationCodeRepository.Add(uvEmail);

                return new InactiveUserResult(
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

                await _userRepository.Update(user);

                var token = _jwtTokenGenerator.GenerateToken(user);

                return new AuthenticationResult(
                    user,             
                    token
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
        
}