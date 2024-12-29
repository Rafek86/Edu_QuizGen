namespace Edu_QuizGen.Contracts.Users;

public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword
    );
