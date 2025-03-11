namespace Edu_QuizGen.Service_Abstraction
{
    public interface IOptionService
    {
        public Task AddOptionAsync(Option option);

        public Task<IEnumerable<Option>> GetOptionsByQuestionId(int QuestionId);
        public Task<IEnumerable<Option>> GetOptionByQuestionText(string QuistionText);

        public void UpdateQuestion(Option option);
        public void DeleteQuestion(Option option);
    }
}
