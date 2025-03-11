namespace Edu_QuizGen.Service_Abstraction
{
    public interface IQuestionSevice
    {
        public Task AddQuestionAsync(Question question);
        public Task AddQuestionAsync(IEnumerable<Question> questions);

        public Task<IEnumerable<Question>> GetAllQuestionsAsync();
        public Task<IEnumerable<Question>> GetQuestionsByTypeAsync(QuestionType type);

        public void UpdateQuestion(Question question);
        public void DeleteQuestion(Question question);
    }
}
