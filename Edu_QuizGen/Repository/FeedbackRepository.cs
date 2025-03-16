using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FeedbackRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Feedback?> GetFeedbackByIdAsync(int id)
    {
        return await _dbContext.Feedbacks
            .Include(f => f.Studnet)
            .FirstOrDefaultAsync(f => f.Id == id && !f.IsDisabled);
    }
}
