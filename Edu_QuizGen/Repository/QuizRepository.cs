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
            .Where(x => x.RoomId == roomId)
            .Select(x => x.Quiz)
            .ToListAsync();
    }
}
