namespace Edu_QuizGen.Models;

public class Student :ApplicationUser
{
    public DateTime EntollmentDate { get; set; } 
    public string GradeLevel { get; set; } = string.Empty;

    public ICollection<TeacherStudent> TeacherStudents { get; set; } = new List<TeacherStudent>();
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    public ICollection<StudentRoom> StudentRooms { get; set; } = new List<StudentRoom>();
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
}
