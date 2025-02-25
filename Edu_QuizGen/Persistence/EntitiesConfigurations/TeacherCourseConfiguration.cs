
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class TeacherCourseConfiguration : IEntityTypeConfiguration<TeacherCourse>
{
    public void Configure(EntityTypeBuilder<TeacherCourse> builder)
    {
        builder.HasKey(sk => new { sk.TeacherId, sk.CourseId });

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.TeacherCourses)
            .HasForeignKey(x => x.TeacherId);

        builder.HasOne(x => x.Course)
            .WithMany(x => x.TeacherCourses)
            .HasForeignKey(x => x.CourseId);

        builder.ToTable("TeacherCourse");
    }
}
