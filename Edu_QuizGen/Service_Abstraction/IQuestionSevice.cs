using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Service_Abstraction
{
    public interface IQuestionSevice
    {
        public Task<Result> AddQuestionAsync(QuestionDTO question);
        public Task<Result> AddQuestionAsync(IEnumerable<QuestionDTO> questions);

        public Task<Result<QuestionResponseDTO>> GetQuestionByIdAsync(int id);
        public Task<Result<PagedResult<QuestionDTO>>> GetPagedQuestionsAsync(int pageNumber, int pageSize);
        public Task<Result<IEnumerable<QuestionResponseDTO>>> GetAllQuestionsAsync();
        public Task<Result<IEnumerable<QuestionResponseDTO>>> GetQuestionsByTypeAsync(QuestionType type);
        public Task<Result<IEnumerable<QuestionResponseDTO>>> GetQuestionsByQuizId(int QuizId);
        public Task<Result<IEnumerable<QuestionResponseDTO>>> GetQuestionsByQuizTitle(string QuizTitle);

        public Task<Result> UpdateQuestion(int id, QuestionDTO questionDto);
        public Task<Result> DeleteQuestion(int id);
    }
}
