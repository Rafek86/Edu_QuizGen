namespace Edu_QuizGen.Errors
{
    public static class QuestionErrors
    {
        public static Error AlreadyExists = new("Question.AlreadyExists", "A question is already exists", StatusCodes.Status409Conflict);

        public static Error questionlistIsEmptyError = new("question list Is Empty", "question list Is Empty Error", StatusCodes.Status404NotFound);
        
        public static Error questionIsEmptyError = new("question Is Empty", "question Is Empty Error", StatusCodes.Status404NotFound);

        public static Error MCQOptionsRequired = new("Question.MCQOptionsRequired", "MCQ questions must have options", StatusCodes.Status400BadRequest);

        public static Error CorrectAnswerNotInOptions = new("Question.CorrectAnswerNotInOptions", "Correct answer must match one of the options", StatusCodes.Status400BadRequest);

        public static Error InvalidTrueFalseAnswer = new("Question.InvalidTrueFalseAnswer", "True/False questions must have 'true' or 'false' as correct answer", StatusCodes.Status400BadRequest);
    }
}
