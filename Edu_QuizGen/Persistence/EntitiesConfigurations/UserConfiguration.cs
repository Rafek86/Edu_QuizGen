using Edu_QuizGen.Abstractions.Consts;

namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.UseTpcMappingStrategy();
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);

        builder.OwnsMany(x => x.RefreshTokens)
                .ToTable("Refresh Tokens")
                .WithOwner()
                .HasForeignKey("UserId");

        //var password = new PasswordHasher<ApplicationUser>();
        //builder.HasData(new ApplicationUser
        //{
        //    Id = DefaultUsers.AdminId,
        //    FirstName = "QuizGen_Admin",
        //    LastName = "Team",
        //    UserName =DefaultUsers.AdminEmail,
        //    NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),   
        //    NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
        //    Email = DefaultUsers.AdminEmail,
        //    SecurityStamp = DefaultUsers.SecurityStamp,
        //    ConcurrencyStamp = DefaultUsers.ConcurrencyStamp,   
        //    EmailConfirmed=true,
        //    PasswordHash =password.HashPassword(null!,DefaultUsers.AdminPassword)
        //});
    }
}
