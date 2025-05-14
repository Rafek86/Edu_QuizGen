using Edu_QuizGen.Models;
using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FeedbackRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Feedback>> GetFeedbackByIdAsync(string id)
    {
        return await _dbContext.Feedbacks
         //  .Include(f => f.Studnet)
           .Where(f => f.StudentId == id && !f.IsDisabled)
           .ToListAsync();
    }
}
