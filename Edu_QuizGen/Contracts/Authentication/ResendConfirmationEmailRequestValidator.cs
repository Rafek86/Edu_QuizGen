namespace Edu_QuizGen.Contracts.Authentication;

public class ResendConfirmationEmailRequestValidator:AbstractValidator<ResendConfirmationEmailRequest>
{

    public ResendConfirmationEmailRequestValidator() {
        RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
    }
}
