namespace Edu_QuizGen.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsDisabled { get; set; } = false;
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<Option>? Options { get; set; } = new List<Option>();

    }
}
