namespace Edu_QuizGen.Service_Abstraction
{
    public interface IQuestionSevice
    {
        public Task<Result> AddQuestionAsync(Question question);
        public Task<Result> AddQuestionAsync(IEnumerable<Question> questions);

        public Task<Result<Question>> GetQuestionByIdAsync(int id);
        public Task<Result<IEnumerable<Question>>> GetAllQuestionsAsync();
        public Task<Result<IEnumerable<Question>>> GetQuestionsByTypeAsync(QuestionType type);
        public Task<Result<IEnumerable<Question>>> GetQuestionsByQuizId(int QuizId);
        public Task<Result<IEnumerable<Question>>> GetQuestionsByQuizTitle(string QuizTitle);

        public Result UpdateQuestion(Question question);
        public Result DeleteQuestion(Question question);
    }
}
