namespace Edu_QuizGen.Models;

public class Course : IBaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public double Grade { get; set; }

    public int Credit { get; set; }

    public string Code { get; set; } = string.Empty;

    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    public ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
    public bool IsDisabled { get ; set ; } = false;
}
