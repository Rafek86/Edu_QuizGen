
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {

        //builder.HasMany(ts => ts.TeacherStudents)
        //       .WithOne(s=>s.Student)
        //       .HasForeignKey(ts => ts.StudentId);

        builder.ToTable("Students");
    }
}
