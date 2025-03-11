namespace Edu_QuizGen.Models
{
    public class QuizRoom : IBaseEntity
    {
        public int QuizId { get; set; }
        public string RoomId { get; set; }
        public Room Room { get; set; }
        public Quiz Quiz { get; set; }
        public bool IsDisabled { get ; set ; } = false;
    }
}
