using Edu_QuizGen.Abstractions.Consts;

namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>>builder)
    {
      builder.HasData(new IdentityUserRole<string> { 
      UserId=DefaultUsers.AdminId,
      RoleId =DefaultRoles.AdminRoleId,
      });
    }
}
