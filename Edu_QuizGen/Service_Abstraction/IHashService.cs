namespace Edu_QuizGen.Service_Abstraction;

public interface IHashService
{
    //public Task<Result> AddHashAsync(Hash hash);
    //public Result UpdateHash(Hash hash);
    //public Result DeleteHash(Hash hash);


    Task<string> CalculatePdfHashAsync(IFormFile file);
    Task<(bool isDuplicate, Hash existingDocument)> IsDuplicatePdfAsync(IFormFile file);
    Task<Hash> SavePdfAsync(IFormFile file, int quizId);

}
