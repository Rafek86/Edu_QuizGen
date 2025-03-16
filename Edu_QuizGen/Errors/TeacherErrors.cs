namespace Edu_QuizGen.Errors;

public static class TeacherErrors
{
    public static readonly Error NotFound =
        new("Teacher.NotFound", "The requested teacher does not exist or has been disabled", StatusCodes.Status404NotFound);

    public static readonly Error AlreadyExists =
        new("Teacher.AlreadyExists", "A teacher with this ID already exists", StatusCodes.Status409Conflict);

    public static readonly Error UnauthorizedAccess =
        new("Teacher.Unauthorized", "You do not have permission to access or modify this teacher's data", StatusCodes.Status403Forbidden);

    public static readonly Error InvalidId =
        new("Teacher.InvalidId", "The provided Teacher ID is invalid", StatusCodes.Status400BadRequest);
}
