namespace Edu_QuizGen.Repository_Abstraction;

public interface IQuizRepository : IGenericRepository<Quiz>
{
    public Task<IEnumerable<Quiz>> GetAllQuizzesByRoomId(string roomId);
}
