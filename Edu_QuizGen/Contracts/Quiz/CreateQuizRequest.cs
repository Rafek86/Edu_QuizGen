public record CreateQuizRequest(
    string Title,
    string Description,
    int TotalQuestions,
    DateTimeOffset StartAt,
    DateTimeOffset EndAt,
    int Duration,
    bool AI = false
);
