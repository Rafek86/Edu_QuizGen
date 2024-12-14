namespace Edu_QuizGen.Contracts.Authentication;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
    );
