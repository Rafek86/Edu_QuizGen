namespace Edu_QuizGen.Models
{
    public class Teacher : ApplicationUser
    {
        public DateTime hireDate { get; set; } = DateTime.Now;

        public int MyProperty { get; set; }

        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public IEnumerable<Course> courses { get; set; } = new List<Course>();
    }
}
