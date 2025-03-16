namespace Edu_QuizGen.Errors;

public static class UserErrors 
{
    public static readonly Error InvalidCredentials = 
        new("User.InvalidCredentials", "Invalid Email/Password",StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);
    
    public static readonly Error DuplicatedEmail =
        new("User.DuplicatedEmail", "The Email is Already Exists", StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed =
        new("User.EmailNotConfirmed", "The Email is not Confirmed", StatusCodes.Status401Unauthorized);
   
    public static readonly Error InvalidCode =
        new("User.InvalidCode", "Code is not Valid", StatusCodes.Status401Unauthorized);
    
    public static readonly Error DuplicatedConfirmation =
        new("User.DuplicatedConfirmation", "Email is Already Confirmed", StatusCodes.Status400BadRequest);
}
