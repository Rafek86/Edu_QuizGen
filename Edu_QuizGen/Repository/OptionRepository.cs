using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository
{
    public class OptionRepository : GenericRepository<Option>, IOptionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OptionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Option>> GetOptionByQuestionText(string QuistionText)
            => await _dbContext.Options.Where(o => o.Question.Text == QuistionText && !o.IsDisabled).ToListAsync();

        public async Task<IEnumerable<Option>> GetOptionsByQuestionId(int QuestionId)
            => await _dbContext.Options.Where(o => o.QuestionId == QuestionId && !o.IsDisabled).ToListAsync();
    }
}
