using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Service_Abstraction
{
    public interface IQuestionSevice
    {
        public Task<Result> AddQuestionAsync(QuestionDTO question);
        public Task<Result> AddQuestionAsync(IEnumerable<QuestionDTO> questions);

        public Task<Result<QuestionDTO>> GetQuestionByIdAsync(int id);
        public Task<Result<IEnumerable<QuestionDTO>>> GetAllQuestionsAsync();
        public Task<Result<IEnumerable<QuestionDTO>>> GetQuestionsByTypeAsync(QuestionType type);
        public Task<Result<IEnumerable<QuestionDTO>>> GetQuestionsByQuizId(int QuizId);
        public Task<Result<IEnumerable<QuestionDTO>>> GetQuestionsByQuizTitle(string QuizTitle);

        public Task<Result> UpdateQuestion(int id, QuestionDTO questionDto);
        public Task<Result> DeleteQuestion(int id);
    }
}
