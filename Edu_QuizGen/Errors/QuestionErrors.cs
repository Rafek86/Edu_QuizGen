namespace Edu_QuizGen.Errors
{
    public static class QuestionErrors
    {
        public static Error AlreadyExists = new("Question.AlreadyExists", "A question is already exists", StatusCodes.Status409Conflict);

        public static Error questionlistIsEmptyError = new("question list Is Empty", "question list Is Empty Error", StatusCodes.Status404NotFound);
        
        public static Error questionIsEmptyError = new("question Is Empty", "question Is Empty Error", StatusCodes.Status404NotFound);
    }
}
