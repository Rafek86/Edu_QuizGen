namespace Edu_QuizGen.Models
{
    public class Course
    {
        public int id { get; set; }
        public string name { get; set; }
        public double grade { get; set; }
        public int credit { get; set; }
        public string code { get; set; }

        public IEnumerable<Teacher> Teachers { get; set; } = new List<Teacher>();
        public IEnumerable<Student> students { get; set; } = new List<Student>();
    }
}
