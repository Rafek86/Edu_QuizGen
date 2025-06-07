namespace Edu_QuizGen.Models
{
    public class QuizQuestions : IBaseEntity
    {
        public int QuizId { get; set; }
        public int QuistionId { get; set; }

        public Quiz quiz { get; set; }
        public Question question { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
