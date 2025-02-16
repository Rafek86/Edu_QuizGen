using Edu_QuizGen.Abstractions.Consts;

namespace Edu_QuizGen.Persistence
{
    public static class ApplicationDbContextSeedData
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> manager)
        {
            if (!manager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    Id = DefaultUsers.AdminId,
                    FirstName = "QuizGen_Admin",
                    LastName = "Team",
                    UserName = DefaultUsers.AdminEmail,
                    NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
                    NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
                    Email = DefaultUsers.AdminEmail,
                    SecurityStamp = DefaultUsers.SecurityStamp,
                    ConcurrencyStamp = DefaultUsers.ConcurrencyStamp,
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null!, DefaultUsers.AdminPassword)
                };
                await manager.CreateAsync(user);
            }
        }
    }
}
