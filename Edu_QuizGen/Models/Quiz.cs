using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Models
{
    public class Quiz : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; } = false;
        public int TotalQuestions { get; set; }
        public DateTimeOffset? StartAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? EndAt { get; set; } = DateTimeOffset.UtcNow;
        public int? Duration { get; set; } // Duration in minutes
        public bool? AI { get; set; } = false;
        public ICollection<QuizRoom> QuizRoom { get; set; } = new HashSet<QuizRoom>(); 
        public ICollection<QuizQuestions> quizQuestions { get; set; } = new List<QuizQuestions>();
        public Hash Hash { get; set; }
    }
}
