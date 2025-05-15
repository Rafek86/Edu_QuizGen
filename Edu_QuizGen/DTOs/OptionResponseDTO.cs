namespace Edu_QuizGen.DTOs
{
    public record OptionResponseDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
    }
}
