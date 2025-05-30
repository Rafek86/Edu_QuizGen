using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Repository_Abstraction;

public interface IHashRepository : IGenericRepository<Hash>
{
    Task<Hash> GetByHashAsync(string fileHash);
    Task<bool> ExistsByHashAsync(string fileHash);
    Task<HashResponse> GetHashResponseByIdAsync(string id);
    Task<HashResponse> GetHashResponseByHashAsync(string fileHash);
    Task<IEnumerable<HashResponse>> GetAllHashResponsesAsync();
}
