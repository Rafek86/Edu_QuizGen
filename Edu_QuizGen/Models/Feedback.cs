namespace Edu_QuizGen.Models;

public class Feedback
{
    public int Id { get; set; }

    public string Comment { get; set; }

    public string StudentId { get; set; }
    public Student Studnet { get; set; }
}
