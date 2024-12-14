using Edu_QuizGen.Contracts.Authentication;

namespace Edu_QuizGen.Services;

public interface IAuthService
{
    Task<AuthResponse> GetTokenAsync(string email, string passowrd, CancellationToken cancellationToken); 
 
    Task<AuthResponse> GetRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken); 
    
    Task<bool> RevokeRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken); 
}
