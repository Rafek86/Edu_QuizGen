namespace Edu_QuizGen.Models;

public class TeacherCourse : IBaseEntity
{
    public string TeacherId { get; set; }
    public int CourseId { get; set; }

    public Teacher Teacher { get; set; }
    public Course Course { get; set; }
    public bool IsDisabled { get ; set ; } = false;
}
