namespace Edu_QuizGen.Models;

public class TeacherStudent
{
    public string StudentId { get; set; }
    public string TeacherId { get; set; }

    public Student Student { get; set; }
    public Teacher Teacher { get; set; }
}
