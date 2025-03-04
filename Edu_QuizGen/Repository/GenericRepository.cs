using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();
        
        public async Task<T> GetByIdAsync(string id) => await _dbContext.Set<T>().FindAsync(id);
        public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);
    }
}
