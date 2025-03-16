namespace Edu_QuizGen.Errors;

public static class FeedbackErrors
{
    public static readonly Error NotFound =
        new("Feedback.NotFound", "The requested feedback does not exist or has been deleted", StatusCodes.Status404NotFound);

    public static readonly Error AlreadyExists =
        new("Feedback.AlreadyExists", "Duplicate feedback is not allowed", StatusCodes.Status409Conflict);

    public static readonly Error UnauthorizedAccess =
        new("Feedback.Unauthorized", "You do not have permission to access or modify this feedback", StatusCodes.Status403Forbidden);

    public static readonly Error InvalidId =
        new("Feedback.InvalidId", "The provided Feedback ID is invalid", StatusCodes.Status400BadRequest);
}
