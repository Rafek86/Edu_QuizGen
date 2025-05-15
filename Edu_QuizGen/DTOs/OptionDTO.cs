using System.ComponentModel.DataAnnotations;

namespace Edu_QuizGen.DTOs
{
    public record OptionDTO
    {
        [Required(ErrorMessage = "Option text is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Option text must be between 1 and 200 characters")]
        public string Text { get; set; }
    }
}
