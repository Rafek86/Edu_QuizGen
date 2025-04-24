namespace Edu_QuizGen.Contracts.Feedback;

public record FeedbackAddRequest(
    string Comment,
    string StudentId
    );
