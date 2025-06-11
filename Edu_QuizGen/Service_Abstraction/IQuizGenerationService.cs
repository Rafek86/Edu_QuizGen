namespace Edu_QuizGen.Service_Abstraction;

public interface IQuizGenerationService
{
    Task<Result<List<Question>>> GenerateQuestionsFromPdfAsync(int quizId, IFormFile pdfFile);
    Task<Result<List<Question>>> GetGeneratedQuestionsAsync(int quizId);
    Task<Result> SaveSelectedQuestionsAsync(int quizId, List<int> selectedQuestionIds);
}
