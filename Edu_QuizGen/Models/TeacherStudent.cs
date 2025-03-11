namespace Edu_QuizGen.Models;

public class TeacherStudent : IBaseEntity
{
    public string StudentId { get; set; }
    public string TeacherId { get; set; }

    public Student Student { get; set; }
    public Teacher Teacher { get; set; }
    public bool IsDisabled { get ; set ; } = false;
}
