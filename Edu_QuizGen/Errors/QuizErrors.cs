namespace Edu_QuizGen.Errors;

public static class QuizErrors
{
    public static readonly Error NotFound = new("Quiz.NotFound", "Quiz not found",StatusCodes.Status404NotFound);
    public static readonly Error AlreadyDisabled = new("Quiz.AlreadyDisabled", "Quiz is already disabled", StatusCodes.Status404NotFound);
    public static readonly Error AlreadyAssigned = new("Quiz.AlreadyAssigned", "Quiz is already assigned to this room", StatusCodes.Status409Conflict);
    public static readonly Error AssignmentNotFound = new("Quiz.AssignmentNotFound", "Quiz assignment not found", StatusCodes.Status404NotFound);
    public static readonly Error InvalidHash = new("Quiz.InvalidHash", "Invalid quiz hash", StatusCodes.Status409Conflict);
}