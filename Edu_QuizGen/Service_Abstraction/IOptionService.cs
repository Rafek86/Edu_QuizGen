using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Service_Abstraction
{
    public interface IOptionService
    {
        public Task<Result> AddOptionAsync(OptionCreateDTO optionDto);

        public Task<Result<IEnumerable<OptionResponseDTO>>> GetOptionsByQuestionId(int QuestionId);
        public Task<Result<IEnumerable<OptionResponseDTO>>> GetOptionsByQuestionText(string QuistionText);

        public Task<Result> UpdateOption(int id, OptionDTO optionDto);
        public Task<Result> DeleteOption(int id);
    }
}
