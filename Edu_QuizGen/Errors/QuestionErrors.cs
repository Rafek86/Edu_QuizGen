namespace Edu_QuizGen.Errors
{
    public static class QuestionErrors
    {
        public static Error AlreadyExists = new("Question.AlreadyExists", "A question is already exists", StatusCodes.Status409Conflict);
    }
}
