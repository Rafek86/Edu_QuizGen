using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;

public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
{
    private readonly ApplicationDbContext _dbContext;

    public QuizRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Quiz>> GetAllQuizzesByRoomId(string roomId)
    {
        return await _dbContext.QuizRooms
            .Include(x => x.Quiz)
            .Where(x => x.RoomId == roomId && !x.IsDisabled)
            .Select(x => x.Quiz)
            .Where(q => !q.IsDisabled)
            .ToListAsync();
    }

    public async Task<Quiz?> GetQuizWithDetailsAsync(int quizId)
    {
        return await _dbContext.Quizzes
            .Include(q => q.QuizRoom.Where(qr => !qr.IsDisabled))
            .ThenInclude(qr => qr.Room)
            .Include(q => q.Hash)
            .FirstOrDefaultAsync(q => q.Id == quizId);
    }

    public async Task<IEnumerable<Quiz>> GetQuizzesByTeacherIdAsync(string teacherId)
    {
        return await _dbContext.QuizRooms
            .Where(qr => qr.Room.TeacherId == teacherId && !qr.IsDisabled)
            .Select(qr => qr.Quiz)
            .Where(q => !q.IsDisabled)
            .Distinct()
            .ToListAsync();
    }

    public async Task<bool> IsQuizAssignedToRoomAsync(int quizId, string roomId)
    {
        return await _dbContext.QuizRooms
            .AnyAsync(qr => qr.QuizId == quizId && qr.RoomId == roomId && !qr.IsDisabled);
    }

    public async Task<IEnumerable<Quiz>> GetActiveQuizzesAsync()
    {
        return await _dbContext.Quizzes
            .AsNoTracking()
            .Where(q => !q.IsDisabled)
            .Include(q => q.Hash)
            .ToListAsync();
    }

    public async Task<Quiz?> GetQuizByHashAsync(string hashValue)
    {
        return await _dbContext.Quizzes
            .Include(q => q.Hash)
            .FirstOrDefaultAsync(q => q.Hash.FileHash == hashValue && !q.IsDisabled);
    }

    public async Task<bool> AssignQuizToRoomAsync(int quizId, string roomId)
    {
        // Check if already assigned
        if (await IsQuizAssignedToRoomAsync(quizId, roomId))
            return false;

        var quizRoom = new QuizRoom
        {
            QuizId = quizId,
            RoomId = roomId,
            IsDisabled = false
        };

        _dbContext.QuizRooms.Add(quizRoom);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveQuizFromRoomAsync(int quizId, string roomId)
    {
        var quizRoom = await _dbContext.QuizRooms
            .FirstOrDefaultAsync(qr => qr.QuizId == quizId && qr.RoomId == roomId && !qr.IsDisabled);

        if (quizRoom == null)
            return false;

        quizRoom.IsDisabled = true;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Quiz>> GetQuizzesByStudentIdAsync(string studentId)
    {
        var studentRoomIds = await _dbContext.StudentRooms
       .Where(sr => sr.StudentId == studentId && !sr.IsDisabled)
       .Select(sr => sr.RoomId)
       .ToListAsync();

        if (!studentRoomIds.Any())
            return new List<Quiz>();

        return await _dbContext.Quizzes
            .Where(q => q.QuizRoom.Any(qr => studentRoomIds.Contains(qr.RoomId)) && !q.IsDisabled)
            .Include(q => q.Hash)
            .Include(q => q.quizQuestions)
            .ToListAsync();
    }
}
