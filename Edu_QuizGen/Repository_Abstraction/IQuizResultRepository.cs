namespace Edu_QuizGen.Repository_Abstraction;

public interface IQuizResultRepository 
{
    Task<QuizResult> AddQuizResultAsync(QuizResult quizResult);
    Task<QuizResult?> GetQuizResultByIdAsync(int id);
    Task<QuizResult> GetQuizResultsByStudentIdAsync(string studentId, int quizId);
    Task<QuizResult> UpdateQuizResultAsync(QuizResult quizResult);
    Task<bool> DeleteQuizResultAsync(int id);
}
