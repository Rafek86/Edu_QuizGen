namespace Edu_QuizGen.Service_Abstraction;

public interface IRoomService
{
    Task<Result> AddRoomAsync(string roomName, string teacherId);
    Task<Result<IEnumerable<Room>>> GetRoomsByTeacherAsync(string teacherId);
    Task<Result> UpdateRoomAsync(string roomId, string teacherId, string newName);
    Task<Result> DeleteRoomAsync(string roomId, string teacherId);
}