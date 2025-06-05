using Edu_QuizGen.Abstractions.Consts;

namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>>builder)
    {
      builder.HasData([
      new IdentityUserRole<string> { 
      UserId= DefaultUsers.AdminId,
      RoleId =DefaultRoles.AdminRoleId,
      },
      new IdentityUserRole<string> {
      UserId= "11caefd4-1787-43cb-92b1-ec7a68d628c0",
      RoleId =DefaultRoles.TeacherRoleId,
      }
      ]);
    }
}
