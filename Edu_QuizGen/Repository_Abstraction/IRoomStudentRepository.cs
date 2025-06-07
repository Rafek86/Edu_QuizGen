namespace Edu_QuizGen.Repository_Abstraction;

public interface IRoomStudentRepository 
{
    Task<StudentRoom> GetStudentRoom(string studnetId,string roomId);
    Task<StudentRoom> GetStudentRoomIncludingDisabled(string studentId, string roomId);
    Task<IEnumerable<StudentRoom>> GetRoomsByStudentIdAsync(string studentId);
    Task<StudentRoom> AddAsync(StudentRoom studentRoom);
    Task<bool> DeleteAsync(StudentRoom studentRoom);
}
