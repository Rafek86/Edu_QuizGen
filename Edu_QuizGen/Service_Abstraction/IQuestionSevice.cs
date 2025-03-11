namespace Edu_QuizGen.Service_Abstraction
{
    public interface IQuestionSevice
    {
        public Task AddQuestionAsync(Question question);
        public Task AddQuestionAsync(IEnumerable<Question> questions);

        public Task<Question> GetQuestionByIdAsync(int id);
        public Task<IEnumerable<Question>> GetAllQuestionsAsync();
        public Task<IEnumerable<Question>> GetQuestionsByTypeAsync(QuestionType type);
        public Task<IEnumerable<Question>> GetQuestionsByQuizId(int QuizId);
        public Task<IEnumerable<Question>> GetQuestionsByQuizTitle(string QuizTitle);

        public void UpdateQuestion(Question question);
        public void DeleteQuestion(Question question);
    }
}
