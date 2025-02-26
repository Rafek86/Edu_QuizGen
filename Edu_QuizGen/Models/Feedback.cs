namespace Edu_QuizGen.Models;

public class Feedback
{
    public int Id { get; set; }

    public string Comment { get; set; }
    public bool IsDisabled { get; set; } = false;
    public string StudentId { get; set; }
    public Student Studnet { get; set; }
}
