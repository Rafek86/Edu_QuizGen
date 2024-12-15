using Edu_QuizGen.Abstractions.Consts;

namespace Edu_QuizGen.Contracts.Authentication;

public class RegisterRequestValidator:AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator() {

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password Should be at least 8 digits and should Contain lowercase and Uppercase");

        RuleFor(x=>x.FirstName)
            .Length(3,100)
            .NotEmpty();    

        RuleFor(x=>x.LastName)
            .Length(3, 100)
            .NotEmpty();    
    
    }
}
