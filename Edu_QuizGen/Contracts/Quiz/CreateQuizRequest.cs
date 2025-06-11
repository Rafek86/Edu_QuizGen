using Edu_QuizGen.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Edu_QuizGen.Contracts.Quiz;

public record CreateQuizRequest(
        string Title,
        string Description,
        int TotalQuestions,
        DateTimeOffset StartAt,
        DateTimeOffset EndAt,
        int Duration,
        bool AI
);
