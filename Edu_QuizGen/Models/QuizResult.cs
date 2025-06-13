namespace Edu_QuizGen.Models;

public class QuizResult : IBaseEntity
{
    public int Id { get; set; }
    public double Score { get; set; }
    public string StudentId { get; set; } = string.Empty; 
    public Student Student { get; set; }
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public bool IsDisabled { get; set; }
    public DateTimeOffset CompletedAt { get; set; } = DateTimeOffset.UtcNow;
}
