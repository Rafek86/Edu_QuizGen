namespace Edu_QuizGen.Service_Abstraction;

public interface IHashService
{
    public Task<Result> AddHashAsync(Hash hash);
    public Result UpdateHash(Hash hash);
    public Result DeleteHash(Hash hash);
}
