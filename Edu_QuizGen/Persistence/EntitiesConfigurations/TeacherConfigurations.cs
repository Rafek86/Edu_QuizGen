namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class TeacherConfigurations : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasMany(t => t.teacherStudents)
                .WithOne()
                .HasForeignKey(t => t.TeacherId);

            builder.HasMany(t => t.teacherCourses)
                .WithOne()
                .HasForeignKey(t => t.TeacherId);
        }
    }
}
