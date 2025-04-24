using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;

public class HashRepository : GenericRepository<Hash>, IHashRepository
{
    private readonly ApplicationDbContext _dbContext;

    public HashRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}
