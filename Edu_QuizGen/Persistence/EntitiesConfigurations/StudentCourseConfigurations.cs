namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class StudentCourseConfigurations : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(c => new { c.StudentId, c.CourseId });
        }
    }
}
