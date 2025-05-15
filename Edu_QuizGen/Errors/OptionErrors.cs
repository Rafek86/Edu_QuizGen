namespace Edu_QuizGen.Errors
{
    public static class OptionErrors
    {
        public static Error NotFound = new("Option.NotFound", "Option not found", StatusCodes.Status404NotFound);
        public static Error QuestionNotFound = new("Option.QuestionNotFound", "Question not found", StatusCodes.Status404NotFound);
        public static Error InvalidText = new("Option.InvalidText", "Option text is invalid", StatusCodes.Status400BadRequest);
        public static Error DuplicateOption = new("Option.DuplicateOption", "Option already exists for this question", StatusCodes.Status409Conflict);
        public static Error InvalidQuestionId = new("Option.InvalidQuestionId", "Invalid question ID. Question ID must be greater than 0", StatusCodes.Status400BadRequest);
        public static Error InvalidOptionId = new("Option.InvalidOptionId", "Invalid option ID. Option ID must be greater than 0", StatusCodes.Status400BadRequest);
        public static Error InvalidQuestionText = new("Option.InvalidQuestionText", "Question text cannot be empty", StatusCodes.Status400BadRequest);
    }
}
