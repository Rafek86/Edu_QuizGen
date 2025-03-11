namespace Edu_QuizGen.Models;

public class StudentRoom : IBaseEntity
{
    public string StudentId { get; set; }
    public string RoomId { get; set; }

    public Student Student { get; set; }
    public Room Room { get; set; }
    public bool IsDisabled { get ; set ; } = false;
}
