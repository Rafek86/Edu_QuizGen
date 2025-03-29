namespace Edu_QuizGen.Service_Abstraction
{
    public interface IOptionService
    {
        public Task<Result> AddOptionAsync(Option option);

        public Task<Result<IEnumerable<Option>>> GetOptionsByQuestionId(int QuestionId);
        public Task<Result<IEnumerable<Option>>> GetOptionByQuestionText(string QuistionText);

        public Result UpdateQuestion(Option option);
        public Result DeleteQuestion(Option option);
    }
}
