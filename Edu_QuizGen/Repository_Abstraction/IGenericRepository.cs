namespace Edu_QuizGen.Repository_Abstraction
{
    public interface IGenericRepository<T> where T : class,IBaseEntity
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> GetByIdAsync(string id); //for (student, teacher, room, hash)
        Task<T> GetByIdAsync(int id); //for other classes

        void Delete(T entity);
        Task Update(T entity);
    }
}
