namespace Edu_QuizGen.Contracts.Questions
{
    public class QuestionValidator : AbstractValidator<QuestionsAddRequest>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.CorrectAnswer).NotEmpty();
            RuleFor(x => x.QuizId).NotEmpty();
            RuleFor(x => x.Options).NotEmpty();
        }
    }
}
