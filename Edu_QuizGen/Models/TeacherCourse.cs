namespace Edu_QuizGen.Models;

public class TeacherCourse
{
    public string TeacherId { get; set; }
    public int CourseId { get; set; }

    public Teacher Teacher { get; set; }
    public Course Course { get; set; }
}
