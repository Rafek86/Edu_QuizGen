namespace Edu_QuizGen.DTOs;


public class FastApiQuestionResponse
{
    public string Text { get; set; }
    public int Type { get; set; }
    public List<FastApiOption> Options { get; set; }
    public string CorrectAnswer { get; set; }
    public string Explanation { get; set; }
    public string Difficulty { get; set; }
    public string Page { get; set; }
    public int Quiz_Id { get; set; }
}
