namespace Edu_QuizGen.Models;

[Owned]
public class RefreshToken
{
    public string Token { get; set; }       

    public DateTime ExpiresOn { get; set; }     

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;   

    public DateTime? RevokedOn { get; set; }

    public bool isExpired => DateTime.UtcNow >= ExpiresOn;

    public bool isActive => RevokedOn is null && !isExpired;   
}
