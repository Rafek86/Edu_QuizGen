namespace Edu_QuizGen.Models
{
    public class Option : IBaseEntity
    {
        public int Id { get; set; }
        public string Text {  get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public bool IsDisabled { get ; set ; } = false;
    }
}
