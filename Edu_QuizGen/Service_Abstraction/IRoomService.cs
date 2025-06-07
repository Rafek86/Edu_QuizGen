using Edu_QuizGen.Contracts.Rooms;

namespace Edu_QuizGen.Service_Abstraction;

public interface IRoomService
{
    Task<Result<string>> AddRoomAsync(string roomName, string teacherId);
    Task<Result<IEnumerable<GerRoomResponse>>> GetRoomsByTeacherAsync(string teacherId);
    Task<Result> UpdateRoomAsync(string roomId, string teacherId, string newName);
    Task<Result> DeleteRoomAsync(string roomId, string teacherId);
    Task<Result<string>> JoinRoomAsync(string roomId, string studentId);
    Task<Result> LeaveRoomAsync(string roomId, string studentId);
    Task<Result<IEnumerable<StudentRoomsResponse>>> GetAllStudentRoomsAsync(string studentId);
}