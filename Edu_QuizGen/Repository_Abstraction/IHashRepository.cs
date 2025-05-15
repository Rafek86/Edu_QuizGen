namespace Edu_QuizGen.Repository_Abstraction;

public interface IHashRepository : IGenericRepository<Hash>
{
    Task<Hash> GetByHashAsync(string fileHash);
    Task<bool> ExistsByHashAsync(string fileHash);
}
