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

        public async Task AddAsync(T entity) => await _dbContext.Set<T>().AddAsync(entity);

        public void Delete(T entity)
        {
            var prop = entity.GetType().GetProperty("IsDisabled");
            prop?.SetValue(entity, true);
            _dbContext.Set<T>().Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();
        
        public async Task<T> GetByIdAsync(string id) => await _dbContext.Set<T>().FindAsync(id);
        public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);
    }
}
