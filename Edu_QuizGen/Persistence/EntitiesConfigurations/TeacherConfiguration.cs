
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {

        //builder.HasMany(ts => ts.TeacherStudents)
        //       .WithOne(s=>s.Teacher)
        //       .HasForeignKey(ts => ts.TeacherId);

        builder.ToTable("Teachers");
    }
}
