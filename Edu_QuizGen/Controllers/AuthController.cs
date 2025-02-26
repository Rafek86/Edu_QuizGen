using Edu_QuizGen.Contracts.Authentication;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Edu_QuizGen.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService,IOptions<JwtOptions> options) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly JwtOptions options = options.Value;

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken) {

        var authResult = await _authService.GetTokenAsync(request.email, request.password, cancellationToken);
        
        return authResult.IsSuccess ?
            Ok(authResult.Value) :
           authResult.ToProblem();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken) {

        var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
       
        return authResult.IsSuccess ?
            Ok(authResult.Value) :
            authResult.ToProblem();
    }


    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken) {

        var result = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
      
        return result.IsSuccess ?
            Ok() :
            result.ToProblem();
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {

        var result = await _authService.RegisterAsync(request, cancellationToken);
      
        return result.IsSuccess ?
            Ok(result) :
            result.ToProblem();
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
    {

        var result = await _authService.ConfirmEmailAsync(request, cancellationToken);
      
        return result.IsSuccess ?
            Ok(result) :
            result.ToProblem();
    }
    

    [HttpPost("resend-confirm-email")]
    public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
    {

        var result = await _authService.ResendConfirmationEmailAsync(request);
       
        return result.IsSuccess ?
            Ok(result) :
            result.ToProblem();
    }



    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
    {

        var result = await _authService.SendResetPasswordCodeAsync(request.Email);

        return result.IsSuccess ?
            Ok(result) :
            result.ToProblem();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {

        var result = await _authService.ResetPasswordAsync(request);

        return result.IsSuccess ?
            Ok(result) :
            result.ToProblem();
    }

}
