namespace Edu_QuizGen.Contracts.Quiz;

public record QuizDetailResponse(
    int Id,
    string Title,
    string Description,
    bool IsDisabled,
    int TotalQuestions,
    string? HashValue,
    IEnumerable<QuizRoomResponse> AssignedRooms
);
