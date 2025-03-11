using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services
{
    public class OptionServices(IGenericRepository<Option> _repository, ApplicationDbContext _dbContext) : IOptionService
    {
        public async Task AddOptionAsync(Option option) => await _repository.AddAsync(option);

        public void DeleteQuestion(Option option) => _repository.Delete(option);

        public async Task<IEnumerable<Option>> GetOptionByQuestionText(string QuistionText)
            => await _dbContext.Options.Where(o => o.Question.Text == QuistionText && !o.IsDisabled).ToListAsync();

        public async Task<IEnumerable<Option>> GetOptionsByQuestionId(int QuestionId)
            => await _dbContext.Options.Where(o => o.QuestionId == QuestionId && !o.IsDisabled).ToListAsync();

        public void UpdateQuestion(Option option) => _repository.Update(option);
    }
}
