namespace Edu_QuizGen.Contracts.Users;

public class UpdateProfileRequestValidator:AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator() {
        RuleFor(x => x.FirstName)
                .Length(3, 100)
                .NotEmpty();
        
        RuleFor(x => x.LastName)
                .Length(3, 100)
                .NotEmpty();
    }
}
