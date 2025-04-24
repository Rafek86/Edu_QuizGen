namespace Edu_QuizGen.Repository_Abstraction;

public interface IRoomRepository :IGenericRepository<Room>
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<IEnumerable<Room>> GetRoomsByTeacherAsync(string teacherId);
}
