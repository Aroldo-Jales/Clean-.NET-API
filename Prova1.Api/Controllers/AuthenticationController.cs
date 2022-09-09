using Microsoft.AspNetCore.Mvc;
using Prova1.Application.Common.Interfaces.Services;
using Prova1.Contracts.Authentication.Request;
using Prova1.Contracts.Authentication.Response;
using Prova1.Application.Services.Authentication.Result;

namespace Prova1.Api.Controllers
{

    [ApiController]
    [Route("auth")]
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
            UserStatusResult? userInactiveResult = await _authenticationService.SignUp(
                request.Name,
                request.Email,
                request.Password
            );

            UserStatusResponse? response = new UserStatusResponse
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
            AuthenticationResult? authServiceResult = await _authenticationService.SignIn(
                request.Email,
                request.Password
            );

            AuthenticationResponse? response = new AuthenticationResponse
            (
                authServiceResult.user.Id,
                authServiceResult.user.Name,
                authServiceResult.user.Email,                
                authServiceResult.AcessToken,
                authServiceResult.RefreshToken
            );
            
            return Ok(response);
        }

        [HttpGet("refresh-token")]
        public ActionResult RefreshToken(RefreshTokenRequest request)
        {
            AuthenticationResult? authResult = _authenticationService.RefreshToken(
                request.RefreshToken,
                request.AccessToken
            );

            RefreshTokenResponse? refreshTokenResponse = new RefreshTokenResponse(
                authResult.RefreshToken,
                authResult.AcessToken
            );

            return Ok(refreshTokenResponse);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            AuthenticationResult? authServiceResult = await _authenticationService.ChangePassword(
                Guid.Parse(request.userId),
                request.NewPassword
            );

            AuthenticationResponse? response = new AuthenticationResponse
            (
                authServiceResult.user.Id,
                authServiceResult.user.Name,
                authServiceResult.user.Email,                
                authServiceResult.AcessToken,
                authServiceResult.RefreshToken
            );
            return Ok(response);
        }

        [HttpPost("email-confirmation")]
        public async Task<IActionResult> EmailConfirmation(ConfirmationRequest request)
        {
            UserStatusResult? authServiceResult = await _authenticationService.ConfirmEmail(
                Guid.Parse(request.userId),
                request.code
            );

            UserStatusResponse? response = new UserStatusResponse
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
            UserStatusResult? authServiceResult = await _authenticationService.ConfirmPhoneNumber(
                Guid.Parse(request.userId),
                request.code
            );

            UserStatusResponse? response = new UserStatusResponse
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

