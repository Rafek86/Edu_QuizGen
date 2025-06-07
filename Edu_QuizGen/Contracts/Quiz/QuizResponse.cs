using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Contracts.Quiz;

public record QuizResponse(
     int Id,
     string Title,
     string Description,
     bool IsDisabled,
     int TotalQuestions,
     string hashVal
 );
