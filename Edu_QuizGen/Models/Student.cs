namespace Edu_QuizGen.Models
{
    public class Student : ApplicationUser
    {
        public DateTime enrollmentDate { get; set; } = DateTime.Now;
        public string gradeLevel { get; set; }

        IEnumerable<Teacher> teachers { get; set; } = new List<Teacher>();
        IEnumerable<Course> courses { get; set; } = new List<Course>();
    }
}
