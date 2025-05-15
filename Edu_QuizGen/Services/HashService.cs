using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services;

public class HashService(IHashRepository _repository) : IHashService
{
    //public async Task<Result> AddHashAsync(Hash hash)
    //{
    //    await _repository.AddAsync(hash);
    //    return Result.Success();
    //}

    //public Result DeleteHash(Hash hash)
    //{
    //    _repository.Delete(hash);
    //    return Result.Success();
    //}

    public async Task<string> CalculatePdfHashAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();

        using var sha256 = SHA256.Create();
        stream.Position = 0;
        var hashBytes = await sha256.ComputeHashAsync(stream);
        stream.Position = 0;

      
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }


    public async Task<(bool isDuplicate, Hash existingDocument)> IsDuplicatePdfAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty or null", nameof(file));
        }

        if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("File is not a PDF", nameof(file));
        }

        string fileHash = await CalculatePdfHashAsync(file);
        var existingDocument = await _repository.GetByHashAsync(fileHash);

        return (existingDocument != null, existingDocument)!;
    }

    public async Task<Hash> SavePdfAsync(IFormFile file,int quizId)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty or null", nameof(file));
        }

        if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("File is not a PDF", nameof(file));
        }

        string fileHash = await CalculatePdfHashAsync(file);

        var existingDocument = await _repository.GetByHashAsync(fileHash);
        if (existingDocument != null)
        {
            return existingDocument;
        }

        var pdfDocument = new Hash
        {
            Id = Guid.NewGuid(),
            FileHash = fileHash,
            QuizId = quizId
        };

        await _repository.AddAsync(pdfDocument);

        return pdfDocument;
    }
}

