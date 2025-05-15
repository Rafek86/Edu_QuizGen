namespace Edu_QuizGen.Models
{
    public class Hash : IBaseEntity
    {
        public Guid Id { get; set; }
        public string FileHash { get; set; }
        public Quiz Quiz { get; set; }
        public int QuizId { get; set; }
        public bool IsDisabled { get ; set ; } = false;
    }
}
