using Edu_QuizGen.Contracts.Users;

namespace Edu_QuizGen.Service_Abstraction;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId);

    Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);

    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
}
