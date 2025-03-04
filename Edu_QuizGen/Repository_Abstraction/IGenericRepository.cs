namespace Edu_QuizGen.Repository_Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> GetByIdAsync(string id); //for (student, teacher, room, hash)
        Task<T> GetByIdAsync(int id); //for other classes
    }
}
