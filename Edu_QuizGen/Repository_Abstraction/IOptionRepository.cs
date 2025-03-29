namespace Edu_QuizGen.Repository_Abstraction
{
    public interface IOptionRepository : IGenericRepository<Option>
    {
        public Task<IEnumerable<Option>> GetOptionByQuestionText(string QuistionText);
        public Task<IEnumerable<Option>> GetOptionsByQuestionId(int QuestionId);
    }
}
