namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasMany(s => s.studentCourses)
                .WithOne()
                .HasForeignKey(s => s.StudentId);

            builder.HasMany(s => s.teachers)
                .WithMany(t => t.Students);

        }
    }
}
