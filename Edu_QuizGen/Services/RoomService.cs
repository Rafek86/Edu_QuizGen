using Edu_QuizGen.Contracts.Rooms;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ITeacherRepository _teacherRepository;

    public RoomService(IRoomRepository roomRepository, ITeacherRepository teacherRepository)
    {
        _roomRepository = roomRepository;
        _teacherRepository = teacherRepository;
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

        _roomRepository.Delete(room);
        return Result.Success();
    }
}
