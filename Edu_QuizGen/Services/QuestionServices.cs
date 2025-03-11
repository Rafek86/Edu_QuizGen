using Edu_QuizGen.Repository;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services
{
    public class QuestionServices(IGenericRepository<Question> _repository, ApplicationDbContext _dbContext) : IQuestionSevice
    {
        public async Task AddQuestionAsync(Question question) => await _repository.AddAsync(question);

        public async Task AddQuestionAsync(IEnumerable<Question> questions)
        {
            foreach (var question in questions)
            {
                await _repository.AddAsync(question);
            }
        }

        public void DeleteQuestion(Question question) => _repository.Delete(question);

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync() => await _repository.GetAllAsync();

        public Task<Question> GetQuestionByIdAsync(int id) 
            => _dbContext.Questions.Where(s => s.Id == id && !s.IsDisabled).FirstOrDefaultAsync();

        public async Task<IEnumerable<Question>> GetQuestionsByQuizId(int QuizId)
            => await _dbContext.Questions.Where(s => s.QuizId == QuizId && !s.IsDisabled).ToListAsync();

        public async Task<IEnumerable<Question>> GetQuestionsByQuizTitle(string QuizTitle)
            => await _dbContext.Questions.Where(s => s.Quiz.Title == QuizTitle && !s.IsDisabled).ToListAsync();

        public async Task<IEnumerable<Question>> GetQuestionsByTypeAsync(QuestionType type) 
            => await _dbContext.Questions.Where(s => s.Type == type && !s.IsDisabled).ToListAsync();
        

        public void UpdateQuestion(Question question) => _repository.Update(question);
    }
}
