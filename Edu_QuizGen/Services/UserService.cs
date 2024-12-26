using Edu_QuizGen.Contracts.Users;

namespace Edu_QuizGen.Services;

public class UserService(UserManager<ApplicationUser>userManager,ApplicationDbContext context) :IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId) { 
    
        var user =await _userManager.FindByIdAsync(userId);

        var userProfileResponse = await _context.Users.Where(x => x.Id == userId).Select(x => new UserProfileResponse(
            user.Email!,
            user.UserName!,
            user.FirstName,
            user.LastName 
            )).FirstAsync();

        return Result.Success(userProfileResponse);
    }
}
