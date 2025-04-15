namespace Edu_QuizGen.Contracts.Questions
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
