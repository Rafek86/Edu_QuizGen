using System.ComponentModel.DataAnnotations;

namespace Edu_QuizGen.DTOs
{
    public record QuestionDTO
    {
        [Required(ErrorMessage = "Question text is required")]
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        [Required(ErrorMessage ="bta3a")]
        public string CorrectAnswer { get; set; }
        public int QuizId { get; set; }
        public ICollection<OptionDTO>? Options { get; set; } = new List<OptionDTO>();
    }
}
