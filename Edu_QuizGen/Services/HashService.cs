using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services;

public class HashService(IHashRepository _repository) : IHashService
{
    public async Task<Result> AddHashAsync(Hash hash)
    {
        await _repository.AddAsync(hash);
        return Result.Success();
    }

    public Result DeleteHash(Hash hash)
    {
        _repository.Delete(hash);
        return Result.Success();
    }

    public Result UpdateHash(Hash hash)
    {
        throw new NotImplementedException();
    }
}
