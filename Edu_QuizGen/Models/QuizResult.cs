namespace Edu_QuizGen.Models;

public class QuizResult
{
    public int Id { get; set; }
    public double Score { get; set; }

    public string StduentId { get; set; }
    public Student Student { get; set; }

    //public string QuizId { get; set; }
    //public Quiz Quiz { get; set; }
}
