using Edu_QuizGen.DTOs;
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
            .Include(h => h.Quiz)
            .FirstOrDefaultAsync(d => d.FileHash == fileHash);
    }

    public async Task<bool> ExistsByHashAsync(string fileHash)
    {
        return await _dbContext.Hashes
            .AnyAsync(d => d.FileHash == fileHash);
    }

    public async Task<HashResponse> GetHashResponseByIdAsync(string id)
    {
        var hash = await _dbContext.Hashes
            .Include(h => h.Quiz)
            .Where(h => h.Id.ToString() == id && !h.IsDisabled)
            .Select(h => new HashResponse
            {
                Id = h.Id,
                FileHash = h.FileHash,
                QuizId = h.QuizId,
                QuizTitle = h.Quiz.Title,
                QuizDescription = h.Quiz.Description
            })
            .FirstOrDefaultAsync();

        return hash;
    }

    public async Task<HashResponse> GetHashResponseByHashAsync(string fileHash)
    {
        var hash = await _dbContext.Hashes
            .Include(h => h.Quiz)
            .Where(h => h.FileHash == fileHash && !h.IsDisabled)
            .Select(h => new HashResponse
            {
                Id = h.Id,
                FileHash = h.FileHash,
                QuizId = h.QuizId,
                QuizTitle = h.Quiz.Title,
                QuizDescription = h.Quiz.Description
            })
            .FirstOrDefaultAsync();

        return hash;
    }

    public async Task<IEnumerable<HashResponse>> GetAllHashResponsesAsync()
    {
        var hashes = await _dbContext.Hashes
            .Include(h => h.Quiz)
            .Where(h => !h.IsDisabled)
            .Select(h => new HashResponse
            {
                Id = h.Id,
                FileHash = h.FileHash,
                QuizId = h.QuizId,
                QuizTitle = h.Quiz.Title,
                QuizDescription = h.Quiz.Description
            })
            .ToListAsync();

        return hashes;
    }
}