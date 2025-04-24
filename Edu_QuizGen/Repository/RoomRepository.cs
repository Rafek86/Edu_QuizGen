using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;


public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _dbContext.Rooms
            .Where(r => !r.IsDisabled)
            .Include(r => r.Teacher)
            .Include(r => r.StudentRooms)
            .Include(r => r.QuizRoom)
            .ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(string id)
    {
        return await _dbContext.Rooms
            .Include(r => r.Teacher)
            .Include(r => r.StudentRooms)
            .Include(r => r.QuizRoom)
            .FirstOrDefaultAsync(r => r.Id == id && !r.IsDisabled);
    }

    public async Task<IEnumerable<Room>> GetRoomsByTeacherAsync(string teacherId)
    {
        return await _dbContext.Rooms
            .Where(r => r.TeacherId == teacherId && !r.IsDisabled)
            .Include(r => r.Teacher)
            .Include(r => r.StudentRooms)
            .Include(r => r.QuizRoom)
            .ToListAsync();
    }
}