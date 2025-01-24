namespace Edu_QuizGen.Authentication;

public interface IJwtProvider
{
    (string token, int expiresIn) GenrateToken(ApplicationUser user, string role);

    string? ValidateToken(string token);
}
