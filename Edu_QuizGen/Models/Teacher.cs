namespace Edu_QuizGen.Models;

public class Teacher : ApplicationUser
{
    public DateTime HireDate { get; set; } = DateTime.UtcNow;

    public ICollection<TeacherStudent> TeacherStudents { get; set; } = new List<TeacherStudent>();
    public ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
