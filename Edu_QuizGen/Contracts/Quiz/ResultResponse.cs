namespace Edu_QuizGen.Contracts.Quiz;

public record ResultResponse(
    double Score,
    string StudentId,
    int QuizId,
    DateTimeOffset CompletedAt
);