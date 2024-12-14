namespace Edu_QuizGen.Contracts.Authentication;

public record LoginRequest(
    string email,
    string password
    );
