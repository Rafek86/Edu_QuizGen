using Edu_QuizGen.Abstractions.Consts;

namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {

        builder.HasData([
            new ApplicationRole{ 
            Id=DefaultRoles.AdminRoleId,
            Name=DefaultRoles.Admin,
            NormalizedName=DefaultRoles.Admin.ToUpper(),
            ConcurrencyStamp=DefaultRoles.AdminRoleConcurrencyStamp,
            },  
            
            new ApplicationRole{ 
            Id=DefaultRoles.StudentRoleId,
            Name=DefaultRoles.Student,
            NormalizedName=DefaultRoles.Student.ToUpper(),
            ConcurrencyStamp=DefaultRoles.StudentRoleConcurrencyStamp,
            IsDefault=true
            },
            
            new ApplicationRole{ 
            Id=DefaultRoles.TeacherRoleId,
            Name=DefaultRoles.Teacher,
            NormalizedName=DefaultRoles.Teacher.ToUpper(),
            ConcurrencyStamp=DefaultRoles.TeacherRoleConcurrencyStamp,
            }]);
    }
}
