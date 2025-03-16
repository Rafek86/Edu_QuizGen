namespace Edu_QuizGen.Errors;

public static class StudentErrors
{
    public static readonly Error NotFound =
        new("Student.NotFound", "The requested student does not exist or has been disabled", StatusCodes.Status404NotFound);

    public static readonly Error AlreadyExists =
        new("Student.AlreadyExists", "A student with this ID already exists", StatusCodes.Status409Conflict);

    public static readonly Error UnauthorizedAccess =
        new("Student.Unauthorized", "You do not have permission to access or modify this student's data", StatusCodes.Status403Forbidden);

    public static readonly Error InvalidId =
        new("Student.InvalidId", "The provided Student ID is invalid", StatusCodes.Status400BadRequest);
}
