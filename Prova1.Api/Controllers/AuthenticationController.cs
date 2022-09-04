using Microsoft.AspNetCore.Mvc;
using Prova1.Contracts.Authentication;
using Prova1.Application.Services.Authentication;

namespace Prova1.Api.Controllers;

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
        var userInactiveResult = await _authenticationService.SignUp(
            request.Name,            
            request.Email,
            request.PhoneNumber,
            request.Password
        );

        var response = new UserInactiveResponse
        (
            userInactiveResult.user.Id,
            userInactiveResult.user.Name,
            userInactiveResult.user.Email,
            userInactiveResult.user.ActiveAccount
        );

        return Ok(response);
    }

    [HttpPost("signin")]   
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        var authResult = await _authenticationService.SignIn(            
            request.Email,
            request.Password
        );

        var response = new AuthenticationResponse
        (
            authResult.user.Id,
            authResult.user.Name,
            authResult.user.Email,
            authResult.user.PhoneNumber!,            
            authResult.Token
        );
        
        return Ok(response);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
         var authResult = await _authenticationService.ChangePassword(            
            Guid.Parse(request.Id),
            request.NewPassword
        );    

        var response = new AuthenticationResponse
        (
            authResult.user.Id,
            authResult.user.Name,
            authResult.user.Email,
            authResult.user.PhoneNumber!,
            authResult.Token
        );
        return Ok(response);
    }
}