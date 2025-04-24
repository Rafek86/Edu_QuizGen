using Edu_QuizGen.Contracts.Feedback;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IStudentRepository _studentRepository;

    public FeedbackService(IFeedbackRepository feedbackRepository, IStudentRepository studentRepository)
    {
        _feedbackRepository = feedbackRepository;
        _studentRepository = studentRepository;
    }

    public async Task<Result> AddFeedbackAsync(FeedbackAddRequest feedbackDto)
    {
        var student = await _studentRepository.GetByIdAsync(feedbackDto.StudentId);
        if (student is null || student.IsDisabled)
            return Result.Failure(StudentErrors.NotFound);

        var feedback = new Feedback
        {
            Comment = feedbackDto.Comment,
            StudentId = feedbackDto.StudentId,
        };

        await _feedbackRepository.AddAsync(feedback);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<Feedback>>> GetFeedbackByStudentAsync(string studentId)
    {
        var result = await _feedbackRepository.GetFeedbackByIdAsync(studentId);
        return Result.Success(result);
    }

    public async Task<Result> UpdateFeedbackAsync(int feedbackId, string studentId, string newComment)
    {
        var feedback = await _feedbackRepository.GetByIdAsync(feedbackId);
        if (feedback is null || feedback.IsDisabled)
            return Result.Failure(FeedbackErrors.NotFound);

        if (feedback.StudentId != studentId)
            return Result.Failure(FeedbackErrors.UnauthorizedAccess);

        feedback.Comment = newComment;
        await _feedbackRepository.Update(feedback);

        return Result.Success();
    }

    public async Task<Result> DeleteFeedbackAsync(int feedbackId, string studentId)
    {
        var feedback = await _feedbackRepository.GetByIdAsync(feedbackId);
        if (feedback is null || feedback.IsDisabled)
            return Result.Failure(FeedbackErrors.NotFound);

        if (feedback.StudentId != studentId)
            return Result.Failure(FeedbackErrors.UnauthorizedAccess);

        _feedbackRepository.Delete(feedback);
        return Result.Success();
    }
}

