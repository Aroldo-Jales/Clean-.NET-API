using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Prova1.Contracts.Authentication.Request;
using Prova1.Contracts.Authentication.Response;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Application.Services.Authentication;

namespace Prova1.Api.Controllers{

    [ApiController]
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {    

        private readonly IAuthenticationService _authenticationService;

        
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var userInactiveResult = await _authenticationService.SignUp(
                request.Name,            
                request.Email,            
                request.Password
            );

            var response = new UserStatusResponse
            (
                userInactiveResult.user.Id,
                userInactiveResult.user.Name,
                userInactiveResult.user.Email,
                userInactiveResult.user.ActiveAccount
            );

            return Ok(response);
        }

        [HttpGet("signin")]   
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var authServiceResult = await _authenticationService.SignIn(            
                request.Email,
                request.Password
            );

            var response = new AuthenticationResponse
            (
                authServiceResult.user.Id,
                authServiceResult.user.Name,
                authServiceResult.user.Email,
                authServiceResult.user.PhoneNumber!,            
                authServiceResult.AcessToken,
                authServiceResult.RefreshToken
            );
            
            return Ok(response);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var authServiceResult = await _authenticationService.ChangePassword(            
                Guid.Parse(request.userId),
                request.NewPassword
            );    

            var response = new AuthenticationResponse
            (
                authServiceResult.user.Id,
                authServiceResult.user.Name,
                authServiceResult.user.Email,
                authServiceResult.user.PhoneNumber!,
                authServiceResult.AcessToken,
                authServiceResult.RefreshToken
            );
            return Ok(response);
        }

        [HttpPost("email-confirmation")]
        public async Task<IActionResult> EmailConfirmation(ConfirmationRequest request)
        {
            var authServiceResult = await _authenticationService.ConfirmEmail(
                Guid.Parse(request.userId),
                request.code
            );

            var response = new UserStatusResponse
            (
                authServiceResult.user.Id,
                authServiceResult.user.Name,
                authServiceResult.user.Email,
                authServiceResult.user.ActiveAccount
            );
            
            return Ok(response);
        }

        [HttpPost("add-phone-number")]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberRequest request)
        {
            await _authenticationService.AddPhoneNumber(
                Guid.Parse(request.userId),
                request.phoneNumber
            );
            
            return Ok("Phonenumber registered for confirmation.");
        }

        [HttpPost("phone-number-confirmation")]
        public async Task<IActionResult> PhoneNumberConfirmation(ConfirmationRequest request)
        {
            var authServiceResult = await _authenticationService.ConfirmPhoneNumber(
                Guid.Parse(request.userId),
                request.code
            );

            var response = new UserStatusResponse
            (
                authServiceResult.user.Id,
                authServiceResult.user.Name,
                authServiceResult.user.Email,
                authServiceResult.user.ActiveAccount
            );
            
            return Ok(response);
        }
    }
}

