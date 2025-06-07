using Edu_QuizGen.Repository_Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Edu_QuizGen.Repository
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Question>> GetAllAsync() =>
            await _dbContext.Questions
                .Where(s => !s.IsDisabled)
                .Include(s => s.Options)
                .Include(s => s.Quiz)
                .ToListAsync();

        public async Task<Question?> GetByIdAsync(int id) => 
             await _dbContext
            .Questions
            .Include(o => o.Options)
            .FirstOrDefaultAsync(q => !q.IsDisabled && q.Id == id);

        public async Task<PagedResult<Question>> GetPagedQuestionsAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            var totalItems = await _dbContext.Questions.CountAsync(q => !q.IsDisabled);
            var questions = await _dbContext.Questions
                .Include(o => o.Options)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Question>
            {
                Items = questions,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
            => await _dbContext.Questions.Include(o => o.Options).Include(q=>q.Quiz).Where(s => s.Id == id && !s.IsDisabled).FirstOrDefaultAsync();
        

        public async Task<IEnumerable<Question>> GetQuestionsByQuizId(int QuizId)
            => await _dbContext.Questions.Include(o => o.Options).Where(s => s.QuizId == QuizId && !s.IsDisabled).ToListAsync();

        public async Task<IEnumerable<Question>> GetQuestionsByQuizTitle(string QuizTitle)
            => await _dbContext.Questions.Include(o => o.Options).Where(s => s.Quiz.Title == QuizTitle && !s.IsDisabled).ToListAsync();

        public async Task<IEnumerable<Question>> GetQuestionsByTypeAsync(QuestionType type)
            => await _dbContext.Questions.Include(o => o.Options).Where(s => s.Type == type && !s.IsDisabled).ToListAsync();

        public async Task UpdateQuestion(Question question)
        {
            var Options = await _dbContext.Options
                .Where(o => o.QuestionId == question.Id)
                .ToListAsync();
            _dbContext.Options.RemoveRange(Options);

            _dbContext.Entry(question).State = EntityState.Modified;

            if (question.Options != null)
            {
                foreach (var option in question.Options)
                {
                    option.QuestionId = question.Id;
                    _dbContext.Options.Add(option);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
