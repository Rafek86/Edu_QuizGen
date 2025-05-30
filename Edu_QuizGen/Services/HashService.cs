using Edu_QuizGen.DTOs;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services;

public class HashService : IHashService
{
    private readonly IHashRepository _repository;

    public HashService(IHashRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> CalculatePdfHashAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty or null", nameof(file));
        }

        using var stream = file.OpenReadStream();
        using var sha256 = SHA256.Create();
        stream.Position = 0;
        var hashBytes = await sha256.ComputeHashAsync(stream);
        stream.Position = 0;

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
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

            string fileHash = await CalculatePdfHashAsync(file);
            var existingDocument = await _repository.GetHashResponseByHashAsync(fileHash);

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

            string fileHash = await CalculatePdfHashAsync(file);
            var existingDocument = await _repository.GetHashResponseByHashAsync(fileHash);

            if (existingDocument != null)
            {
                return Result.Success(existingDocument);
            }

            var pdfDocument = new Hash
            {
                Id = Guid.NewGuid(),
                FileHash = fileHash,
                QuizId = quizId
            };

            await _repository.AddAsync(pdfDocument);

            var savedDocument = await _repository.GetHashResponseByHashAsync(fileHash);

            if (savedDocument == null)
            {
                return Result.Failure<HashResponse>(HashErrors.RetrievalFailed);
            }

            return Result.Success(savedDocument);
        }
        catch (Exception)
        {
            return Result.Failure<HashResponse>(HashErrors.SaveFailed);
        }
    }
}