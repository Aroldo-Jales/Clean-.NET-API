using Microsoft.AspNetCore.Mvc;
using Prova1.Api.Contracts.Authentication;
using Prova1.Api.Application.Authentication;

namespace Prova1.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
    
        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("signup")]
        public IActionResult SignUp(SignUpRequest request)
        {
            var authResult = _authenticationService.SignUp(
                request.Name,
                request.Email,
                request.Telefone,
                request.Password
            );

            var response = new AuthenticationResponse
            (
                authResult.Result.user.Id,
                authResult.Result.user.Name,
                authResult.Result.user.Email,
                authResult.Result.user.Telefone,
                authResult.Result.Token
            );

            return Ok(response);
        }

        [HttpPost("signin")]   
        public IActionResult SignIn(SignInRequest request)
        {
            var authResult = _authenticationService.SignIn(            
                request.Email,
                request.Password
            );

            var response = new AuthenticationResponse
            (
                authResult.user.Id,
                authResult.user.Name,
                authResult.user.Email,
                authResult.user.Telefone,
                authResult.Token
            );
            
            return Ok(response);
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword(ChangePasswordRequest request)
        {
            var authResult = _authenticationService.ChangePassword(            
                Guid.Parse(request.Id),
                request.NewPassword
            );    

            var response = new AuthenticationResponse
            (
                authResult.Result.user.Id,
                authResult.Result.user.Name,
                authResult.Result.user.Email,
                authResult.Result.user.Telefone,
                authResult.Result.Token
            );
            return Ok(response);
        }               
    }
}