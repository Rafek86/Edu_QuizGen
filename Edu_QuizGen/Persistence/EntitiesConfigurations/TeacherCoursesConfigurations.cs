
namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class TeacherCoursesConfigurations : IEntityTypeConfiguration<TeacherCourses>
    {
        public void Configure(EntityTypeBuilder<TeacherCourses> builder)
        {
            builder.HasKey(t => new { t.TeacherId, t.CourseId });
        }
    }
}
