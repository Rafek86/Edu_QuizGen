using System.ComponentModel.DataAnnotations;

namespace Edu_QuizGen.DTOs
{
    public record QuestionDTO
    {
        [Required(ErrorMessage = "Question text is required")]
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public string CorrectAnswer { get; set; }
        public ICollection<Option>? Options { get; set; } = new List<Option>();
    }
}
