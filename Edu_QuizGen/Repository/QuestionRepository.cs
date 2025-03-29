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

        public async Task<Question> GetQuestionByIdAsync(int id)
            => await _dbContext.Questions.Where(s => s.Id == id && !s.IsDisabled).FirstOrDefaultAsync();
        

        public async Task<IEnumerable<Question>> GetQuestionsByQuizId(int QuizId)
            => await _dbContext.Questions.Where(s => s.QuizId == QuizId && !s.IsDisabled).ToListAsync();

        public async Task<IEnumerable<Question>> GetQuestionsByQuizTitle(string QuizTitle)
            => await _dbContext.Questions.Where(s => s.Quiz.Title == QuizTitle && !s.IsDisabled).ToListAsync();

        public async Task<IEnumerable<Question>> GetQuestionsByTypeAsync(QuestionType type)
            => await _dbContext.Questions.Where(s => s.Type == type && !s.IsDisabled).ToListAsync();
    }
}
