namespace Edu_QuizGen.Contracts.Users;

public record UserProfileResponse(
    string Email,
    string Username,
    string FirstName,
    string LastName
    );
