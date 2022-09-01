using Prova1.Api.Domain;
using Prova1.Api.Infrastructure.Authentication;
using Prova1.Api.Infrastructure.Repositories;
using Prova1.Api.Application.Helpers;

namespace Prova1.Api.Application.Authentication;
public class AuthenticationService : IAuthenticationService
{   
    private readonly JwtTokenGenerator _jwtTokenGenerator; 
    private readonly UserRepository _userRepository;
    public AuthenticationService(JwtTokenGenerator jwtTokenGenerator, UserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult SignIn(string email, string password)
    {            
        // Change implementation to uses hash email+password in database

        if(_userRepository.GetUserByEmail(email) is User user && user.Password == password)
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

    public async Task<AuthenticationResult> SignUp(string Name, string Email, string Telefone, string Password)
    {   
        if(_userRepository.GetUserByEmail(Email) is not User)
        {
            if(Validation.IsValidEmail(Email))
            {
                var user = new User
                (
                    Name, 
                    Email, 
                    Telefone,
                    Password
                );
                
                await _userRepository.Add(user);

                var token = _jwtTokenGenerator.GenerateToken(user); 

                return new AuthenticationResult(
                    user,             
                    token
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
        if(_userRepository.GetUserById(id) is User user)
        {
            if(Validation.IsValidPassword(password))
            {                    

                user.Password = password;

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