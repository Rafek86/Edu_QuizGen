namespace Edu_QuizGen.Contracts.Authentication;

public record ResetPasswordRequest(
     string Email,
     string Code,
     string NewPassword
    );
