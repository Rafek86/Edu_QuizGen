using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Edu_QuizGen.DTOs;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Models;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;
using Edu_QuizGen.Abstractions;

namespace Edu_QuizGen.Services;

public class HashService : IHashService
{
    private readonly IHashRepository _repository;
    private readonly IQuizRepository _quizRepository;

    public HashService(IHashRepository repository, IQuizRepository quizRepository)
    {
        _repository = repository;
        _quizRepository = quizRepository; 
    }

    public async Task<Result<string>> CalculatePdfHashAsync(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure<string>(HashErrors.FileEmpty);
            }

            using var stream = file.OpenReadStream();
            using var sha256 = SHA256.Create();

            stream.Position = 0;
            var hashBytes = await sha256.ComputeHashAsync(stream);
            var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

            return Result.Success(hash);
        }
        catch (Exception)
        {
            return Result.Failure<string>(HashErrors.HashCalculationFailed);
        }
    }

    public async Task<Result<HashCheckResponse>> IsDuplicatePdfAsync(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure<HashCheckResponse>(HashErrors.FileEmpty);
            }

            if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
            {
                return Result.Failure<HashCheckResponse>(HashErrors.InvalidFileType);
            }

            var hashResult = await CalculatePdfHashAsync(file);
            if (hashResult.IsFailure)
            {
                return Result.Failure<HashCheckResponse>(hashResult.Error);
            }

            var existingDocument = await _repository.GetHashResponseByHashAsync(hashResult.Value);

            if (existingDocument != null)
            {
                return Result.Success(new HashCheckResponse
                {
                    Exists = true,
                    Message = "This PDF has already been uploaded",
                    Document = existingDocument
                });
            }

            return Result.Success(new HashCheckResponse
            {
                Exists = false,
                Message = "This PDF has not been uploaded before",
                Document = null
            });
        }
        catch (Exception)
        {
            return Result.Failure<HashCheckResponse>(HashErrors.DuplicateCheckFailed);
        }
    }

    public async Task<Result<HashResponse>> SavePdfAsync(IFormFile file, int quizId)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure<HashResponse>(HashErrors.FileEmpty);
            }

            if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
            {
                return Result.Failure<HashResponse>(HashErrors.InvalidFileType);
            }

            var quizExists = await _quizRepository.GetByIdAsync(quizId);
            if (quizExists is null)
            {
                return Result.Failure<HashResponse>(HashErrors.QuizNotFound);
            }

            var hashResult = await CalculatePdfHashAsync(file);
            if (hashResult.IsFailure)
            {
                return Result.Failure<HashResponse>(hashResult.Error);
            }

            var existingDocument = await _repository.GetHashResponseByHashAsync(hashResult.Value);
            if (existingDocument != null)
            {
                return Result.Success(existingDocument);
            }

            var pdfDocument = new Hash
            {
                Id = Guid.NewGuid(),
                FileHash = hashResult.Value,
                QuizId = quizId
            };

            await _repository.AddAsync(pdfDocument);

            var savedDocument = await _repository.GetHashResponseByHashAsync(hashResult.Value);
            if (savedDocument == null)
            {
                return Result.Failure<HashResponse>(HashErrors.RetrievalFailed);
            }

            return Result.Success(savedDocument);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SavePdfAsync: {ex.Message}");
            return Result.Failure<HashResponse>(HashErrors.SaveFailed);
        }
    }
}