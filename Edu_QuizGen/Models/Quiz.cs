namespace Edu_QuizGen.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; } = false;
        public int TotalQuestions { get; set; }
        public ICollection<QuizRoom> QuizRoom { get; set; } = new HashSet<QuizRoom>();
        public Hash Hash { get; set; }
    }
}
