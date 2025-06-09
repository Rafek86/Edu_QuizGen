using Edu_QuizGen.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Edu_QuizGen.Contracts.Quiz;

public record CreateQuizRequest(
        string Title,
        string Description,
        int TotalQuestions,
        DateTime StartAt,
        DateTime EndAt,
        int Duration
);
