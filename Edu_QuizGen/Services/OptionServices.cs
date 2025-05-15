using Edu_QuizGen.DTOs;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Repository;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services
{
    public class OptionServices(IOptionRepository _repository, IQuestionRepository _questionRepository) : IOptionService
    {
        public async Task<Result> AddOptionAsync(OptionCreateDTO optionDto)
        {
            // Validate question exists
            var question = await _questionRepository.GetQuestionByIdAsync(optionDto.QuestionId);
            if (question == null)
                return Result.Failure(OptionErrors.QuestionNotFound);

            // Check for duplicate options
            var Options = await _repository.GetOptionsByQuestionId(optionDto.QuestionId);
            if (Options.Any(o => o.Text.Equals(optionDto.Text, StringComparison.OrdinalIgnoreCase)))
                return Result.Failure(OptionErrors.DuplicateOption);

            var option = new Option
            {
                Text = optionDto.Text,
                QuestionId = optionDto.QuestionId
            };

            await _repository.AddAsync(option);
            return Result.Success();
        }

        public async Task<Result> DeleteOption(int id)
        {
            var option = await _repository.GetByIdAsync(id);
            if (option == null)
                return Result.Failure(OptionErrors.NotFound);

            await _repository.Delete(option);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<OptionResponseDTO>>> GetOptionsByQuestionText(string questionText)
        {
            var options = await _repository.GetOptionByQuestionText(questionText);
            if (!options.Any())
                return Result.Failure<IEnumerable<OptionResponseDTO>>(OptionErrors.NotFound);

            var optionsDto = options.Select(o => new OptionResponseDTO
            {
                Id = o.Id,
                Text = o.Text,
                QuestionId = o.QuestionId
            });

            return Result.Success(optionsDto);
        }

        public async Task<Result<IEnumerable<OptionResponseDTO>>> GetOptionsByQuestionId(int questionId)
        {
            var options = await _repository.GetOptionsByQuestionId(questionId);
            if (!options.Any())
                return Result.Failure<IEnumerable<OptionResponseDTO>>(OptionErrors.NotFound);

            var optionsDto = options.Select(o => new OptionResponseDTO
            {
                Id = o.Id,
                Text = o.Text,
                QuestionId = o.QuestionId
            });

            return Result.Success(optionsDto);
        }

        public async Task<Result> UpdateOption(int id, OptionDTO optionDto)
        {
            var option = await _repository.GetByIdAsync(id);
            if (option == null)
                return Result.Failure(OptionErrors.NotFound);

            // Check for duplicate options in the same question
            var Options = await _repository.GetOptionsByQuestionId(option.QuestionId);
            if (Options.Any(o => o.Id != id && o.Text.Equals(optionDto.Text, StringComparison.OrdinalIgnoreCase)))
                return Result.Failure(OptionErrors.DuplicateOption);

            option.Text = optionDto.Text;
            await _repository.Update(option);
            return Result.Success();
        }
    }
}
