using Edu_QuizGen.Contracts.Feedback;

namespace Edu_QuizGen.Service_Abstraction;

public interface IFeedbackService
{
    Task<Result> AddFeedbackAsync(FeedbackAddRequest feedbackDto);
    Task<Result<IEnumerable<Feedback>>> GetFeedbackByStudentAsync(string studentId);
    Task<Result> UpdateFeedbackAsync(int feedbackId, string studentId, string newComment);
    Task<Result> DeleteFeedbackAsync(int feedbackId, string studentId);
}
