namespace Edu_QuizGen.Models
{
    public class Student : ApplicationUser
    {
        public DateTime enrollmentDate { get; set; } = DateTime.Now;
        public string gradeLevel { get; set; } = string.Empty;

        public IEnumerable<TeacherStudent> teacherStudents { get; set; } = new List<TeacherStudent>();
        public IEnumerable<StudentCourse> studentCourses { get; set; } = new List<StudentCourse>();
    }
}
