using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Contracts.Quiz;

public record QuizResponse(
     int Id,
     string Title,
     string Description,
     bool IsDisabled,
     int TotalQuestions,
     DateTimeOffset StartAt,
     DateTimeOffset EndAt,
     int Duration,
     bool AI,
     string hashVal
 );
