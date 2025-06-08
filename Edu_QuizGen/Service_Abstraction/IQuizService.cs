using Edu_QuizGen.Contracts.Quiz;

namespace Edu_QuizGen.Service_Abstraction;

public interface IQuizService
{
    Task<Result<IEnumerable<QuizResponse>>> GetAllQuizzesAsync();
    Task<Result<QuizResponse>> GetQuizByIdAsync(int id);
    Task<Result<QuizResponse>> CreateQuizAsync(string roomId,CreateQuizRequest request);
    Task<Result<QuizResponse>> UpdateQuizAsync(int id, UpdateQuizRequest request);
    Task<Result> DeleteQuizAsync(int id);
    Task<Result<IEnumerable<QuizResponse>>> GetQuizzesByRoomIdAsync(string roomId);
    Task<Result<IEnumerable<QuizResponse>>> GetQuizzesByTeacherIdAsync(string teacherId);
    Task<Result> AssignQuizToRoomAsync(int quizId, string roomId);
    Task<Result> RemoveQuizFromRoomAsync(int quizId, string roomId);
    Task<Result<QuizDetailResponse>> GetQuizWithDetailsAsync(int id);
    //Task<Result<IEnumerable<QuizResponse>>> GetActiveQuizzesAsync();
    Task<Result<QuizResponse>> GetQuizByHashAsync(string hashValue);
}