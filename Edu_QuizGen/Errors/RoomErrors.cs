namespace Edu_QuizGen.Errors;

public static class RoomErrors
{
    public static readonly Error InvalidCredentials =
       new("Room.InvalidCredentials", "Invalid Email/Password", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidJwtToken =
        new("Room.InvalidJwtToken", "Invalid JWT token", StatusCodes.Status401Unauthorized);

    public static readonly Error AlreadyExists =
        new("Room.AlreadyExists", "A room with this name already exists", StatusCodes.Status409Conflict);

    public static readonly Error NotFound =
        new("Room.NotFound", "The requested room does not exist or has been deleted", StatusCodes.Status404NotFound);

    public static readonly Error UnauthorizedAccess =
        new("Room.Unauthorized", "You do not have permission to access or modify this room", StatusCodes.Status403Forbidden);

    public static readonly Error InvalidId =
        new("Room.InvalidId", "The provided Room ID is invalid", StatusCodes.Status400BadRequest);


    public static readonly Error AlreadyJoined =
        new("Room.AlreadyJoined", "You already join this Room", StatusCodes.Status204NoContent);

    public static readonly Error NotJoined =
        new("Room.NotJoined", "You have not joined this Room", StatusCodes.Status204NoContent);
}
