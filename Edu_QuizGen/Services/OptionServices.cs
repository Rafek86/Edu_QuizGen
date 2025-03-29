using Edu_QuizGen.Repository;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services
{
    public class OptionServices(IOptionRepository _repository) : IOptionService
    {
        public async Task<Result> AddOptionAsync(Option option)
        {
            await _repository.AddAsync(option);
            return Result.Success();
        }

        public Result DeleteQuestion(Option option)
        {
            _repository.Delete(option);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<Option>>> GetOptionByQuestionText(string QuistionText)
        {
            var options =  await _repository.GetOptionByQuestionText(QuistionText);
            return Result.Success(options);
        }

        public async Task<Result<IEnumerable<Option>>> GetOptionsByQuestionId(int QuestionId)
        {
            var options = await _repository.GetOptionsByQuestionId(QuestionId);
            return Result.Success(options);
        }

        public Result UpdateQuestion(Option option)
        {
            _repository.Update(option);
            return Result.Success();
        }
    }
}
