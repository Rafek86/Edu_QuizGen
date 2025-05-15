using System.ComponentModel.DataAnnotations;

namespace Edu_QuizGen.DTOs
{
    public record OptionCreateDTO
    {
        [Required(ErrorMessage = "Option text is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Option text must be between 1 and 200 characters")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Question ID is required")]
        public int QuestionId { get; set; }
    }
}
