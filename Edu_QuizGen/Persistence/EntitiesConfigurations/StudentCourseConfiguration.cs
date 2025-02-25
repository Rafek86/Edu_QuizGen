
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.HasKey(sk => new { sk.StudentId,sk.CourseId});

        builder.HasOne(x => x.Student)
            .WithMany(x => x.StudentCourses)
            .HasForeignKey(x => x.StudentId);

        builder.HasOne(x => x.Course)
            .WithMany(x => x.StudentCourses)
            .HasForeignKey(x => x.CourseId);

        builder.ToTable("StudentCourse");
    }
}
