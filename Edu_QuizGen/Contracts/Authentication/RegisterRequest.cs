namespace Edu_QuizGen.Contracts.Authentication;

public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTime EntollmentDate,
    string GradLevel
    );