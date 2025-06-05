using Edu_QuizGen.Contracts.Users;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services;

public class UserService(UserManager<ApplicationUser>userManager,ApplicationDbContext context) :IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId) 
    { 
    
        var user =await _userManager.FindByIdAsync(userId);

        //TODO :: Auto Mapper
        var userProfileResponse = await _context.Users.Where(x => x.Id == userId).Select(x => new UserProfileResponse(
            user.Email!,
            user.UserName!,
            user.FirstName,
            user.LastName 
            )).FirstAsync();

        return Result.Success(userProfileResponse);
    }

    public async Task<Result> UpdateProfileAsync(string userId,UpdateProfileRequest request)
    {
    
        var user =await _userManager.FindByIdAsync(userId);

        user.FirstName = request.FirstName; 
        user.LastName = request.LastName;

        await _userManager.UpdateAsync(user);

        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request)
    {
     
        var user = await _userManager.FindByIdAsync(userId);

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        
        if (result.Succeeded) 
           return Result.Success(); 

        var error =result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
