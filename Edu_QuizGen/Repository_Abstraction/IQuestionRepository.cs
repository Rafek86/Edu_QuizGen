namespace Edu_QuizGen.Repository_Abstraction
{
    public interface IQuestionRepository : IGenericRepository<Question> 
    {
        public Task<Question> GetQuestionByIdAsync(int id);
        public Task<PagedResult<Question>> GetPagedQuestionsAsync(int pageNumber,int pageSize);
        public Task<IEnumerable<Question>> GetQuestionsByQuizId(int QuizId);
        public Task<IEnumerable<Question>> GetQuestionsByQuizTitle(string QuizTitle);
        public Task<IEnumerable<Question>> GetQuestionsByTypeAsync(QuestionType type);
        public Task UpdateQuestion(Question question);
        Task AddQuizQuestionAsync(QuizQuestions quizQuestion);
        Task<List<QuizQuestions>> GetQuizQuestionsByQuizIdAsync(int quizId);
    }
}
