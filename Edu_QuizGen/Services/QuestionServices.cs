using Edu_QuizGen.Repository;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services
{
    public class QuestionServices(IGenericRepository<Question> _repository) : IQuestionSevice
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

        public Task<IEnumerable<Question>> GetQuestionsByTypeAsync(QuestionType type)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuestion(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
