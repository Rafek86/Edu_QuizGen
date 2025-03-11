
namespace Edu_QuizGen.Models;

public class ApplicationUser : IdentityUser, IBaseEntity
{
    public string FirstName { get; set; } =string.Empty;    
    public string LastName { get; set; } =string.Empty;
    public string profilePicture { get; set; } = string.Empty;

    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public bool IsDisabled { get; set ; } = false;
}
