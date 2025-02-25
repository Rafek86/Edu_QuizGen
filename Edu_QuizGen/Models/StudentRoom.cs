namespace Edu_QuizGen.Models;

public class StudentRoom
{
    public string StudentId { get; set; }
    public string RoomId { get; set; }

    public Student Student { get; set; }
    public Room Room { get; set; }
}
