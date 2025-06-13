namespace Edu_QuizGen.Repository_Abstraction;

public interface IQuizRepository : IGenericRepository<Quiz>
{
    Task<IEnumerable<Quiz>> GetAllQuizzesByRoomId(string roomId);
    Task<Quiz?> GetQuizWithDetailsAsync(int quizId);
    Task<IEnumerable<Quiz>> GetQuizzesByTeacherIdAsync(string teacherId);
    Task<IEnumerable<Quiz>> GetQuizzesByStudentIdAsync(string studentId);
    Task<bool> IsQuizAssignedToRoomAsync(int quizId, string roomId);
    Task<IEnumerable<Quiz>> GetActiveQuizzesAsync();
    Task<Quiz?> GetQuizByHashAsync(string hashValue);
    Task<bool> AssignQuizToRoomAsync(int quizId, string roomId);
    Task<bool> RemoveQuizFromRoomAsync(int quizId, string roomId);
}