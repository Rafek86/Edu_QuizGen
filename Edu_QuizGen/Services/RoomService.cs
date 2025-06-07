using Edu_QuizGen.Contracts.Rooms;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Models;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IRoomStudentRepository _roomStudentRepository;

    public RoomService(IRoomRepository roomRepository, ITeacherRepository teacherRepository, IStudentRepository studentRepository, IRoomStudentRepository roomStudentRepository)
    {
        _roomRepository = roomRepository;
        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
        _roomStudentRepository = roomStudentRepository;
    }

    public async Task<Result> AddRoomAsync(string roomName, string teacherId)
    {
        var teacher = await _teacherRepository.GetByIdAsync(teacherId);
        if (teacher is null || teacher.IsDisabled)
            return Result.Failure(TeacherErrors.NotFound);

        var room = new Room
        {
            Id = Guid.NewGuid().ToString(),
            Name = roomName,
            TeacherId = teacherId
        };

        await _roomRepository.AddAsync(room);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<GerRoomResponse>>> GetRoomsByTeacherAsync(string teacherId)
    {
        var teacher = await _teacherRepository.GetByIdAsync(teacherId);
        if (teacher is null || teacher.IsDisabled)
            return Result.Failure<IEnumerable<GerRoomResponse>>(TeacherErrors.NotFound);

        var rooms = await _roomRepository.GetRoomsByTeacherAsync(teacherId);


        return Result.Success(rooms.Select(r => new GerRoomResponse (r.Id,r.Name,r.TeacherId)));
    }

    public async Task<Result> UpdateRoomAsync(string roomId, string teacherId, string newName)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room is null || room.IsDisabled)
            return Result.Failure(RoomErrors.NotFound);

        if (room.TeacherId != teacherId)
            return Result.Failure(RoomErrors.UnauthorizedAccess);

        room.Name = newName;
        await _roomRepository.Update(room);

        return Result.Success();
    }

    public async Task<Result> DeleteRoomAsync(string roomId, string teacherId)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room is null || room.IsDisabled)
            return Result.Failure(RoomErrors.NotFound);

        if (room.TeacherId != teacherId)
            return Result.Failure(RoomErrors.UnauthorizedAccess);

        await _roomRepository.Delete(room);
        return Result.Success();
    }

    public async Task<Result> JoinRoomAsync(string roomId, string studentId)
    {
        var room =await  _roomRepository.GetByIdAsync(roomId);
        var student = await _studentRepository.GetByIdAsync(studentId);
        var studentRoom = await _roomStudentRepository.GetStudentRoom(studentId, roomId);

        if (room is null || room.IsDisabled)
            return Result.Failure(RoomErrors.NotFound);

        if (student is null || student.IsDisabled)
            return Result.Failure(StudentErrors.NotFound);

       if(studentRoom is not null)
            return Result.Failure(RoomErrors.AlreadyJoined);


        var newStudentRoom = new StudentRoom
        {
            RoomId = roomId,
            StudentId = studentId
        };
        await _roomStudentRepository.AddAsync(newStudentRoom);
        return Result.Success();
    }

    public async Task<Result> LeaveRoomAsync(string roomId, string studentId)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        var student = await _studentRepository.GetByIdAsync(studentId);

        if (room is null || room.IsDisabled)
            return Result.Failure(RoomErrors.NotFound);

        if (student is null || student.IsDisabled)
            return Result.Failure(StudentErrors.NotFound);

        var studentRoom = await _roomStudentRepository.GetStudentRoom(studentId, roomId);

        if (studentRoom is null)
            return Result.Failure(RoomErrors.NotJoined);

        await _roomStudentRepository.DeleteAsync(studentRoom);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<StudentRoomsResponse>>> GetAllStudentRoomsAsync(string studentId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);

        if (student is null || student.IsDisabled)
            return Result.Failure<IEnumerable<StudentRoomsResponse>>(StudentErrors.NotFound);

        var studentRoom = await _roomStudentRepository.GetRoomsByStudentIdAsync(studentId);

        if (studentRoom is null || !studentRoom.Any())
            return Result.Failure<IEnumerable<StudentRoomsResponse>>(RoomErrors.NotFound);
    
        return Result.Success(studentRoom.Select(st => new StudentRoomsResponse
        (
        st.RoomId,
        _roomRepository.GetByIdAsync(st.RoomId).Result?.Name ?? "Unknown Room",
        _roomRepository.GetByIdAsync(st.RoomId).Result?.TeacherId ?? "Unknown Teacher"
        )));
    }
}
