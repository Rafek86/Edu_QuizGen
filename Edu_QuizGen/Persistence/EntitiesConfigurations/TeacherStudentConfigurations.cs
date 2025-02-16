
namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class TeacherStudentConfigurations : IEntityTypeConfiguration<TeacherStudent>
    {
        public void Configure(EntityTypeBuilder<TeacherStudent> builder)
        {
            builder.HasKey(t => new {t.StudentId,t.TeacherId});
        }
    }
}
