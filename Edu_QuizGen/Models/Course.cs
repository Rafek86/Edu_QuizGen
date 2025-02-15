namespace Edu_QuizGen.Models
{
    public class Course
    {
        public int id { get; set; }
        public string name { get; set; }
        public double grade { get; set; }
        public int credit { get; set; }
        public string code { get; set; }

        public IEnumerable<TeacherCourses> teacherCourses { get; set; } = new List<TeacherCourses>();
        public IEnumerable<StudentCourse> studentCourses { get; set; } = new List<StudentCourse>();
    }
}
