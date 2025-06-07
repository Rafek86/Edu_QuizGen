using Edu_QuizGen.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Edu_QuizGen.Contracts.Quiz;

public record CreateQuizRequest(
      [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        string Title,

      [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        string Description,

      [Range(1, int.MaxValue, ErrorMessage = "Total questions must be at least 1")]
        int TotalQuestions
);
