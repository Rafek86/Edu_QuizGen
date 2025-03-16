namespace Edu_QuizGen.Repository_Abstraction;

public interface IFeedbackRepository : IGenericRepository<Feedback>
{
    public Task<Feedback> GetFeedbackByIdAsync(int id);
}
