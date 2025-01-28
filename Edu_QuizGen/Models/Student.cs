namespace Edu_QuizGen.Models
{
    public class Student : ApplicationUser
    {
        public DateTime enrollmentDate { get; set; } = DateTime.Now;
        public string gradeLevel { get; set; }

        public IEnumerable<Teacher> teachers { get; set; } = new List<Teacher>();
        public IEnumerable<StudentCourse> studentCourses { get; set; } = new List<StudentCourse>();
    }
}
