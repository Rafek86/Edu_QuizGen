namespace Edu_QuizGen.Contracts.FastApi;

public class QuestionResponse
{
    public int Id { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public string CorrectAnswer { get; set; }
    public string? Explanation { get; set; }
    public List<OptionResponse>? Options { get; set; }
}

public class OptionResponse
{
    public int Id { get; set; }
    public string Text { get; set; }
}