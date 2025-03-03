using Edu_QuizGen.Contracts.Users;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Extensions;

namespace Edu_QuizGen.Controllers;
[Route("me")]
[ApiController]
//[Authorize]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("{Id}")]
    public async Task<IActionResult> Info([FromRoute] string Id) 
    {
        var result = await _userService.GetProfileAsync(Id); 
        return Ok(result.Value);
    }

    [HttpPut("info/{Id}")]
    public async Task<IActionResult> Info([FromRoute] string Id,[FromBody] UpdateProfileRequest request) {

        await _userService.UpdateProfileAsync(Id,request);

        return NoContent();
    }
    
    [HttpPut("change-password/{Id}")]
    public async Task<IActionResult> ChangePassword([FromRoute] string Id,[FromBody] ChangePasswordRequest request) {

        var result = await _userService.ChangePasswordAsync(Id,request);

       return result.IsSuccess ? NoContent()
            : result.ToProblem();
    }

}
