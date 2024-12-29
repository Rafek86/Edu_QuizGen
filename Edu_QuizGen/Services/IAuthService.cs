namespace Edu_QuizGen.Services;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetTokenAsync(string email, string passowrd, CancellationToken cancellationToken);

    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken);

    Task<Result> RevokeRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken);

    Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);

    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken);

    Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request);

    Task<Result> SendResetPasswordCodeAsync(string email);

    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
}
