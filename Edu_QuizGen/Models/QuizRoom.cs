namespace Edu_QuizGen.Models
{
    public class QuizRoom
    {
        public int QuizId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public Quiz Quiz { get; set; }
    }
}
