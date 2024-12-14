using Edu_QuizGen.Contracts.Authentication;
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

    [HttpPost("")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken) {

        var authResult = await _authService.GetTokenAsync(request.email, request.password, cancellationToken);
        return authResult is null ? BadRequest() : Ok(authResult);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken) {

        var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
        return authResult is null ? BadRequest("Invalid Token") : Ok(authResult);
    }


    [HttpPost("revoke-refress-token")]
    public async Task<IActionResult> RevokeRefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken) {

        var isRevoked = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
        return isRevoked ?  Ok() :BadRequest("Operation Failed") ;
    }
}
