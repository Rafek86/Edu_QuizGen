using Edu_QuizGen.Contracts.Users;

namespace Edu_QuizGen.Services;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
}
