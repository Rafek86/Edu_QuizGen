using Edu_QuizGen.Contracts.Quiz;
using FluentValidation;

public class CreateQuizRequestValidator : AbstractValidator<CreateQuizRequest>
{
    public CreateQuizRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

        RuleFor(x => x.TotalQuestions)
            .GreaterThan(0).WithMessage("Total questions must be at least 1");

        RuleFor(x => x.Duration)
            .InclusiveBetween(1, 180).WithMessage("Duration must be between 1 and 180 minutes");

        RuleFor(x => x.StartAt)
            .LessThan(x => x.EndAt)
            .WithMessage("Start date must be before end date");
    }
}
