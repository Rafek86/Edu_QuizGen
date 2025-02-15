namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasMany(c => c.studentCourses)
                .WithOne()
                .HasForeignKey(c => c.CourseId);

            builder.HasMany(c => c.teacherCourses)
                .WithOne()
                .HasForeignKey(c => c.CourseId);
        }
    }
}
