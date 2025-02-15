namespace Edu_QuizGen.Models
{
    public class Teacher : ApplicationUser
    {
        public DateTime hireDate { get; set; } = DateTime.Now;


        public IEnumerable<TeacherStudent> teacherStudents { get; set; } = new List<TeacherStudent>();
        public IEnumerable<TeacherCourses> teacherCourses { get; set; } = new List<TeacherCourses>();
    }
}
