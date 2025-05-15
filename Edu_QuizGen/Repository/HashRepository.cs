using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;

public class HashRepository : GenericRepository<Hash>, IHashRepository
{
    private readonly ApplicationDbContext _dbContext;

    public HashRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Hash> GetByHashAsync(string fileHash)
    {
        return await _dbContext.Hashes
            .FirstOrDefaultAsync(d => d.FileHash == fileHash);
    }

    public async Task<bool> ExistsByHashAsync(string fileHash)
    {
        return await _dbContext.Hashes
            .AnyAsync(d => d.FileHash == fileHash);
    }
}
