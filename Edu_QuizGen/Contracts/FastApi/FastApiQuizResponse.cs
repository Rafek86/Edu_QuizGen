using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Contracts.FastApi;

public class FastApiQuizResponse
{
    public List<FastApiQuestionResponse> Questions { get; set; }
    public string Message { get; set; }
    public string? Error { get; set; }
}