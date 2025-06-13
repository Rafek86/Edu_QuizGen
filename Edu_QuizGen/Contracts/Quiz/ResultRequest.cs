namespace Edu_QuizGen.Contracts.Quiz;

public record ResultRequest(
    double Score,
    string StudentId,
    int QuizId
); 