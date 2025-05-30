namespace Edu_QuizGen.DTOs;


public record HashResponse
{
    public Guid Id { get; init; }
    public string FileHash { get; init; }
    public int QuizId { get; init; }
    public string QuizTitle { get; init; }
    public string QuizDescription { get; init; }
}

public record HashUploadRequest
{
    public IFormFile File { get; init; }
    public int QuizId { get; init; }
}

public record HashCheckRequest
{
    public IFormFile File { get; init; }
}

public record HashCheckResponse
{
    public bool Exists { get; init; }
    public string Message { get; init; }
    public HashResponse Document { get; init; }
}

public record HashUploadResponse
{
    public string Message { get; init; }
    public HashResponse Document { get; init; }
}

public record HashDuplicateResponse
{
    public string Message { get; init; }
    public HashResponse Document { get; init; }
}