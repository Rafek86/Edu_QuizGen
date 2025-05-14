namespace Edu_QuizGen.Contracts.Questions;

public record QuestionsAddRequest(
    string Text,
    QuestionType Type,
    string CorrectAnswer,
    string QuizId,
    IEnumerable<OptionsAddRequest> Options
    );
