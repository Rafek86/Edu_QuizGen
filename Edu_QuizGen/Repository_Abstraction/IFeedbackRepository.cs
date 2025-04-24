namespace Edu_QuizGen.Repository_Abstraction;

public interface IFeedbackRepository : IGenericRepository<Feedback>
{
    Task<IEnumerable<Feedback>> GetFeedbackByIdAsync(string id);
}
