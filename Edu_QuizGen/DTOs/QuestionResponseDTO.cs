namespace Edu_QuizGen.DTOs
{
    public class QuestionResponseDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public string CorrectAnswer { get; set; }
        public int QuizId { get; set; }
        public ICollection<OptionDTO>? Options { get; set; } = new List<OptionDTO>();
    }
}
