using Edu_QuizGen.Abstractions.Consts;

namespace Edu_QuizGen.Contracts.Users;

public class ChangePasswordRequestValidator :AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator() {

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password Should be at least 8 digits and should Contain lowercase and Uppercase")
            .NotEqual(x=>x.CurrentPassword)
            .WithMessage("New Password shouldn't be same as the current password ");

        RuleFor(x => x.CurrentPassword)
            .NotEmpty();

    }
}
