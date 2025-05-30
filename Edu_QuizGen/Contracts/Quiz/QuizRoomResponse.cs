namespace Edu_QuizGen.Contracts.Quiz;

public record QuizRoomResponse(
    string RoomId,
    string RoomName,
    bool IsDisabled
);
