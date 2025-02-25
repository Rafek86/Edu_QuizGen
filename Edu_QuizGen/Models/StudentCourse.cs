namespace Edu_QuizGen.Models;

public class StudentCourse
{
    public string StudentId { get; set; }
    public int CourseId { get; set; }
    public double Grade { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }
}
