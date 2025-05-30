using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Service_Abstraction;

public interface IHashService
{
    Task<string> CalculatePdfHashAsync(IFormFile file);
    Task<Result<HashCheckResponse>> IsDuplicatePdfAsync(IFormFile file);
    Task<Result<HashResponse>> SavePdfAsync(IFormFile file, int quizId);
}
