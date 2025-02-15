
namespace Edu_QuizGen.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } =string.Empty;    
    public string LastName { get; set; } =string.Empty;
    public bool? Status { get; set; } = true;
    public string? profilePicture { get; set; } = string.Empty;

    public List<RefreshToken> RefreshTokens { get; set; } = [];
}
