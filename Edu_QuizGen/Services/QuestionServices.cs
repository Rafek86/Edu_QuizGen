using Edu_QuizGen.Models;
using Edu_QuizGen.Repository;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services
{
    public class QuestionServices(IQuestionRepository _repository) : IQuestionSevice
    {
        public async Task<Result> AddQuestionAsync(Question question)
        {
            await _repository.AddAsync(question);
            return Result.Success();
        }

        public async Task<Result> AddQuestionAsync(IEnumerable<Question> questions)
        {
            foreach (var question in questions)
            {
                await _repository.AddAsync(question);
            }
            return Result.Success();
        }

        public Result DeleteQuestion(Question question)
        {
            _repository.Delete(question);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<Question>>> GetAllQuestionsAsync()
        {
            var Questions = await _repository.GetAllAsync();
            if (Questions == null)
            return Result.Success(Questions);
        }

        public async Task<Result<Question>> GetQuestionByIdAsync(int id)
        {
            var question = await _repository.GetQuestionByIdAsync(id);
            return Result.Success(question);
        }

        public async Task<Result<IEnumerable<Question>>> GetQuestionsByQuizId(int QuizId)
        {
            var question = await _repository.GetQuestionsByQuizId(QuizId);
            return Result.Success(question);
        }
            
        public async Task<Result<IEnumerable<Question>>> GetQuestionsByQuizTitle(string QuizTitle)
        {
            var question = await _repository.GetQuestionsByQuizTitle(QuizTitle);
            return Result.Success(question);
        }
            

        public async Task<Result<IEnumerable<Question>>> GetQuestionsByTypeAsync(QuestionType type)
        {
            var question = await _repository.GetQuestionsByTypeAsync(type);
            return Result.Success(question);
        }

        public Result UpdateQuestion(Question question)
        {
            _repository.Update(question);
            return Result.Success(question);
        }
    }
}
