using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;

public class RoomStudentRepository : IRoomStudentRepository
{
    private readonly ApplicationDbContext _context;

    public RoomStudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }   


    public Task<StudentRoom> GetStudentRoom(string studnetId, string roomId)
    {
        return _context.StudentRooms
            .FirstOrDefaultAsync(sr => sr.StudentId == studnetId && sr.RoomId == roomId && !sr.IsDisabled)!;
    }

    public Task<StudentRoom> GetStudentRoomIncludingDisabled(string studentId, string roomId)
    {
        return _context.StudentRooms
            .FirstOrDefaultAsync(sr => sr.StudentId == studentId && sr.RoomId == roomId)!;
    }


    public async Task<StudentRoom> AddAsync(StudentRoom studentRoom)
    {
        var existingStudentRoom = await GetStudentRoomIncludingDisabled(studentRoom.StudentId, studentRoom.RoomId);

        if (existingStudentRoom != null)
        {
            existingStudentRoom.IsDisabled = false;
        }
        else
        {
            await _context.StudentRooms.AddAsync(studentRoom);
        }

        await _context.SaveChangesAsync();

        return existingStudentRoom ?? studentRoom;
    }

    public async Task<bool> DeleteAsync(StudentRoom studentRoom)
    {
        studentRoom.IsDisabled = true;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<StudentRoom>> GetRoomsByStudentIdAsync(string studentId)
    {
        return await _context.StudentRooms
             .Where(sr => sr.StudentId == studentId && !sr.IsDisabled).ToListAsync();
    }
}
