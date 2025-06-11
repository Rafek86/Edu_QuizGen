namespace Edu_QuizGen.Contracts.FastApi;


public class QuestionSelectionRequest
{
    public int QuizId { get; set; }
    public List<int> SelectedQuestionIds { get; set; }
}