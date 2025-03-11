namespace Edu_QuizGen.Models;

public class ApplicationRole:IdentityRole , IBaseEntity
{
    public bool IsDefault { get; set; }
    public bool IsDisabled { get ; set ; } = false;
}
